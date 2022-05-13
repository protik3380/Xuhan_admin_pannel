using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.RequestDto
{
    public class CancelOrderDto
    {
        [JsonProperty("orderId")]
        public long OrderId { get; set; }

        [JsonProperty("orderStateId")]
        public long OrderStateId { get; set; }

        [JsonProperty("orderRejectId")]
        public long OrderRejectId { get; set; }

        [JsonProperty("userId")]
        public long UserId { get; set; }
    }
}