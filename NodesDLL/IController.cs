namespace NodesDLL
{
    public interface IController
    {
        Node node { get; set; }
        string json { get; set; }
        bool state { get; set; }
        int? groupId { get; set; }
    }

    public class Controller : IController
    {
        public Node node { get; set; }
        public string json { get; set; }
        public bool state { get; set; }
        public int? groupId { get; set; }
    }
}
