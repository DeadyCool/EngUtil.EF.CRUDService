// --------------------------------------------------------------------------------
// <copyright filename="AddressBookContext.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace EngUtil.EF.CRUDService.Core_Tests.DataAccess
{
    public class AddressBookContext : DbContext
    {

        #region ctr

        public AddressBookContext(DbContextOptions options) : base(options)
        {
        }

        #endregion

        #region entities

        public DbSet<PersonEntity> Persons { get; set; }

        public DbSet<PhoneNumberEntity> PhoneNumbers { get; set; }

        public DbSet<EmailEntity> Emails { get; set; }

        #endregion

        #region configuration

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonEntity>(entity =>
            {
                entity
                    .HasMany(x => x.Numbers)
                    .WithOne(x => x.Person)
                    .HasForeignKey(x => x.PersonId)
                    .OnDelete(DeleteBehavior.Cascade);
                //entity.HasData(
                //    new PersonEntity { RecId = Guid.NewGuid, Created = DateTime.Now()}
                //    );
            });

            modelBuilder.Entity<PersonEntity>(entity =>
            {
                entity
                    .HasMany(x => x.EMails)
                    .WithOne(x => x.Person)
                    .HasForeignKey(x => x.PersonId)
                    .OnDelete(DeleteBehavior.Cascade);
            }); 
        }

        #endregion
    }
}
