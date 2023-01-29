using Account.Core.Entities.Access;
using Account.Core.Entities.BaseEntity;
using Account.Core.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Account.Repository.Context
{
    public class AccountContext:DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options):base(options)
        {
                
        }
        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        #endregion
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        {
            return base.AddAsync(entity, cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Query Filters
            Filters(modelBuilder);

            //Relationships
            Relationships(modelBuilder);

            //Set Key
            //modelBuilder.Entity<Token>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            base.OnModelCreating(modelBuilder);
        }

        #region Filters Method
        private static void Filters(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            //modelBuilder.Entity<Role>().HasQueryFilter(u => !u.IsDelete);
            //modelBuilder.Entity<Permission>().HasQueryFilter(u => !u.IsDelete);
            //modelBuilder.Entity<Tweet>().HasQueryFilter(u => !u.IsDelete);
            //modelBuilder.Entity<Tag>().HasQueryFilter(u => !u.IsDelete);
        }
        #endregion
        #region Relationships Method
        private static void Relationships(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Tag>().HasOne(t => t.Tweet).WithMany(t => t.Tags).OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<Tag>().HasOne(t => t.User).WithMany(t => t.Tags).OnDelete(DeleteBehavior.NoAction);
        }
        #endregion
    }
}
