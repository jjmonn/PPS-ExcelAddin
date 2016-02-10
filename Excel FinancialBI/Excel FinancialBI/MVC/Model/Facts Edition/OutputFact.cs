using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using Microsoft.Office.Interop.Excel;
  using FBI.MVC.Model.CRUD;

  class OutputFact : EditedFact
  {
    public OutputFact(Fact p_fact, Range p_cell) : base(p_fact, p_cell, Account.AccountProcess.FINANCIAL) { }

    // Attention : le fact est à créer, c'est un output qui ne vient pas de la DB
    // Renommer ou ne pas utiliser l'objet fact ?

   

  }
}
