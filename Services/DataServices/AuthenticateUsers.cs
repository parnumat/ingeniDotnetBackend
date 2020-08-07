using System;
using System.DirectoryServices;
using WebApi.Models;

namespace WebApi.Services.DataServices {
    public class AuthenticateUsers {
        internal static Boolean AuthenticateUser (AuthenticateRequest model) {
            DirectoryEntry entry = new DirectoryEntry ("LDAP://192.168.1.2", model.Username, model.Password);
            entry.AuthenticationType = AuthenticationTypes.Secure;
            try {
                DirectorySearcher search = new DirectorySearcher (entry);
                search.SearchRoot = entry;
                search.Filter = "(&(ObjectClass=user)SAMAccountName=" + model.Username + ")";
                SearchResultCollection result = search.FindAll ();
                if (null == result) {
                    return false;
                }
            } catch (Exception ex) {
                return false;
                throw new Exception ("Error authenticating user." + ex.Message);
            }
            return true;
        }
    }
}