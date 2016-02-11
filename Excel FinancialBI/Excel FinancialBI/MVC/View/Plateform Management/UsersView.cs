using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIBlend.Utilities;
using VIBlend.WinForms.Controls;
using VIBlend.WinForms.DataGridView;

namespace FBI.MVC.View
{
  using Controller;
  using Model;
  using Model.CRUD;
  using Forms;
  using Utils;
  using Network;

  public partial class UsersView : UserControl, IView
  {

    #region Variables

    private UserController m_controller = null;

    private FbiDataGridView m_userDGV = new FbiDataGridView();
    private FbiTreeView<AxisElem> m_entitiesTV = null;
    private TextBoxEditor m_entitiesTextBoxEditor = new TextBoxEditor();
    private ComboBoxEditor m_allocatedComboxBoxEditor = new ComboBoxEditor();
    private SafeDictionary<UInt32, ListItem> m_groupsIdItemDict = new SafeDictionary<UInt32, ListItem>();
    private UInt32 m_userClicked;
    private bool m_displayEntities = false;
    private bool m_isLoadingEntities = false;

    #endregion

    #region Initialize

    public UsersView()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {
      this.m_controller = p_controller as UserController;
    }

    public void InitView()
    {
      this.m_entitiesTextBoxEditor.Readonly = true;
      this.m_entitiesTextBoxEditor.MouseDown += OnEntitiesTextBoxEditorMouseDown;
      this.m_entitiesTextBoxEditor.LostFocus += OnEntitiesTextBoxEditorLostFocus;

      UserModel.Instance.UpdateEvent += OnUserModelUpdateEvent;
      UserModel.Instance.ReadEvent += OnUserModelReadEvent;
      UserAllowedEntityModel.Instance.UpdateEvent += OnUserAllowedModelUpdateEvent;
      UserAllowedEntityModel.Instance.ReadEvent += OnUserAllowedModelReadEvent;
      UserAllowedEntityModel.Instance.DeleteEvent += OnUserAllowedModelDeleteEvent;
      UserAllowedEntityModel.Instance.CreationEvent += OnUserAllowedModelCreationEvent;

      this.DisplayEntities(false);
      this.ComboBoxInit();
      this.UserDGVInit();
      this.EntitiesTVInit();
    }

    private void EntitiesTVInit()
    {
      this.m_entitiesTV = new FbiTreeView<AxisElem>(AxisElemModel.Instance.GetDictionary(AxisType.Entities));

      this.m_entitiesTV.NodeChecked += OnEntitiesTVNodeChecked;

      this.m_entitiesTV.CheckBoxes = true;
      this.m_entitiesTV.TriStateMode = true;
      this.m_entitiesTV.Dock = DockStyle.Fill;
      this.PanelEntities.Controls.Add(this.m_entitiesTV);
    }

    private void ComboBoxInit()
    {
      MultiIndexDictionary<UInt32, string, Group> l_groupMID = GroupModel.Instance.GetDictionary();

      foreach (Group l_group in l_groupMID.SortedValues)
      {
        ListItem l_itemGroup = new ListItem();

        l_itemGroup.Text = l_group.Name;
        l_itemGroup.Value = l_group.Id;
        this.m_groupsIdItemDict[(UInt32)l_itemGroup.Value] = l_itemGroup;
        this.m_allocatedComboxBoxEditor.Items.Add(l_itemGroup);
      }
      this.m_allocatedComboxBoxEditor.DropDownList = true;
    }

