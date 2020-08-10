using System;
using ingeniProjectFDotnetBackend.Models.Profiles;
using WebApi.Entities;

namespace WebApi.Models {
    public class AuthenticateResponse {
        public string ORG_ID { get; set; }
        public string EMP_ID { get; set; }
        public string EMP_NAME { get; set; }
        // public string EMP_NAME_ENG { get; set; }
        public string NICKNAME { get; set; }
        public string EMAIL { get; set; }
        public string POS_ROLE { get; set; }
        // public string AD_USER_ID { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse (UserProfile user, string token) {
            ORG_ID = user.ORG_ID;
            EMP_ID = user.EMP_ID;
            EMP_NAME = user.EMP_NAME;
            // EMP_NAME_ENG = user.EMP_NAME_ENG;
            NICKNAME = user.NICKNAME;
            EMAIL = user.EMAIL;
            POS_ROLE = user.POS_ROLE;
            // AD_USER_ID=user.AD_USER_ID;
            Token = token;
        }
    }
}