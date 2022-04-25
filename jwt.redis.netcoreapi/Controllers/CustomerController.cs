using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jwt.redis.netcoreapi.Filters;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace jwt.redis.netcoreapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TokenAuthenticationFilter]
    public class CustomerController : ControllerBase
    {
        // GET: api/<CustomerController>
        [SwaggerHeader("token")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            

            return new string[] { "value1", "value2" };
        }

        [SwaggerHeader("token")]
        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IEnumerable<string> Get(int id)
        {
            return null;
        }

        // POST api/<CustomerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
