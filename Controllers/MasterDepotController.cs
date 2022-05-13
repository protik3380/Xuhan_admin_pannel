using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Models.RequestDto;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class MasterDepotController : Controller
    {
        // GET: MasterDepot
        public ActionResult Index()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                ViewBag.MasterDeports = Dropdown.MasterDepots()
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
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public ActionResult Create(MasterDepotDto masterDepot)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Districts = Dropdown.ActiveDistricts();
                using (var client = new HttpClientDemo())
                {
                    masterDepot.CreatedBy = (long)Session["userId"];
                    masterDepot.IsDeleted = false;

                    var postTask = client.PostAsJsonAsync("masterdepot/create", masterDepot);
                    postTask.Wait();
                    var result = postTask.Result;
                    // var data = result.Content.ReadAsAsync<dynamic>().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Master depot created successfully.";
                        return RedirectToAction("Index", "MasterDepot");
                    }
                    if (result.StatusCode == HttpStatusCode.BadRequest)
                    {
                        string validationMessage = string.Empty;
                        var data = result.Content.ReadAsStringAsync().Result;
                        if (data.Contains("email"))
                        {
                            validationMessage += "The email has already been taken.\n";
                        }
                        TempData["Class"] = Utility.Utility.Error;
                        TempData["Message"] = validationMessage;
                        return RedirectToAction("Create", "MasterDepot");
                    }

                    TempData["Class"] = Utility.Utility.Error;
                    TempData["Message"] = "Something went wrong";
                    return RedirectToAction("Create", "MasterDepot");
                }
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
                MasterDepot masterDepot = new MasterDepot();
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("masterdepot/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
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

                MasterDepotDto masterDepotDto = new MasterDepotDto
                {
                    Id = masterDepot.Id,
                    Name = masterDepot.Name,
                    ThanaId = masterDepot.Thana.Id,
                    Address = masterDepot.Address,
                    ContactPerson = masterDepot.ContactPerson,
                    Phone = masterDepot.Phone,
                    Email = masterDepot.Email,
                    IsDeleted = masterDepot.IsDeleted,
                    IsActive = masterDepot.IsActive,
                    CreatedBy = masterDepot.CreatedBy,
                    ModifiedBy = masterDepot.ModifiedBy,
                    DistrictId = masterDepot.Thana.District.Id,
                    UserId = masterDepot.UserId     
                };
                ViewBag.Districts = Dropdown.ActiveDistricts();
                return View(masterDepotDto);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public ActionResult Edit(MasterDepotDto masterDepotDto)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                if (masterDepotDto != null)
                {
                    masterDepotDto.ModifiedBy = (long)Session["userId"];
                    masterDepotDto.IsDeleted = false;

                    using (var client = new HttpClientDemo())
                    {
                        var putTask = client.PostAsJsonAsync<MasterDepotDto>("masterdepot/update", masterDepotDto);
                        putTask.Wait();
                        var result = putTask.Result;
                        var data = result.Content.ReadAsStringAsync().Result;
                        if (result.IsSuccessStatusCode)
                        {
                            TempData["Class"] = Utility.Utility.Success;
                            TempData["Message"] = "Master depot updated successfully.";
                            return RedirectToAction("Index", "MasterDepot");
                        }
                        if (result.StatusCode == HttpStatusCode.Conflict)
                        {
                            TempData["Class"] = Utility.Utility.Error;
                            TempData["Message"] = "This item already exist.";
                            return RedirectToAction("Index", "MasterDepot");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return RedirectToAction("Index", "MasterDepot");
            }
            catch (Exception)
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
                        return RedirectToAction("Index", "MasterDepot");
                    }
                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        TempData["Class"] = Utility.Utility.Error;
                        TempData["Message"] = "This item already exist.";
                        return RedirectToAction("Index", "MasterDepot");
                    }
                }
                TempData["Class"] = Utility.Utility.Error;
                TempData["Message"] = "Something went wrong";
                return RedirectToAction("Index", "MasterDepot");

            }
            catch (Exception exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

    }
}