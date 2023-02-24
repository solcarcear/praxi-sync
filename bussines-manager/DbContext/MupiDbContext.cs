using bussines_manager.Model;
using Microsoft.EntityFrameworkCore;

namespace bussines_manager.DbContext
{

    public class MupiDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public MupiDbContext(DbContextOptions options)
       : base(options)
        {
        }

        public DbSet<Contact> Contact { get; set; }
        public DbSet<SyncCreatioLogs> SyncCreatioLog{ get; set; }
    }
}