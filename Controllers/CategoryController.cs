using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class CategoryController : Controller
    {
       
        // GET: Category
        public ActionResult Index()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                IEnumerable<Category> categories;
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("category/getall");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Category>>();
                        readTask.Wait();
                        categories = readTask.Result;
                    }
                    else
                    {
                        categories = new List<Category>();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                Session["activeClass"] = "Setup";
                ViewBag.categories = categories.Where(x => !x.IsDeleted)
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
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                if (category.ImageFile != null)
                {
                    var categoryName = category.Name.Replace(" ", String.Empty);
                    var filename = categoryName + Path.GetExtension(category.ImageFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/upload/category/"), filename);
                    category.ImageFile.SaveAs(path);
                    category.Image = "upload/category/" + filename;
                    category.ImageFile = null;
                }

                using (var client = new Utility.HttpClientDemo())
                {
                    category.CreatedBy = (long)Session["userId"];
                    var postTask = client.PostAsJsonAsync("category/create", category);

                    postTask.Wait();
                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Category created successfully.";

                        return RedirectToAction("Index", "Category");
                    }

                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        TempData["Class"] = Utility.Utility.Error;
                        TempData["Message"] = "This Category already exist.";
                        return RedirectToAction("Create", "Category");
                    }
                }
                return RedirectToAction("Index", "Category");
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
                Category aCategory = new Category();
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("category/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Category>();
                        readTask.Wait();
                        aCategory = readTask.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                return View(aCategory);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                if (category != null)
                {
                    category.ModifiedBy = (long)Session["userId"];
                    category.UpdatedAt = DateTime.UtcNow.AddHours(6);
                    category.IsDeleted = false;

                    if (category.ImageFile != null)
                    {
                        var categoryName = category.Name.Replace(" ", String.Empty);
                        var filename = categoryName + Path.GetExtension(category.ImageFile.FileName);
                        string path = Path.Combine(Server.MapPath("~/upload/category/"), filename);
                        category.ImageFile.SaveAs(path);
                        category.Image = "upload/category/" + filename;
                        category.ImageFile = null;
                    }

                    using (var client = new HttpClientDemo())
                    {

                        var putTask = client.PostAsJsonAsync<Category>("category/update", category);
                        putTask.Wait();

                        var result = putTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            TempData["Class"] = Utility.Utility.Success;
                            TempData["Message"] = "Category updated successfully.";
                            return RedirectToAction("Index", "Category");
                        }
                        if (result.StatusCode == HttpStatusCode.Conflict)
                        {
                            TempData["Class"] = Utility.Utility.Error;
                            TempData["Message"] = "This Category already exist.";
                            return RedirectToAction("Index", "Category");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return RedirectToAction("Index", "Category");
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

                    var responseTask = client.GetAsync("category/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Category>();
                        readTask.Wait();
                        var category = readTask.Result;

                        category.ModifiedBy = (long)Session["userId"];
                        category.UpdatedAt = DateTime.UtcNow.AddHours(6);
                        category.IsDeleted = true;
                        category.IsActive = false;

                        var putTask = client.PostAsJsonAsync<Category>("category/update", category);
                        putTask.Wait();

                        var resultUpdate = putTask.Result;
                        if (resultUpdate.IsSuccessStatusCode)
                        {
                            TempData["Class"] = Utility.Utility.Success;
                            TempData["Message"] = "Category deleted successfully.";
                            return RedirectToAction("Index", "Category");
                        }
                    }
                }
                return RedirectToAction("Index", "Category");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}