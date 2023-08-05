using Microsoft.EntityFrameworkCore;
using PasswordManager.Core.Common.DataModels.Database;
using PasswordManager.Data.DataModels;

namespace PasswordManager.Data
{
    public class ClientDataStoreDbContext : DbContext {

        #region DbSets
        /// <summary>
        /// login information
        /// </summary>
        public DbSet<UserDataModel> LoginCredentials { get; set; }

        /// <summary>
        /// User content store
        /// </summary>
        public DbSet<UserContentDataModel> UserContent { get; set; }

        public DbSet<UserContentGroupDataModel> UserContentGroups { get; set; }

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
            modelBuilder.Entity<UserDataModel>().HasKey(a => a.UserId);
            //modelBuilder.Entity<LoginDatabaseModel>().Property(a => a.Guid).IsRequired(true);
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

            modelBuilder.Entity<UserContentGroupDataModel>().HasKey(a => a.Id);
            modelBuilder.Entity<UserContentGroupDataModel>().Property(a => a.GroupName).HasMaxLength(256).IsRequired(true);


        }


        #endregion



    }
}
