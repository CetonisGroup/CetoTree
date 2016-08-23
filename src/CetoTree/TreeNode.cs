using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CetoTree
{
    public class TreeNode<T>
    {
        public virtual int Id { get; set; }

        public virtual T NodeContent { get; set; }

        public virtual TreeNode<T> ParentNode { get; set; }

        public virtual List<TreeNode<T>> ChildNodes { get; set; }
        = new List<TreeNode<T>>();

        public virtual bool IsLeafNode { get { return ChildNodes.Count == 0; } }

        #region constructors
        public TreeNode() { }
        public TreeNode(T content)
        {
            NodeContent = content;
        }
        public TreeNode(T content, int id)
        {
            Id = id;
            NodeContent = content;
        }
        #endregion
    }
}
