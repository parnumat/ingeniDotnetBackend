using System.Data;
using ingeniProjectFDotnetBackend.Models.Profiles;
using Newtonsoft.Json;

namespace ingeniProjectFDotnetBackend.Services.DataServices {
    public class ConnectionProfile {
        public static string checkUsersALL () {
            DataTable dataTable = Sql.Execute ("SELECT AD_USER_ID, EMP_ID, EMP_FNAME, EMP_LNAME FROM  PORTAL_PROD.dbo.MV_ALL_USER WHERE AD_USER_ID IS NOT NULL");
            //Convert DataTable to Json
            string jsonMessage = JsonConvert.SerializeObject (dataTable);
            return jsonMessage;
        }
        public static string SetProfile (string user) {
            DataTable dataTable = Sql.Execute ("SELECT distinct ORG_ID,EMP_ID,EMP_FNAME,EMP_LNAME,POS_ID,ROLE_ID,IsNull(E_MAIL,'') AS E_MAIL,IsNull(EMP_NICKNAME,'') AS EMP_NICKNAME FROM  PORTAL_PROD.dbo.MV_ALL_USER WHERE AD_USER_ID = '" + user + "'");
            string jsonMessage = JsonConvert.SerializeObject (dataTable);
            return jsonMessage;
        }

    }
}