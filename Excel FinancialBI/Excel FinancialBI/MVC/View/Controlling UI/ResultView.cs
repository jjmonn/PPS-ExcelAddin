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
      m_builderList.Add(new Tuple<Type, dgvBuilder>(typeof(PeriodModel), PeriodBuilder));
      m_builderList.Add(new Tuple<Type, dgvBuilder>(typeof(VersionModel), VersionBuilder));
      m_builderList.Add(new Tuple<Type, dgvBuilder>(typeof(AxisElemModel), AxisElemBuilder));
      m_builderList.Add(new Tuple<Type, dgvBuilder>(typeof(FilterValueModel), FilterValueBuilder));
      m_builderList.Add(new Tuple<Type, dgvBuilder>(typeof(AccountModel), AccountBuilder));
    }

    public void PrepareDgv(CuiDgvConf p_rows, CuiDgvConf p_columns)
    {
      FindMethod(p_rows, FbiDataGridView.Dimension.ROW);
      FindMethod(p_columns, FbiDataGridView.Dimension.COLUMN);
    }

    private void FindMethod(CuiDgvConf p_conf, FbiDataGridView.Dimension p_dimension)
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

    private void PeriodBuilder(CuiDgvConf p_conf, FbiDataGridView.Dimension p_dimension)
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

    private void VersionBuilder(CuiDgvConf p_conf, FbiDataGridView.Dimension p_dimension)
    {

    }

    private void AxisElemBuilder(CuiDgvConf p_conf, FbiDataGridView.Dimension p_dimension)
    {
      AxisElemConf l_conf = p_conf as AxisElemConf;
    }

    private void FilterValueBuilder(CuiDgvConf p_conf, FbiDataGridView.Dimension p_dimension)
    {

    }

    private void AccountBuilder(CuiDgvConf p_conf, FbiDataGridView.Dimension p_dimension)
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
