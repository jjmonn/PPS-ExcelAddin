using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class ChartSettings : CRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 PanelId { get; set; }
    public string Name { get; set; }
    public UInt32 Image { get; set; }

    //NOT ON DATABASE, ONLY IN SESSION !
    public bool HasDeconstruction { get; set; }
    public List<UInt32> Versions { get; set; }
    public Tuple<bool, AxisType, UInt32> Deconstruction { get; set; }

    public ChartSettings() { }
    private ChartSettings(UInt32 p_id)
    {
      Id = p_id;
    }

    public static ChartSettings BuildChart(ByteBuffer p_packet)
    {
      ChartSettings l_chart = new ChartSettings(p_packet.ReadUint32());

      l_chart.PanelId = p_packet.ReadUint32();
      l_chart.Name = p_packet.ReadString();

      return (l_chart);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(PanelId);
      p_packet.WriteString(Name);
    }

    public void CopyFrom(ChartSettings p_model)
    {
      PanelId = p_model.PanelId;
      Name = p_model.Name;
    }

    public ChartSettings Clone()
    {
      ChartSettings l_clone = new ChartSettings(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      ChartSettings l_cmpChart = p_obj as ChartSettings;

      if (l_cmpChart == null)
        return 0;
      if (l_cmpChart.PanelId > PanelId)
        return -1;
      else
        return 1;
    }
  }
}
