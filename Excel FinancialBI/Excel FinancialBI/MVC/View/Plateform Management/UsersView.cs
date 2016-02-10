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
using VIBlend.WinForms.DataGridView;

namespace FBI.MVC.View
{
  using Controller;
  using Model;
  using Model.CRUD;
  using Forms;
  using Utils;

  public partial class UsersView : UserControl, IView
  {

    #region Variables

    private UserController m_controller = null;

    private FbiDataGridView m_userDGV = new FbiDataGridView();

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
      this.UserDGVInit();
    }

    private void UserDGVInit()
    {
      this.m_userDGV.SetDimension(FbiDataGridView.Dimension.COLUMN, 0, "Name"); //TODO : no hardcoded string
      this.m_userDGV.SetDimension(FbiDataGridView.Dimension.COLUMN, 1, "Group"); //TODO : no hardcoded string
      this.m_userDGV.SetDimension(FbiDataGridView.Dimension.COLUMN, 2, "Entities"); //TODO : no hardcoded string

      MultiIndexDictionary<UInt32, string, User> l_userMID = UserModel.Instance.GetDictionary();
      this.m_userDGV.InitializeRows<User>(UserModel.Instance, l_userMID);

      foreach (User l_user in l_userMID.Values)
        this.m_userDGV.FillField<string, TextBoxEditor>(l_user.Id, 0, l_user.Name, this.CreateTextBoxEditor());
      this.m_userDGV.Dock = DockStyle.Fill;
      this.LayoutPanel.Controls.Add(this.m_userDGV);
    }

    #endregion

    #region Utils

    private TextBoxEditor CreateTextBoxEditor()
    {
      TextBoxEditor l_textBoxEditor = new TextBoxEditor();

      l_textBoxEditor.Enabled = false;
      return (l_textBoxEditor);
    }

    #endregion

  }
}
