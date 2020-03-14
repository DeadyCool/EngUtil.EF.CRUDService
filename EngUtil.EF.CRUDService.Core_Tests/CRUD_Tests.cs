using engUtil.Dto;
using EngUtil.EF.CRUDService.Core;
using EngUtil.EF.CRUDService.Core_Tests.DataAccess;
using EngUtil.EF.CRUDService.Core_Tests.Models;
using EngUtil.EF.CRUDService.Core_Tests.Repos;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace EngUtil.EF.CRUDService.Core_Tests
{
    public class CRUD_Tests
    {        
        private IMapper _mapper;
        private ISessionContext<PhoneBookContext> _session;
        private IRepository<PersonModel> _personRepo;
        private IRepository<PhoneNumberModel> _phoneNumberRepo;
        private IRepository<EmailModel> _emailRepo;

        [SetUp]
        public void Setup()
        {
            if (_mapper == null)
            {
                var mapper = new Mapper();
                mapper.ScanForExpressionMappings();
                _mapper = mapper;
            }

            if(_session == null)
            {
                _session = new PhoneBookSession($"Data Source={Path.Combine(Path.GetTempPath(), "phonebook.sqlite")}");
                using (var ctx = _session.GetContext())
                    ctx.Database.EnsureCreated();
            }

            if (_personRepo == null)
                _personRepo = new PersonRepository(_session, _mapper);
            if (_phoneNumberRepo == null)
                _phoneNumberRepo = new PhoneNumberRepository(_session, _mapper);
            if (_emailRepo == null)
                _emailRepo = new EmailRepository(_session, _mapper);
        }

        [Test]
        public void CreatePerson_Test()
        {
            var createdPerson = _personRepo.Insert(new PersonModel
            {
                Forename = "Hans",
                Surename = "Mustermann"                
            });
            
            for(int i = 0; i< 10; i++)
            {
                _phoneNumberRepo.Insert(new PhoneNumberModel
                {
                    PersonId = createdPerson.Id,
                    Number = $"134567-{i}",
                    NumberType = "Private"
                });
            }

            var numbers = _phoneNumberRepo.Get(x => x.PersonId == createdPerson.Id);

            _phoneNumberRepo.Delete(numbers.FirstOrDefault());

            numbers = _phoneNumberRepo.Get(x => x.PersonId == createdPerson.Id);

            var number = numbers.FirstOrDefault();
            number.Number = "987654321-1";

            _phoneNumberRepo.Update(number);

            var updated = _phoneNumberRepo.GetFirst(x => x.Id == number.Id);
            
            for(int i= 0; i<5; i++)
            {
                _emailRepo.Insert(new EmailModel
                {
                    PersonId = createdPerson.Id,
                    EMailAddress = $"{createdPerson.Surename}.{createdPerson.Forename}@domain_{i}.com"
                });
            }

            var person = _personRepo.GetFirst(x => x.Id == createdPerson.Id);

            Assert.Pass();
        }
    }
}
