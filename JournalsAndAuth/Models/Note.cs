using JournalsAndAuth.Areas.Identity.Data;

namespace JournalsAndAuth.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Body { get; set; }


        public int JournalId { get; set; }
        public virtual Journal? Journal {get;set;}

        public string UserId { get; set; }
        public virtual JournalsUser? User { get; set; }
    }
}
