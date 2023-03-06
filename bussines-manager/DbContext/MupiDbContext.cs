using bussines_manager.Model;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace bussines_manager.DbContext
{

    public class MupiDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public MupiDbContext(DbContextOptions options)
       : base(options)
        {
        }

        public DbSet<Contact> Contact { get; set; }
        public DbSet<StateManager> StateManager { get; set; }
        public DbSet<SyncLog> SyncLog { get; set; }
        public DbSet<SyncLogDetail> SyncLogDetail { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SyncLog>()
               .HasMany(c => c.SyncLogDetails)
               .WithOne(e => e.SyncLog)
               .IsRequired();

            modelBuilder.Entity<StateManager>()
               .HasMany(c => c.SyncLogs)
               .WithOne(e => e.Status)
               .IsRequired();

            modelBuilder.Entity<StateManager>()
               .HasMany(c => c.SyncLogDetails)
               .WithOne(e => e.Status)
               .IsRequired();



        }
    }
}