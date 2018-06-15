using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// https://nodogmablog.bryanhogan.net/2016/01/asp-net-5-web-api-controller-with-multiple-get-methods/

namespace todos.Controllers
{

  

    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values DEFAULT
        /*[Route("api/values")]
        [HttpGet]
        public ActionResult<string> Get()
        {
            var db = new DBinterface();
            var data = db.Get("listtasks");
            return data;
        }*/

        // GET api/list/1
        [Route("api/list/{id}")]
        [HttpGet]
        public ActionResult<string> GetTasks(String id)
        {
            //return "api/list/{id} " + id.ToString();
            //return "value " + id.ToString();
            var db = new DBinterface();
            var data = db.Get(id);
            return data;
        }


        // GET api/employeedetails/id/teammember
        [Route("api/user/details/{userid}")]
        [HttpGet]
        public ActionResult<string> GetUser(String userid)
        {
            //return "api/details/user/{userid}" + userid;
            var db = new DBinterface();
            var data = db.Get("listtasks");
            return data;
        }

        // GET 
        [Route("api/task/details/{tid}")]
        [HttpGet]
        public ActionResult<string> GetTask(String tid)
        {
            return "api/details/task/{tid} " + tid.ToString();

        }

        // POST 
        [Route("api/save/task")]
        [HttpPost]
        public ActionResult<string> PostTask([FromBody] todos.Task data)
        {
            if (ModelState.IsValid)
                
            {
                var db = new DBinterface();
                var query = db.saveTask(data);
                return Ok("ok Task: " + data.description + " query: " + query ); 
            }
            return "";

        }
        // POST 
        [Route("api/update/task")]
        [HttpPost]
        public ActionResult<string> PostUser([FromBody] todos.Task data)
        {
            if (ModelState.IsValid)
            {
                var db = new DBinterface();
                var qr = db.UpdateTask(data);
                return Ok("ok Update task: " + data.description +  " query: " + qr);
            }
            return "";
        }

        // POST 
        [Route("api/save/user")]
        [HttpPost]
        public ActionResult<string> PostUser([FromBody] todos.User data)
        {
            if (ModelState.IsValid)
            {
                var db = new DBinterface();
                 var qr = db.saveUser(data);
                return Ok("ok User: " + data.name + " -> " + data.position+ " query: " + qr);
            }
            return "";
        }

    }
}