using Microsoft.Extensions.Logging;
using Yalla_Notlob_Akl.DB;
using Yalla_Notlob_Akl.Models;

namespace Yalla_Notlob_Akl.Controllers
{

    public class ItemsController : BaseController<Item,ItemDao>
    {
        private readonly ILogger<ItemsController> _logger;
        
        public ItemsController(ILogger<ItemsController> logger): base(new ItemDao())
        {
            _logger = logger;
        }
   }
}
