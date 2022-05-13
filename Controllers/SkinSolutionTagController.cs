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
    public class SkinSolutionTagController : Controller
    {
        // GET: SkinSolutionTag
        public ActionResult Index()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.SkinSolutionTagList = Dropdown.SkinSolutionTags()
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
        public ActionResult Create(SolutionTag skinSolutionTag)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                using (var client = new HttpClientDemo())
                {
                    skinSolutionTag.CreatedBy = (long)Session["userId"];
                    skinSolutionTag.CreatedAt = DateTime.UtcNow.AddHours(6);
                    skinSolutionTag.IsDeleted = false;
                    var postTask = client.PostAsJsonAsync("skinsolutiontag/create", skinSolutionTag);
                    postTask.Wait();
                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        FlashMessage.Confirmation("Skin solution tag created successfully.");
                        return RedirectToAction("Index", "SkinSolutionTag");
                    }
                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        FlashMessage.Warning("This tag is already exist.");
                        return RedirectToAction("Create", "SkinSolutionTag");
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
                SolutionTag solutionTag = null;
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("skinsolutiontag/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<SolutionTag>();
                        readTask.Wait();
                        solutionTag = readTask.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                return View(solutionTag);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(SolutionTag skinSolutionTag)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                if (skinSolutionTag != null)
                {
                    skinSolutionTag.ModifiedBy = (long)Session["userId"];
                    skinSolutionTag.UpdatedAt = DateTime.UtcNow.AddHours(6);
                    skinSolutionTag.IsDeleted = false;

                    using (var client = new HttpClientDemo())
                    {
                        var putTask = client.PostAsJsonAsync<SolutionTag>("skinsolutiontag/update", skinSolutionTag);
                        putTask.Wait();

                        var result = putTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            FlashMessage.Confirmation("Skin solution tag updated successfully.");
                            return RedirectToAction("Index", "SkinSolutionTag");
                        }
                        if (result.StatusCode == HttpStatusCode.Conflict)
                        {
                            FlashMessage.Confirmation("This Skin solution tag already exist.");
                            return RedirectToAction("Index", "SkinSolutionTag");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return RedirectToAction("Index", "SkinSolutionTag");
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
                SolutionTag solutionTag = null;
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("skinsolutiontag/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<SolutionTag>();
                        readTask.Wait();
                        solutionTag = readTask.Result;

                        solutionTag.ModifiedBy = (long)Session["userId"];
                        solutionTag.UpdatedAt = DateTime.UtcNow.AddHours(6);
                        solutionTag.IsDeleted = true;

                        var putTask = client.PostAsJsonAsync<SolutionTag>("skinsolutiontag/update", solutionTag);
                        putTask.Wait();

                        var resultUpdate = putTask.Result;
                        if (resultUpdate.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "SkinSolutionTag");
                        }
                    }
                }
                return RedirectToAction("Index", "SkinSolutionTag");
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }
    }
}