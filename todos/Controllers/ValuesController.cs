using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// using System.Web.Http.Cors;

namespace todos.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // https://stackoverflow.com/questions/18784697/how-to-import-jsonconvert-in-c-sharp-application
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var db = new DBinterface();
            var data = db.Get("listtasks");
            return data;
        }
        // GET api/values
        /*[HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET api/values/5
        [HttpGet("{value}")]
        public ActionResult<string> Get(String value)
        {
            //return "value " + id.ToString();
            var db = new DBinterface();
            var data = db.Get(value);
            return data;
        }

        /*// GET api/values/5
        [HttpGet("{value}")]
        public ActionResult<string> Get(int id)
        {
            //return "value " + id.ToString();
            var db = new DBinterface();
            var data = db.Get(value.ToString());
            return data;
        }*/

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
