using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  public class Serie
  {
    public Color Color;
    public Account Account;

    public Serie(Account p_acc, Color p_color)
    {
      this.Account = p_acc;
      this.Color = p_color;
    }

    public static List<Serie> FromChartSettings(ChartSettings p_settings)
    {
      Account l_account;
      List<Serie> l_series = new List<Serie>();

      foreach (var l_item in ChartAccountModel.Instance.GetDictionary(p_settings.Id))
      {
        if ((l_account = AccountModel.Instance.GetValue(l_item.Value.AccountId)) != null)
        {
          l_series.Add(new Serie(l_account, Color.FromArgb(l_item.Value.Color)));
        }
      }
      return (l_series);
    }
  }
}
