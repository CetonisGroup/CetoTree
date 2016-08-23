using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CetoTree.UnitTests.TestUtil;
using CetoTree;
using System.Text;
using CetoTree.UnitTests.TestDataClasses;

namespace Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var tree = TestTreeGenerator.GenerateTree(3, 3);

            var sb = new StringBuilder();
            Print(tree, sb);
            sb.AppendLine("");
            sb.AppendLine("");

            var storage = new TreeSQLStorage();
            var _context = new AppDbContext();
            storage.StoreTree(tree, _context);
            _context.SaveChanges();

            var loadedTree = storage.LoadTree<TestContent>(1, _context);
            if (loadedTree?.RootNode != null)
                Print(loadedTree, sb);

            sb.AppendLine("");
            sb.Append("Stored and loaded tree");

            Console.Write(sb.ToString());
            Console.ReadLine();
        }

        static void Print<T>(Tree<T> tree, StringBuilder sb)
        {
            int cnt = 0;
            var nodes = new List<TreeNode<T>>();
            sb.Append(cnt + " " + tree.RootNode.NodeContent);
            nodes.Add(tree.RootNode);
            cnt++;

            while (nodes.Count > 0)
            {
                var node = nodes.ElementAt(0);
                nodes.RemoveAt(0);
                if (node == null)
                {
                    sb.Append("\n");
                    cnt++;
                }
                else
                {
                    sb.Append(node.NodeContent + " ");

                    node.ChildNodes.ForEach(x => nodes.Add(x));

                    if (nodes.Count > 0 && nodes.ElementAt(0) == null)
                    {
                        nodes.Add(null);
                    }
                }
            }
        }
    }
}
