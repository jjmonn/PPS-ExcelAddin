using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.View
{
  using Controller;
  using FBI;
  using Utils;
  using Model;
  using Network;

  public partial class StatusReportInterfaceUI : Form, IView
  {
    IFactEditionController m_controller;
    AEditedFactsModel m_model;

    public StatusReportInterfaceUI(AEditedFactsModel p_model)
    {
      InitializeComponent();
      MultilangueSetup();
      m_model = p_model;
      SuscribeEvents();
    }

    public void Close()
    {
      m_model.OnCommitError -= OnCommitError;
    }

    void SuscribeEvents()
    {
      m_model.OnCommitError += OnCommitError;
    }

    private void MultilangueSetup()
    {
      this.Text = Local.GetValue("title_upload_error_messages");
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as IFactEditionController;
    }

    public void OnCommitError(string p_address, ErrorMessage p_message)
    {
      m_errorsListBox.Items.Add(p_address + ": " + Error.GetMessage(p_message));
    }
  }
}
