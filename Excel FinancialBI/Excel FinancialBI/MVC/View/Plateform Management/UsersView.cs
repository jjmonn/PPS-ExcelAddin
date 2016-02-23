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
    enum Column
    {
      NAME,
      GROUP,
      ENTITIES
    }

    #region Variables

    UserController m_controller = null;

    FbiDataGridView m_userDGV = new FbiDataGridView();
    FbiTreeView<AxisElem> m_entitiesTV = null;
    ComboBoxEditor m_allocatedComboxBoxEditor = new ComboBoxEditor();
    SafeDictionary<UInt32, ListItem> m_groupsIdItemDict = new SafeDictionary<UInt32, ListItem>();
    UInt32 m_userClicked;
    bool m_displayEntities = false;
    bool m_isLoadingEntities = false;

    #endregion

    #region Initialize

    public UsersView()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as UserController;
    }

    public void LoadView()
    {
      DisplayEntities(false);
      ComboBoxInit();
      UserDGVInit();
      EntitiesTVInit();
      SuscribeEvents();
    }
    
    void SuscribeEvents()
    {
      m_entitiesTV.NodeChecked += OnEntitiesTVNodeChecked;
      UserModel.Instance.UpdateEvent += OnUserModelUpdateEvent;
      UserModel.Instance.ReadEvent += OnUserModelReadEvent;
      UserAllowedEntityModel.Instance.UpdateEvent += OnUserAllowedModelUpdateEvent;
      UserAllowedEntityModel.Instance.ReadEvent += OnUserAllowedModelReadEvent;
      UserAllowedEntityModel.Instance.DeleteEvent += OnUserAllowedModelDeleteEvent;
      UserAllowedEntityModel.Instance.CreationEvent += OnUserAllowedModelCreationEvent;
      m_userDGV.CellMouseDown += OnCellMouseDown;
      Addin.SuscribeAutoLock(this);
    }

    void EntitiesTVInit()
    {
      m_entitiesTV = new FbiTreeView<AxisElem>(AxisElemModel.Instance.GetDictionary(AxisType.Entities));

      m_entitiesTV.CheckBoxes = true;
      m_entitiesTV.TriStateMode = true;
      m_entitiesTV.Dock = DockStyle.Fill;
      PanelEntities.Controls.Add(m_entitiesTV);
    }

    void ComboBoxInit()
    {
      MultiIndexDictionary<UInt32, string, Group> l_groupMID = GroupModel.Instance.GetDictionary();

      foreach (Group l_group in l_groupMID.SortedValues)
      {
        ListItem l_itemGroup = new ListItem();

        l_itemGroup.Text = l_group.Name;
        l_itemGroup.Value = l_group.Id;
        m_groupsIdItemDict[(UInt32)l_itemGroup.Value] = l_itemGroup;
        m_allocatedComboxBoxEditor.Items.Add(l_itemGroup);
      }
      m_allocatedComboxBoxEditor.DropDownList = true;
    }

    void UserDGVInit()
    {
      m_userDGV.SetDimension(FbiDataGridView.Dimension.COLUMN, (UInt32)Column.GROUP, Local.GetValue("general.group"));
      m_userDGV.SetDimension(FbiDataGridView.Dimension.COLUMN, (UInt32)Column.ENTITIES, Local.GetValue("general.entities"));

      MultiIndexDictionary<UInt32, string, User> l_userMID = UserModel.Instance.GetDictionary();
      m_userDGV.InitializeRows<User>(UserModel.Instance, l_userMID);
      User l_currentUser = UserModel.Instance.GetCurrentUser();

      foreach (User l_user in l_userMID.Values)
      {
        if (m_groupsIdItemDict.ContainsKey(l_user.GroupId))
          m_userDGV.FillField<ListItem, ComboBoxEditor>(l_user.Id, 1, m_groupsIdItemDict[l_user.GroupId], 
            (l_currentUser.Id != l_user.Id) ? m_allocatedComboxBoxEditor : null);
        else
          m_userDGV.FillField<ListItem, ComboBoxEditor>(l_user.Id, 1, null, (l_currentUser.Id != l_user.Id) ? m_allocatedComboxBoxEditor : null);
        SetEntities(l_user);
      }

      m_userDGV.CellChangedAndValidated += OnUserDGVCellChangedAndValidated;
      m_userDGV.CellEditorActivate += OnUserDGVCellEditorActivate;

      m_userDGV.Dock = DockStyle.Fill;
      LayoutPanel.Controls.Add(m_userDGV);
    }

    #endregion

    #region Event

    #region DataGridView

    void OnCellMouseDown(object p_sender, CellMouseEventArgs p_args)
    {
      if (p_args.Cell == null || p_args.Cell.ColumnItem == null || p_args.Cell.RowItem == null)
        return;
      if ((Column)(UInt32)p_args.Cell.ColumnItem.ItemValue == Column.ENTITIES)
      {
        m_userClicked = (UInt32)p_args.Cell.RowItem.ItemValue;
        if (m_userClicked != UserModel.Instance.GetCurrentUser().Id)
         DisplayEntities(!m_displayEntities);
      }
    }

    void OnUserDGVCellChangedAndValidated(object p_sender, CellEventArgs p_args)
    {
      ListItem l_listItem = null; ;

      if (p_args.Cell == null || p_args.Cell.Editor == null || p_args.Cell.Editor.GetType() != typeof(ComboBoxEditor))
        return;
      l_listItem = ((ComboBoxEditor)p_args.Cell.Editor).SelectedItem;

      User l_user = UserModel.Instance.GetValue((UInt32)p_args.Cell.RowItem.ItemValue);

      if (l_user != null)
      {
        p_args.Cell.Value = GroupModel.Instance.GetValueName(l_user.GroupId);
        if (l_listItem != null)
        {
          l_listItem = l_listItem.Clone();
          if (m_groupsIdItemDict.ContainsKey(l_user.GroupId))
            ((ComboBoxEditor)p_args.Cell.Editor).SelectedItem = m_groupsIdItemDict[l_user.GroupId];
          l_user = l_user.Clone();
          l_user.GroupId = (UInt32)l_listItem.Value;
        }
        m_controller.UpdateUser(l_user);
      }
    }

    void OnUserDGVCellEditorActivate(object p_sender, EditorActivationCancelEventArgs p_args)
    {
      m_userClicked = (UInt32)p_args.Cell.RowItem.ItemValue;
    }

    #endregion

    #region TreeView

    void OnEntitiesTVNodeChecked(object p_sender, vTreeViewEventArgs p_e)
    {
      if (!m_isLoadingEntities)
      {
        m_isLoadingEntities = true;
        if (p_e.Node.Checked == CheckState.Checked)
        {
          AxisElem l_entity = AxisElemModel.Instance.GetValue(AxisType.Entities, (UInt32)p_e.Node.Value);
          UserAllowedEntity l_userAllowed = new UserAllowedEntity();

          p_e.Node.Checked = CheckState.Unchecked;
          if (l_entity != null)
          {
            l_userAllowed.UserId = m_userClicked;
            l_userAllowed.EntityId = l_entity.Id;
            m_controller.CreateUserAllowed(l_userAllowed);
          }
        }
        else
        {
          UserAllowedEntity l_userAllowed = UserAllowedEntityModel.Instance.GetValue(m_userClicked, (UInt32)p_e.Node.Value);

          p_e.Node.Checked = CheckState.Checked;
          if (l_userAllowed != null)
            m_controller.DeleteUserAllowed(l_userAllowed.Id, l_userAllowed.UserId, l_userAllowed.EntityId);
        }
        m_isLoadingEntities = false;
      }
    }

    #endregion

    #region TextBoxEditor

    void OnEntitiesTextBoxEditorMouseDown(object p_sender, MouseEventArgs p_e)
    {
      DisplayEntities(!m_displayEntities);
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
          if (m_groupsIdItemDict.ContainsKey(p_attributes.GroupId))
            m_userDGV.FillField<ListItem, ComboBoxEditor>(p_attributes.Id, 1, m_groupsIdItemDict[p_attributes.GroupId], m_allocatedComboxBoxEditor);
          else
            m_userDGV.FillField<ListItem, ComboBoxEditor>(p_attributes.Id, 1, null, m_allocatedComboxBoxEditor);
        }
      }
    }

    delegate void OnUserModelUpdateEvent_delegate(ErrorMessage p_status, UInt32 p_id);
    void OnUserModelUpdateEvent(ErrorMessage p_status, UInt32 p_id)
    {
      if (p_status != Network.ErrorMessage.SUCCESS)
        MessageBox.Show(Error.GetMessage(p_status));
    }

    #endregion

    #region UserAllowed

    delegate void OnUserAllowedModelReadEvent_delegate(ErrorMessage p_status, UserAllowedEntity p_attributes);
    void OnUserAllowedModelReadEvent(ErrorMessage p_status, UserAllowedEntity p_attributes)
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
          if (p_attributes.UserId == m_userClicked)
          {
            MultiIndexDictionary<UInt32, string, User> l_userMID = UserModel.Instance.GetDictionary();

            SetEntities(UserModel.Instance.GetValue(m_userClicked));
            m_userDGV.Refresh();
            if (m_displayEntities)
              DisplayEntities(true);
          }
        }
      }
    }

    delegate void OnUserAllowedModelUpdateEvent_delegate(ErrorMessage p_status, UInt32 p_id);
    void OnUserAllowedModelUpdateEvent(ErrorMessage p_status, UInt32 p_id)
    {
      if (InvokeRequired)
      {
        OnUserAllowedModelUpdateEvent_delegate func = new OnUserAllowedModelUpdateEvent_delegate(OnUserAllowedModelUpdateEvent);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != Network.ErrorMessage.SUCCESS)
          MessageBox.Show(Error.GetMessage(p_status));
      }
    }

    delegate void OnUserAllowedModelDeleteEvent_delegate(ErrorMessage p_status, UInt32 p_id);
    void OnUserAllowedModelDeleteEvent(ErrorMessage p_status, UInt32 p_id)
    {
      if (InvokeRequired)
      {
        OnUserAllowedModelDeleteEvent_delegate func = new OnUserAllowedModelDeleteEvent_delegate(OnUserAllowedModelDeleteEvent);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != Network.ErrorMessage.SUCCESS)
          MessageBox.Show(Error.GetMessage(p_status));
        SetEntities(UserModel.Instance.GetValue(m_userClicked));
        m_userDGV.Refresh();
        if (m_displayEntities)
          DisplayEntities(true);
      }
    }

    delegate void OnUserAllowedModelCreationEvent_delegate(ErrorMessage p_status, UInt32 p_id);
    void OnUserAllowedModelCreationEvent(ErrorMessage p_status, UInt32 p_id)
    {
      if (InvokeRequired)
      {
        OnUserAllowedModelCreationEvent_delegate func = new OnUserAllowedModelCreationEvent_delegate(OnUserAllowedModelCreationEvent);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != Network.ErrorMessage.SUCCESS)
          MessageBox.Show(Error.GetMessage(p_status));
      }
    }

    #endregion

    #endregion

    #endregion

    #region Utils

    void DisplayEntities(bool p_status)
    {
      m_displayEntities = p_status;
      if (p_status)
      {
        LayoutPanel.ColumnStyles[1].Width = 350;
        LoadCurrentEntities();
      }
      else
        LayoutPanel.ColumnStyles[1].Width = 0;
      LayoutPanel.ResumeLayout();
    }

    void LoadCurrentEntities()
    {
      m_isLoadingEntities = true;
      CheckEntityTree(m_entitiesTV.Nodes, false);
      m_entitiesTV.Refresh();
      m_isLoadingEntities = false;
    }

    void CheckEntityTree(vTreeNodeCollection p_nodes, bool p_status)
    {
      foreach (vTreeNode l_node in p_nodes)
      {
        bool l_checkedParent = SetCaseStatus(l_node, p_status);

        foreach (vTreeNode l_subNode in l_node.Nodes)
        {
          bool l_checkedChild = SetCaseStatus(l_subNode, l_checkedParent);
          CheckEntityTree(l_subNode.Nodes, l_checkedChild);
        }
      }
    }

    bool SetCaseStatus(vTreeNode p_node, bool p_status)
    {
      p_node.ShowCheckBox = true;
      if (p_status)
      {
        p_node.ShowCheckBox = false;
        return (true);
      }
      if (m_controller.IsAllowedEntity(m_userClicked, (UInt32)p_node.Value))
      {
        p_node.Checked = CheckState.Checked;
        return (true);
      }
      p_node.Checked = CheckState.Unchecked;
      return (false);
    }

    void SetEntities(User p_user)
    {
      if (p_user == null)
        return;
      MultiIndexDictionary<UInt32, UInt32, UserAllowedEntity> l_entitiesAllowed = UserAllowedEntityModel.Instance.GetDictionary(p_user.Id);
      string l_stringEntities = "";

      m_userDGV.FillField<string, TextBoxEditor>(p_user.Id, 2, l_stringEntities, null);
      if (l_entitiesAllowed != null)
      {
        foreach (UserAllowedEntity l_allowed in l_entitiesAllowed.Values)
        {
          AxisElem l_axisEntity;

          if ((l_axisEntity = AxisElemModel.Instance.GetValue(AxisType.Entities, l_allowed.EntityId)) != null)
            l_stringEntities += l_axisEntity.Name + "; ";
        }
      }
      m_userDGV.FillField<string, TextBoxEditor>(p_user.Id, 2, l_stringEntities, null);
    }

    #endregion

  }
}
