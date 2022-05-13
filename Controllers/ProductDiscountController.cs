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
    public class ProductDiscountController : Controller
    {
        // GET: ProductDiscount
        public ActionResult Index()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.ProductDiscounts = Dropdown.ProductDiscounts()
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
                ViewBag.Products = Dropdown.ActiveProducts();
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult Create(ProductDiscountDto productDiscount)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Categories = Dropdown.ActiveCategories();

                using (var client = new HttpClientDemo())
                {

                    var discount = GetByProductUnitId(productDiscount.ProductUnitId);
                    if (discount != null)
                    {
                        ProductDiscountDto previousDiscount = new ProductDiscountDto
                        {
                            Id = discount.Id,
                            DiscountPercentage = discount.DiscountPercentage,
                            CreatedAt = discount.CreatedAt,
                            IsDeleted = discount.IsDeleted,
                            IsActive = discount.IsActive,
                            ActivateDate = discount.ActivateDate,
                            EndValidityDate = discount.EndValidityDate,
                            ProductUnitId = discount.ProductUnitId,
                            CreatedBy = discount.CreatedBy
                        };
                        previousDiscount.IsActive = false;
                        previousDiscount.IsDeleted = true;
                        previousDiscount.ModifiedBy = (long)Session["userId"];

                        var putTask = client.PostAsJsonAsync<ProductDiscountDto>("productDiscount/update", previousDiscount);
                        putTask.Wait();
                        var resultUp = putTask.Result;
                    }

                    productDiscount.CreatedAt = DateTime.UtcNow.AddHours(6);
                    productDiscount.IsActive = true;
                    productDiscount.IsDeleted = false;
                    productDiscount.CreatedBy = (long)Session["userId"];

                    var postTask = client.PostAsJsonAsync("productDiscount/create", productDiscount);
                    postTask.Wait();
                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        FlashMessage.Confirmation("Product discount created successfully.");
                        return RedirectToAction("Index", "ProductDiscount");
                    }

                }
                return View();
            }
            catch (Exception exception)
            {
                return RedirectToAction("Error", "Home");
            }


        }

        private ProductDiscount GetByProductUnitId(long id)
        {
            ProductDiscount productDis = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("productDiscount/getall/productUnitId/" + id);
                responseTask.Wait();
                var result = responseTask.Result;
                IEnumerable<ProductDiscount> productDiscounts = null;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductDiscount>>();
                    readTask.Wait();
                    productDiscounts = readTask.Result;
                    productDis = productDiscounts.ToList().FirstOrDefault(x => x.IsDeleted == false);
                }
                else
                {
                    productDiscounts = new List<ProductDiscount>();
                }
            }

            return productDis;
        }

        // GET: ProductDiscount/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Categories = Dropdown.ActiveCategories();
                ProductDiscountDto productDiscountDto = null;
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("productDiscount/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ProductDiscountDto>();
                        readTask.Wait();
                        productDiscountDto = readTask.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }

                ViewBag.Products = Dropdown.ActiveProducts();
                ViewBag.ProductUnits = Dropdown.ActiveProductUnits();
                var productUnits = Dropdown.ProductUnits()
                    .FirstOrDefault(x => x.Id == productDiscountDto.ProductUnitId);
                ViewBag.ProductID = productUnits.ProductId;;
                return View(productDiscountDto);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: ProductDiscount/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductDiscountDto productDiscount)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                using (var client = new HttpClientDemo())
                {
                    productDiscount.IsActive = true;
                    productDiscount.IsDeleted = false;
                    productDiscount.ModifiedBy = (long)Session["userId"];

                    var postTask = client.PostAsJsonAsync<ProductDiscountDto>("productDiscount/update", productDiscount);
                    postTask.Wait();
                    var result = postTask.Result;
                    var data = result.Content.ReadAsAsync<dynamic>().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        FlashMessage.Confirmation("Product discount Update successfully.");
                        return RedirectToAction("Index", "ProductDiscount");
                    }
                    FlashMessage.Danger("Something Went Wrong!!!");
                }
                return RedirectToAction("Index", "ProductDiscount");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }


    }
}
