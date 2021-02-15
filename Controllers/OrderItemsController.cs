using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Yalla_Notlob_Akl.DB;
using Yalla_Notlob_Akl.Models;
using Yalla_Notlob_Akl.ViewModels;

namespace Yalla_Notlob_Akl.Controllers
{

    public class OrderItemsController : BaseController<OrderItem,OrderItemDao>
    {
        private readonly ILogger<OrderItemsController> _logger;
        
        public OrderItemsController(ILogger<OrderItemsController> logger): base(new OrderItemDao())
        {
            _logger = logger;
        }

        public override IActionResult Index(){
            var vm = new OrderItemViewModel
            {
                Persons = new PersonDao().GetAll(),
                Items = new ItemDao().GetAll(),
                OrderItems = new OrderItemDao().GetAll()
            };
            return View(vm);
        }
   }
}
