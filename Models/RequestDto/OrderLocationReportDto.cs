using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.RequestDto
{
    public class OrderLocationReportDto
    {
        public DateTime? StartDate { get; set; }

        [JsonProperty("startDate")]
        public string FromDate
        {
            get { return StartDate.HasValue ? StartDate.Value.ToString("yyyy-MM-dd") : ""; }
        }
        public DateTime? EndDate { get; set; }
        [JsonProperty("endDate")]
        public string ToDate
        {
            get { return EndDate.HasValue ? EndDate.Value.ToString("yyyy-MM-dd") : ""; }
        }
        [JsonProperty("thanaIds")]
        public List<long> ThanaIds { get; set; } = new List<long>();

        [JsonProperty("districtIds")]
        public List<long> DistrictIds { get; set; } = new List<long>();
    }
}