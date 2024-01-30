using Microsoft.EntityFrameworkCore;
using Sticks.Web.Models.Domain;

namespace Sticks.Web.Data
{
    public class SticksDbContext : DbContext
    {
        public SticksDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}
