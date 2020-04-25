namespace NodesDLL.Models
{
    public class Tree
    {
        public Node node { get; set; }
        public Groups group { get; set; }

        public Tree(Node node, Groups group)
        {
            this.node = node;
            this.group = group;
        }
    }
}
