// using System.Globalization;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Converters;

namespace ingeniProjectFDotnetBackend.Models.Profiles {
    public partial class UserProfileFromSQl {
        public string ORG_ID { get; set; }
        public string EMP_ID { get; set; }
        public string EMP_FNAME { get; set; }
        public string EMP_LNAME { get; set; }
        public string POS_ID { get; set; }
        public decimal ROLE_ID { get; set; }
        public string E_MAIL { get; set; }
        public string EMP_NICKNAME { get; set; }
    }
    // public partial class UserProfileFromSQl {
    //     public static UserProfileFromSQl[] FromJson (string json) => JsonConvert.DeserializeObject<UserProfileFromSQl[]> (json, ingeniProjectFDotnetBackend.Models.Profiles.Converter.Settings);
    // }
    // internal static class Converter {
    //     public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings {
    //         MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
    //         DateParseHandling = DateParseHandling.None,
    //         Converters = {
    //         new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
    //         },
    //     };
    // }
}