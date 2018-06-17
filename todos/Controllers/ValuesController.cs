using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace todos.Controllers
{
    
    [ApiController]
    public class ValuesController : ControllerBase
    {

        // GET -generic lists
        [Route("api/list/{id}")]
        [HttpGet]
        public ActionResult<string> GetTasks(String id)
        {
            var db = new DBinterface();
            var data = db.Get(id);
            return data;
        }


        // GET details for spesific user
        [Route("api/user/details/{userid}")]
        [HttpGet]
        public ActionResult<string> GetUser(String userid)
        {

            var db = new DBinterface();
            var data = db.GetUser(userid);
            return data;
        }


        //GET -spesific task matching ID
        [Route("api/task/details/{taskid}")]
        [HttpGet]
        public ActionResult<string> GetTaskUsingId(String taskid)
        {
            
            var db = new DBinterface();
            return db.GetTaskWithSpesificId(taskid);

        }

        // GET -task for spesific user 
        [Route("api/task/user/{userId}")]
        [HttpGet]
        public ActionResult<string> getTaskForUser(String userId)
        {
            var db = new DBinterface();
            return db.GetTaskForSpesificUser(userId);

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
        public ActionResult<string> UpdateTask([FromBody] todos.Task data)
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
                return Ok("ok save user, query: " + qr);
            }
            return "";
        }
        // POST 
        [Route("api/update/user")]
        [HttpPost]
        public ActionResult<string> UpdateUser([FromBody] todos.User data)
        {
            if (ModelState.IsValid)
            {
                var db = new DBinterface();
                var qr = db.UpdateUser(data);
                return Ok("ok update user, query: " + qr);
            }
            return "";
        }

    }
}