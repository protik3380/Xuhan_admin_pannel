using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.View_Model
{
    public class LoginVM
    {
        [JsonProperty("id")]
        public int UserId { get; set; }

        [JsonProperty("userTypeId")]
        public int UserTypeId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobileNo")]
        public string MobileNo { get; set; }

        [JsonProperty("facebookId")]
        public object FacebookId { get; set; }

        [JsonProperty("email_verified_at")]
        public object EmailVerifiedAt { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdBy")]
        public long? CreatedBy { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("loginType")]
        public int LoginType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}