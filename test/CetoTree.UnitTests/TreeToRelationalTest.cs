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
            Tree<TestContent> tree = CreateTestTree();

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


        [Fact]
        public void RelationalToTreeCorrectTest()
        {
            var converter = new TreeRelationalModelConverter<TestContent>();

            var nodes = new List<RelationalTreeNode<TestContent>>()
            {
                new RelationalTreeNode<TestContent>() {PreOrderId =  0,PostOrderId=25, Data = new TestContent() {Content="Root" } },
                new RelationalTreeNode<TestContent>() {PreOrderId =  1,PostOrderId=8, Data = new TestContent() {Content="Hello" } },
                  new RelationalTreeNode<TestContent>() {PreOrderId =  2,PostOrderId=3, Data = new TestContent() {Content="<0 0>" } },
                   new RelationalTreeNode<TestContent>() {PreOrderId =  4,PostOrderId=5, Data = new TestContent() {Content="<0 1>" } },
                    new RelationalTreeNode<TestContent>() {PreOrderId =  6,PostOrderId=7, Data = new TestContent() {Content="<0 2>" } },
                     new RelationalTreeNode<TestContent>() {PreOrderId =  9,PostOrderId=16, Data = new TestContent() {Content="Tree" } },
                      new RelationalTreeNode<TestContent>() {PreOrderId =  10,PostOrderId=11, Data = new TestContent() {Content="<1 0>" } },
                       new RelationalTreeNode<TestContent>() {PreOrderId =  12,PostOrderId=13, Data = new TestContent() {Content="<1 1>" } },
                        new RelationalTreeNode<TestContent>() {PreOrderId =  14,PostOrderId=15, Data = new TestContent() {Content="<1 2>" } },
                         new RelationalTreeNode<TestContent>() {PreOrderId =  17,PostOrderId=24, Data = new TestContent() {Content="Mapper" } },
                          new RelationalTreeNode<TestContent>() {PreOrderId =  18,PostOrderId=19, Data = new TestContent() {Content="<2 0>" } },
                           new RelationalTreeNode<TestContent>() {PreOrderId =  20,PostOrderId=21, Data = new TestContent() {Content="<2 1>" } },
                            new RelationalTreeNode<TestContent>() {PreOrderId =  22,PostOrderId=23, Data = new TestContent() {Content="<2 2>" } }
            };

            RelationalTree t = new RelationalTree();

            var tree = converter.ConvertFromRelationalModel(t, nodes);

            Assert.Equal("Root", tree.RootNode.NodeContent.Content);
            Assert.Equal("Hello", tree.RootNode.ChildNodes[0].NodeContent.Content);
            Assert.Equal("<0 0>", tree.RootNode.ChildNodes[0].ChildNodes[0].NodeContent.Content);
            Assert.Equal("<0 1>", tree.RootNode.ChildNodes[0].ChildNodes[1].NodeContent.Content);
            Assert.Equal("<0 2>", tree.RootNode.ChildNodes[0].ChildNodes[2].NodeContent.Content);

            Assert.Equal("Tree", tree.RootNode.ChildNodes[1].NodeContent.Content);
            Assert.Equal("<1 0>", tree.RootNode.ChildNodes[1].ChildNodes[0].NodeContent.Content);
            Assert.Equal("<1 1>", tree.RootNode.ChildNodes[1].ChildNodes[1].NodeContent.Content);
            Assert.Equal("<1 2>", tree.RootNode.ChildNodes[1].ChildNodes[2].NodeContent.Content);

            Assert.Equal("Mapper", tree.RootNode.ChildNodes[2].NodeContent.Content);
            Assert.Equal("<2 0>", tree.RootNode.ChildNodes[2].ChildNodes[0].NodeContent.Content);
            Assert.Equal("<2 1>", tree.RootNode.ChildNodes[2].ChildNodes[1].NodeContent.Content);
            Assert.Equal("<2 2>", tree.RootNode.ChildNodes[2].ChildNodes[2].NodeContent.Content);
        }

        private Tree<TestContent> CreateTestTree()
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
            return tree;
        }
    }
}
