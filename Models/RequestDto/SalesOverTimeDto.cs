using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.RequestDto
{
    public class SalesOverTimeDto
    {
        public long ReportType { get; set; }

        [JsonProperty("fromMonth")]
        public long FromMonth { get; set; }

        [JsonProperty("toMonth")]
        public long ToMonth { get; set; }

        [JsonProperty("year")]
        public long Year { get; set; }

        [JsonProperty("fromYear")]
        public long FromYear { get; set; }

        [JsonProperty("toYear")]
        public long ToYear { get; set; }
    }
}