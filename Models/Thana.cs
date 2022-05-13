using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xtbdEcommerceAdminPanel.Models
{
    public class Thana
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        [Required(ErrorMessage = "Please enter thana name")]
        public string ThanaName { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("districtId")]
        [Required(ErrorMessage = "Please choose district")]
        public long DistrictId { get; set; }


        [JsonProperty("deliveryCharge")]
        [Required(ErrorMessage = "Please enter delivery charge")]
        public decimal DeliveryCharge { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdBy")]
        public long? CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }

        [JsonProperty("district")]
        public District District { get; set; }
    }
    
}