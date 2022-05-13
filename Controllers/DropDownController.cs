using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Controllers
{
    public class DropDownController : Controller
    {

        public JsonResult GetProductUnitsByProduct(long? id)
        {
            IEnumerable<ProductUnit> productUnits = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("productUnit/getall/productId/"+id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductUnit>>();
                    readTask.Wait();
                    productUnits = readTask.Result;
                }
                else
                {
                    productUnits = new List<ProductUnit>();
                }
            }        
            return Json(productUnits.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductsByCategory(long categoryId)
        {
            IEnumerable<Product> products = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("product/getall/categoryId/"+categoryId);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Product>>();
                    readTask.Wait();
                    products = readTask.Result;
                }
                else
                {
                    products = new List<Product>();
                }
            }
            return Json(products.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDiscountByProductUnit(long id)
        {
            ProductDiscount productDis = new ProductDiscount();
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("productDiscount/getall/productUnitId/"+id);
                responseTask.Wait();
                var result = responseTask.Result;
                IEnumerable<ProductDiscount> productDiscounts = null;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductDiscount>>();
                    readTask.Wait();
                    productDiscounts = readTask.Result;
                    var enumerable = productDiscounts.ToList();
                    if (enumerable.Any())
                    {
                        productDis = enumerable.FirstOrDefault(x => x.IsDeleted==false);
                    }                    
                }
                else
                {
                    productDiscounts = new List<ProductDiscount>();
                }
            }
            return Json(productDis, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetThanaByDistrict(long id)
        {
            IEnumerable<Thana> thanas = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("thana/getByDistrictId/" + id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Thana>>();
                    readTask.Wait();
                    thanas = readTask.Result;
                }
                else
                {
                    thanas = new List<Thana>();
                }
            }
            return Json(thanas.ToList(), JsonRequestBehavior.AllowGet);
        }

    }
}