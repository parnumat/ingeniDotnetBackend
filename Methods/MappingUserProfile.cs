using AutoMapper;
using ingeniProjectFDotnetBackend.Models.Profiles;
using ingeniProjectFDotnetBackend.Services.DataServices;
using WebApi.Models;

namespace ingeniProjectFDotnetBackend.Methods {
    public class MappingUserProfile {
        private readonly IMapper _mapper;
        public MappingUserProfile (IMapper mapper) => _mapper = mapper;
        public UserProfileFromSQl UserProfileMapped (AuthenticateRequest model) {
            var results = ConnectionProfile.SetProfile (model.Username);
            // var res = UserProfileFromSQl.FromJson(results);
            var result = _mapper.Map<UserProfileFromSQl> (results);
            return result;
        }
    }
}