// --------------------------------------------------------------------------------
// <copyright filename="AddressBookContext.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using EngUtil.EF.CRUDService.Core_Tests.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace EngUtil.EF.CRUDService.Core_Tests.DataAccess
{
    public class NewspaperContext : DbContext
    {

        #region ctr

        public NewspaperContext(DbContextOptions options) 
            : base(options)
        {
        }

        #endregion

        #region entities

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<CommentEntity> Commets { get; set; }

        public DbSet<NewsEntity> News { get; set; }

        #endregion

        #region configuration

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity
                    .HasMany(x => x.News)
                    .WithOne(x => x.Reporter)
                    .HasForeignKey(x => x.ReporterId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity
                     .HasMany(x => x.Comments)
                     .WithOne(x => x.User)
                     .HasForeignKey(x => x.UserId)
                     .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<NewsEntity>(entity =>
            {
                entity
                    .HasMany(x => x.Comments)
                    .WithOne(x => x.News)
                    .HasForeignKey(x => x.NewsId)
                    .OnDelete(DeleteBehavior.Cascade);
            }); 
        }

        #endregion
    }
}
