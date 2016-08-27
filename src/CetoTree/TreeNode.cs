using System.Collections.Generic;

namespace CetoTree
{
    /// <summary>
    /// Class representing a node of a tree.
    /// The class contains links to parent and child nodes of this node.
    /// </summary>
    /// <typeparam name="T">The content type (payload) of the node, for the data
    /// to be represented by the tree</typeparam>
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
