using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Yalla_Notlob_Akl.DB;
using Yalla_Notlob_Akl.Models;
using Yalla_Notlob_Akl.Business;

namespace Yalla_Notlob_Akl.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static double taxFees = 0.0;
        private static double deliveryFees = 0.0;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var OrderItems = new OrderItemDao().GetAll();
            double OrderBasePrice = OrderCalculations.GetOrderBasePrice(OrderItems);
            OrderStatsVM vm = new OrderStatsVM
            {
                OrderSummary = OrderCalculations.GetOrderSummary(OrderItems),
                OrderReciept = OrderCalculations.GetOrderReceipt(OrderItems, taxFees, deliveryFees),
                OrderBasePrice = OrderBasePrice,
                OrderTaxCost = taxFees,
                OrderDeliveryCost = deliveryFees,
                OrderTotalPrice = OrderCalculations.GetTotalOrderPrice(OrderBasePrice, taxFees, deliveryFees)
            };
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
