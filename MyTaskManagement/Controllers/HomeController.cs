using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTaskManagement.Models;
using MyTaskManagement.Persistence;

namespace MyTaskManagement.Controllers
{
    public class HomeController : Controller
    {

        private UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());

        
        public ActionResult Index()
        {
            var projects = _unitOfWork.ProjectRepositry.GetAll(); 
            return View(projects);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}