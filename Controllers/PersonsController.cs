using Microsoft.Extensions.Logging;
using Yalla_Notlob_Akl.DB;
using Yalla_Notlob_Akl.Models;

namespace Yalla_Notlob_Akl.Controllers
{

    public class PersonsController : BaseController<Person,PersonDao>
    {
        private readonly ILogger<PersonsController> _logger;
        
        public PersonsController(ILogger<PersonsController> logger): base(new PersonDao())
        {
            _logger = logger;
        }
   }
}
