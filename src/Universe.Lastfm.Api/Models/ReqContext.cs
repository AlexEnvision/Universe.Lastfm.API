using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Universe.Lastfm.Api.Models.Base;

namespace Universe.Lastfm.Api.Models
{
    public class ReqContext : BaseRequest
    {
        public string SessionKey { get; set; }

        public string Token { get; set; }

        /// <summary>
        ///     The secret key from setting of the application
        /// </summary>
        public string SecretKey { get; set; }

        public string User { get; set; }

        public string Period { get; set; }

        public int Page { get; set; }

        public int Limit { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Extend { get; set; }

        public string Taggingtype { get; set; }

        public string Tag { get; set; }

        public string Performer { get; set; }

        public string Album { get; set; }

        public string Track { get; set; }

        public string[] Tags { get; set; }

        public string RemTag { get; set; }

        public long? Timestamp { get; set; }
    }
}