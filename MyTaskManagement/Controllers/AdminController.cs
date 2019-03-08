using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTaskManagement.Core.ViewModel;
using MyTaskManagement.Models;
using MyTaskManagement.Persistence;

namespace MyTaskManagement.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        UnitOfWork _unitOfWork =   new UnitOfWork(new ApplicationDbContext());
        // GET: Admin
        public ActionResult Index()
        {
            var allProjects = _unitOfWork.ProjectRepositry.GetAllProjectsWithClientAndUsersAndTasksWithFiles().ToList();
            var allEmployees = _unitOfWork.UserRepositry.GetAllUsersWithProjectsAndTasksAndRolesAndFinanicalWithFiles().ToList();
            var allTasks = _unitOfWork.TTaskRepositry.GetAllTasksWithUserAndUserAndProject().ToList();

            var vm = new IndexAdminViewModel()
            {
                Projects = allProjects,
                Users = allEmployees,
                Tasks = allTasks
            };
            return View(vm);
        }

        // GET: Admin/Details/5
        public ActionResult AllProjects(int id)
        {
            return View();
        }
        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
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

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
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
