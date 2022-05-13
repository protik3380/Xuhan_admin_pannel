using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xtbdEcommerceAdminPanel.Models
{
    public class District
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        [Required(ErrorMessage = "Please enter district name")]
        public string DistrictName { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdBy")]
        public long? CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }
    }
}