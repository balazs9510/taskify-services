using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Taskify.DAL.Models;

namespace Taskify.DAL
{
    public class TaskifyDbContext : IdentityDbContext
    {
        public DbSet<UserRefreshToken> RefreshTokens { get; set; }
        public DbSet<SystemLog> SystemLogs { get; set; }

        public TaskifyDbContext(DbContextOptions<TaskifyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SystemLog>()
                .Property(b => b.Created)
                //.HasDefaultValueSql("getdate()"); // MSSQL
                .HasDefaultValueSql("now()"); // postgres
        }
    }
}
