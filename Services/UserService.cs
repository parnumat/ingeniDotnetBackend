using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebApi.Models;
using WebApi.Models.Profiles;
using WebApi.Security;

namespace WebApi.Services {
    public interface IUserService {
        AuthenticateResponse Authenticate (UserProfile model, string userName);
        IEnumerable<DataORG> GetAll ();
        DataORG GetByOrg (string org);
        UserProfile DecodeToken (string token);
        // string getsss (string org);
    }

    public class UserService : IUserService {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        // DataBaseHostEnum _user = DataBaseHostEnum.;
        List<DataORG> _users = new List<DataORG> {
            new DataORG {
            org = "OPPN"
            },
            new DataORG {
            org = "LAP"
            },
            new DataORG {
            org = "KPP"
            },
            new DataORG {
            org = "KPR"
            },
            new DataORG {
            org = "IGS"
            }
        };
        private readonly AppSettings _appSettings;

        public UserService (IOptions<AppSettings> appSettings) {
            _appSettings = appSettings.Value;
            // client = new FireSharp.FirebaseClient (config);
            // FirebaseResponse response = client.Get ("Users/");
            // _users = response.ResultAs<List<User>> ();
        }

        public AuthenticateResponse Authenticate (UserProfile model, string userName) {

            // var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (model == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken (model, userName);

            return new AuthenticateResponse (model, token);
        }

        public IEnumerable<DataORG> GetAll () {
            return _users;
        }

        // public static string GetDatabase (string org) {
        //     // ConnectionFactory cf = new ConnectionFactory();
        //     // return cf.GetNewInstance(databasehost);
        //     // DataBaseHostEnum host = DataContextConfiguration.DEFAULT_DATABASE;
        //     string name = null;
        //     switch (org) {
        //         case "OPP":
        //         case "TOPP":
        //             name = DataBaseHostEnum.OPPN.ToString();
        //             break;
        //         case "LAP":
        //             name = DataBaseHostEnum.LAP.ToString();
        //             break;
        //         case "KPP":
        //             name = DataBaseHostEnum.KPP.ToString();
        //             break;

        //     }
        //     return name;
        // }

        public DataORG GetByOrg (string org) {
            return _users.FirstOrDefault (x => x.org == org);
        }

        // helper methods

        private string generateJwtToken (UserProfile user, string userName) {
            // generate token that is valid for 1 days
            var claims = new List<Claim> {
                new Claim ("org", user.org),
                new Claim ("userID", user.userID),
                new Claim ("userName", user.userName),
                // new Claim ("EMP_NAME_ENG", user.EMP_NAME_ENG),
                new Claim ("aduserID", userName),
                new Claim ("nickname", user.nickname),
                new Claim ("email", user.email),
                new Claim ("posrole", user.posrole)
            };
            var tokenHandler = new JwtSecurityTokenHandler ();
            var key = Encoding.ASCII.GetBytes (_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.UtcNow.AddDays (1),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken (tokenDescriptor);
            return tokenHandler.WriteToken (token);
        }
        public UserProfile DecodeToken (string token) {
            var jwtHandler = new JwtSecurityTokenHandler ();
            if (!jwtHandler.CanReadToken (token))
                return null;

            var tokenJwt = jwtHandler.ReadJwtToken (token);
            var jwtPayload = JsonConvert.SerializeObject (tokenJwt.Claims.Select (c => new { c.Type, c.Value }));

            List<JWTDecode> jwtDecode = JsonConvert.DeserializeObject<List<JWTDecode>> (jwtPayload);

            UserProfile user = new UserProfile ();
            foreach (JWTDecode j in jwtDecode) {
                switch (j.Type) {
                    case "userName":
                        user.userName = j.Value;
                        break;
                    case "userID":
                        user.userID = j.Value;
                        break;
                    case "aduserID":
                        user.aduserID = j.Value;
                        break;
                    case "nickname":
                        user.nickname = j.Value;
                        break;
                    case "email":
                        user.email = j.Value;
                        break;
                    case "org":
                        user.org = j.Value;
                        break;
                    case "posrole":
                        user.posrole = j.Value;
                        break;
                    default:
                        break;
                }
            }

            return user;

            // var tokenHandler = new JwtSecurityTokenHandler();
            // var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            // var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            // return stringClaimValue;
        }
    }
}