    private void UserDGVInit()
    {
      this.m_userDGV.SetDimension(FbiDataGridView.Dimension.COLUMN, 0, "Name"); //TODO : no hardcoded string
      this.m_userDGV.SetDimension(FbiDataGridView.Dimension.COLUMN, 1, "Group"); //TODO : no hardcoded string
      this.m_userDGV.SetDimension(FbiDataGridView.Dimension.COLUMN, 2, "Entities"); //TODO : no hardcoded string

      MultiIndexDictionary<UInt32, string, User> l_userMID = UserModel.Instance.GetDictionary();
      this.m_userDGV.InitializeRows<User>(UserModel.Instance, l_userMID);

      foreach (User l_user in l_userMID.Values)
      {
        if (this.m_groupsIdItemDict.ContainsKey(l_user.GroupId))
          this.m_userDGV.FillField<ListItem, ComboBoxEditor>(l_user.Id, 1, this.m_groupsIdItemDict[l_user.GroupId], this.m_allocatedComboxBoxEditor);
        else
          this.m_userDGV.FillField<ListItem, ComboBoxEditor>(l_user.Id, 1, null, this.m_allocatedComboxBoxEditor);
        this.m_userDGV.FillField<string, TextBoxEditor>(l_user.Id, 0, l_user.Name, null);
        this.SetEntities(l_user);
      }

      this.m_userDGV.CellChangedAndValidated += OnUserDGVCellChangedAndValidated;
      this.m_userDGV.CellEditorActivate += OnUserDGVCellEditorActivate;

      this.m_userDGV.Dock = DockStyle.Fill;
      this.LayoutPanel.Controls.Add(this.m_userDGV);
    }

    #endregion

    #region Event

    #region DataGridView

    private void OnUserDGVCellChangedAndValidated(object p_sender, CellEventArgs p_args)
    {
      ListItem l_listItem = null; ;
      try
      {
        l_listItem = ((ComboBoxEditor)p_args.Cell.Editor).SelectedItem;
      }
      catch (Exception e)
      {
        return;
      }
      User l_user = UserModel.Instance.GetValue((UInt32)p_args.Cell.RowItem.ItemValue);

      if (l_user != null)
      {
        if (l_listItem != null)
        {
          l_listItem = l_listItem.Clone();
          if (m_groupsIdItemDict.ContainsKey(l_user.GroupId))
            ((ComboBoxEditor)p_args.Cell.Editor).SelectedItem = m_groupsIdItemDict[l_user.GroupId];
          l_user = l_user.Clone();
          l_user.GroupId = (UInt32)l_listItem.Value;
        }
        this.m_controller.UpdateUser(l_user);
      }
    }

    private void OnUserDGVCellEditorActivate(object p_sender, EditorActivationCancelEventArgs p_args)
    {
      this.m_userClicked = (UInt32)p_args.Cell.RowItem.ItemValue;
    }

    #endregion

    #region TreeView

    private void OnEntitiesTVNodeChecked(object p_sender, vTreeViewEventArgs p_e)
    {
      if (!m_isLoadingEntities)
      {
        this.m_isLoadingEntities = true;
        if (p_e.Node.Checked == CheckState.Checked)
        {
          AxisElem l_entity = AxisElemModel.Instance.GetValue(AxisType.Entities, (UInt32)p_e.Node.Value);
          UserAllowedEntity l_userAllowed = new UserAllowedEntity();

          p_e.Node.Checked = CheckState.Unchecked;
          if (l_entity != null)
          {
            l_userAllowed.UserId = this.m_userClicked;
            l_userAllowed.EntityId = l_entity.Id;
            this.m_controller.CreateUserAllowed(l_userAllowed);
          }
        }
        else
        {
          UserAllowedEntity l_userAllowed = UserAllowedEntityModel.Instance.GetValue(this.m_userClicked, (UInt32)p_e.Node.Value);

          p_e.Node.Checked = CheckState.Checked;
          if (l_userAllowed != null)
            this.m_controller.DeleteUserAllowed(l_userAllowed.Id, l_userAllowed.UserId, l_userAllowed.EntityId);
        }
        this.m_isLoadingEntities = false;
      }
    }

    #endregion

    #region TextBoxEditor

    private void OnEntitiesTextBoxEditorLostFocus(object p_sender, EventArgs p_e)
    {
      /*if (m_displayEntities) //TODO : See things
        this.DisplayEntities(false);*/
    }

