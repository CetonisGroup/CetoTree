using CetoTree.UnitTests.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CetoTree.UnitTests
{
    public class TestTreeGenerator
    {
        public static Tree<TestContent> GenerateTree(uint depth, int childrenPerNode)
        {
            Tree<TestContent> tree = new Tree<TestContent>();

            TreeNode<TestContent> rootNode = new TreeNode<TestContent>(new TestContent() { Content = "Root Node" });

            List<TreeNode<TestContent>> currentLayer = new List<TreeNode<TestContent>>() { rootNode };
            List<TreeNode<TestContent>> newLayer = new List<TreeNode<TestContent>>();

            tree.RootNode = rootNode;

            for(uint i = 0; i<depth;++i)
            {
                uint nr = 0;
                foreach(var node in currentLayer)
                {
                    for(int j=0; j<childrenPerNode;++j)
                    {
                        var childNode = new TreeNode<TestContent>() { NodeContent = new TestContent() { Content = "Layer " + i + " Parent: " + nr + " Child: " + j } };
                        node.ChildNodes.Add(childNode);
                        newLayer.Add(childNode);
                    }
                    nr++;
                }

                currentLayer = new List<TreeNode<TestContent>>(newLayer);
                newLayer.Clear();
            }
            return tree;
        }


    }
}
