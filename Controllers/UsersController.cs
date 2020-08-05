using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class UsersController : ControllerBase {
        private IUserService _userService;

        IFirebaseConfig config = new FirebaseConfig {
            BasePath = "https://cworkshop-a0be0.firebaseio.com/",
            AuthSecret = "47FZbETdl6nAXR5rIgrXvXUq1ktJ6lfKt6f287HY"
        };
        IFirebaseClient client;

        public UsersController (IUserService userService) {
            _userService = userService;
            client = new FireSharp.FirebaseClient (config);
        }

        [HttpPost ("authenticate")]
        public IActionResult Authenticate (AuthenticateRequest model) {
            var response = _userService.Authenticate (model);

            if (response == null)
                return BadRequest (new { message = "Username or password is incorrect" });

            return Ok (response);
        }

        [HttpPost ("Login")]
        public async Task<string> TestAuthen (AuthenticateRequest model) {
            var response = _userService.Authenticate (model);
            SetResponse res = await client.SetTaskAsync ("Login/" + DateTime.Now, response);
            if (response == null)
                return null;
            return $"{response.UserLevel}";
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll () {
            var users = _userService.GetAll ();
            return Ok (users);
        }

        [HttpGet ("test")] //Test connect DB
        public ActionResult<string> Getstrings () {
            if (client != null) {
                return "Database connected";
            } else {
                return "Please try again later !!";
            }
        }
        [HttpGet ("data")] //response all USERs
        public async Task<List<Register>> Getdata () {
            FirebaseResponse response = await client.GetTaskAsync("Users/");
            List<Register> _allUser = response.ResultAs<List<Register>>();
            return _allUser;
        }

        [HttpGet ("cnt")] //auto ID
        public async Task<int> Getcnt () {
            FirebaseResponse res = await client.GetTaskAsync("auto/id");
            AutoIncrement get = res.ResultAs<AutoIncrement>();
            int i = get.cnt+1;
            return i;
        }

        [HttpPost ("Register")]
        public async Task<string> Register (Register model) {
            FirebaseResponse res = await client.GetTaskAsync("auto/id");
            AutoIncrement get = res.ResultAs<AutoIncrement>();
            model.Id = get.cnt+1;
            SetResponse response = await client.SetTaskAsync ("Users/" + model.Id, model);
            var obj = new AutoIncrement {
                cnt = model.Id
            };
            FirebaseResponse resp = await client.UpdateTaskAsync ("auto/id", obj);
            return model.UserLevel;
        }

        [HttpPost ("Search/{id?}")]
        public async Task<User> Search (int id) {
            FirebaseResponse response = await client.GetTaskAsync ("Users/" + id);
            User _user = response.ResultAs<User> ();
            return _user;
        }
    }
}