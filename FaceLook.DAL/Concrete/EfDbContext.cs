#region

using System.Data.Entity;
using FaceLook.DAL.Entities;

#endregion

namespace FaceLook.DAL.Concrete
{
    public class EfDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AuthenticationTicket> AuthTicket { get; set; }
        public DbSet<Conversation> Conversations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Friends);
        }
    }
}