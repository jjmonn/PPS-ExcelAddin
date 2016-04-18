using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class ChartConf : CRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 PanelId { get; set; }
    public string Name { get; set; }
    public UInt32 Image { get; set; }

    public ChartConf() { }
    private ChartConf(UInt32 p_id)
    {
      Id = p_id;
    }

    public static ChartConf BuildChart(ByteBuffer p_packet)
    {
      ChartConf l_chart = new ChartConf(p_packet.ReadUint32());

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

    public void CopyFrom(ChartConf p_model)
    {
      PanelId = p_model.PanelId;
      Name = p_model.Name;
    }

    public ChartConf Clone()
    {
      ChartConf l_clone = new ChartConf(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      ChartConf l_cmpChart = p_obj as ChartConf;

      if (l_cmpChart == null)
        return 0;
      if (l_cmpChart.PanelId > PanelId)
        return -1;
      else
        return 1;
    }
  }
}
