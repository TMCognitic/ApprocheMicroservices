namespace ReadApi.Models.Entities
{
    public class Message
    {
        public Guid Uid { get; }
        public DateTime Date { get; }
        public string Nom { get; }
        public string Content { get; }
        public Message(Guid uid, DateTime date, string nom, string content)
        {
            Uid = uid;
            Date = date;
            Nom = nom;
            Content = content;
        }
    }
}
