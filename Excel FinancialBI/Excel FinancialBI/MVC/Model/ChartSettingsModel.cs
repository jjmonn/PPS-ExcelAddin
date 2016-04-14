using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using Network;
  using Forms;
  using Model.CRUD;

  class ChartSettingsModel
  {
    public const UInt32 INVALID = 0;

    static ChartSettingsModel s_instance = new ChartSettingsModel();
    public static ChartSettingsModel Instance { get { return (s_instance); } }

    private SafeDictionary<UInt32, ChartSettings> m_settings = new SafeDictionary<uint, ChartSettings>();
    private UInt32 m_lastSettingsId = INVALID + 1;

    //Events
    public event CreationEventHandler CreationEvent;
    public delegate void CreationEventHandler(ErrorMessage status, UInt32 id);
    public event UpdateEventHandler UpdateEvent;
    public delegate void UpdateEventHandler(ErrorMessage status, UInt32 id);
    /*public event DeleteEventHandler DeleteEvent;
    public delegate void DeleteEventHandler(ErrorMessage status, UInt32 id);*/

    public void Create(ChartSettings p_settings)
    {
      ChartSettings l_settings = new ChartSettings(m_lastSettingsId);

      l_settings.Name = p_settings.Name;
      l_settings.Series = p_settings.Series;
      l_settings.HasDeconstruction = p_settings.HasDeconstruction;
      l_settings.Versions = p_settings.Versions;
      l_settings.Deconstruction = p_settings.Deconstruction;
      m_settings[m_lastSettingsId] = l_settings;

      if (CreationEvent != null)
        CreationEvent(ErrorMessage.SUCCESS, m_lastSettingsId);
      m_lastSettingsId++;
    }

    public void Update(UInt32 p_id, ChartSettings p_settings)
    {
      ChartSettings l_settings;

      if ((l_settings = m_settings[p_id]) == null)
        return;

      l_settings.Name = p_settings.Name;
      l_settings.Series = p_settings.Series;
      l_settings.HasDeconstruction = p_settings.HasDeconstruction;
      l_settings.Versions = p_settings.Versions;
      l_settings.Deconstruction = p_settings.Deconstruction;

      if (UpdateEvent != null)
        UpdateEvent(ErrorMessage.SUCCESS, p_id);
    }

    //TODO Remove...

    public SafeDictionary<UInt32, ChartSettings> GetDictionnary()
    {
      return (m_settings);
    }

    public ChartSettings GetValue(UInt32 p_settingsId)
    {
      return (m_settings[p_settingsId]);
    }
  }
}
