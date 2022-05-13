using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Vereyon.Web;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Models.RequestDto;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class ProductStorageController : Controller
    {
        // GET: ProductStorage
        public ActionResult Index()
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ProductStorages = Dropdown.ProductStorages()
                .Where(x => !x.IsDeleted)
                .ToList();
            return View();
        }

        public ActionResult Create()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Categories = Dropdown.Categories();
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductStorageDto productStorageDto)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                using (var client = new HttpClientDemo())
                {
                    productStorageDto.CreatedBy = (long)Session["userId"];
                    productStorageDto.IsDeleted = false;
                    productStorageDto.IsActive = true;

                    var postTask = client.PostAsJsonAsync("productStorage/create", productStorageDto);
                    postTask.Wait();
                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        FlashMessage.Confirmation("Product stock in successfully.");
                        return RedirectToAction("Index", "ProductStorage");
                    }
                }
                ViewBag.Categories = Dropdown.Categories();
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Categories = Dropdown.ActiveCategories();
                ProductStorageDto productStorageDto = null;
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("productStorage/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ProductStorageDto>();
                        readTask.Wait();
                        productStorageDto = readTask.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }

                ViewBag.Products = Dropdown.ActiveProducts();
                ViewBag.ProductUnits = Dropdown.ActiveProductUnits();
                var productUnits = Dropdown.ProductUnits()
                    .FirstOrDefault(x => x.Id == productStorageDto.ProductUnitId);
                ViewBag.ProductID = productUnits.ProductId;
                ViewBag.CategoryID = productUnits.Product.CategoryId;
                return View(productStorageDto);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: ProductDiscount/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductStorageDto productStorageDto)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                using (var client = new HttpClientDemo())
                {
                    productStorageDto.IsActive = true;
                    productStorageDto.IsDeleted = false;
                    productStorageDto.ModifiedBy = (long)Session["userId"];

                    var postTask = client.PostAsJsonAsync<ProductStorageDto>("productStorage/update", productStorageDto);
                    postTask.Wait();
                    var result = postTask.Result;
                    var data = result.Content.ReadAsAsync<dynamic>().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        FlashMessage.Confirmation("Product storage Update successfully.");
                        return RedirectToAction("Index", "ProductStorage");
                    }
                    FlashMessage.Danger("Something Went Wrong!!!");
                }
                return RedirectToAction("Index", "ProductStorage");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

    }
}