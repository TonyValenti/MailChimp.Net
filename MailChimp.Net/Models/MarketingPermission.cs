using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics;

namespace MailChimp.Net.Models
{
    public class MarketingPermission : Base, IId<string>
    {
        /// <summary>
        /// Gets or sets the the id for the marketing permission on the list.
        /// </summary>
        [JsonProperty("marketing_permission_id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the text permission.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets if the subscriber has opted-in to the marketing permission.
        /// </summary>
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        internal override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Id.Add(Id)
                .Data.Add(Text)
                .Status.IsEnabled(Enabled)
                ;
        }
    }
}
