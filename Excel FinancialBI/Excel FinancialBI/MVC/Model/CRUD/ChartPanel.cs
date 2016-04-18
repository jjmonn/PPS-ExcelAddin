using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class ChartPanel : CRUDEntity, IComparable, NamedCRUDEntity
  {
    public UInt32 Id { get; private set; }
    public UInt32 UserId { get; set; }
    public string Name { get; set; }
    public UInt32 Image { get; set; }

    public ChartPanel() { }
    private ChartPanel(UInt32 p_id)
    {
      Id = p_id;
    }

    public static ChartPanel BuildChartPanel(ByteBuffer p_packet)
    {
      ChartPanel l_panel = new ChartPanel(p_packet.ReadUint32());

      l_panel.UserId = p_packet.ReadUint32();
      l_panel.Name = p_packet.ReadString();

      return (l_panel);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(UserId);
      p_packet.WriteString(Name);
    }

    public void CopyFrom(ChartPanel p_model)
    {
      UserId = p_model.UserId;
      Name = p_model.Name;
    }

    public ChartPanel Clone()
    {
      ChartPanel l_clone = new ChartPanel(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      ChartPanel l_cmpPanel = p_obj as ChartPanel;

      if (l_cmpPanel == null)
        return 0;
      if (l_cmpPanel.UserId > UserId)
        return -1;
      else
        return 1;
    }
  }
}
