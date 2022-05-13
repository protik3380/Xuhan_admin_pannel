using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.RequestDto
{
    public class OrderSearchDto
    {
        [JsonProperty("orderStateIds")]
        public List<long> OrderStateIds { get; set; }
    }
}