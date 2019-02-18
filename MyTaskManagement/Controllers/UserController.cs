using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MyTaskManagement.Core.ViewModel;
using MyTaskManagement.Models;
using MyTaskManagement.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTaskManagement.Core.Domain;

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
            var roles = new List<string>();
            // get all names of role for user ...
            //i use this method because there is a role id inside user , not role name
            foreach (var user in listUser)
            {
                string str = "";
                foreach (var role in UserManager.GetRoles(user.Id))
                {
                    str = (str == "") ? role.ToString() : str + " - " + role.ToString();
                }
                roles.Add(str);
            }
            var model = new ListUserViewModel()
            {
                 Users = listUser,
                 RolesNames = roles.ToList()
            };

            return View(model);
        }
 

        // GET: Employee/Details/fds 
        public ActionResult Details(string id)
        {

            if (id == null)
            {
                return HttpNotFound();
            }

            return View(_unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRolesAndFiles(id));
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View(new RegisterViewModel());
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(RegisterViewModel  model, HttpPostedFileBase upload)
        {
            try
            {
                // TODO: Add insert logic here


                var newUser = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IsAcceptedOnCondition = model.IsAcceptedOnCondition,
                    JopTitle = model.JopTitle,
                    O_T_H_Rate = model.O_T_H_Rate,
                    HourlyRate = model.HourlyRate,
                    MyFiles = new List<MyFile>()


                };
                //Guid is used as a file name on the basis that it can pretty much guarantee uniqueness
                if (upload != null && upload.ContentLength > 0)
                {
                    var photo = new MyFile
                    {
                        FileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName),
                        MyFileType = MyFileType.Photo
                    };


                   string physicalPath = Server.MapPath("~/images/" + photo.FileName);

                    // save image in folder
                    upload.SaveAs(physicalPath);
                    newUser.MyFiles.Add(photo);
                    
                }
                _unitOfWork.UserRepositry.AddUser(newUser,model.Password);
                _unitOfWork.Complete();

                //_unitOfWork.UserRepositry.AddUser(newUser, model.Password);
                //var oldRoleId = newUser.Roles.SingleOrDefault().RoleId;
                //var oldRoleName = ApplicationDbContext.Create().Roles.SingleOrDefault(r => r.Id == oldRoleId).Name;
                //if (oldRoleName != role)
                //{
                //    UserManager.RemoveFromRole(user.Id, oldRoleName);
                //    UserManager.AddToRole(user.Id, role);
                //}

                return RedirectToAction("ListUser");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(string id)
        {
            var editUser = _unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRolesAndFiles(id);
            ViewBag.AllRoles = ApplicationDbContext.Create().Roles.ToList();

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


                string newRole = Request.Form["CurrenrRole"];
                     _unitOfWork.UserRepositry.UpdateRolesToUser(id, newRole);

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
                var deleteUser = _unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRolesAndFiles(id);
                _unitOfWork.UserRepositry.Remove(deleteUser);
                _unitOfWork.Complete();
                return RedirectToAction("ListUser");
            }
            catch 
            {
                return View();
            }
        }
    }
}
