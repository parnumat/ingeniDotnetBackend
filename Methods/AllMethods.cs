using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Models;
using WebApi.Persistence.Helpers;

namespace WebApi.Methods {
    public class AllMethods {
        private readonly IMapper _mapper;
        public AllMethods (IMapper mapper) => _mapper = mapper;
        public async System.Threading.Tasks.Task<List<GetMenuModel>> GetIconMenu (UserInputModel model) {
            List<Param> param = new List<Param> () {
            new Param () { ParamName = "AS_USER_ID", ParamType = ParamMeterTypeEnum.STRING, ParamValue = model.user_id },
            new Param () { ParamName = "AS_MENU_GRP", ParamType = ParamMeterTypeEnum.STRING, ParamValue = model.group_id },
            };
            var results = await new DataContext (_mapper).CallStoredProcudure (DataBaseHostEnum.KPR, "KPDBA.SP_GET_APP_USER_TOOL", param);
            if (results == null)
                return null;
            var result = _mapper.Map<IEnumerable<AppUserToolModel>> (results);
            var resultReal = _mapper.Map<IEnumerable<AppUserToolModel>, IEnumerable<GetMenuModel>> (result).ToList ();
            return resultReal;
        }
    }
}