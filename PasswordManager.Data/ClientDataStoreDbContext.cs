using Microsoft.EntityFrameworkCore;
using PasswordManager.Core;
using PasswordManager.Data.DataModels;

namespace PasswordManager.Data {
    public class ClientDataStoreDbContext : DbContext {

        #region DbSets
        /// <summary>
        /// login information
        /// </summary>
        public DbSet<LoginDatabaseModel> LoginCredentials { get; set; }

        /// <summary>
        /// User content store
        /// </summary>
        public DbSet<UserContentDatabaseModel> UserContent { get; set; }

        #endregion

        #region Constructor

        public ClientDataStoreDbContext(DbContextOptions<ClientDataStoreDbContext> options) : base(options) {}

        #endregion

        #region Model Creating

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Fluent API

            // Configure LoginCredentials
            // -------------------------
            modelBuilder.Entity<LoginDatabaseModel>().HasKey(a => a.UserId);
            modelBuilder.Entity<LoginDatabaseModel>().Property(a => a.Guid).IsRequired(true);
            modelBuilder.Entity<LoginDatabaseModel>().Property(a => a.Email).HasMaxLength(256).IsRequired(true);
            modelBuilder.Entity<LoginDatabaseModel>().Property(a => a.Password).HasMaxLength(256).IsRequired(true);

            // Configure UserContent
            //-------------------------
            modelBuilder.Entity<UserContentDatabaseModel>().HasKey(a => new { a.User, a.Id });
            modelBuilder.Entity<UserContentDatabaseModel>().Property(a => a.AccountNameHash).HasMaxLength(256).IsRequired(true);
            modelBuilder.Entity<UserContentDatabaseModel>().Property(a => a.EmailHash).HasMaxLength(256).IsRequired(true);
            modelBuilder.Entity<UserContentDatabaseModel>().Property(a => a.UsernameHash).HasMaxLength(256);
            modelBuilder.Entity<UserContentDatabaseModel>().Property(a => a.WebsiteHash).HasMaxLength(256);
            modelBuilder.Entity<UserContentDatabaseModel>().Property(a => a.PasswordHash).HasMaxLength(256).IsRequired(true);
            modelBuilder.Entity<UserContentDatabaseModel>().Property(a => a.NotesHash).HasMaxLength(2048);

        }

        #endregion



    }
}
