using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Vereyon.Web;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class DistrictController : Controller
    {
        // GET: District
        public ActionResult Index()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                List<District> districts = Dropdown.Districts()
                    .Where(x => !x.IsDeleted)
                    .OrderBy(x => x.DistrictName)
                    .ToList();
                ViewBag.districts = districts;
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
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public ActionResult Create(District aDistrict)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                using (var client = new HttpClientDemo())
                {
                    aDistrict.CreatedBy = (long)Session["userId"];
                    aDistrict.IsDeleted = false;

                    var postTask = client.PostAsJsonAsync("district/create", aDistrict);
                    postTask.Wait();
                    var result = postTask.Result;
                    var data = result.Content.ReadAsAsync<dynamic>().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "District created successfully.";
                        return RedirectToAction("Index", "District");
                    }
                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        TempData["Class"] = Utility.Utility.Error;
                        TempData["Message"] = "This district is already exist.";
                        return RedirectToAction("Create", "District");
                    }
                }
                return View();
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
                District district = null;
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("district/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<District>();
                        readTask.Wait();
                        district = readTask.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                return View(district);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public ActionResult Edit(District aDistrict)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                if (aDistrict != null)
                {
                    aDistrict.ModifiedBy = (long)Session["userId"];
                    aDistrict.IsDeleted = false;

                    using (var client = new HttpClientDemo())
                    {
                        var putTask = client.PostAsJsonAsync<District>("district/update", aDistrict);
                        putTask.Wait();

                        var result = putTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            TempData["Class"] = Utility.Utility.Success;
                            TempData["Message"] = "District updated successfully.";
                            return RedirectToAction("Index", "District");
                        }
                        if (result.StatusCode == HttpStatusCode.Conflict)
                        {
                            TempData["Class"] = Utility.Utility.Error;
                            TempData["Message"] = "This district already exist.";
                            return RedirectToAction("Index", "District");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return RedirectToAction("Index", "District");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }


        public ActionResult Delete(long id)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("district/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<District>();
                        readTask.Wait();
                        var district = readTask.Result;

                        district.ModifiedBy = (long)Session["userId"];
                        district.IsDeleted = true;
                        district.IsActive = false;

                        var putTask = client.PostAsJsonAsync<District>("district/update", district);
                        putTask.Wait();

                        var resultUpdate = putTask.Result;
                        if (resultUpdate.IsSuccessStatusCode)
                        {
                            TempData["Class"] = Utility.Utility.Success;
                            TempData["Message"] = "District deleted successfully.";
                            return RedirectToAction("Index", "District");
                        }
                    }
                }
                return RedirectToAction("Index", "District");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}