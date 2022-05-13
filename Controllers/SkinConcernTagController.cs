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
    public class SkinConcernTagController : Controller
    {
        // GET: SkinConcernTag
        public ActionResult Index()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.SkinConcernTagList = Dropdown.SkinConcernTags()
                    .Where(x => !x.IsDeleted)
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
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConcernTag skinConcernTag)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                using (var client = new HttpClientDemo())
                {
                    skinConcernTag.CreatedBy = (long)Session["userId"];
                    skinConcernTag.CreatedAt = DateTime.UtcNow.AddHours(6);
                    skinConcernTag.IsDeleted = false;
                    var postTask = client.PostAsJsonAsync("skinconcerntag/create", skinConcernTag);
                    postTask.Wait();
                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        FlashMessage.Confirmation("Skin concern tag created successfully.");
                        return RedirectToAction("Index", "SkinConcernTag");
                    }
                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        FlashMessage.Warning("This tag is already exist.");
                        return RedirectToAction("Create", "SkinConcernTag");
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
                ConcernTag concernTag = null;
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("skinconcerntag/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ConcernTag>();
                        readTask.Wait();
                        concernTag = readTask.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                return View(concernTag);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public ActionResult Edit(ConcernTag skinConcernTag)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                if (skinConcernTag != null)
                {
                    skinConcernTag.ModifiedBy = (long)Session["userId"];
                    skinConcernTag.UpdatedAt = DateTime.UtcNow.AddHours(6);
                    skinConcernTag.IsDeleted = false;

                    using (var client = new HttpClientDemo())
                    {
                        var putTask = client.PostAsJsonAsync<ConcernTag>("skinconcerntag/update", skinConcernTag);
                        putTask.Wait();

                        var result = putTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            FlashMessage.Confirmation("Skin concern tag updated successfully.");
                            return RedirectToAction("Index", "SkinConcernTag");
                        }
                        if (result.StatusCode == HttpStatusCode.Conflict)
                        {
                            FlashMessage.Confirmation("This Skin concern tag already exist.");
                            return RedirectToAction("Index", "SkinConcernTag");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return RedirectToAction("Index", "SkinConcernTag");
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
                ConcernTag concernTag = null;
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("skinconcerntag/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ConcernTag>();
                        readTask.Wait();
                        concernTag = readTask.Result;

                        concernTag.ModifiedBy = (long)Session["userId"];
                        concernTag.UpdatedAt = DateTime.UtcNow.AddHours(6);
                        concernTag.IsDeleted = true;
                        concernTag.IsActive = false;

                        var putTask = client.PostAsJsonAsync<ConcernTag>("skinconcerntag/update", concernTag);
                        putTask.Wait();

                        var resultUpdate = putTask.Result;
                        if (resultUpdate.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "SkinConcernTag");
                        }
                    }
                }
                return RedirectToAction("Index", "SkinConcernTag");
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }
    }
}