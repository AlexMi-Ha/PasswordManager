
using Microsoft.EntityFrameworkCore;
using PasswordManager.Core.Common.DataModels.Database;

namespace PasswordManager.Data.Database {
    internal class ApplicationDbContext : DbContext {

        public DbSet<UserDataModel> LoginCredentials { get; set; }
        public DbSet<UserContentDataModel> UserContent { get; set; }
        public DbSet<UserContentGroupDataModel> UserContentGroups { get; set; }

        public ApplicationDbContext(DbContextOptions<ClientDataStoreDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);

            // Fluent API

            // Configure LoginCredentials
            // -------------------------
            modelBuilder.Entity<UserDataModel>().HasKey(a => a.UserId);
            modelBuilder.Entity<UserDataModel>().Property(a => a.Email).HasMaxLength(256).IsRequired(true);
            modelBuilder.Entity<UserDataModel>().Property(a => a.Password).HasMaxLength(256).IsRequired(true);

            // Configure UserContent
            //-------------------------
            modelBuilder.Entity<UserContentDataModel>().HasKey(a => a.Id);
            modelBuilder.Entity<UserContentDataModel>().Property(a => a.AccountNameHash).HasMaxLength(256).IsRequired(true);
            modelBuilder.Entity<UserContentDataModel>().Property(a => a.EmailHash).HasMaxLength(256).IsRequired(true);
            modelBuilder.Entity<UserContentDataModel>().Property(a => a.UsernameHash).HasMaxLength(256);
            modelBuilder.Entity<UserContentDataModel>().Property(a => a.WebsiteHash).HasMaxLength(256);
            modelBuilder.Entity<UserContentDataModel>().Property(a => a.PasswordHash).HasMaxLength(256).IsRequired(true);
            modelBuilder.Entity<UserContentDataModel>().Property(a => a.NotesHash).HasMaxLength(2048);
            modelBuilder.Entity<UserContentDataModel>().HasOne(a => a.ContentGroup).WithMany(a => a.Content).IsRequired();

            // Configure UserContentGroup
            modelBuilder.Entity<UserContentGroupDataModel>().HasKey(a => a.Id);
            modelBuilder.Entity<UserContentGroupDataModel>().Property(a => a.GroupName).HasMaxLength(256).IsRequired(true);
            modelBuilder.Entity<UserContentGroupDataModel>().HasMany(a => a.Content).WithOne(a => a.ContentGroup).HasForeignKey(a => a.ContentGroupId);
            modelBuilder.Entity<UserContentGroupDataModel>().HasOne(a => a.User).WithMany().HasForeignKey(a => a.UserId).IsRequired();

        }
    }
}
