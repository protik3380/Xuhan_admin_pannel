using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models
{
    public class Customer
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("mobileNo")]
        public string MobileNo { get; set; }

        [JsonProperty("alternativeMobileNo")]
        public string AlternativeMobileNo { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("deliveryAddress1")]
        public string DeliveryAddress1 { get; set; }

        [JsonProperty("deliveryAddress2")]
        public string DeliveryAddress2 { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdBy")]
        public long? CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
}