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
using Yalla_Notlob_Akl.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Yalla_Notlob_Akl.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private static Dictionary<string, double> _TaxFees = new Dictionary<string, double>();
        private static Dictionary<string, double> _DeliveryFees = new Dictionary<string, double>();

        private static double TaxFees {
            get
            {
                string _sessionId = new HttpContextAccessor().HttpContext.Session.Id;
                Console.WriteLine(_sessionId);
                if(_TaxFees.ContainsKey(_sessionId)) return _TaxFees[_sessionId];
                else return 0.0;
            }
            set
            {
                string _sessionId = new HttpContextAccessor().HttpContext.Session.Id;
                _TaxFees[_sessionId] = value;
            }
        }

        private static double DeliveryFees {
            get
            {
                string _sessionId = new HttpContextAccessor().HttpContext.Session.Id;
                if(_DeliveryFees.ContainsKey(_sessionId)) return _DeliveryFees[_sessionId];
                else return 0.0;
            }
            set
            {
                string _sessionId = new HttpContextAccessor().HttpContext.Session.Id;
                _DeliveryFees[_sessionId] = value;
            }
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Set("init", BitConverter.GetBytes(true));
            var OrderItems = new OrderItemDao().GetAll();
            double OrderBasePrice = OrderCalculations.GetOrderBasePrice(OrderItems);
            OrderStatsVM vm = new OrderStatsVM
            {
                OrderSummary = OrderCalculations.GetOrderSummary(OrderItems),
                OrderReciept = OrderCalculations.GetOrderReceipt(OrderItems, TaxFees, DeliveryFees),
                OrderBasePrice = OrderBasePrice,
                OrderTaxCost = TaxFees,
                OrderDeliveryCost = DeliveryFees,
                OrderTotalPrice = OrderCalculations.GetTotalOrderPrice(OrderBasePrice, TaxFees, DeliveryFees)
            };
            return View("Index",vm);
        }

        [HttpPost]
        public IActionResult SetOrderOptions( double taxFees, double deliveryFees)
        {
            TaxFees = taxFees;
            DeliveryFees = deliveryFees;
            return Redirect("Index");
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
