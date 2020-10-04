using EngUtil.EF.CRUDService.Core;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess;
using EngUtil.EF.CRUDService.Core_Tests.DependencyResolution;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using EngUtil.Mock.Helper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EngUtil.EF.CRUDService.Core_Tests
{
    public class CRUD_Tests
    {
        private bool init = false;
        private PersonModel _newPersonA;
        private PersonModel _newPersonB;
        private IServiceProvider _serviceProvider;
            

        [SetUp]
        public void Setup()
        {
            if (!init)
            {
                if (File.Exists(TestSettings.DbLitePath))
                    File.Delete(TestSettings.DbLitePath);

                _serviceProvider = TestServiceProvider.BuildProvider();

                using (var ctx = _serviceProvider.GetService<AddressBookContext>())
                {
                    ctx.Database.EnsureCreated();
                }
            }
            init = true;
        }

        [Test]
        [Order(0)]
        public void Insert_Test()
        {
            var persons = _serviceProvider.GetService<IRepository<PersonModel>>();

            _newPersonA = persons.Insert(CreatePerson());

            Assert.IsTrue(_newPersonA.Id != Guid.Empty);
        }

        [Test]
        [Order(1)]
        public async Task InsertAsync_Test()
        {
            var persons = _serviceProvider.GetService<IRepository<PersonModel>>();

            _newPersonB = await persons.InsertAsync(CreatePerson());

            Assert.IsTrue(_newPersonB.Id != Guid.Empty);
        }

        [Test]
        [Order(2)]
        public void Get_Test()
        {
            var persons = _serviceProvider.GetService<IRepository<PersonModel>>();

            var resultSet = persons.Get(x=> x.Id == _newPersonA.Id);

            Assert.IsTrue(resultSet.Any());
        }

        [Test]
        [Order(3)]
        public async Task GetAsync_Test()
        {
            var persons = _serviceProvider.GetService<IRepository<PersonModel>>();

            var resultSet = await persons.GetAsync(x => x.Id == _newPersonB.Id);

            Assert.IsTrue(resultSet.Any());
        }
                
        [Test]
        [Order(4)]
        public void GetFirst_Test()
        {
            var persons = _serviceProvider.GetService<IRepository<PersonModel>>();

            var result = persons.GetFirst(x => x.Id == _newPersonA.Id);

            Assert.IsTrue(result != null);
        }

        [Test]
        [Order(5)]
        public async Task GetFirstAsync_Test()
        {
            var persons = _serviceProvider.GetService<IRepository<PersonModel>>();

            var result = await persons.GetFirstAsync(x => x.Id == _newPersonB.Id);

            Assert.IsTrue(result != null);
        }

        [Test]
        [Order(6)]
        public void UpdateFirst_Test()
        {
            var persons = _serviceProvider.GetService<IRepository<PersonModel>>();
            var randomLocation = RandomData.RandomLocation;

            _newPersonA.Bundesland = randomLocation.Bundesland;
            _newPersonA.PLZ = randomLocation.PLZ;
            _newPersonA.Ort = randomLocation.Ort;
            _newPersonA.Strasse = RandomData.RandomStreet;

            persons.Update(_newPersonA);

            var result = persons.GetFirst(x => x.Id == _newPersonA.Id);

            Assert.IsTrue(
                result?.Bundesland == _newPersonA.Bundesland &&
                result?.PLZ == _newPersonA.PLZ &&
                result?.Ort == _newPersonA.Ort &&
                result?.Strasse == _newPersonA.Strasse);
        }

        [Test]
        [Order(7)]
        public async Task UpdateAsync_Test()
        {
            var persons = _serviceProvider.GetService<IRepository<PersonModel>>();
            var randomLocation = RandomData.RandomLocation;

            _newPersonB.Bundesland = randomLocation.Bundesland;
            _newPersonB.PLZ = randomLocation.PLZ;
            _newPersonB.Ort = randomLocation.Ort;
            _newPersonB.Strasse = RandomData.RandomStreet;

            await persons.UpdateAsync(_newPersonB);

            var result = await persons.GetFirstAsync(x => x.Id == _newPersonB.Id);

            Assert.IsTrue(
                result?.Bundesland == _newPersonB.Bundesland &&
                result?.PLZ == _newPersonB.PLZ &&
                result?.Ort == _newPersonB.Ort &&
                result?.Strasse == _newPersonB.Strasse);
        }

        [Test]
        [Order(8)]
        public void Count_Test()
        {
            var persons = _serviceProvider.GetService<IRepository<PersonModel>>();

            var count = persons.Count();

            Assert.IsTrue(count == 2);
        }

        [Test]
        [Order(9)]
        public async Task CountAsync_Test()
        {
            var persons = _serviceProvider.GetService<IRepository<PersonModel>>();

            var count = await persons.CountAsync();

            Assert.IsTrue(count == 2);
        }

        [Test]
        [Order(10)]
        public void DeleteByKey_Test()
        {
            var persons = _serviceProvider.GetService<IRepository<PersonModel>>();

            persons.Delete(_newPersonA.Id);

            var count = persons.Count();

            Assert.IsTrue(count == 1);
        }

        [Test]
        [Order(11)]
        public async Task DeleteByKeyAsync_Test()
        {
            var persons = _serviceProvider.GetService<IRepository<PersonModel>>();

            await persons.DeleteAsync(_newPersonB.Id);

            var count = persons.Count();

            Assert.IsTrue(count == 0);
        }

        [Test]
        [Order(12)]
        public void DeleteByModel_Test()
        {
            var persons = _serviceProvider.GetService<IRepository<PersonModel>>();

            var personToDelete = persons.Insert(CreatePerson());

            var beforeCount = persons.Count();

            persons.Delete(personToDelete);

            var afterCount = persons.Count();

            Assert.IsTrue(beforeCount == 1 && afterCount == 0);
        }

        [Test]
        [Order(13)]
        public async Task DeleteByModelAsync_Test()
        {
            var persons = _serviceProvider.GetService<IRepository<PersonModel>>();

            var personToDelete = await persons.InsertAsync(CreatePerson());

            var beforeCount = await persons.CountAsync();

            await persons.DeleteAsync(personToDelete);

            var afterCount = await persons.CountAsync();

            Assert.IsTrue(beforeCount == 1 && afterCount == 0);
        }


        public PersonModel CreatePerson()
        {
            var sex = Randomizor.RandomNumber(0, 1);
            var forename = sex == 0  ? RandomData.RandomMalename : RandomData.RandomFemalename;
            var surename = RandomData.RandomSurename;
            var randomLocation = RandomData.RandomLocation;

            var randomPerson = new PersonModel
            {
                Bundesland = randomLocation.Bundesland,
                PLZ = randomLocation.PLZ,
                Ort = randomLocation.Ort,
                Strasse = RandomData.RandomStreet,
                Vorname = forename,
                Nachname = surename,
                Name = $"{surename}, {forename}",
                Erstellt = DateTime.Now
            };

            return randomPerson;
        }
    }
}
