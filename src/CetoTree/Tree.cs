using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CetoTree
{
    public class Tree<T>
    {
        public virtual int Id { get; set; }

        public virtual TreeNode<T> RootNode { get; set; }

        #region constructors
        public Tree() { }
        public Tree(int id, TreeNode<T> rootNode)
        {
            Id = id;
            RootNode = rootNode;
        }
        #endregion
    }
}
