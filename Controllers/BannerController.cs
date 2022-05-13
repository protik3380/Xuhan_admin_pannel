using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class BannerController : Controller
    {
        // GET: Banner
        public ActionResult Index()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.BannerList = Dropdown.Banners().Where(x => !x.IsDeleted).ToList();
                return View();
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
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
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Banner banner)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (banner.Image != null)
                {
                    var bannerName = banner.Page.Replace(" ", String.Empty) + Utility.Utility.GenerateImageNameFromTimestamp();
                    var filename = bannerName + Path.GetExtension(banner.Image.FileName);
                    string path = Path.Combine(Server.MapPath("~/upload/banner/"), filename);
                    banner.Image.SaveAs(path);
                    banner.ImageUrl = "upload/banner/" + filename;
                    banner.Image = null;
                }
                using (var client = new HttpClientDemo())
                {
                    var postTask = client.PostAsJsonAsync("banner/create", banner);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Banner created successfully.";
                        return RedirectToAction("Index", "Banner");
                    }
                }
                return RedirectToAction("Index", "Banner");
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
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
                Banner banner = new Banner();
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("banner/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Banner>();
                        readTask.Wait();
                        banner = readTask.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                return View(banner);
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public ActionResult Edit(Banner banner)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                long userId = (long)Session["userId"];
                banner.IsDeleted = false;
                bool hasNewImage = banner.Image != null;
                if (hasNewImage)
                {
                    var filePath = Server.MapPath("~/" + banner.ImageUrl);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    banner.ImageUrl = null;

                    var bannerName = banner.Page.Replace(" ", String.Empty) + Utility.Utility.GenerateImageNameFromTimestamp();
                    var filename = bannerName + Path.GetExtension(banner.Image.FileName);
                    string path = Path.Combine(Server.MapPath("~/upload/banner/"), filename);
                    banner.Image.SaveAs(path);
                    banner.ImageUrl = "upload/banner/" + filename;
                    banner.Image = null;
                }

                using (var client = new HttpClientDemo())
                {
                    var putTask = client.PostAsJsonAsync<Banner>("banner/update", banner);
                    putTask.Wait();

                    var result = putTask.Result;
                    var data = result.Content.ReadAsAsync<dynamic>().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Banner updated successfully.";
                        return RedirectToAction("Index", "Banner");
                    }             
                    TempData["Class"] = Utility.Utility.Error;
                    TempData["Message"] = "Something went wrong.";
                    return RedirectToAction("Index", "Banner");
                }
            }
            catch (Exception exception)
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
                    var responseTask = client.GetAsync("banner/edit/" + id);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Banner>();
                        readTask.Wait();
                        var banner = readTask.Result;
                        banner.IsDeleted = true;
                        banner.IsActive = false;
                        var putTask = client.PostAsJsonAsync<Banner>("banner/update", banner);
                        putTask.Wait();
                        var resultUpdate = putTask.Result;
                        if (resultUpdate.IsSuccessStatusCode)
                        {
                            TempData["Class"] = Utility.Utility.Success;
                            TempData["Message"] = "Banner deleted successfully.";
                            return RedirectToAction("Index", "Banner");
                        }
                    }
                }
                return RedirectToAction("Index", "Banner");
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }
    }
}