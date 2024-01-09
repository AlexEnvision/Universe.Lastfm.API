using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Universe.Lastfm.Api.Models.Base;

namespace Universe.Lastfm.Api.Models
{
    public class ReqContext : BaseRequest
    {
        public string User { get; set; }

        public string Period { get; set; }

        public int Page { get; set; }

        public int Limit { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Extend { get; set; }

        public string Taggingtype { get; set; }

        public string Tag { get; set; }
    }
}