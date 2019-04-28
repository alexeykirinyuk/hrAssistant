using HRAssistant.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRAssistant.DataAccess
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserEntity>().ToTable("User");
        }
    }
}
