namespace CetoTree
{
    /// <summary>
    /// Class for representing a tree node in a relational database.
    /// This class can be stored in a single table and 
    /// the corresponding tree can be reconstructructed uniquely 
    /// at runtime from this data.
    /// </summary>
    /// <typeparam name="T">The content type the tree node stores.
    /// The content is linked via a relation in the database</typeparam>
    public class RelationalTreeNode<T>
    {
        public RelationalTreeNode() { }
        public RelationalTreeNode(int id, RelationalTree tree, T data)
        {
            Id = id;
            Tree = tree;
            Data = data;
        }

        public int Id { get; set; }

        public int PreOrderId { get; set; }
        = -1;

        public int PostOrderId { get; set; }
        = -1;

        public int TreeId { get; set; }
        public RelationalTree Tree { get; set; }

        public T Data { get; set; }
    }
}
