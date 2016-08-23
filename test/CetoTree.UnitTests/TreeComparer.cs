using CetoTree.UnitTests.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CetoTree.UnitTests
{
    public class TreeComparer
    {
        public static bool TreesEqual(Tree<TestContent> tree1, Tree<TestContent> tree2)
        {
            var stack1 = new Stack<TreeNode<TestContent>>();
            var stack2 = new Stack<TreeNode<TestContent>>();

            stack1.Push(tree1.RootNode);
            stack2.Push(tree2.RootNode);

            while(stack1.Count>0 && stack2.Count>0)
            {
                var i1 = stack1.Pop();
                var i2 = stack2.Pop();

                if (i1.NodeContent.Content != i2.NodeContent.Content)
                    return false;

                i1.ChildNodes.ForEach(x => stack1.Push(x));
                i2.ChildNodes.ForEach(x => stack2.Push(x));

                if (stack1.Count != stack2.Count)
                    return false;
            }
            return true;
        }
    }
}
