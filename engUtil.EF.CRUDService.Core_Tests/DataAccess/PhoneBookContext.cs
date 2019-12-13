// --------------------------------------------------------------------------------
// <copyright filename="PhoneBookContext.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using EngUtil.EF.CRUDService.Core_Tests.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EngUtil.EF.CRUDService.Core_Tests.DataAccess
{
    public class PhoneBookContext : DbContext
    {
        private string _connection;

        public PhoneBookContext(string connection)
        {
            _connection = connection; 
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_connection);
            optionsBuilder.UseSqlite(_connection);
        }
    }
}
