using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace MailChimp.Net.Models
{
    public class WebHook : Base, IId<string>
    {

        public WebHook()
        {
            Links = new HashSet<Link>();
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("events")]
        public Event Event { get; set; }

        [JsonProperty("sources")]
        public Source Source { get; set; }

        [JsonProperty("list_id")]
        public string ListId { get; set; }

        [JsonProperty("_links")]
        public IEnumerable<Link> Links { get; set; }

        internal override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Id.Add(Id)
                .Data.Add(Url)
                ;
        }
    }
}
