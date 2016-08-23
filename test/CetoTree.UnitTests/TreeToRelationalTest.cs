using CetoTree.UnitTests.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CetoTree.UnitTests
{
    public class TreeToRelationalTest
    {
        [Fact]
        public void TreeToRelationalCorrectTest()
        {
            Tree<TestContent> tree = new Tree<TestContent>();

            tree.RootNode = new TreeNode<TestContent>(new TestContent() { Content = "Root" });
            List<TreeNode<TestContent>> firstLayer = new List<TreeNode<TestContent>>()
             {
                 new TreeNode<TestContent>(new TestContent() { Content = "Hello" }),
                 new TreeNode<TestContent>(new TestContent() { Content = "Tree" }),
                 new TreeNode<TestContent>(new TestContent() { Content = "Mapper" })
             };
            int cnt = 0;
            foreach (var node in firstLayer)
            {
                for (int i = 0; i < 3; ++i)
                    node.ChildNodes.Add(new TreeNode<TestContent>(new TestContent() { Content = "<" + cnt + " " + i + ">" }));
                cnt++;
            }
            tree.RootNode.ChildNodes = firstLayer;

            List<string> referenceContents = new List<string>()
            {
                "Root",
                "Hello",
                "<0 0>",
                "<0 1>",
                "<0 2>",
                "Tree",
                "<1 0>",
                "<1 1>",
                "<1 2>",
                "Mapper",
                "<2 0>",
                "<2 1>",
                "<2 2>"
            };

            List<Tuple<int, int>> referenceResult = new List<Tuple<int, int>>()
            {
                Tuple.Create(0, 25),
                Tuple.Create(1,8),
                Tuple.Create( 2,3),
                Tuple.Create( 4,5),
                Tuple.Create( 6,7),
                Tuple.Create( 9, 16),
                Tuple.Create( 10,11),
                Tuple.Create( 12,13),
                Tuple.Create( 14,15),
                Tuple.Create( 17,24),
                Tuple.Create( 18,19),
                Tuple.Create( 20,21),
                Tuple.Create( 22,23)
            };

            var converter = new TreeRelationalModelConverter<TestContent>();
            var res = converter.ConvertToRelationalModel(tree);

            Assert.NotNull(res.Item1);
            var nodes = res.Item2;
            var contents = res.Item3;

            Assert.Equal(13, nodes.Count);
            Assert.Equal(13, contents.Count);

            contents.ForEach(c => Assert.True(referenceContents.Contains(c.Content)));

            nodes.ForEach(node =>
            {
                Assert.NotNull(referenceResult.FirstOrDefault(x => x.Item1 == node.PreOrderId && x.Item2 == node.PostOrderId));
            });
        }
    }
}
