using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xtbdEcommerceAdminPanel.Models
{
    public class Distributor
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("masterDepotId")]
        public long MasterDepotId { get; set; }

        [JsonProperty("thanaId")]
        public long ThanaId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("contactPerson")]
        public string ContactPerson { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("masterDepot")]
        public MasterDepot MasterDepot { get; set; }

        [JsonProperty("thana")]
        public Thana Thana { get; set; }
    }
}