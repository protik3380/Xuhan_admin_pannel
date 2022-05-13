using System;
using System.Collections.Generic;
using System.IO;
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
    public class ProductUnitController : Controller
    {
        // GET: ProductUnit
        public ActionResult Index()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                       
                ViewBag.ProductUnits = Dropdown.ProductUnits()/*.OrderBy(x=>x.Product.Name)*/
                    .Where(x => !x.IsDeleted)
                    .ToList();
                return View();
            }
            catch (Exception exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }


        // GET: ProductUnit/Create
        public ActionResult Create()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Products = Dropdown.ActiveProducts();
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: ProductUnit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductUnitDto productUnitvm)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                long userId = (long)Session["userId"];
                productUnitvm.IsDeleted = false;
                productUnitvm.CreatedBy = userId;
                productUnitvm.ProductImages = new List<ProductImageDto>();

                var count = 1;
                foreach (var pic in productUnitvm.ProductImagesForm)
                {
                    if (pic.ImageLocation != null)
                    {
                        var imageName = productUnitvm.ProductId+productUnitvm.StockKeepingUnit+count;
                        var imageNameTrim = imageName.Replace("/", String.Empty)
                            .Replace(@"\", String.Empty)
                            .Replace("-", String.Empty)
                            .Replace("+", String.Empty)
                            .Replace(" ", String.Empty);
                                                           
                        var filename = imageNameTrim + Path.GetExtension(pic.ImageLocation.FileName);
                        string path = Path.Combine(Server.MapPath("~/upload/product/"), filename);
                        pic.ImageLocation.SaveAs(path);
                        pic.ImageUrl = "upload/product/" + filename;
                        pic.IsDeleted = false;
                        pic.CreatedBy = userId;
                        pic.ImageLocation = null;

                        var pImages = new ProductImageDto();
                        pImages.ImageUrl = pic.ImageUrl;
                        pImages.IsDeleted = false;
                        pImages.CreatedBy = userId;
                        pImages.ImageLocation = null;

                        productUnitvm.ProductImages.Add(pImages);
                        count++;
                    }
                }
                using (var client = new HttpClientDemo())
                {
                    var postTask = client.PostAsJsonAsync("productUnit/create", productUnitvm);
                    postTask.Wait();
                    var result = postTask.Result;
                    var data = result.Content.ReadAsAsync<dynamic>().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Product details has been saved successfully.";
                        return RedirectToAction("Index", "ProductUnit");
                    }
                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        TempData["Class"] = Utility.Utility.Error;
                        TempData["Message"] = "This barcode is already exist.";
                        return RedirectToAction("Create", "ProductUnit");
                    }
                }
                TempData["Class"] = Utility.Utility.Error;
                TempData["Message"] = "Failed to save product details.";

                ViewBag.Products = Dropdown.ActiveProducts();
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: ProductUnit/Edit/5
        public ActionResult Edit(long id)
        {

            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Products = Dropdown.ActiveProducts();
                ProductUnitDto productDetails = null;
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("productUnit/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ProductUnitDto>();
                        readTask.Wait();
                        productDetails = readTask.Result;
                    }
                    else
                    {
                        productDetails = new ProductUnitDto();
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }

                var existingImage = productDetails.ProductImages.Count();
                for (int i = existingImage; i < 5; i++)
                {
                    productDetails.ProductImages.Insert(i, new ProductImageDto());
                }
                return View(productDetails);
            }
            catch (Exception exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductUnitDto productUnitvm)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                long userId = (long)Session["userId"];
                productUnitvm.IsDeleted = false;
                productUnitvm.ModifiedBy = userId;
                bool hasNewImage = productUnitvm.ProductImagesForm.Any(x => x.ImageLocation != null);
                if (hasNewImage)
                {
                    if (productUnitvm.ProductImages!=null)
                    {
                        foreach (var pic in productUnitvm.ProductImages)
                        {
                            var filePath = Server.MapPath("~/" + pic.ImageUrl);
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                        }
                    }
                    
                    productUnitvm.ProductImages = new List<ProductImageDto>();
                }
                var count = 1;
                foreach (var pic in productUnitvm.ProductImagesForm)
                {
                    if (pic.ImageLocation != null)
                    {
                        var imageName = productUnitvm.ProductId + productUnitvm.StockKeepingUnit + count;
                        var imageNameTrim = imageName.Replace("/", String.Empty)
                            .Replace(@"\", String.Empty)
                            .Replace("-", String.Empty)
                            .Replace("+", String.Empty)
                            .Replace(" ", String.Empty);
                        var filename = imageNameTrim + Path.GetExtension(pic.ImageLocation.FileName);
                        string path = Path.Combine(Server.MapPath("~/upload/product/"), filename);
                        pic.ImageLocation.SaveAs(path);
                        pic.ImageUrl = "upload/product/" + filename;
                        pic.IsDeleted = false;
                        pic.CreatedBy = userId;
                        pic.ImageLocation = null;

                        var pImages = new ProductImageDto();
                        pImages.ImageUrl = pic.ImageUrl;
                        pImages.IsDeleted = false;
                        pImages.ModifiedBy = userId;
                        pImages.CreatedBy = userId;
                        pImages.ImageLocation = null;

                        productUnitvm.ProductImages.Add(pImages);
                        count++;
                    }
                }

                using (var client = new HttpClientDemo())
                {
                    var putTask = client.PostAsJsonAsync<ProductUnitDto>("productUnit/update", productUnitvm);
                    putTask.Wait();

                    var result = putTask.Result;
                    var data = result.Content.ReadAsAsync<dynamic>().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Product details updated successfully.";
                        return RedirectToAction("Index", "ProductUnit");
                    }
                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        TempData["Class"] = Utility.Utility.Error;
                        TempData["Message"] = "This barcode is already exist.";
                        return RedirectToAction("Index", "ProductUnit");
                    }
                    TempData["Class"] = Utility.Utility.Error;
                    TempData["Message"] = "Something went wrong.";
                    return RedirectToAction("Index", "ProductUnit");
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

                ProductUnitDto productDetails = new ProductUnitDto();
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("productUnit/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ProductUnitDto>();
                        readTask.Wait();
                        productDetails = readTask.Result;

                        long userId = (long)Session["userId"];
                        productDetails.ModifiedBy = userId;
                        productDetails.IsDeleted = true;
                        productDetails.IsActive = false;
                        var putTask = client.PostAsJsonAsync<ProductUnitDto>("productUnit/update", productDetails);
                        putTask.Wait();

                        var resultUpdate = putTask.Result;
                        var data = resultUpdate.Content.ReadAsAsync<dynamic>().Result;
                        if (resultUpdate.IsSuccessStatusCode)
                        {
                            TempData["Class"] = Utility.Utility.Success;
                            TempData["Message"] = "Product details deleted successfully.";
                            return RedirectToAction("Index", "ProductUnit");
                        }
                    }
                    
                    TempData["Class"] = Utility.Utility.Error;
                    TempData["Message"] = "Something went wrong.";
                    return RedirectToAction("Index", "ProductUnit");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public ActionResult GetProductUnitDetails(long id)
        {
            try
            {

                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ProductUnitDto productDetails = null;
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("productUnit/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ProductUnitDto>();
                        readTask.Wait();
                        productDetails = readTask.Result;
                        return PartialView(productDetails);
                    }
                    return RedirectToAction("Index", "ProductUnit");
                }
            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

    }
}
