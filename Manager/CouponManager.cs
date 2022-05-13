using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Manager
{
    public class CouponManager
    {
        public List<Coupon> Coupons()
        {
            IEnumerable<Coupon> coupons = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("coupon/getall");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Coupon>>();
                    readTask.Wait();
                    coupons = readTask.Result;
                }
                else
                {
                    coupons = new List<Coupon>();
                }
            }
            return coupons.ToList();
        }

        public List<Coupon> GetCouponsForAllUser()
        {
            var coupons = Coupons();
            List<Coupon> generalCoupons = new List<Coupon>();
            if (coupons.Any())
            {
                generalCoupons = coupons.Where(x => x.UserId == 0 && !x.IsDeleted).ToList();
            }

            return generalCoupons;
        }

        public List<Coupon> GetCouponsForSingleUser(long userId)
        {
            var coupons = Coupons();
            List<Coupon> generalCoupons = new List<Coupon>();
            if (coupons.Any())
            {
                generalCoupons = coupons.Where(x => x.UserId == userId && !x.IsDeleted).ToList();
            }

            return generalCoupons;
        }

    }
}