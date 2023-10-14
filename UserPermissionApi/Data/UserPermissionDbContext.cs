using Microsoft.EntityFrameworkCore;
using UserPermissionApi.Model;

namespace UserPermissionApi.Data
{
    public class UserPermissionDbContext : DbContext
    {
        public UserPermissionDbContext(DbContextOptions<UserPermissionDbContext> options)
            : base(options)
        {
        }

        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<PermissionTypes> PermissionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permissions>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<PermissionTypes>()
                .Property(pt => pt.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Permissions>()
                .HasOne(p => p.PermissionTypes)
                .WithMany(pt => pt.Permissions)
                .HasForeignKey(p => p.TipoPermiso)
                .OnDelete(DeleteBehavior.NoAction); // Configura la eliminación en cascada como "NO ACTION"

        }
    }
}
