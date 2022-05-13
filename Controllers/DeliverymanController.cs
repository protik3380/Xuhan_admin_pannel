using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Models.RequestDto;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class DeliverymanController : Controller
    {
        // GET: Deliveryman
        public ActionResult Index()
        {

            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                ViewBag.DeliverymanList = Dropdown.DeliverymanList()
                    .Where(x => !x.IsDeleted)
                    .OrderBy(x => x.Name).ToList();

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult Create()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Districts = Dropdown.ActiveDistricts();
                ViewBag.MasterDeports = Dropdown.ActiveMasterDepots();
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DeliverymanDto deliveryman)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (deliveryman.DeliverymanImage != null)
                {
                    var deliverymanName = deliveryman.Name.Replace(" ", String.Empty);
                    var filename = deliverymanName + Path.GetExtension(deliveryman.DeliverymanImage.FileName);
                    string path = Path.Combine(Server.MapPath("~/upload/deliveryman/"), filename);
                    deliveryman.DeliverymanImage.SaveAs(path);
                    deliveryman.ImageUrl = "upload/deliveryman/" + filename;
                    deliveryman.DeliverymanImage = null;
                }

                using (var client = new Utility.HttpClientDemo())
                {

                    deliveryman.CreatedBy = (long)Session["userId"];
                    deliveryman.IsDeleted = false;
                    var postTask = client.PostAsJsonAsync("deliveryman/create", deliveryman);
                    postTask.Wait();
                    var result = postTask.Result;
                    var data1 = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Deliveryman created successfully.";
                        return RedirectToAction("Index", "Deliveryman");
                    }
                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        ViewBag.Districts = Dropdown.ActiveDistricts();
                        ViewBag.MasterDeports = Dropdown.ActiveMasterDepots();
                        string validationMessage = string.Empty;
                        var data = result.Content.ReadAsStringAsync().Result;

                        if (data.Contains("mobileNo"))
                        {
                            validationMessage += "The mobile no has already been taken.\n";
                        }
                        if (data.Contains("email"))
                        {
                            validationMessage += "The email has already been taken.\n";
                        }
                        if (data.Contains("nid"))
                        {
                            validationMessage += "The nid has already been taken.\n";
                        }
                        TempData["Class"] = Utility.Utility.Error;
                        TempData["Message"] = validationMessage;
                        return RedirectToAction("Create", "Deliveryman");
                    }
                }
                return RedirectToAction("Index", "Deliveryman");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult Edit(long id)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                Deliveryman deliveryman = new Deliveryman();
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("deliveryman/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Deliveryman>();
                        readTask.Wait();
                        deliveryman = readTask.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }

                DeliverymanDto deliverymanDto = new DeliverymanDto
                {
                    Id = deliveryman.Id,
                    Name = deliveryman.Name,
                    Address = deliveryman.Address,
                    CreatedBy = deliveryman.CreatedBy,
                    DistrictId = deliveryman.Thana.DistrictId,
                    ModifiedBy = deliveryman.ModifiedBy,
                    MobileNo = deliveryman.MobileNo,
                    Email = deliveryman.Email,
                    Nid = deliveryman.Nid,
                    ThanaId = deliveryman.ThanaId,
                    MasterDepotId = deliveryman.MasterDepotId,
                    ImageUrl = deliveryman.ImageUrl,
                    IsActive = deliveryman.IsActive,
                    IsDeleted = deliveryman.IsDeleted,
                    UserId = deliveryman.UserId
                   
                };
                ViewBag.Districts = Dropdown.ActiveDistricts();
                ViewBag.MasterDeports = Dropdown.ActiveMasterDepots();
                return View(deliverymanDto);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public ActionResult Edit(DeliverymanDto deliveryman)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                long userId = (long)Session["userId"];
                deliveryman.ModifiedBy = userId;
                deliveryman.IsDeleted = false;
                bool hasNewImage = deliveryman.DeliverymanImage != null;
                if (hasNewImage)
                {
                    var filePath = Server.MapPath("~/" + deliveryman.ImageUrl);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    deliveryman.ImageUrl = null;

                    var deliverymanName = deliveryman.Name.Replace(" ", String.Empty);
                    var filename = deliverymanName + Path.GetExtension(deliveryman.DeliverymanImage.FileName);
                    string path = Path.Combine(Server.MapPath("~/upload/deliveryman/"), filename);
                    deliveryman.DeliverymanImage.SaveAs(path);
                    deliveryman.ImageUrl = "upload/deliveryman/" + filename;
                    deliveryman.DeliverymanImage = null;
                }

                using (var client = new HttpClientDemo())
                {
                    var postTask = client.PostAsJsonAsync<DeliverymanDto>("deliveryman/update", deliveryman);
                    postTask.Wait();
                    postTask.Wait();
                    var result = postTask.Result;
                    var data1 = result.Content.ReadAsStringAsync().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Deliveryman updated successfully.";
                        return RedirectToAction("Index", "Deliveryman");
                    }
                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        ViewBag.Districts = Dropdown.ActiveDistricts();
                        ViewBag.MasterDeports = Dropdown.ActiveMasterDepots();
                        string validationMessage = string.Empty;
                        var data = result.Content.ReadAsStringAsync().Result;

                        if (data.Contains("mobileNo"))
                        {
                            validationMessage += "The mobile no has already been taken.\n";
                        }
                        if (data.Contains("email"))
                        {
                            validationMessage += "The email has already been taken.\n";
                        }
                        if (data.Contains("nid"))
                        {
                            validationMessage += "The nid has already been taken.\n";
                        }
                        TempData["Class"] = Utility.Utility.Error;
                        TempData["Message"] = validationMessage;
                        return RedirectToAction("Edit", "Deliveryman");
                    }
                }
                return RedirectToAction("Index", "Deliveryman");
            }
            catch (Exception exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult ChangePassword(long id, long userId)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                PasswordChangeDto modelDto = new PasswordChangeDto { Id = id, UserId = userId };
                return View(modelDto);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult ConfirmPassWord(PasswordChangeDto modelDto)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                Deliveryman deliveryman = new Deliveryman();
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("deliveryman/edit/" + modelDto.Id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Deliveryman>();
                        readTask.Wait();
                        deliveryman = readTask.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }

                DeliverymanDto deliverymanDto = new DeliverymanDto
                {
                    Id = deliveryman.Id,
                    Name = deliveryman.Name,
                    Address = deliveryman.Address,
                    CreatedBy = deliveryman.CreatedBy,
                    DistrictId = deliveryman.Thana.DistrictId,
                    ModifiedBy = deliveryman.ModifiedBy,
                    MobileNo = deliveryman.MobileNo,
                    Email = deliveryman.Email,
                    Nid = deliveryman.Nid,
                    ThanaId = deliveryman.ThanaId,
                    MasterDepotId = deliveryman.MasterDepotId,
                    ImageUrl = deliveryman.ImageUrl,
                    IsActive = deliveryman.IsActive,
                    IsDeleted = deliveryman.IsDeleted,
                    UserId = deliveryman.UserId

                };
                long userId = (long)Session["userId"];
                deliverymanDto.ModifiedBy = userId;
                deliverymanDto.Password = modelDto.Password;

                using (var client = new HttpClientDemo())
                {
                    var postTask = client.PostAsJsonAsync<DeliverymanDto>("deliveryman/update", deliverymanDto);
                    postTask.Wait();
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Password Change successfully";
                        return RedirectToAction("Index", "Deliveryman");
                    }
                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        TempData["Class"] = Utility.Utility.Error;
                        TempData["Message"] = "This item already exist.";
                        return RedirectToAction("Index", "Deliveryman");
                    }
                }
                TempData["Class"] = Utility.Utility.Error;
                TempData["Message"] = "Something went wrong";
                return RedirectToAction("Index", "Deliveryman");

            }
            catch (Exception exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}