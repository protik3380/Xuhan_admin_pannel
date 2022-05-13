using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models
{
    public class AssignOrder
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("isDelivered")]
        public bool IsDelivered { get; set; }

        [JsonProperty("orderId")]
        public long OrderId { get; set; }

        [JsonProperty("deliveryManId")]
        public long DeliveryManId { get; set; }

        [JsonProperty("assignedBy")]
        public long AssignedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("deliveryMan")]
        public Deliveryman DeliveryMan { get; set; }

        [JsonProperty("order")]
        public Order Order { get; set; }
    }
}