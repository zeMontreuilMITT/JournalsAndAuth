using JournalsAndAuth.Areas.Identity.Data;

namespace JournalsAndAuth.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime PublicationTime { get; set; }
        public Blog Blog { get; set; }
        public int BlogId { get; set; }

        public JournalsUser User { get; set; }
        public string UserId { get; set; }
    }
}
