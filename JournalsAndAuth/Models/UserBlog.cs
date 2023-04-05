using JournalsAndAuth.Areas.Identity.Data;

namespace JournalsAndAuth.Models
{
    public class UserBlog
    {
        public int Id { get; set; }
        public JournalsUser User { get; set; }
        public string UserId { get; set; }
        public Blog Blog { get; set; }
        public int BlogId { get; set; }
    }
}
