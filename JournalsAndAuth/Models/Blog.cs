namespace JournalsAndAuth.Models
{
    public class Blog
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public HashSet<Journal> Journals { get; set; } = new HashSet<Journal>();
    }
}
