using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using xtbdEcommerceAdminPanel.Models;
using xtbdEcommerceAdminPanel.Models.RequestDto;
using xtbdEcommerceAdminPanel.Models.View_Model;
using xtbdEcommerceAdminPanel.Utility;

namespace xtbdEcommerceAdminPanel.Manager
{
    public class ReportManager
    {
        public List<SalesByProductVM> GetSaleByProductReportData(SalesByProductDto searchDto)
        {
            IEnumerable<SalesByProductVM> sales = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.PostAsJsonAsync("report/getSalesByProduct", searchDto);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<SalesByProductVM>>();
                    readTask.Wait();
                    sales = readTask.Result;
                }
                else
                {
                    sales = new List<SalesByProductVM>();
                }
            }
            return sales.ToList();
        }

        public List<OrderReportVM> GetOrderReportData(ReportSearchCriteria searchDto)
        {
            IEnumerable<OrderReportVM> sales = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.PostAsJsonAsync("report/getTotalOrders", searchDto);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<OrderReportVM>>();
                    readTask.Wait();
                    sales = readTask.Result;
                }
                else
                {
                    sales = new List<OrderReportVM>();
                }
            }
            return sales.ToList();
        }

        public List<OrderByStatusVM> GetOrderReportByStatus(OrderByStatusDto searchDto)
        {
            IEnumerable<OrderByStatusVM> sales = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.PostAsJsonAsync("report/getOrdersByOrderStatus", searchDto);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<OrderByStatusVM>>();
                    readTask.Wait();
                    sales = readTask.Result;
                }
                else
                {
                    sales = new List<OrderByStatusVM>();
                }
            }
            return sales.ToList();
        }

        public List<Order> GetOrderByLocationReport(OrderLocationReportDto searchDto)
        {
            IEnumerable<Order> sales = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.PostAsJsonAsync("order/getAllOrderByLocation", searchDto);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Order>>();
                    readTask.Wait();
                    sales = readTask.Result;
                }
                else
                {
                    sales = new List<Order>();
                }
            }
            return sales.ToList();
        }

        public ReturningCustomerVM GetReturningCustomer(ReportSearchCriteria searchCriteria)
        {
            ReturningCustomerVM returningCustomer = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.PostAsJsonAsync("report/returningCustomerReport", searchCriteria);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ReturningCustomerVM>();
                    readTask.Wait();
                    returningCustomer = readTask.Result;
                }
                else
                {
                    returningCustomer = new ReturningCustomerVM();
                }
            }
            return returningCustomer;
        }

        public CountDashboardVM CountDashboard()
        {
            CountDashboardVM count = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.GetAsync("report/countDashboard");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CountDashboardVM>();
                    readTask.Wait();
                    count = readTask.Result;
                }
                else
                {
                    count = new CountDashboardVM();
                }
            }
            return count;
        }

        public List<OrderVsMonthOrYearVm> GetYearlyOrderRate(SalesOverTimeDto salesOverTime)
        {
            IEnumerable<OrderVsMonthOrYearVm> yearlyReport = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.PostAsJsonAsync("report/salesOverYear", salesOverTime);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<OrderVsMonthOrYearVm>>();
                    readTask.Wait();
                    yearlyReport = readTask.Result;
                }
                else
                {
                    yearlyReport = new List<OrderVsMonthOrYearVm>();
                }
            }
            return yearlyReport.ToList();
        }

        public List<OrderVsMonthOrYearVm> GetMonthlyOrderRate(SalesOverTimeDto salesOverTime)
        {
            IEnumerable<OrderVsMonthOrYearVm> monthlyReport = null;
            using (var client = new HttpClientDemo())
            {
                var responseTask = client.PostAsJsonAsync("report/salesOverMonth", salesOverTime);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<OrderVsMonthOrYearVm>>();
                    readTask.Wait();
                    monthlyReport = readTask.Result;
                }
                else
                {
                    monthlyReport = new List<OrderVsMonthOrYearVm>();
                }
            }
            return monthlyReport.ToList();
        }
    }
}