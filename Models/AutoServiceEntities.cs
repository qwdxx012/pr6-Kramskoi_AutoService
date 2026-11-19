using System.Data.Entity;

namespace Kramskoi_AutoService.Models
{
    public class AutoServiceEntities : DbContext
    {
        public AutoServiceEntities() : base("name=AutoServiceEntities")
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }

        private static AutoServiceEntities _context;

        public static AutoServiceEntities GetContext()
        {
            if (_context == null)
                _context = new AutoServiceEntities();
            return _context;
        }

        static AutoServiceEntities()
        {
            Database.SetInitializer<AutoServiceEntities>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(u => u.UserID);

            modelBuilder.Entity<Role>()
                .ToTable("Roles")
                .HasKey(r => r.RoleID);

            modelBuilder.Entity<User>()
                .HasRequired(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
