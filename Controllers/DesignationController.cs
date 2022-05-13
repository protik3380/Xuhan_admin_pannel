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
    public class DesignationController : Controller
    {
        // GET: Designation
        public ActionResult Index()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Designations = Dropdown.Designations().OrderBy(x=>x.Name)
                    .Where(x => !x.IsDeleted)
                    .ToList();

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Designation/Create
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(Designation designation)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                using (var client = new HttpClientDemo())
                {
                    designation.CreatedBy = (long)Session["userId"];
                    designation.CreatedOn = DateTime.UtcNow.AddHours(6);
                    designation.IsDeleted = false;

                    var postTask = client.PostAsJsonAsync("designation/create", designation);
                    postTask.Wait();
                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Designation created successfully.";
                        return RedirectToAction("Index", "Designation");
                    }
                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        TempData["Class"] = Utility.Utility.Error;
                        TempData["Message"] = "This Designation is already exist.";
                        return RedirectToAction("Create", "Designation");
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
                Designation designation = null;
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("designation/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Designation>();
                        readTask.Wait();
                        designation = readTask.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                return View(designation);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public ActionResult Edit(Designation designation)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (designation != null)
                {
                    designation.ModifiedBy = (long)Session["userId"];
                    designation.IsDeleted = false;

                    using (var client = new HttpClientDemo())
                    {
                        var putTask = client.PostAsJsonAsync<Designation>("designation/update", designation);
                        putTask.Wait();
                        var result = putTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            TempData["Class"] = Utility.Utility.Success;
                            TempData["Message"] = "Designation updated successfully.";
                            return RedirectToAction("Index", "Designation");
                        }
                        if (result.StatusCode == HttpStatusCode.Conflict)
                        {
                            TempData["Class"] = Utility.Utility.Error;
                            TempData["Message"] = "This Designation is already exist.";
                            return RedirectToAction("Index", "Designation");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return RedirectToAction("Index", "Designation");
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
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                using (var client = new HttpClientDemo())
                {
                    var responseTask = client.GetAsync("designation/edit/" + id);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Designation>();
                        readTask.Wait();
                        var designation = readTask.Result;
                        designation.ModifiedBy = (long)Session["userId"];
                        designation.IsDeleted = true;
                        designation.IsActive = false;
                        var putTask = client.PostAsJsonAsync<Designation>("designation/update", designation);
                        putTask.Wait();
                        var resultUpdate = putTask.Result;
                        if (resultUpdate.IsSuccessStatusCode)
                        {
                            TempData["Class"] = Utility.Utility.Success;
                            TempData["Message"] = "Designation deleted successfully.";
                            return RedirectToAction("Index", "Designation");
                        }
                    }
                }
                return RedirectToAction("Index", "Designation");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
