using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HalloASP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HalloASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EisApiController : ControllerBase
    {
        EisDataService data = new EisDataService();

        // GET: api/EisApi
        [HttpGet]
        public IEnumerable<Eis> Get()
        {
            return data.GetEisListe();
        }

        // GET: api/EisApi/5
        [HttpGet("{id}", Name = "Get")]
        public Eis Get(int id)
        {
            return data.GetById(id);
        }

        // POST: api/EisApi
        [HttpPost]
        public void Post([FromBody] Eis eis)
        {
            data.AddEis(eis);
        }

        // PUT: api/EisApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Eis eis)
        {
            data.UpdateEis(eis);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            data.DeleteEis(data.GetById(id));
        }
    }
}
