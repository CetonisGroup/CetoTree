using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CetoTree.UnitTests
{
    public class TreeTest
    {
        [Fact]
        public void TestTreeEquals()
        {
            var tree = TestTreeGenerator.GenerateTree(5, 5);
            var corrTree = TestTreeGenerator.GenerateTree(5, 5);
                        
            var wrongTree1 = TestTreeGenerator.GenerateTree(4, 5);
            var wrongTree2 = TestTreeGenerator.GenerateTree(5, 4);
            var wrongTree3 = TestTreeGenerator.GenerateTree(5, 5);
            wrongTree3.RootNode.ChildNodes[1].ChildNodes[2].NodeContent.Content = "Wr";
            var wrongTree4 = TestTreeGenerator.GenerateTree(4, 5);
            wrongTree4.RootNode.ChildNodes[0].ChildNodes[1].NodeContent.Content = "Wr";
            var wrongTree5 = TestTreeGenerator.GenerateTree(5, 4);
            wrongTree5.RootNode.ChildNodes[2].NodeContent.Content = "Wr";

            Assert.True(tree.Equals(corrTree));
            Assert.False(tree.Equals(wrongTree1));
            Assert.False(tree.Equals(wrongTree2));
            Assert.False(tree.Equals(wrongTree3));
            Assert.False(tree.Equals(wrongTree4));
            Assert.False(tree.Equals(wrongTree5));
        }
    }
}
