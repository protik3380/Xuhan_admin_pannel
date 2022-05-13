using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.RequestDto
{
    public class ReportSearchCriteria
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


        [JsonProperty("brandIds")]
        public List<long> BrandIds { get; set; } = new List<long>();

        [JsonProperty("categoryIds")]
        public List<long> CategoryIds { get; set; } = new List<long>();

        [JsonProperty("masterDeportIds")]
        public List<long> MasterDepotIds { get; set; } = new List<long>();
    }
}