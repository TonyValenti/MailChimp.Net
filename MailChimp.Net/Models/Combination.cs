using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MailChimp.Net.Models
{
    public class Combination : Base, IId<string>
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}