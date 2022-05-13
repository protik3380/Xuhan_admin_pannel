using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Models.View_Model;
using Category = xtbdEcommerceAdminPanel.Models.Category;


namespace xtbdEcommerceAdminPanel.Utility
{
    public static class Dropdown
    {
        public static List<Category> Categories()
        {
            IEnumerable<Category> categories = null;
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
                }
            }
            return categories.ToList();
        }

        public static List<Category> ActiveCategories()
        {
            IEnumerable<Category> categories = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("category/getallactive");
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
                }
            }
            return categories.ToList();
        }

        public static List<Brand> Brands()
        {
            IEnumerable<Brand> brands = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("brand/getall");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Brand>>();
                    readTask.Wait();
                    brands = readTask.Result;
                }
                else
                {
                    brands = new List<Brand>();
                }
            }
            return brands.ToList();
        }

        public static List<Brand> ActiveBrands()
        {
            IEnumerable<Brand> brands = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("brand/getallactive");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Brand>>();
                    readTask.Wait();
                    brands = readTask.Result;
                }
                else
                {
                    brands = new List<Brand>();
                }
            }
            return brands.ToList();
        }

        public static List<Product> Products()
        {
            IEnumerable<Product> products = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("product/getall");
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
            return products.ToList();
        }

        public static List<Product> ActiveProducts()
        {
            IEnumerable<Product> products = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("product/getall");
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
            return products.ToList();
        }

        public static List<ConcernTag> ActiveSkinConcernTags()
        {
            IEnumerable<ConcernTag> skinConcernTags = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("skinconcerntag/getallactive");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ConcernTag>>();
                    readTask.Wait();
                    skinConcernTags = readTask.Result;
                }
                else
                {
                    skinConcernTags = new List<ConcernTag>();
                }
            }
            return skinConcernTags.ToList();
        }

        public static List<ConcernTag> SkinConcernTags()
        {
            IEnumerable<ConcernTag> skinConcernTags = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("skinconcerntag/getall");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ConcernTag>>();
                    readTask.Wait();
                    skinConcernTags = readTask.Result;
                }
                else
                {
                    skinConcernTags = new List<ConcernTag>();
                }
            }
            return skinConcernTags.ToList();
        }

        public static List<SolutionTag> ActiveSkinSolutionTags()
        {
            IEnumerable<SolutionTag> skinSolutionTags = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("skinsolutiontag/getallactive");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<SolutionTag>>();
                    readTask.Wait();
                    skinSolutionTags = readTask.Result;
                }
                else
                {
                    skinSolutionTags = new List<SolutionTag>();
                }
            }
            return skinSolutionTags.ToList();
        }

        public static List<SolutionTag> SkinSolutionTags()
        {
            IEnumerable<SolutionTag> skinSolutionTags = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("skinsolutiontag/getall");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<SolutionTag>>();
                    readTask.Wait();
                    skinSolutionTags = readTask.Result;
                }
                else
                {
                    skinSolutionTags = new List<SolutionTag>();
                }
            }
            return skinSolutionTags.ToList();
        }

        public static List<Department> Departments()
        {
            IEnumerable<Department> departments = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("department/getall");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Department>>();
                    readTask.Wait();
                    departments = readTask.Result;
                }
                else
                {
                    departments = new List<Department>();
                }
            }
            return departments.ToList();
        }

        public static List<Department> ActiveDepartments()
        {
            IEnumerable<Department> departments = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("department/getallactive");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Department>>();
                    readTask.Wait();
                    departments = readTask.Result;
                }
                else
                {
                    departments = new List<Department>();
                }
            }
            return departments.ToList();
        }

        public static List<Designation> Designations()
        {
            IEnumerable<Designation> designations = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("designation/getall");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Designation>>();
                    readTask.Wait();
                    designations = readTask.Result;
                }
                else
                {
                    designations = new List<Designation>();
                }
            }
            return designations.ToList();
        }

        public static List<Designation> ActiveDesignations()
        {
            IEnumerable<Designation> designations = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("designation/getallactive");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Designation>>();
                    readTask.Wait();
                    designations = readTask.Result;
                }
                else
                {
                    designations = new List<Designation>();
                }
            }
            return designations.ToList();
        }

        public static List<ProductUnit> ProductUnits()
        {
            IEnumerable<ProductUnit> productUnits = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("productUnit/getall");
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
            return productUnits.ToList();
        }

        public static List<ProductUnit> ActiveProductUnits()
        {
            IEnumerable<ProductUnit> productUnits = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("productUnit/getallactive");
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
            return productUnits.ToList();
        }

        public static List<District> Districts()
        {
            IEnumerable<District> districts = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("district/getall");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<District>>();
                    readTask.Wait();
                    districts = readTask.Result;
                }
                else
                {
                    districts = new List<District>();
                }
            }
            return districts.ToList();
        }

        public static List<District> ActiveDistricts()
        {
            IEnumerable<District> districts = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("district/getallactive");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<District>>();
                    readTask.Wait();
                    districts = readTask.Result;
                }
                else
                {
                    districts = new List<District>();
                }
            }
            return districts.ToList();
        }

        public static List<Thana> Thanas()
        {
            IEnumerable<Thana> thanas = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("thana/getall");
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
            return thanas.ToList();
        }

        public static List<Thana> ActiveThanas()
        {
            IEnumerable<Thana> thanas = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("thana/getallactive");
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
            return thanas.ToList();
        }
        public static List<ProductDiscount> ProductDiscounts()
        {
            IEnumerable<ProductDiscount> productDiscounts = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("productDiscount/getall");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductDiscount>>();
                    readTask.Wait();
                    productDiscounts = readTask.Result;
                }
                else
                {
                    productDiscounts = new List<ProductDiscount>();
                }
            }
            return productDiscounts.ToList();
        }

        public static List<ProductDiscount> ActiveProductDiscounts()
        {
            IEnumerable<ProductDiscount> productDiscounts = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("productDiscount/getallactive");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductDiscount>>();
                    readTask.Wait();
                    productDiscounts = readTask.Result;
                }
                else
                {
                    productDiscounts = new List<ProductDiscount>();
                }
            }
            return productDiscounts.ToList();
        }

        public static List<ProductStorage> ProductStorages()
        {
            IEnumerable<ProductStorage> productStorageList = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("productStorage/getall");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductStorage>>();
                    readTask.Wait();
                    productStorageList = readTask.Result;
                }
                else
                {
                    productStorageList = new List<ProductStorage>();
                }
            }
            return productStorageList.ToList();
        }

        public static List<Banner> Banners()
        {
            IEnumerable<Banner> banners = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("banner/getall");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Banner>>();
                    readTask.Wait();
                    banners = readTask.Result;
                }
                else
                {
                    banners = new List<Banner>();
                }
            }
            return banners.ToList();
        }
        public static List<MasterDepot> MasterDepots()
        {
            IEnumerable<MasterDepot> masterDepots = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("masterdepot/getall");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<MasterDepot>>();
                    readTask.Wait();
                    masterDepots = readTask.Result;
                }
                else
                {
                    masterDepots = new List<MasterDepot>();
                }
            }
            return masterDepots.ToList();
        }
        public static List<MasterDepot> ActiveMasterDepots()
        {
            IEnumerable<MasterDepot> masterDepots = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("masterdepot/getallactive");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<MasterDepot>>();
                    readTask.Wait();
                    masterDepots = readTask.Result;
                }
                else
                {
                    masterDepots = new List<MasterDepot>();
                }
            }
            return masterDepots.ToList();
        }
        public static List<Distributor> Distributors()
        {
            IEnumerable<Distributor> distributors = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("distributor/getall");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Distributor>>();
                    readTask.Wait();
                    distributors = readTask.Result;
                }
                else
                {
                    distributors = new List<Distributor>();
                }
            }
            return distributors.ToList();
        }
        public static List<Distributor> ActiveDistributors()
        {
            IEnumerable<Distributor> distributors = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("distributor/getallactive");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode) 
                {
                    var readTask = result.Content.ReadAsAsync<IList<Distributor>>();
                    readTask.Wait();
                    distributors = readTask.Result;
                }
                else
                {
                    distributors = new List<Distributor>();
                }
            }
            return distributors.ToList();
        }
        public static List<Deliveryman> DeliverymanList()
        {
            IEnumerable<Deliveryman> deliverymanList = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("deliveryman/getall");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Deliveryman>>();
                    readTask.Wait();
                    deliverymanList = readTask.Result;
                }
                else
                {
                    deliverymanList = new List<Deliveryman>();
                }
            }
            return deliverymanList.ToList();
        }
        public static List<Deliveryman> ActiveDeliverymanList()
        {
            IEnumerable<Deliveryman> deliverymanList = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("deliveryman/getallactive");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Deliveryman>>();
                    readTask.Wait();
                    deliverymanList = readTask.Result;
                }
                else
                {
                    deliverymanList = new List<Deliveryman>();
                }
            }
            return deliverymanList.ToList();
        }
        public static List<Coupon> Coupons()
        {
            IEnumerable<Coupon> coupons = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("coupon/getall");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Coupon>>();
                    readTask.Wait();
                    coupons = readTask.Result;
                }
                else
                {
                    coupons = new List<Coupon>();
                }
            }
            return coupons.ToList();
        }

        public static List<OrderRejectStatus> RejectStatusList()
        {
            IEnumerable<OrderRejectStatus> rejectStatus = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("order/rejectStatus");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<OrderRejectStatus>>();
                    readTask.Wait();
                    rejectStatus = readTask.Result;
                }
                else
                {
                    rejectStatus = new List<OrderRejectStatus>();
                }
            }
            return rejectStatus.ToList();
        }

        public static List<Order> OrderList()
        {
            IEnumerable<Order> orders = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("order/getAllOrder");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Order>>();
                    readTask.Wait();
                    orders = readTask.Result;
                }
                else
                {
                    orders = new List<Order>();
                }
            }
            return orders.ToList();
        }

        public static List<Customer> Customers()
        {
            IEnumerable<Customer> customers = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("customer/getall");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Customer>>();
                    readTask.Wait();
                    customers = readTask.Result;
                }
                else
                {
                    customers = new List<Customer>();
                }
            }
            return customers.ToList();
        }

        public static List<OrderStatus> OrderStates()
        {
            IEnumerable<OrderStatus> orderStateList = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("order/stateStatus");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<OrderStatus>>();
                    readTask.Wait();
                    orderStateList = readTask.Result;
                }
                else
                {
                    orderStateList = new List<OrderStatus>();
                }
            }
            return orderStateList.ToList();
        }

        public static List<UseProcessVM> UseProcessImages()
        {
            IEnumerable<UseProcessVM> useProcessImages = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("UseProces/GetAll");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<UseProcessVM>>();
                    readTask.Wait();
                    useProcessImages = readTask.Result;
                }
                else
                {
                    useProcessImages = new List<UseProcessVM>();
                }
            }
            return useProcessImages.ToList();
        }


    }


}
