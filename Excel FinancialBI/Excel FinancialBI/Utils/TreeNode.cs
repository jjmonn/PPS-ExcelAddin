using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils
{
  public class TreeNode<T>
  {
    private T m_value;
    private readonly List<TreeNode<T>> m_children = new List<TreeNode<T>>();

    public TreeNode(T p_value)
    {
      m_value = p_value;
    }

    public TreeNode<T> this[int p_index]
    {
      get { return (m_children[p_index]); }
    }

    public TreeNode<T> Parent { get; private set; }

    public T Value { get { return (m_value); } }

    public ReadOnlyCollection<TreeNode<T>> Children
    {
      get { return (m_children.AsReadOnly()); }
    }

    public TreeNode<T> AddChild(T p_value)
    {
      TreeNode<T> l_node = new TreeNode<T>(p_value) { Parent = this };
      m_children.Add(l_node);
      return (l_node);
    }

    public TreeNode<T> AddChild(TreeNode<T> p_node)
    {
      p_node.Parent = this;
      m_children.Add(p_node);
      return (p_node);
    }

    public bool RemoveChild(TreeNode<T> p_node)
    {
      return (m_children.Remove(p_node));
    }

    public void Traverse(Action<T> p_action)
    {
      p_action(Value);
      foreach (TreeNode<T> l_child in m_children)
        l_child.Traverse(p_action);
    }

    public IEnumerable<T> Flatten()
    {
      return new[] { Value }.Union(m_children.SelectMany(x => x.Flatten()));
    }
  }
}