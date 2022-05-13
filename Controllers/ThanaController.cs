using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class ThanaController : Controller
    {
        // GET: Thana
        public ActionResult Index()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Thanas = Dropdown.Thanas()
                    .Where(x=>!x.IsDeleted)
                    .OrderBy(x=>x.ThanaName)
                    .ToList();
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
        public ActionResult Create(Thana aThana)
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
                    aThana.CreatedBy = (long)Session["userId"];
                    aThana.IsDeleted = false;

                    var postTask = client.PostAsJsonAsync("thana/create", aThana);
                    postTask.Wait();
                    var result = postTask.Result;
                    var data = result.Content.ReadAsAsync<dynamic>().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Thana created successfully.";
                        return RedirectToAction("Index", "Thana");
                    }
                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        TempData["Class"] = Utility.Utility.Error;
                        TempData["Message"] = "This Thana is already exist.";
                        return RedirectToAction("Create", "Thana");
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
                ViewBag.Districts = Dropdown.ActiveDistricts();
                Thana thana = null;
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("thana/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Thana>();
                        readTask.Wait();
                        thana = readTask.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                return View(thana);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(Thana aThana)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Districts = Dropdown.ActiveDistricts();
                if (aThana != null)
                {
                    aThana.ModifiedBy = (long)Session["userId"];
                    aThana.IsDeleted = false;

                    using (var client = new HttpClientDemo())
                    {
                        var putTask = client.PostAsJsonAsync<Thana>("thana/update", aThana);
                        putTask.Wait();

                        var result = putTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            TempData["Class"] = Utility.Utility.Success;
                            TempData["Message"] = "Thana updated successfully.";
                            return RedirectToAction("Index", "Thana");
                        }
                        if (result.StatusCode == HttpStatusCode.Conflict)
                        {
                            TempData["Class"] = Utility.Utility.Error;
                            TempData["Message"] = "This Thana already exist.";
                            return RedirectToAction("Index", "Thana");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return RedirectToAction("Index", "Thana");
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

                    var responseTask = client.GetAsync("thana/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Thana>();
                        readTask.Wait();
                        var thana = readTask.Result;

                        thana.ModifiedBy = (long)Session["userId"];
                        thana.IsDeleted = true;
                        thana.IsActive = false;

                        var putTask = client.PostAsJsonAsync<Thana>("thana/update", thana);
                        putTask.Wait();

                        var resultUpdate = putTask.Result;
                        var data1 = resultUpdate.Content.ReadAsAsync<dynamic>().Result;
                        if (resultUpdate.IsSuccessStatusCode)
                        {
                            TempData["Class"] = Utility.Utility.Success;
                            TempData["Message"] = "thana deleted successfully.";
                            return RedirectToAction("Index", "Thana");
                        }
                    }
                }
                return RedirectToAction("Index", "Thana");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}