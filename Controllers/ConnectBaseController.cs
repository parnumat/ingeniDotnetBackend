using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
//using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectBaseController : ControllerBase
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            BasePath = "https://cworkshop-a0be0.firebaseio.com/",
            AuthSecret = "47FZbETdl6nAXR5rIgrXvXUq1ktJ6lfKt6f287HY"
        };
        IFirebaseClient client;
        private string message;

        public ConnectBaseController()
        {
            client = new FireSharp.FirebaseClient(config);
        }


        // GET api/connectbase
        [HttpGet("")]
        public ActionResult<string> Getstrings()
        {
            if (client != null)
            {
                return "Database connected";
            }
            else
            {
                return "Please try again later !!";
            }
        }
        /*[HttpPost("{id?}")]
        public async void Poststring(int id)
        {
            client = new FireSharp.FirebaseClient(config);
            var myData = new User
            {
                FirstName = "parnumat",
                LastName = "Niropas",
                Username = "parnumat",
                Password = "parnumat",
                UserLevel = "User"
            };
            SetResponse response = await client.SetTaskAsync("User/" + id, myData);
        }
        */

        [HttpPost("Register")]
        public async void Register(Register model)
        {
            SetResponse response = await client.SetTaskAsync("User/" + model.Id, model);
            /*
            FirebaseResponse res = await client.GetTaskAsync("auto/id");
            AutoIncrement get = res.ResultAs<AutoIncrement>();
            model.Id = get.cnt + 1;
            SetResponse response = await client.SetTaskAsync("User/" + model.Id, model);
            var obj = new AutoIncrement
            {
                cnt = model.Id
            };
            SetResponse response1 = await client.SetTaskAsync("auto/id", obj);*/
        }
        [HttpPost("Search/{id}")]
        public async Task<User> Search(int id)
        {
            FirebaseResponse response = await client.GetTaskAsync("User/" + id);
            User _user = response.ResultAs<User>();
            return _user;
        }
        
        /*
        // GET api/connectbase/5
        [HttpGet("{id}")]
        public ActionResult<string> GetstringById(int id)
        {
            return null;
        }

        // POST api/connectbase
        [HttpPost("")]
        public void Poststring(string value)
        {
        }

        // PUT api/connectbase/5
        [HttpPut("{id}")]
        public void Putstring(int id, string value)
        {
        }

        // DELETE api/connectbase/5
        [HttpDelete("{id}")]
        public void DeletestringById(int id)
        {
        }*/
    }
}