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

        public override bool Equals(object obj)
        {
            Tree<T> treeToCompare = (Tree<T>)obj;

            var stack1 = new Stack<TreeNode<T>>();
            var stack2 = new Stack<TreeNode<T>>();

            stack1.Push(this.RootNode);
            stack2.Push(treeToCompare.RootNode);

            while (stack1.Count > 0 && stack2.Count > 0)
            {
                var i1 = stack1.Pop();
                var i2 = stack2.Pop();

                if (!i1.NodeContent.Equals(i2.NodeContent))
                    return false;

                i1.ChildNodes.ForEach(x => stack1.Push(x));
                i2.ChildNodes.ForEach(x => stack2.Push(x));

                if (stack1.Count != stack2.Count)
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
