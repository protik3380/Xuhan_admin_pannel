using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using Vereyon.Web;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Models.RequestDto;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {

            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Products = Dropdown.Products().OrderBy(x => x.CreatedAt)
                    .Where(x => !x.IsDeleted)
                    .ToList();

                return View(ViewBag.Products);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Product
        public ActionResult Create()
        {

            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Categories = Dropdown.ActiveCategories();
                ViewBag.Brands = Dropdown.ActiveBrands();
                ViewBag.ConcernTags = Dropdown.ActiveSkinConcernTags();
                ViewBag.SolutionTags = Dropdown.ActiveSkinSolutionTags();

                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductDto product)
        {
            try
            {
                ViewBag.Categories = Dropdown.ActiveCategories();
                ViewBag.Brands = Dropdown.ActiveBrands();
                ViewBag.ConcernTags = Dropdown.ActiveSkinConcernTags();
                ViewBag.SolutionTags = Dropdown.ActiveSkinSolutionTags();

                using (var client = new Utility.HttpClientDemo())
                {
                    product.CreatedBy = (long)Session["userId"];
                    product.CreatedAt = DateTime.UtcNow.AddHours(6);
                    product.IsDeleted = false;
                    var postTask = client.PostAsJsonAsync("product/create", product);
                    postTask.Wait();
                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        FlashMessage.Confirmation("Product created successfully.");
                        return RedirectToAction("Index", "Product");
                    }
                    if (result.StatusCode == HttpStatusCode.Conflict)
                    {
                        FlashMessage.Warning("This product is already exist.");
                        return RedirectToAction("Create", "Product");
                    }
                }

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }


        // GET: Product
        public ActionResult Edit(long id)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                Product product = new Product();
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("product/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Product>();
                        readTask.Wait();
                        product = readTask.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }

                ProductDto productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    BrandId = product.BrandId,
                    CategoryId = product.CategoryId,
                    IsDeleted = product.IsDeleted,
                    IsActive = product.IsActive,
                    CreatedAt = product.CreatedAt,
                    CreatedBy = product.CreatedBy,
                    UpdatedAt = product.UpdatedAt,
                    ModifiedBy = product.ModifiedBy,
                    Description = product.Description,
                    Ingredients = product.Ingredients,
                    UseProcess = product.UseProcess,
                    VideoLink = product.VideoLink

                };
                var concernTagIds = new List<long>();
                var solutionTagIds = new List<long>();
                foreach (var concernTag in product.ConcernTags)
                {
                    long concernTagId = concernTag.Id;
                    concernTagIds.Add(concernTagId);
                }

                productDto.ConcernTagId = concernTagIds;
                foreach (var solutionTag in product.SolutionTags)
                {
                    long tagId = solutionTag.Id;
                    solutionTagIds.Add(tagId);
                }
                productDto.SolutionTagId = solutionTagIds;

                ViewBag.Categories = Dropdown.ActiveCategories();
                ViewBag.Brands = Dropdown.ActiveBrands();
                ViewBag.ConcernTags = Dropdown.ActiveSkinConcernTags();
                ViewBag.SolutionTags = Dropdown.ActiveSkinSolutionTags();

                return View(productDto);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductDto product)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                if (product != null)
                {
                    product.ModifiedBy = (long)Session["userId"];
                    product.UpdatedAt = DateTime.UtcNow.AddHours(6);
                    product.IsDeleted = false;

                    using (var client = new HttpClientDemo())
                    {
                        var putTask = client.PostAsJsonAsync<ProductDto>("product/update", product);
                        putTask.Wait();

                        var result = putTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            FlashMessage.Confirmation("Product updated successfully.");
                            return RedirectToAction("Index", "Product");
                        }
                        if (result.StatusCode == HttpStatusCode.Conflict)
                        {
                            FlashMessage.Confirmation("Product already exist.");
                            return RedirectToAction("Index", "Product");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return RedirectToAction("Index", "Product");
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

                    var responseTask = client.GetAsync("product/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Product>();
                        readTask.Wait();
                        var product = readTask.Result;

                        ProductDto productDto = new ProductDto
                        {
                            Id = product.Id,
                            Name = product.Name,
                            BrandId = product.BrandId,
                            CategoryId = product.CategoryId,
                            IsDeleted = product.IsDeleted,
                            IsActive = product.IsActive,
                            CreatedAt = product.CreatedAt,
                            CreatedBy = product.CreatedBy,
                            UpdatedAt = product.UpdatedAt,
                            ModifiedBy = product.ModifiedBy,
                            Description = product.Description,
                            Ingredients = product.Ingredients,
                            UseProcess = product.UseProcess
                        };
                        var concernTagIds = new List<long>();
                        var solutionTagIds = new List<long>();
                        foreach (var concernTag in product.ConcernTags)
                        {
                            long concernTagId = concernTag.Id;
                            concernTagIds.Add(concernTagId);
                        }

                        productDto.ConcernTagId = concernTagIds;
                        foreach (var solutionTag in product.SolutionTags)
                        {
                            long tagId = solutionTag.Id;
                            solutionTagIds.Add(tagId);
                        }
                        productDto.SolutionTagId = solutionTagIds;


                        productDto.ModifiedBy = (long)Session["userId"];
                        productDto.UpdatedAt = DateTime.UtcNow.AddHours(6);
                        productDto.IsDeleted = true;
                        productDto.IsActive = false;

                        var putTask = client.PostAsJsonAsync<ProductDto>("product/update", productDto);
                        putTask.Wait();

                        var resultUpdate = putTask.Result;
                        if (resultUpdate.IsSuccessStatusCode)
                        {
                            FlashMessage.Confirmation("Product deleted successfully.");                            
                            return RedirectToAction("Index", "Product");
                        }
                    }
                }
                return RedirectToAction("Index", "Product");
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home");
            }
        }
    }
}