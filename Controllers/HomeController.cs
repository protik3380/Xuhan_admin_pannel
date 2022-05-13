using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using xtbdEcommerceAdminPanel.Enum;
using xtbdEcommerceAdminPanel.Manager;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Models.RequestDto;
using xtbdEcommerceAdminPanel.Models.View_Model;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["email"]==null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult ChangeCrediential()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult ChangeCrediential(AdminAccessDto adminAccess)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                LoginVM loginVm = (LoginVM)Session["LoginVM"];
                adminAccess.UserId = loginVm.UserId;
                using (var client = new HttpClientDemo())
                {
                    var putTask = client.PostAsJsonAsync<AdminAccessDto>("change-password-admin", adminAccess);
                    putTask.Wait();
                    var result = putTask.Result;
                    var data = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Admin Credential change";
                        return View();
                    }
                    TempData["Class"] = Utility.Utility.Error;
                    TempData["Message"] = "Something went wrong!!";
                }
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult ChangeCredientialMasterDepot()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                LoginVM loginVm = (LoginVM)Session["LoginVM"];
                long masterDepotId = 0;
                using (var client = new HttpClientDemo())
                {
                    var responseTask = client.GetAsync("user-to-master-depo-details/"+loginVm.UserId);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsAsync<dynamic>().Result;
                        masterDepotId = data["id"];
                    }                   
                }
                PasswordChangeDto modelDto = new PasswordChangeDto { Id = masterDepotId, UserId = (long)loginVm.UserId };
                return View(modelDto);
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult ChangeCredientialMasterDepot(PasswordChangeDto modelDto)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                MasterDepot masterDepot = new MasterDepot();
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("masterdepot/edit/" + modelDto.Id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    var data = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<MasterDepot>();
                        readTask.Wait();
                        masterDepot = readTask.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }

                MasterDepotDto masterDepotDto = new MasterDepotDto();
                masterDepotDto.Id = masterDepot.Id;
                masterDepotDto.Name = masterDepot.Name;
                masterDepotDto.ThanaId = masterDepot.Thana.Id;
                masterDepotDto.Address = masterDepot.Address;
                masterDepotDto.ContactPerson = masterDepot.ContactPerson;
                masterDepotDto.Phone = masterDepot.Phone;
                masterDepotDto.Email = masterDepot.Email;
                masterDepotDto.IsDeleted = masterDepot.IsDeleted;
                masterDepotDto.IsActive = masterDepot.IsActive;
                masterDepotDto.CreatedBy = masterDepot.CreatedBy;
                masterDepotDto.DistrictId = masterDepot.Thana.District.Id;
                masterDepotDto.ModifiedBy = (long)Session["userId"];
                masterDepotDto.Password = modelDto.Password;
                masterDepotDto.UserId = modelDto.UserId;

                using (var client = new HttpClientDemo())
                {
                    var putTask = client.PostAsJsonAsync<MasterDepotDto>("masterdepot/update", masterDepotDto);
                    putTask.Wait();
                    var result = putTask.Result;
                    //var data = result.Content.ReadAsAsync<dynamic>().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Password Change successfully";
                        return RedirectToAction("ChangeCredientialMasterDepot", "Home");
                    }
                    
                }
                TempData["Class"] = Utility.Utility.Error;
                TempData["Message"] = "Something went wrong";
                return RedirectToAction("ChangeCredientialMasterDepot", "Home");

            }
            catch (Exception exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }



        [HttpPost]
        public ActionResult Login(User aUser)
        {
            using (var client = new HttpClientDemo())
            {
                var user = new
                {
                    email = aUser.Email,
                    password = aUser.password
                };
                var postTask = client.PostAsJsonAsync("login", user);
                postTask.Wait();
                var result = postTask.Result;
                var data1 = result.Content.ReadAsAsync<dynamic>().Result;
                if (result.IsSuccessStatusCode)
                {
                    var dataObj = result.Content.ReadAsStringAsync().Result;
                    JObject responseData = JObject.Parse(dataObj);

                    var vmObj = responseData["data"].Value<JObject>();
                    LoginVM movementData = vmObj.ToObject<LoginVM>();

                    Session["LoginVM"] = movementData;
                    if (movementData.UserTypeId==(int)EnumClass.UserTypeEnum.DeliveryMan)
                    {
                        ViewBag.Message = "Access Denied";
                        return View();
                    }

                    OrderManager orderManager = new OrderManager();
                    aUser.Id = 1;
                    Session["email"] = aUser.Email;
                    Session["userId"] = aUser.Id;
                    //Session["OrderCount"] = orderManager.CountAllOrder();
                    var data = result.Content.ReadAsStringAsync();
                    return RedirectToAction("Product", "DashBoard");
                }           
            }
            ViewBag.Message = "Incorrect email or password! Please try again";
            return View();
        }
        public ActionResult Logout()
        {
            Session["email"] = null;
            return RedirectToAction("Login", "Home");

        }
        public ActionResult Error()
        {
            Session["email"] = null;
            return View();

        }
    }
}