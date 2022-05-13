using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.RequestDto
{
    public class AssignOrderDto
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("orderId")]
        public long OrderId { get; set; }

        [JsonProperty("isDelivered")]
        public bool IsDelivered { get; set; }

        [JsonProperty("deliveryManId")]
        public long DeliveryManId { get; set; }

        [JsonProperty("assignedBy")]
        public long AssignedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long ModifiedBy { get; set; }
    }
}