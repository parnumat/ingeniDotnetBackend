using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ingeniProjectFDotnetBackend.Models.Profiles;
using ingeniProjectFDotnetBackend.Services.DataServices;
using Newtonsoft.Json.Linq;
using WebApi.Models;

namespace ingeniProjectFDotnetBackend.Methods {
    public class MappingUserProfile {
        private readonly IMapper _mapper;
        public MappingUserProfile (IMapper mapper) => _mapper = mapper;
        public UserProfile UserProfileMapped (AuthenticateRequest model) {
            var results = ConnectionProfile.SetProfile (model.Username);
            var result = _mapper.Map<IEnumerable<UserProfile>> (results);
            UserProfile user = result.ElementAt(0);
            return user;
        }
    }
}