// --------------------------------------------------------------------------------
// <copyright filename="PhoneBookContext.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using EngUtil.CRUDService.CoreASP_Test.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EngUtil.CRUDService.CoreASP_Test.DataAccess
{
    public class PhoneBookContext : DbContext
    {
        public PhoneBookContext(DbContextOptions<PhoneBookContext> options) 
            : base(options)
        {
        }

        public DbSet<PersonEntity> Persons { get; set; }

        public DbSet<PhoneNumberEntity> PhoneNumbers { get; set; }

        public DbSet<EmailEntity> Emails { get; set; }

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
