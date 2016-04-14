using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  public class ChartSettings
  {
    private UInt32 m_id;

    private string m_name = "";
    private List<Serie> m_series = new List<Serie>();

    private bool m_hasDeconstruction;
    private List<UInt32> m_versions;
    private Tuple<bool, AxisType, UInt32> m_deconstruction;
    private TimeConfig m_timeConfig;

    public ChartSettings(UInt32 p_id = ChartSettingsModel.INVALID)
    {
      m_id = p_id;
    }

    public UInt32 Id
    {
      get { return (m_id); }
    }

    public string Name
    {
      get { return (m_name); }
      set { m_name = value; }
    }

    public List<Serie> Series
    {
      get { return (m_series); }
      set { m_series = value; }
    }

    public Serie this[int p_key]
    {
      get { return (m_series[p_key]); }
      set { m_series[p_key] = value; }
    }

    //

    public TimeConfig TimeConfig
    {
      get { return (m_timeConfig); }
      set { m_timeConfig = value; }
    }

    public bool HasDeconstruction
    {
      get { return (m_hasDeconstruction); }
      set { m_hasDeconstruction = value; }
    }

    public List<UInt32> Versions
    {
      get { return (m_versions); }
      set { m_versions = value; }
    }

    public Tuple<bool, AxisType, UInt32> Deconstruction
    {
      get { return (m_deconstruction); }
      set { m_deconstruction = value; }
    }

    //

    public bool Has(int p_index)
    {
      return (p_index < m_series.Count);
    }

    public bool AddSerie(string p_accountName, Color p_color)
    {
      Serie l_serie;
      Account l_account;

      if ((l_account = AccountModel.Instance.GetValue(p_accountName)) == null)
        return (false);
      l_serie = new Serie();
      l_serie.Account = l_account;
      l_serie.Color = p_color;
      m_series.Add(l_serie);
      return (true);
    }

    public bool UpdateSerie(int p_index, string p_accountName, Color p_color)
    {
      Account l_account;

      if (!this.Has(p_index))
        return (false);
      if ((l_account = AccountModel.Instance.GetValue(p_accountName)) == null)
        return (false);
      m_series[p_index].Account = l_account;
      m_series[p_index].Color = p_color;
      return (true);
    }

    public bool AddUpdateSerie(int p_index, string p_accountName, Color p_color)
    {
      if (this.Has(p_index))
      {
        return (this.UpdateSerie(p_index, p_accountName, p_color));
      }
      return (this.AddSerie(p_accountName, p_color));
    }
  }
}
