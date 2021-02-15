using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Yalla_Notlob_Akl.DB;
using Yalla_Notlob_Akl.Models;

namespace Yalla_Notlob_Akl.Controllers
{
    public abstract class BaseController<Model, Dao> : Controller
            where Model : BaseModel
    {
        private Dao<Model> dao;

        public BaseController(Dao<Model> dao)
        {
            this.dao = dao;
        }

        public virtual IActionResult Index()
        {
            return View(dao.GetAll());
        }

        [HttpPost]
        public ActionResult Add([FromBody] Model m)
        {
            try
            {
                var created = dao.Create(m);
                return Json(created);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost]
        public ActionResult Update([FromBody] Model m)
        {
            return Json(dao.Update(m));
        }

        [HttpPost]
        public ActionResult Delete([FromBody] Model m)
        {
            Console.WriteLine($"Deleting {m.id}");
            return Json(dao.Delete(m.id));
        }
    }
}