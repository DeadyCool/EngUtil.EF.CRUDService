using EngUtil.EF.CRUDService.Core;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess;
using EngUtil.EF.CRUDService.Core_Tests.DependencyResolution;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using EngUtil.Mock.Helper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace EngUtil.EF.CRUDService.Core_Tests
{
    public class CRUD_Tests
    {
        private bool init = false;
        private UserModel? _newUserA;
        private UserModel? _newUserB;
        private UserModel? _newUserC;
        private UserModel? _newUserD;
        private IServiceProvider? _serviceProvider;


        [SetUp]
        public void Setup()
        {
            if (!init)
            {
                if (File.Exists(TestSettings.DbLitePath))
                    File.Delete(TestSettings.DbLitePath);

                _serviceProvider = TestServiceProvider.BuildProvider();

                using (var ctx = _serviceProvider.GetService<NewspaperContext>())
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
            var userRepo = _serviceProvider?.GetService<IRepository<UserModel>>();

            _newUserA = userRepo?.Insert(CreatePerson());
            _newUserB = userRepo?.Insert(CreatePerson());

            Assert.IsTrue(_newUserA.Id != Guid.Empty);
        }

        [Test]
        [Order(1)]
        public async Task InsertAsync_Test()
        {
            var userRepo = _serviceProvider?.GetService<IRepository<UserModel>>();

            _newUserC = await userRepo?.InsertAsync(CreatePerson());
            _newUserD = await userRepo?.InsertAsync(CreatePerson());

            Assert.IsTrue(_newUserB.Id != Guid.Empty && _newUserC.Id != Guid.Empty);
        }

        [Test]
        [Order(2)]
        public void Get_Test()
        {
            var userRepo = _serviceProvider.GetService<IRepository<UserModel>>();

            var resultSet = userRepo?.Get(x=> x.Id == _newUserA.Id);

            Assert.IsTrue(resultSet.Any());
        }

        [Test]
        [Order(3)]
        public async Task GetAsync_Test()
        {
            var userRepo = _serviceProvider.GetService<IRepository<UserModel>>();

            var resultSet = await userRepo?.GetAsync(x => x.Id == _newUserB.Id);

            Assert.IsTrue(resultSet.Any());
        }

        [Test]
        [Order(4)]
        public void GetFirst_Test()
        {
            var userRepo = _serviceProvider.GetService<IRepository<UserModel>>();

            var result = userRepo?.GetFirst(x => x.Id == _newUserA.Id);

            Assert.IsTrue(result != null);
        }

        [Test]
        [Order(5)]
        public async Task GetFirstAsync_Test()
        {
            var userRepo = _serviceProvider.GetService<IRepository<UserModel>>();

            var result = await userRepo?.GetFirstAsync(x => x.Id == _newUserB.Id);

            Assert.IsTrue(result != null);
        }

        [Test]
        [Order(6)]
        public void UpdateFirst_Test()
        {
            var userRepo = _serviceProvider.GetService<IRepository<UserModel>>();
            var RandomPlace = RandomData.RandomPlace;

            _newUserA.State = RandomPlace.State;
            _newUserA.ZIPCode = RandomPlace.ZIPCode;
            _newUserA.Location = RandomPlace.Location;
            _newUserA.StreetAddress = RandomData.RandomStreet;

            userRepo?.Update(_newUserA);

            var result = userRepo?.GetFirst(x => x.Id == _newUserA.Id);

            Assert.IsTrue(
                result?.State == _newUserA.State &&
                result?.ZIPCode == _newUserA.ZIPCode &&
                result?.Location == _newUserA.Location &&
                result?.StreetAddress == _newUserA.StreetAddress);
        }

        [Test]
        [Order(7)]
        public async Task UpdateAsync_Test()
        {
            var userRepo = _serviceProvider.GetService<IRepository<UserModel>>();
            var RandomPlace = RandomData.RandomPlace;

            _newUserB.State = RandomPlace.State;
            _newUserB.ZIPCode = RandomPlace.ZIPCode;
            _newUserB.Location = RandomPlace.Location;
            _newUserB.StreetAddress = RandomData.RandomStreet;

            await userRepo?.UpdateAsync(_newUserB);

            var result = await userRepo?.GetFirstAsync(x => x.Id == _newUserB.Id);

            Assert.IsTrue(
                result?.State == _newUserB.State &&
                result?.ZIPCode == _newUserB.ZIPCode &&
                result?.Location == _newUserB.Location &&
                result?.StreetAddress == _newUserB.StreetAddress);
        }

        [Test]
        [Order(8)]
        public void Count_Test()
        {
            var userRepo = _serviceProvider.GetService<IRepository<UserModel>>();

            var count = userRepo?.Count();

            Assert.IsTrue(count == 4);
        }

        [Test]
        [Order(9)]
        public async Task CountAsync_Test()
        {
            var userRepo = _serviceProvider.GetService<IRepository<UserModel>>();

            var count = await userRepo?.CountAsync();

            Assert.IsTrue(count == 4);
        }

        [Test]
        [Order(10)]
        public void DeleteByKey_Test()
        {
            var userRepo = _serviceProvider.GetService<IRepository<UserModel>>();

            userRepo?.Delete(_newUserA.Id);

            var count = userRepo?.Count();

            Assert.IsTrue(count == 3);
        }

        [Test]
        [Order(11)]
        public async Task DeleteByKeyAsync_Test()
        {
            var userRepo = _serviceProvider.GetService<IRepository<UserModel>>();

            await userRepo?.DeleteAsync(_newUserB.Id);

            var count = userRepo?.Count();

            Assert.IsTrue(count == 2);
        }

        [Test]
        [Order(12)]
        public void DeleteByModel_Test()
        {
            var userRepo = _serviceProvider.GetService<IRepository<UserModel>>();

            var personToDelete = userRepo?.Insert(CreatePerson());

            var beforeCount = userRepo?.Count();

            userRepo?.Delete(personToDelete);

            var afterCount = userRepo?.Count();

            Assert.IsTrue(beforeCount == 3 && afterCount == 2);
        }

        [Test]
        [Order(13)]
        public async Task DeleteByModelAsync_Test()
        {
            var userRepo = _serviceProvider.GetService<IRepository<UserModel>>();

            var personToDelete = await userRepo?.InsertAsync(CreatePerson());

            var beforeCount = await userRepo?.CountAsync();

            await userRepo?.DeleteAsync(personToDelete);

            var afterCount = await userRepo?.CountAsync();

            Assert.IsTrue(beforeCount == 3 && afterCount == 2);
        }

        [Test]
        [Order(14)]
        public void InserRange_Test()
        {
            var newsRepo = _serviceProvider.GetService<IRepository<NewsModel>>();
            var commentRepo = _serviceProvider.GetService<IRepository<CommentModel>>();

            var newNews = newsRepo.Insert(new NewsModel
            {
                ReporterId = _newUserC.Id,
                Header = "Attention Attention",
                Content = "Some cool news",
                CreatedBy = _newUserC.Id,
                Created = DateTime.UtcNow,
            });

            var comments = new List<CommentModel>();
            for (var i = 0; i < 10; i++)
            {
                comments.Add(new CommentModel
                {
                    NewsId = newNews.Id,
                    UserId = _newUserC.Id,
                    CreatedBy = _newUserD.Id,
                    Content = $"Comment {i}"
                });
            }

            commentRepo.InsertRange(comments);

            var count = commentRepo.Count(x => x.NewsId == newNews.Id);

            Assert.IsTrue(count == 10);
        }

        [Test]
        [Order(15)]
        public async Task InserRangeAsync_Test()
        {
            var news = _serviceProvider.GetService<IRepository<NewsModel>>();
            var commentRepo = _serviceProvider.GetService<IRepository<CommentModel>>();

            var newNews = await news.InsertAsync(new NewsModel
            {
                ReporterId = _newUserC.Id,
                Header = "Attention Attention",
                Content = "Some cool news",
                CreatedBy = _newUserC.Id,
                Created = DateTime.UtcNow,
            });

            var comments = new List<CommentModel>();
            for (var i = 0; i < 10; i++)
            {
                comments.Add(new CommentModel
                {
                    NewsId = newNews.Id,
                    UserId = _newUserC.Id,
                    CreatedBy = _newUserD.Id,
                    Content = $"Comment {i}"
                });
            }

            await commentRepo.InsertRangeAsync(comments);

            var count = await commentRepo.CountAsync(x=> x.NewsId == newNews.Id);

            Assert.IsTrue(count == 10);
        }


        public UserModel CreatePerson()
        {
            var sex = Randomizor.RandomNumber(0, 1);
            var forename = sex == 0  ? RandomData.RandomMalename : RandomData.RandomFemalename;
            var surename = RandomData.RandomSurename;
            var RandomPlace = RandomData.RandomPlace;

            var randomPerson = new UserModel
            {
                State = RandomPlace.State,
                ZIPCode = RandomPlace.ZIPCode,
                Location = RandomPlace.Location,
                StreetAddress = RandomData.RandomStreet,
                Name = forename,
                Surename = surename,   
                EMail = $"{forename}.{surename}@{RandomData.RandomEmailSuffix}",
                Password = RandomData.CreatePassword(12),
                Created = DateTime.Now,
                CreatedBy = Guid.NewGuid()
            };

            return randomPerson;
        }
    }
}
