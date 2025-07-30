namespace DataAccess
{
    public class Note
    {
        public int ID { get; set; }
        public string Text { get; set; } = "";
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
