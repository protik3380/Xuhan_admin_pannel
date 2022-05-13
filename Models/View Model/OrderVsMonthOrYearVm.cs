using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.View_Model
{
    public class OrderVsMonthOrYearVm
    {

        [JsonProperty("month")]
        public long Month { get; set; }

        [JsonProperty("year")]
        public long Year { get; set; }

        [JsonProperty("orderRate")]
        public double OrderRate { get; set; }
    }
}