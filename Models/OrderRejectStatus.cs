using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models
{
    public class OrderRejectStatus
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("rejectStatus")]
        public string RejectStatus { get; set; }
    }
}