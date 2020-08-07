using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Methods;
using WebApi.Models;
using WebApi.Persistence.Helpers;

namespace WebApi.Controllers {
    [Route ("[controller]")]
    [ApiController]
    public class MenuController : ControllerBase {
        private readonly IMapper _mapper;
        public MenuController (IMapper mapper) => _mapper = mapper;
        private object _results = null;

        // GET api/test
        [HttpGet ("")]
        public ActionResult<IEnumerable<string>> Getstrings () {
            return new List<string> { };
        }

        [Authorize]
        [HttpPost ("")]
        public async Task<ActionResult<AppUserToolModel>> Poststring (UserInputModel model) {

            // List<Param> param = new List<Param> () {
            //     new Param () { ParamName = "AS_USER_ID", ParamType = ParamMeterTypeEnum.STRING, ParamValue = model.user_id },
            //     new Param () { ParamName = "AS_MENU_GRP", ParamType = ParamMeterTypeEnum.STRING, ParamValue = model.group_id },
            // };
            // var results = await new DataContext (_mapper).CallStoredProcudure (DataBaseHostEnum.KPR, "KPDBA.SP_GET_APP_USER_TOOL", param);
            // if (results == null)
            //     return null;
            // var result = _mapper.Map<IEnumerable<AppUserToolModel>> (results);
            // var resultReal = _mapper.Map<IEnumerable<AppUserToolModel>, IEnumerable<testModel>> (result).ToList ();
            // return Ok (resultReal);
            try {
                switch (model.fn) {
                    case "fn1":
                        _results = new AllMethods (_mapper).GetIconMenu (model);
                        break;
                    default://ยังไม่สนใจ fn 
                        _results = new AllMethods (_mapper).GetIconMenu (model);
                        break;
                }
                if (_results != null)
                    return Ok (_results);
                return BadRequest (new { message = "Something wa wrong!!" });
            } catch (Exception e) {
                return BadRequest (new { message = e.Message });
            }
        }
        //Ex
        // [{
        //     "id": "1",
        //     "appName": "KIMPAI QR Code",
        //     "img": "assets/img/qr code.png"
        // }, {
        //     "id": "2",
        //     "appName": "จับเวลาการผลิตห้าง(PROOF)",
        //     "img": "assets/img/time.png"
        // }]
    }
}