using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Vereyon.Web;
using xtbdEcommerceAdminPanel.Manager;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Models.RequestDto;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class CouponController : Controller
    {
        // GET: Coupon
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApplyCouponAllUser()
        {            
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                ViewBag.AllUserCoupon = new CouponManager().GetCouponsForAllUser();
                return View();
            }
            catch (Exception exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }


        public JsonResult GetCouponForAllUser()
        {
            List<Coupon> generalCoupons = new CouponManager().GetCouponsForAllUser();
            Coupon previousCoupon = new Coupon();
            if (generalCoupons.Count>0)
            {
                previousCoupon = generalCoupons.SingleOrDefault();
            }
            return Json(previousCoupon, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ApplyCouponAllUser(Coupon coupon)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
              
                using (var client = new HttpClientDemo())
                {

                    var previousCoupon = new CouponManager().GetCouponsForAllUser().SingleOrDefault();
                    if (previousCoupon != null)
                    {

                        previousCoupon.IsActive = false;
                        previousCoupon.IsDeleted = true;
                        previousCoupon.ModifiedBy = (long)Session["userId"];

                        var putTask = client.PostAsJsonAsync<Coupon>("coupon/update", previousCoupon);
                        putTask.Wait();
                        var resultUp = putTask.Result;
                        var data1 = resultUp.Content.ReadAsAsync<dynamic>().Result;
                    }
                    coupon.IsDeleted = false;
                    coupon.CreatedBy = (long)Session["userId"];
                    coupon.UserId = 0;
                    var postTask = client.PostAsJsonAsync("coupon/create", coupon);
                    postTask.Wait();
                    var result = postTask.Result;
                    var data = result.Content.ReadAsAsync<dynamic>().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Coupon created successfully.";
                        return RedirectToAction("ApplyCouponAllUser", "Coupon");
                    }
                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        TempData["Class"] = Utility.Utility.Error;
                        TempData["Message"] = "This coupon already exists,";
                        return RedirectToAction("ApplyCouponAllUser", "Coupon");
                    }
                }
                return RedirectToAction("ApplyCouponAllUser", "Coupon");
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }

        public JsonResult GetCouponDetailsById(long couponId)
        {
            Coupon coupon = new Coupon();
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("coupon/edit/" + couponId);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Coupon>();
                    readTask.Wait();
                    coupon = readTask.Result;
                    return Json(new { Status = "Ok", Coupon = coupon });
                }
            }

            return Json(new { Status = "Failed" });
        }

        [HttpPost]
        public ActionResult Edit(Coupon aCoupon)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                long userId = Convert.ToInt64(Session["UserId"]);
                if (aCoupon != null)
                {
                    aCoupon.ModifiedBy = userId;
                    aCoupon.IsDeleted = false;
                    using (var client = new HttpClientDemo())
                    {
                        var putTask = client.PostAsJsonAsync<Coupon>("coupon/update", aCoupon);
                        putTask.Wait();
                        var result = putTask.Result;
                        var data = result.Content.ReadAsAsync<dynamic>().Result;
                        if (result.IsSuccessStatusCode)
                        {
                            TempData["Class"] = Utility.Utility.Success;
                            TempData["Message"] = "Coupon updated successfully.";
                            if (aCoupon.UserId==0)
                            {
                                return RedirectToAction("ApplyCouponAllUser", "Coupon");
                            }
                            return RedirectToAction("CouponUser", "Coupon", new { @id = aCoupon.UserId });

                        }
                        if (result.StatusCode == HttpStatusCode.Conflict)
                        {
                            TempData["Class"] = Utility.Utility.Error;
                            TempData["Message"] = "This coupon already exist.";
                            if (aCoupon.UserId == 0)
                            {
                                return RedirectToAction("ApplyCouponAllUser", "Coupon");
                            }
                            return RedirectToAction("CouponUser", "Coupon", new { @id = aCoupon.UserId });
                        }
                        ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);
                    }
                }  
                return RedirectToAction("ApplyCouponAllUser", "Coupon");
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult CouponUser(long id)
        {            
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                ViewBag.UserCoupon = new CouponManager().GetCouponsForSingleUser(id);
                ViewBag.UserId = id;
                return View();
            }
            catch (Exception exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public JsonResult GetCouponForSingleUser(long userId)
        {
            List<Coupon> generalCoupons = new CouponManager().GetCouponsForSingleUser(userId);
            Coupon previousCoupon = new Coupon();
            if (generalCoupons.Count > 0)
            {
                previousCoupon = generalCoupons.SingleOrDefault();
            }
            return Json(previousCoupon, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ApplyCouponSingleUser(Coupon coupon)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                using (var client = new HttpClientDemo())
                {

                    var previousCoupon = new CouponManager().GetCouponsForSingleUser(coupon.UserId).SingleOrDefault();
                    if (previousCoupon != null)
                    {

                        previousCoupon.IsActive = false;
                        previousCoupon.IsDeleted = true;
                        previousCoupon.ModifiedBy = (long)Session["userId"];

                        var putTask = client.PostAsJsonAsync<Coupon>("coupon/update", previousCoupon);
                        putTask.Wait();
                        var resultUp = putTask.Result;
                        var data1 = resultUp.Content.ReadAsAsync<dynamic>().Result;
                    }
                    coupon.IsDeleted = false;
                    coupon.CreatedBy = (long)Session["userId"];
                    var postTask = client.PostAsJsonAsync("coupon/create", coupon);
                    postTask.Wait();
                    var result = postTask.Result;
                    var data = result.Content.ReadAsAsync<dynamic>().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Coupon created successfully.";
                        return RedirectToAction("CouponUser", "Coupon", new { @id = coupon.UserId });
                    }
                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        TempData["Class"] = Utility.Utility.Error;
                        TempData["Message"] = "This coupon already exists,";
                        return RedirectToAction("CouponUser", "Coupon", new { @id = coupon.UserId });
                    }
                }
                return RedirectToAction("CouponUser", "Coupon", new { @id = coupon.UserId });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}