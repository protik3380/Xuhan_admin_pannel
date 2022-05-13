using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.RequestDto
{
    public class DeliverymanDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("thanaId")]
        public long ThanaId { get; set; }

        [JsonProperty("masterDepotId")]
        public long MasterDepotId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("nid")]
        public string Nid { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobileNo")]
        public string MobileNo { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdBy")]
        public long? CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }
        public HttpPostedFileBase DeliverymanImage { get; set; }
        public long DistrictId { get; set; }

        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}