using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using ingeniProjectFDotnetBackend.Methods;
using ingeniProjectFDotnetBackend.Models.Profiles;
using ingeniProjectFDotnetBackend.Services.DataServices;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.Methods;
using WebApi.Models;
using WebApi.Persistence.Helpers;
using WebApi.Services;
using WebApi.Services.DataServices;

namespace WebApi.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class UsersController : ControllerBase {
        private IUserService _userService;
        private readonly IMapper _mapper;
        // private object _results = null;

        IFirebaseConfig config = new FirebaseConfig {
            BasePath = "https://cworkshop-a0be0.firebaseio.com/",
            AuthSecret = "47FZbETdl6nAXR5rIgrXvXUq1ktJ6lfKt6f287HY"
        };
        IFirebaseClient client;

        public UsersController (IUserService userService, IMapper mapper) {
            _userService = userService;
            _mapper = mapper;
            client = new FireSharp.FirebaseClient (config);
        }

        // [Authorize]
        [HttpPost ("authenticate")]
        public IActionResult Authenticate (AuthenticateRequest model) {
            var response = _userService.Authenticate (model);

            if (response == null)
                return BadRequest (new { message = "Username or password is incorrect" });

            return Ok (response);
        }

        [HttpPost ("Login")]
        public ActionResult Login (AuthenticateRequest model) {
            // var responses = _userService.Authenticate (model);
            Boolean status = AuthenticateUsers.AuthenticateUser (model);
            if (status == true) {
                // var response = ConnectionProfile.SetProfile (model.Username);
                var response = new MappingUserProfile (_mapper).UserProfileMapped (model);
                return Ok (response);
            }
            /*"[\r\n  {\r\n    \"ORG_ID\": \"IGS\",\r\n    \"EMP_ID\": \"550019\",\r\n    \"EMP_FNAME\": \"นิติศักดิ์\",\r\n    \"EMP_LNAME\": \"จั่นแจ่ม\",\r\n    \"POS_ID\": \"104\",\r\n    \"ROLE_ID\": 0,\r\n    \"E_MAIL\": \"nitisak@ingeni.co.th\",\r\n    \"EMP_NICKNAME\": \"นอร์ธ\"\r\n  }\r\n]"*/
            /* [{
                 "ORG_ID": "IGS",
                 "EMP_ID": "550019",
                 "EMP_FNAME": "นิติศักดิ์",
                 "EMP_LNAME": "จั่นแจ่ม",
                 "POS_ID": "104",
                 "ROLE_ID": 0.0,
                 "E_MAIL": "nitisak@ingeni.co.th",
                 "EMP_NICKNAME": "นอร์ธ"
             }]*/
            return BadRequest (new { message = "Username or password is incorrect" });

        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll () {
            var users = _userService.GetAll ();
            return Ok (users);
        }

        [Authorize]
        [HttpPost ("check")]
        public IActionResult PostAll () {
            var users = _userService.GetAll ();
            return Ok (users);
        }

        // [HttpGet ("test")] //Test connect DB
        // public ActionResult<string> Getstrings () {
        //     if (client != null) {
        //         return "Database connected";
        //     } else {
        //         return "Please try again later !!";
        //     }
        // }

        // [HttpGet ("data")] //response all USERs
        // public async Task<List<Register>> Getdata () {
        //     FirebaseResponse response = await client.GetTaskAsync ("Users/");
        //     List<Register> _allUser = response.ResultAs<List<Register>> ();
        //     return _allUser;
        // }

        // [HttpGet ("cnt")] //auto ID
        // public async Task<int> Getcnt () {
        //     FirebaseResponse res = await client.GetTaskAsync ("auto/id");
        //     AutoIncrement get = res.ResultAs<AutoIncrement> ();
        //     int i = get.cnt + 1;
        //     return i;
        // }

        // [HttpPost ("Register")]
        // public async Task<string> Register (Register model) {
        //     FirebaseResponse res = await client.GetTaskAsync ("auto/id");
        //     AutoIncrement get = res.ResultAs<AutoIncrement> ();
        //     model.Id = get.cnt + 1;
        //     SetResponse response = await client.SetTaskAsync ("Users/" + model.Id, model);
        //     var obj = new AutoIncrement {
        //         cnt = model.Id
        //     };
        //     FirebaseResponse resp = await client.UpdateTaskAsync ("auto/id", obj);
        //     return response.Body;
        // }

        // [HttpPost ("AddProfile")]
        // public async Task<ActionResult> AddProfile (UserProfile model) {
        //     FirebaseResponse res = await client.GetTaskAsync ("auto/profile");
        //     AutoIncrement get = res.ResultAs<AutoIncrement> ();
        //     model.ORG_ID = get.cnt + 1;
        //     SetResponse response = await client.SetTaskAsync ("Profiles/" + model.ORG_ID, model);
        //     var obj = new AutoIncrement {
        //         cnt = model.ORG_ID
        //     };
        //     FirebaseResponse resp = await client.UpdateTaskAsync ("auto/profile", obj);
        //     return Ok (response.Body);
        // }

        // [HttpPost ("Search/{id?}")]
        // public async Task<User> Search (int id) {
        //     FirebaseResponse response = await client.GetTaskAsync ("Users/" + id);
        //     User _user = response.ResultAs<User> ();
        //     return _user;
        // }

        // [HttpPost ("testcon")]
        // public ActionResult Testcon (AuthenticateRequest model) {
        //     string _message;
        //     Boolean status = AuthenticateUsers.AuthenticateUser (model);
        //     if (status == true) {
        //         _message = ConnectionProfile.SetProfile (model.Username);
        //         return Ok (_message);
        //     }
        //     return BadRequest (new { message = "Username or password is incorrect" });
        // }
    }
}