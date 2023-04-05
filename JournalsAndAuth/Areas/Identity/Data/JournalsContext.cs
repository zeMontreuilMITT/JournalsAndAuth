using JournalsAndAuth.Areas.Identity.Data;
using JournalsAndAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JournalsAndAuth.Data;

public class JournalsContext : IdentityDbContext<JournalsUser>
{
    public JournalsContext(DbContextOptions<JournalsContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Journal> Journals { get; set; } = default!;
    public DbSet<Blog> Blogs { get; set; } = default!;
    public DbSet<UserBlog> UserBlogs { get; set; } = default!;
}
