using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using xtbdEcommerceAdminPanel.Models.RequestDto;
using xtbdEcommerceAdminPanel.Models.View_Model;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class UseProcessImageController : Controller
    {
        // GET: UseProcessImage
        public ActionResult Index()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                var useProcessImages = Dropdown.UseProcessImages();
                ViewBag.useProcessImages = useProcessImages.Where(x => x.UseProcessImage.Count > 0).ToList();

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

        [HttpGet]
        public ActionResult GetImage(long id)
        {
            try
            {

                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                var userProcessImages = Dropdown.UseProcessImages()
                    .FirstOrDefault(x => x.ProductUnitId == id && x.UseProcessImage.Count > 0);
                if (userProcessImages != null)
                {
                    return PartialView(userProcessImages);
                }

                return RedirectToAction("Index", "UseProcessImage");

            }
            catch (Exception ex)
            {
                Session["ex"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult Create(UseProcessDto userProcessDto)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                long userId = (long)Session["userId"];
                userProcessDto.UseProcessImages = new List<UseProcessImageDto>();
                var count = 1;
                foreach (var pic in userProcessDto.UseProcessImagesForm)
                {
                    if (pic.ImageLocation != null)
                    {
                        var imageName = userProcessDto.ProductName + count;
                        var imageNameTrim = String.Concat(imageName.Where(c => !Char.IsWhiteSpace(c)));

                        var filename = imageNameTrim + Path.GetExtension(pic.ImageLocation.FileName);
                        string path = Path.Combine(Server.MapPath("~/upload/userprocess/"), filename);
                        pic.ImageLocation.SaveAs(path);
                        pic.ImageUrl = "upload/userprocess/" + filename;
                        pic.IsDeleted = false;
                        pic.IsActive = true;
                        pic.CreatedBy = userId;
                        pic.ProductUnitId = userProcessDto.ProductUnitId;
                        pic.ImageLocation = null;

                        var pImages = new UseProcessImageDto();
                        pImages.ImageUrl = pic.ImageUrl;
                        pImages.IsDeleted = false;
                        pImages.CreatedBy = userId;
                        pImages.IsActive = true;
                        pImages.ProductUnitId = userProcessDto.ProductUnitId;
                        pImages.ImageLocation = null;

                        userProcessDto.UseProcessImages.Add(pImages);
                        count++;
                    }
                }
                using (var client = new HttpClientDemo())
                {
                    var postTask = client.PostAsJsonAsync("UseProcess/create", userProcessDto);
                    postTask.Wait();
                    var result = postTask.Result;
                    var data = result.Content.ReadAsAsync<dynamic>().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Use process image has been saved successfully.";
                        return RedirectToAction("Index", "UseProcessImage");
                    }
                    
                }
                TempData["Class"] = Utility.Utility.Error;
                TempData["Message"] = "Failed to save use process image";
                ViewBag.Products = Dropdown.ActiveProducts();
                return View();
            }
            catch (Exception exception)
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
                ViewBag.Products = Dropdown.ActiveProducts();
                UseProcessVM productDetails = Dropdown.UseProcessImages()
                    .FirstOrDefault(x => x.ProductUnitId == id && x.UseProcessImage.Count > 0);

                if (productDetails != null)
                {
                    var existingImage = productDetails.UseProcessImage.Count();
                    for (int i = existingImage; i < 8; i++)
                    {
                        productDetails.UseProcessImage.Insert(i, new UseProcessImageVM());
                    }
                }
                return View(productDetails);
            }
            catch (Exception exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }


        [HttpPost]
        public ActionResult Edit(UseProcessVM userUseProcessVm)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                long userId = (long)Session["userId"];
                bool hasNewImage = userUseProcessVm.UseProcessImagesForm.Any(x => x.ImageLocation != null);
                if (hasNewImage)
                {
                    if (userUseProcessVm.UseProcessImage != null)
                    {
                        foreach (var pic in userUseProcessVm.UseProcessImage)
                        {
                            var filePath = Server.MapPath("~/" + pic.ImageUrl);
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                        }
                    }

                    userUseProcessVm.UseProcessImages = new List<UseProcessImageVM>();
                }
                var count = 1;
                foreach (var pic in userUseProcessVm.UseProcessImagesForm)
                {
                    if (pic.ImageLocation != null)
                    {
                        var imageName = userUseProcessVm.ProductName + count;
                        var imageNameTrim = String.Concat(imageName.Where(c => !Char.IsWhiteSpace(c)));

                        var filename = imageNameTrim + Path.GetExtension(pic.ImageLocation.FileName);
                        string path = Path.Combine(Server.MapPath("~/upload/userprocess/"), filename);
                        pic.ImageLocation.SaveAs(path);
                        pic.ImageUrl = "upload/userprocess/" + filename;
                        pic.IsDeleted = false;
                        pic.IsActive = true;
                        pic.CreatedBy = userId;
                        pic.ModifiedBy = userId;
                        pic.ProductUnitId = userUseProcessVm.ProductUnitId;
                        pic.ImageLocation = null;

                        var pImages = new UseProcessImageVM();
                        pImages.ImageUrl = pic.ImageUrl;
                        pImages.IsDeleted = false;
                        pImages.CreatedBy = userId;
                        pImages.ModifiedBy = userId;
                        pImages.IsActive = true;
                        pImages.ProductUnitId = userUseProcessVm.ProductUnitId;
                        pImages.ImageLocation = null;

                        userUseProcessVm.UseProcessImages.Add(pImages);
                        count++;
                    }
                }

                using (var client = new HttpClientDemo())
                {
                    string url = "UseProcess/update/" + userUseProcessVm.ProductUnitId;
                    var putTask = client.PostAsJsonAsync<UseProcessVM>(url, userUseProcessVm);
                    putTask.Wait();

                    var result = putTask.Result;
                    var data = result.Content.ReadAsAsync<dynamic>().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "User process image updated successfully.";
                        return RedirectToAction("Index", "UseProcessImage");
                    }
                   
                    TempData["Class"] = Utility.Utility.Error;
                    TempData["Message"] = "Something went wrong.";
                    return RedirectToAction("Index", "UseProcessImage");
                }
            }
            catch (Exception exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}