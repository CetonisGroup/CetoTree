using System;
using System.Collections.Generic;
using System.Linq;

namespace CetoTree
{
    /// <summary>
    /// Class for converting an in-memory tree to a relational model for 
    /// storing in a relation database (e.g. SQL database) and converting this
    /// back to an in-memory tree.
    /// </summary>
    public class TreeRelationalModelConverter
    {
        /// <summary>
        /// Converts the given tree to a relational representation suitable
        /// for storage in a database.
        /// </summary>
        /// <typeparam name="T">The type of the content of the tree</typeparam>
        /// <param name="tree">The tree to be converted</param>
        /// <returns>Tuple, 1. The relational tree, 2. a list of relational representation of nodes and 3. the content of the nodes.</returns>
        public Tuple<RelationalTree, List<RelationalTreeNode<T>>, List<T>> ConvertToRelationalModel<T>(Tree<T> tree)
        {
            var stack = new Stack<Tuple<RelationalTreeNode<T>, TreeNode<T>>>();

            var relationalNodes = new List<RelationalTreeNode<T>>();
            var contents = new List<T>();

            var relationalTree = new RelationalTree();
            var relationalRootNode = new RelationalTreeNode<T>(tree.RootNode.Id, relationalTree, tree.RootNode.NodeContent);
            stack.Push(Tuple.Create(relationalRootNode, tree.RootNode));

            int currentId = 0;
            while (stack.Count > 0)
            {
                var nodeRepresentations = stack.Pop();
                var relationalNodeRepresentation = nodeRepresentations.Item1;

                //If a node was already processed add the post order id and write to the store lists.
                if (relationalNodeRepresentation.PreOrderId != -1)
                {
                    relationalNodeRepresentation.PostOrderId = currentId;

                    relationalNodes.Add(relationalNodeRepresentation);
                    contents.Add(relationalNodeRepresentation.Data);
                }
                else
                {
                    relationalNodeRepresentation.PreOrderId = currentId;
                    stack.Push(Tuple.Create(relationalNodeRepresentation, nodeRepresentations.Item2));

                    var children = new List<TreeNode<T>>(nodeRepresentations.Item2.ChildNodes);
                    children.Reverse(); //Needed for correct order on stack (first child at top position of stack)
                    children.ForEach(child =>
                                     {
                                         var relationalChildNode = new RelationalTreeNode<T>(child.Id, relationalTree, child.NodeContent);
                                         stack.Push(Tuple.Create(relationalChildNode, child));
                                     });
                }
                ++currentId;
            }

            return Tuple.Create(relationalTree, relationalNodes, contents);
        }


        /// <summary>
        /// Converts a relational tree to a in-memory tree structure.
        /// </summary>
        /// <typeparam name="T">The type of the content of the tree</typeparam>
        /// <param name="relationalTree">The relational tree object</param>
        /// <param name="relationalNodes">The nodes of the tree from a database. Attention the content
        /// of the nodes has to be set externally. This cannot be done in this method because of missing database access.</param>
        /// <returns>The in-memory tree</returns>
        public Tree<T> ConvertFromRelationalModel<T>(RelationalTree relationalTree, List<RelationalTreeNode<T>> relationalNodes)
        {
            var associatedNodes = relationalNodes
                .Select(x => Tuple.Create(x, new TreeNode<T>(x.Data, x.Id)))
                .ToList();

            var sortedNodeTuples = new List<TreeNode<T>>();
            for (int i = 0; i <= associatedNodes.FirstOrDefault(x => x.Item1.PreOrderId == 0).Item1.PostOrderId; ++i)
                sortedNodeTuples.Add(associatedNodes.FirstOrDefault(x => x.Item1.PreOrderId == i || x.Item1.PostOrderId == i).Item2);

            var rootNode = sortedNodeTuples[0];
            rootNode.ChildNodes.AddRange(sortedNodeTuples.GetRange(1, sortedNodeTuples.Count - 2));

            var stack = new Stack<TreeNode<T>>();
            stack.Push(rootNode);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                var childNodes = new List<TreeNode<T>>(node.ChildNodes);
                var newChildNodes = new List<TreeNode<T>>();
                while (childNodes.Count > 0)
                {
                    var child = childNodes.First();
                    int lastChildPosition = childNodes.LastIndexOf(child);

                    stack.Push(child);
                    for (int i = 1; i < lastChildPosition; ++i)
                    {
                        child.ChildNodes.Add(childNodes[i]);
                        childNodes[i].ParentNode = child;
                    }

                    childNodes.RemoveRange(0, lastChildPosition + 1);
                    newChildNodes.Add(child);
                }
                node.ChildNodes = newChildNodes;
            }
            return new Tree<T>(relationalTree.Id, rootNode);
        }
    }
}