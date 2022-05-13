using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Models.RequestDto;
using xtbdEcommerceAdminPanel.Utility;


namespace xtbdEcommerceAdminPanel.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand
        public ActionResult Index()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                ViewBag.Brands = Dropdown.Brands()
                    .Where(x => !x.IsDeleted)
                    .OrderBy(x => x.Name).ToList();

                Session["activeClass"] = "Setup";
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Create
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
        public ActionResult Create(BrandDto aBrand)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (aBrand.BrandImage != null)
                {
                    var brandName = aBrand.Name.Replace(" ", String.Empty);
                    var filename = brandName + Path.GetExtension(aBrand.BrandImage.FileName);
                    string path = Path.Combine(Server.MapPath("~/upload/brands/"), filename);
                    aBrand.BrandImage.SaveAs(path);
                    aBrand.Logo = "upload/brands/" + filename;
                    aBrand.BrandImage = null;
                }

                using (var client = new Utility.HttpClientDemo())
                {
                    aBrand.CreatedBy = (long)Session["userId"];
                    var postTask = client.PostAsJsonAsync("brand/create", aBrand);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Brand created successfully.";
                        return RedirectToAction("Index", "Brand");
                    }
                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        TempData["Class"] = Utility.Utility.Error;
                        TempData["Message"] = "This brand is already exist.";
                        return RedirectToAction("Create", "Brand");
                    }
                }
                return RedirectToAction("Index", "Brand");
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }

        // POST: EDIT
        public ActionResult Edit(long id)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                BrandDto aBrand = new BrandDto();
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("brand/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<BrandDto>();
                        readTask.Wait();
                        aBrand = readTask.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                return View(aBrand);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public ActionResult Edit(Brand brand)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                if (brand != null)
                {
                    brand.ModifiedBy = (long)Session["userId"];
                    brand.UpdatedAt = DateTime.UtcNow.AddHours(6);
                    brand.IsDeleted = false;

                    if (brand.BrandImage != null)
                    {
                        var brandName = brand.Name.Replace(" ", String.Empty);
                        var filename = brandName + Path.GetExtension(brand.BrandImage.FileName);
                        string path = Path.Combine(Server.MapPath("~/upload/brands/"), filename);
                        brand.BrandImage.SaveAs(path);
                        brand.Logo = "upload/brands/" + filename;
                        brand.BrandImage = null;
                    }

                    using (var client = new HttpClientDemo())
                    {

                        var putTask = client.PostAsJsonAsync<Brand>("brand/update", brand);
                        putTask.Wait();

                        var result = putTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            TempData["Class"] = Utility.Utility.Success;
                            TempData["Message"] = "Brand updated successfully.";
                            return RedirectToAction("Index", "Brand");
                        }
                        if (result.StatusCode == HttpStatusCode.Conflict)
                        {
                            TempData["Class"] = Utility.Utility.Error;
                            TempData["Message"] = "This brand already exist.";
                            return RedirectToAction("Index", "Brand");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return RedirectToAction("Index", "Brand");
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

                    var responseTask = client.GetAsync("brand/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Brand>();
                        readTask.Wait();
                        var brand = readTask.Result;

                        brand.ModifiedBy = (long)Session["userId"];
                        brand.UpdatedAt = DateTime.UtcNow.AddHours(6);
                        brand.IsDeleted = true;
                        brand.IsActive = false;

                        var putTask = client.PostAsJsonAsync<Brand>("brand/update", brand);
                        putTask.Wait();

                        var resultUpdate = putTask.Result;
                        if (resultUpdate.IsSuccessStatusCode)
                        {
                            TempData["Class"] = Utility.Utility.Success;
                            TempData["Message"] = "Brand deleted successfully.";
                            return RedirectToAction("Index", "Brand");
                        }
                    }
                }
                return RedirectToAction("Index", "Brand");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}