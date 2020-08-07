using ingeniProjectFDotnetBackend.Models.Profiles;
using Newtonsoft.Json.Linq;
using WebApi.Models;

namespace WebApi.Profiles {
    public class MapperProfile : AutoMapper.Profile {
        public MapperProfile () {
            CreateMap<AppUserToolModel, testModel> ()
                .ForMember (d => d.ID, o => o.MapFrom (s => s.TOOL_ID))
                .ForMember (d => d.appName, o => o.MapFrom (s => s.TOOL_NAME))
                .ForMember (d => d.img, o => o.MapFrom (s => s.ICON_SRC));

            CreateMap<JObject, UserProfileFromSQl> ()
                .ForMember (d => d.ORG_ID, o => o.MapFrom (s => s["ORG_ID"]))
                .ForMember (d => d.EMP_ID, o => o.MapFrom (s => s["EMP_ID"]))
                .ForMember (d => d.EMP_FNAME, o => o.MapFrom (s => s["EMP_FNAME"]))
                .ForMember (d => d.EMP_LNAME, o => o.MapFrom (s => s["EMP_LNAME"]))
                .ForMember (d => d.POS_ID, o => o.MapFrom (s => s["POS_ID"]))
                .ForMember (d => d.ROLE_ID, o => o.MapFrom (s => s["ROLE_ID"]))
                .ForMember (d => d.E_MAIL, o => o.MapFrom (s => s["E_MAIL"]))
                .ForMember (d => d.EMP_NICKNAME, o => o.MapFrom (s => s["EMP_NICKNAME"]));
        }
    }
}