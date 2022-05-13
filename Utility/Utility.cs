using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xtbdEcommerceAdminPanel.Utility
{
    public static class Utility
    {
        public static string Success = "alert alert-success";
        public static string Error = "alert alert-danger";
        public static bool IsLogin()
        {
            return true;
        }
        public static string GenerateImageNameFromTimestamp()
        {
            DateTime value = DateTime.UtcNow.AddHours(6);
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}