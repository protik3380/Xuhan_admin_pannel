using System;
using System.Collections.Generic;
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
    public class DistributorController : Controller
    {
        // GET: Distributor
        public ActionResult Index()
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                ViewBag.DistributorList = Dropdown.Distributors()
                    .Where(x => !x.IsDeleted)
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
                ViewBag.Districts = Dropdown.ActiveDistricts();
                ViewBag.MasterDeports = Dropdown.ActiveMasterDepots();
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public ActionResult Create(DistributorDto distributor)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.Districts = Dropdown.ActiveDistricts();
                ViewBag.MasterDeports = Dropdown.ActiveMasterDepots();

                using (var client = new HttpClientDemo())
                {
                    distributor.CreatedBy = (long)Session["userId"];
                    distributor.IsDeleted = false;

                    var postTask = client.PostAsJsonAsync("distributor/create", distributor);
                    postTask.Wait();
                    var result = postTask.Result;
                    var data = result.Content.ReadAsAsync<dynamic>().Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Class"] = Utility.Utility.Success;
                        TempData["Message"] = "Distributor created successfully.";
                        return RedirectToAction("Index", "Distributor");
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
                Distributor distributor = new Distributor();
                using (var client = new HttpClientDemo())
                {

                    var responseTask = client.GetAsync("distributor/edit/" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Distributor>();
                        readTask.Wait();
                        distributor = readTask.Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }

                DistributorDto distributorDto = new DistributorDto
                {
                    Id = distributor.Id,
                    Name = distributor.Name,
                    ThanaId = distributor.ThanaId,
                    Address = distributor.Address,
                    ContactPerson = distributor.ContactPerson,
                    Phone = distributor.Phone,
                    Email = distributor.Email,
                    IsDeleted = distributor.IsDeleted,
                    IsActive = distributor.IsActive,
                    DistrictId = distributor.Thana.DistrictId,
                    MasterDepotId = distributor.MasterDepotId
                };

                ViewBag.Districts = Dropdown.ActiveDistricts();
                ViewBag.MasterDeports = Dropdown.ActiveMasterDepots();

                return View(distributorDto);
            }
            catch (Exception exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public ActionResult Edit(DistributorDto distributor)
        {
            try
            {
                if (Session["email"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }

                if (distributor != null)
                {
                    distributor.ModifiedBy = (long)Session["userId"];
                    distributor.IsDeleted = false;

                    using (var client = new HttpClientDemo())
                    {
                        var putTask = client.PostAsJsonAsync<DistributorDto>("distributor/update", distributor);
                        putTask.Wait();

                        var result = putTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            TempData["Class"] = Utility.Utility.Success;
                            TempData["Message"] = "Distributor updated successfully.";
                            return RedirectToAction("Index", "Distributor");
                        }
                        if (result.StatusCode == HttpStatusCode.Conflict)
                        {
                            TempData["Class"] = Utility.Utility.Error;
                            TempData["Message"] = "This item already exist.";
                            return RedirectToAction("Index", "Distributor");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                return RedirectToAction("Index", "Distributor");
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}