// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Runtime.cs" company="Brandon Seydel">
//   N/A
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace MailChimp.Net.Models
{
    /// <summary>
    /// The runtime.
    /// </summary>
    public class Runtime : Base
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Runtime"/> class.
        /// </summary>
        public Runtime()
        {
            Days = new HashSet<string>();
        }

        /// <summary>
        /// Gets or sets the days.
        /// </summary>
        [JsonProperty("days")]
        public IEnumerable<string> Days { get; set; }

        /// <summary>
        /// Gets or sets the hours.
        /// </summary>
        [JsonProperty("hours")]
        public Hours Hours { get; set; }

        internal override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                ;
        }
    }
}