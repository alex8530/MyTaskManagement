using MyTaskManagement.Models;
using MyTaskManagement.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTaskManagement.Controllers
{
    public class FinancialStatusController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());


        // GET: FinancialStatus  return list of All  Employee Financial 
        public ActionResult Index()
        {
            //so will pass list of users with thier financials 

            var listEmployeeFinanical = _unitOfWork.UserRepositry.GetAllUsersWithProjectsAndTasksAndRolesAndFinanical();
            return View(listEmployeeFinanical);
        }

        // GET: FinancialStatus/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FinancialStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FinancialStatus/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FinancialStatus/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FinancialStatus/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FinancialStatus/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FinancialStatus/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
