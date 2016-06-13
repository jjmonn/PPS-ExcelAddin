using System;
using System.Collections.Generic;

using FBI.Network;
/*
namespace FBI.MVC.Model.CRUD
{
  class ClassModelCRUD
  {
    public UInt32 m_id;
    public Int32 m_value;

    public void BuildClassModelCRUD(ByteBuffer p_packet); // optional: Fill object with data contained in p_packet
    public static void Dump(ClassModelCRUD p_crud, ByteBuffer p_packet); // optional: Fill p_packet with data from p_crud
  }
}

namespace FBI.MVC.Model
{
  using CRUD;

  static class ClassModel
  {
    static private Dictionary<UInt32, ClassModelCRUD> m_valuesDictionary;

    public ClassModel()
    {
      m_valuesDictionary = new Dictionary<UInt32, ClassModelCRUD>();
    }

    static public void GetValue(UInt32 p_dictionaryKey);
    static public void GetDictionary();
    static public void Create(ClassModelCRUD p_value);
  }
}

namespace FBI.MVC.Controller
{
  using View;
  using Model;
  using Model.CRUD;

  class ClassController : IController
  {
    private ClassView m_view;
    public IView View { get { return (m_view); } }
    public string Error { get; set; }

    public ClassController()
    {
      m_view = new ClassView();
      m_view.SetController(this);
      LoadView();
    }

    public void LoadView()
    {
      m_view.LoadView();
    }

    public bool CheckCreate(ClassModelCRUD p_value)
    {
      if (p_value.m_value < 0)
      {
        Error = "Value is under 0";
        return (false);
      }
      return (true);
    }

    public bool Create(ClassModelCRUD p_value)
    {
      if (CheckCreate(p_value) == false)
        return (false);
      ClassModel.Create(p_value);
      return (true);
    }
  }
}

namespace FBI.MVC.View
{
  using Controller;

  class ClassView : IView
  {
    private ClassController m_controller;

    public ClassView();
    public void LoadView();
    public void SetController(IController p_controller);
  }
}
*/