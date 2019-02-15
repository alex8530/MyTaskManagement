using Microsoft.AspNet.Identity.Owin;
using MyTaskManagement.Core.ViewModel;
using MyTaskManagement.Models;
using MyTaskManagement.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTaskManagement.Controllers
{
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());

        // GET: Employee
        public ActionResult ListUser()
        {

            var listUser = _unitOfWork.UserRepositry.GetAllUsersWithProjectsAndTasksAndRoles();
 
            return View(listUser);
        }
 

        // GET: Employee/Details/fdsfdsfdsf
        public ActionResult Details(string id)
        {
            return View(_unitOfWork.UserRepositry.SingleOrDefault(user => user.Id == id));
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
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

        // GET: Employee/Edit/5
        public ActionResult Edit(string id)
        {
            var editUser = _unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRoles(id);

            return View(editUser);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, ApplicationUser model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var oldUser = _unitOfWork.UserRepositry.SingleOrDefault(user => user.Id== id);
                    oldUser.FirstName =model.FirstName ;
                    oldUser.LastName = model.LastName;
                    oldUser.JopTitle = model.JopTitle;
                    oldUser.HourlyRate = model.HourlyRate;
                    oldUser.O_T_H_Rate = model.O_T_H_Rate;
                    oldUser.UserName = model.UserName;
                    oldUser.Email = model.Email;
                    

                    _unitOfWork.Complete();

                }


                return RedirectToAction("ListUser");
            }
            catch(Exception exception)
            {
                return View( model);
            }
        }

        // GET: Employee/Delete/asdasd
        public ActionResult Delete(string id)
        {
          

            return View(_unitOfWork.UserRepositry.SingleOrDefault(user => user.Id == id));
        }

        // POST: Employee/Delete/asdasd
        [HttpPost]
        public ActionResult Delete(string id, ApplicationUser  model)
        {
            try
            {
                var deleteUser = _unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRoles(id);
                _unitOfWork.UserRepositry.Remove(deleteUser);

                return RedirectToAction("ListUser");
            }
            catch 
            {
                return View();
            }
        }
    }
}