    private void OnEntitiesTextBoxEditorMouseDown(object p_sender, MouseEventArgs p_e)
    {
      if (m_displayEntities)
      {
        this.DisplayEntities(false);
      }
      else
      {
        this.DisplayEntities(true);
      }
    }

    #endregion

    #region Server

    #region User

    delegate void OnUserModelReadEvent_delegate(ErrorMessage p_status, User p_attributes);
    void OnUserModelReadEvent(ErrorMessage p_status, User p_attributes)
    {
      if (InvokeRequired)
      {
        OnUserModelReadEvent_delegate func = new OnUserModelReadEvent_delegate(OnUserModelReadEvent);
        Invoke(func, p_status, p_attributes);
      }
      else
      {
        if (p_status == ErrorMessage.SUCCESS)
        {
          if (this.m_groupsIdItemDict.ContainsKey(p_attributes.GroupId))
            this.m_userDGV.FillField<ListItem, ComboBoxEditor>(p_attributes.Id, 1, this.m_groupsIdItemDict[p_attributes.GroupId], this.m_allocatedComboxBoxEditor);
          else
            this.m_userDGV.FillField<ListItem, ComboBoxEditor>(p_attributes.Id, 1, null, this.m_allocatedComboxBoxEditor);
        }
      }
    }

    delegate void OnUserModelUpdateEvent_delegate(ErrorMessage p_status, UInt32 p_id);
    private void OnUserModelUpdateEvent(ErrorMessage p_status, UInt32 p_id)
    {
      if (InvokeRequired)
      {
        OnUserModelUpdateEvent_delegate func = new OnUserModelUpdateEvent_delegate(OnUserModelUpdateEvent);
        Invoke(func, p_status, p_id);
      }
      else
      {
          if (p_status != Network.ErrorMessage.SUCCESS)
          {
            MessageBox.Show(Local.GetValue("users.msg_error_update"));
          }
      }
    }

    #endregion

    #region UserAllowed

    delegate void OnUserAllowedModelReadEvent_delegate(ErrorMessage p_status, UserAllowedEntity p_attributes);
    private void OnUserAllowedModelReadEvent(ErrorMessage p_status, UserAllowedEntity p_attributes)
    {
      if (InvokeRequired)
      {
        OnUserAllowedModelReadEvent_delegate func = new OnUserAllowedModelReadEvent_delegate(OnUserAllowedModelReadEvent);
        Invoke(func, p_status, p_attributes);
      }
      else
      {
        if (p_status == ErrorMessage.SUCCESS)
        {
          if (p_attributes.UserId == this.m_userClicked)
          {
            MultiIndexDictionary<UInt32, string, User> l_userMID = UserModel.Instance.GetDictionary();

            this.SetEntities(UserModel.Instance.GetValue(this.m_userClicked));
            this.m_userDGV.Refresh();
            if (this.m_displayEntities)
              this.DisplayEntities(true);
          }
        }
      }
    }

    delegate void OnUserAllowedModelUpdateEvent_delegate(ErrorMessage p_status, UInt32 p_id);
    private void OnUserAllowedModelUpdateEvent(ErrorMessage p_status, UInt32 p_id)
    {
      if (InvokeRequired)
      {
        OnUserAllowedModelUpdateEvent_delegate func = new OnUserAllowedModelUpdateEvent_delegate(OnUserAllowedModelUpdateEvent);
        Invoke(func, p_status, p_id);
      }
      else
      {
          if (p_status != Network.ErrorMessage.SUCCESS)
          {
            MessageBox.Show(Local.GetValue("users.msg_error_update"));
          }
      }
    }

