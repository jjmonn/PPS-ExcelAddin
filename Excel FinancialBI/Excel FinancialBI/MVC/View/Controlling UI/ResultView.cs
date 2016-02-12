using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace FBI.MVC.View.Controlling_UI
{
  using Controller;
  using FBI.Forms;
  using Model;
  using Model.CRUD;
  using Utils;

  public partial class ResultView : UserControl, IView
  {
    BaseFbiDataGridView<ResultViewDgvKey> m_dgv = new BaseFbiDataGridView<ResultViewDgvKey>();
    private delegate void dgvBuilder(CuiDgvConf p_conf, FbiDataGridView.Dimension p_dimension);
    List<Tuple<Type, dgvBuilder>> m_builderList;
 
    public ResultView()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {

    }

    public void LoadView()
    {
      m_builderList.Add(new Tuple<Type, dgvBuilder>(typeof(PeriodModel), periodBuilder));
      m_builderList.Add(new Tuple<Type, dgvBuilder>(typeof(VersionModel), versionBuilder));
      m_builderList.Add(new Tuple<Type, dgvBuilder>(typeof(AxisElemModel), axisElemBuilder));
      m_builderList.Add(new Tuple<Type, dgvBuilder>(typeof(FilterValueModel), filterValueBuilder));
      m_builderList.Add(new Tuple<Type, dgvBuilder>(typeof(AccountModel), accountBuilder));
    }

    public void PrepareDgv(CuiDgvConf p_rows, CuiDgvConf p_collumns)
    {
      findMethod(p_rows, FbiDataGridView.Dimension.ROW);
      findMethod(p_collumns, FbiDataGridView.Dimension.COLUMN);
    }

    private void findMethod(CuiDgvConf p_conf, FbiDataGridView.Dimension p_dimension)
    {
      foreach (Tuple<Type, dgvBuilder> l_elem in m_builderList)
      {
        if (p_conf.ModelType == l_elem.Item1)
        {
          l_elem.Item2(p_conf, p_dimension);
          break;
        }
      }
    }

    private void periodBuilder(CuiDgvConf p_conf, FbiDataGridView.Dimension p_dimension)
    {
      PeriodConf l_conf = p_conf as PeriodConf;
      List<int> l_periodList = PeriodModel.GetPeriodList(l_conf.StartPeriod, l_conf.NbPeriods, l_conf.PeriodRange);
      string l_formatedDate;


      foreach (int l_date in l_periodList)
      {
        l_formatedDate = DateTime.FromOADate(42004).ToString("MMM d yyyy", DateTimeFormatInfo.InvariantInfo);
        ResultViewDgvKey l_key = new ResultViewDgvKey();
       /* m_dgv.SetDimension(p_dimension, )*/
      }
    }

    private void versionBuilder(CuiDgvConf p_conf, FbiDataGridView.Dimension p_dimension)
    {

    }

    private void axisElemBuilder(CuiDgvConf p_conf, FbiDataGridView.Dimension p_dimension)
    {
      AxisElemConf l_conf = p_conf as AxisElemConf;
    }

    private void filterValueBuilder(CuiDgvConf p_conf, FbiDataGridView.Dimension p_dimension)
    {

    }

    private void accountBuilder(CuiDgvConf p_conf, FbiDataGridView.Dimension p_dimension)
    {

    }
  }

  public class ResultViewDgvKey
  {
    public enum ModelType
    {
      PERIOD,
      VERSION,
      AXISELEM,
      FILTERVALUE,
      ACCOUNTMODEL
    };/*
    ResultViewDgvKey(ModelType p_model, int p_ke
    ModelType m_model;
    int m_key;*/
  }
}
