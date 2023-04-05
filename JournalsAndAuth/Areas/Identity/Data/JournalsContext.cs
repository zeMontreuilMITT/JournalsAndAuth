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
        
        builder.Entity<Journal>().HasMany(j => j.Notes)
            .WithOne(n => n.Journal)
            .HasForeignKey(n => n.JournalId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    public DbSet<Journal> Journals { get; set; } = default!;
    public DbSet<Blog> Blogs { get; set; } = default!;
    public DbSet<UserBlog> UserBlogs { get; set; } = default!;
    public DbSet<Note> Notes { get; set; } = default!;
}