    delegate void OnUserAllowedModelDeleteEvent_delegate(ErrorMessage p_status, UInt32 p_id);
    private void OnUserAllowedModelDeleteEvent(ErrorMessage p_status, UInt32 p_id)
    {
      if (InvokeRequired)
      {
        OnUserAllowedModelDeleteEvent_delegate func = new OnUserAllowedModelDeleteEvent_delegate(OnUserAllowedModelDeleteEvent);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != Network.ErrorMessage.SUCCESS)
        {
          MessageBox.Show(Local.GetValue("users.msg_error_delete"));
        }
        this.SetEntities(UserModel.Instance.GetValue(this.m_userClicked));
        this.m_userDGV.Refresh();
        if (this.m_displayEntities)
          this.DisplayEntities(true);
      }
    }

    delegate void OnUserAllowedModelCreationEvent_delegate(ErrorMessage p_status, UInt32 p_id);
    private void OnUserAllowedModelCreationEvent(ErrorMessage p_status, UInt32 p_id)
    {
      if (InvokeRequired)
      {
        OnUserAllowedModelCreationEvent_delegate func = new OnUserAllowedModelCreationEvent_delegate(OnUserAllowedModelCreationEvent);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != Network.ErrorMessage.SUCCESS)
        {
          MessageBox.Show(Local.GetValue("users.msg_error_create"));
        }
      }
    }

    #endregion

    #endregion

    #endregion

    #region Utils

    private void DisplayEntities(bool p_status)
    {
      this.m_displayEntities = p_status;
      if (p_status)
      {
        this.LayoutPanel.ColumnStyles[1].Width = 350;
        this.LoadCurrentEntities();
      }
      else
        this.LayoutPanel.ColumnStyles[1].Width = 0;
      this.LayoutPanel.ResumeLayout();
    }

    private void LoadCurrentEntities()
    {
      this.m_isLoadingEntities = true;
      this.CheckEntityTree(this.m_entitiesTV.Nodes, false);
      this.m_entitiesTV.Refresh();
      this.m_isLoadingEntities = false;
    }

    private void CheckEntityTree(vTreeNodeCollection p_nodes, bool p_status)
    {
      foreach (vTreeNode l_node in p_nodes)
      {
        bool l_checkedParent = SetCaseStatus(l_node, p_status);

        foreach (vTreeNode l_subNode in l_node.Nodes)
        {
          bool l_checkedChild = SetCaseStatus(l_subNode, l_checkedParent);
          this.CheckEntityTree(l_subNode.Nodes, l_checkedChild);
        }
      }
    }

    private bool SetCaseStatus(vTreeNode p_node, bool p_status)
    {
      p_node.ShowCheckBox = true;
      if (p_status)
      {
        p_node.ShowCheckBox = false;
        return (true);
      }
      if (this.m_controller.IsAllowedEntity(this.m_userClicked, (UInt32)p_node.Value))
      {
        p_node.Checked = CheckState.Checked;
        return (true);
      }
      p_node.Checked = CheckState.Unchecked;
      return (false);
    }

    private void SetEntities(User p_user)
    {
      if (p_user == null)
        return;
      MultiIndexDictionary<UInt32, UInt32, UserAllowedEntity> l_entitiesAllowed = UserAllowedEntityModel.Instance.GetDictionary(p_user.Id);
      string l_stringEntities = "";

      this.m_userDGV.FillField<string, TextBoxEditor>(p_user.Id, 2, l_stringEntities, this.m_entitiesTextBoxEditor);
      if (l_entitiesAllowed != null)
      {
        foreach (UserAllowedEntity l_allowed in l_entitiesAllowed.Values)
        {
          AxisElem l_axisEntity;

          if ((l_axisEntity = AxisElemModel.Instance.GetValue(AxisType.Entities, l_allowed.EntityId)) != null)
            l_stringEntities += l_axisEntity.Name + "; ";
        }
      }
      this.m_userDGV.FillField<string, TextBoxEditor>(p_user.Id, 2, l_stringEntities, this.m_entitiesTextBoxEditor);
    }

    #endregion

  }
}
