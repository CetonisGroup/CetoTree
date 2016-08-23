namespace CetoTree
{
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
