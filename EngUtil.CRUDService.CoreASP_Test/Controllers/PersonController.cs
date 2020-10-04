using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EngUtil.CRUDService.CoreASP_Test.DataAccess;
using EngUtil.CRUDService.CoreASP_Test.Models;
using EngUtil.EF.CRUDService.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EngUtil.CRUDService.CoreASP_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IRepository<PersonModel> _personRepo;

        public PersonController(IRepository<PersonModel> personRepo)
        {
            _personRepo = personRepo;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<PersonModel>> Get()
        {
            var result = await _personRepo.GetAsync();
            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<PersonModel> Get(Guid id)
        {
            return await _personRepo.GetFirstAsync(x=> x.Id == id);
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] PersonModel person)
        {
           await  _personRepo.InsertAsync(person);
        }

        // PUT api/values/5
        [HttpPut]
        public async Task Put([FromBody] PersonModel person)
        { 
            await _personRepo.UpdateAsync(person);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _personRepo.DeleteAsync(id);            
        }
    }
}
