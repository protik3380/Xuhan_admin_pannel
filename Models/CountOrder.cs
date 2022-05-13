using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models
{
    public class CountOrder
    {
        [JsonProperty("Pending")]
        public int Pending { get; set; }

        [JsonProperty("Processing")]
        public int Processing { get; set; }

        [JsonProperty("Packaging")]
        public int Packaging { get; set; }

        [JsonProperty("Shipped")]
        public int Shipped { get; set; }

        [JsonProperty("Delivered")]
        public int Delivered { get; set; }

        [JsonProperty("Received")]
        public int Received { get; set; }

        [JsonProperty("Cancelled")]
        public int Cancelled { get; set; }
    }
}