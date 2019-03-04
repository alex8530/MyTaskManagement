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
            var listUser = _unitOfWork.UserRepositry.GetAllUsersWithProjectsAndTasksAndRolesAndFinanicalWithFiles();
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

            return View(_unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRolesAndFilesAndFinanicalWithFiles(id));
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
                    MyFiles = new List<MyUserFile>()


                };
                //Guid is used as a file name on the basis that it can pretty much guarantee uniqueness
                if (upload != null && upload.ContentLength > 0)
                {
                    var photo = new MyUserFile()
                    {
                        FileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName),
                        MyFileType = MyFileType.Photo
                    };


                   string physicalPath = Server.MapPath("~/images/" + photo.FileName);

                    // save image in folder
                    upload.SaveAs(physicalPath);
                    newUser.MyFiles.Add(photo);
                    
                }
                //i dont user Manager here, because no need to write here.. 
                _unitOfWork.UserRepositry.AddUser(newUser,model.Password);
                _unitOfWork.Complete();

               
                return RedirectToAction("ListUser");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        //int month =0 , int year = 0  will be avairiaable when i click on sort button in Edit View ,
       

        public ActionResult Edit(string id ,int month =0 , int year = 0 )
        {
            var editUser = _unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRolesAndFilesAndFinanicalWithFiles(id);
            var listOwnFinan = editUser.FinancialstatusList ;

            if (month == 0 && year != 0)
            { //No sorting  for month
                listOwnFinan = listOwnFinan.Where( f => f.Date.Year == year).ToList();
 
            }
            else if (month != 0 && year == 0)
            {//No sorting  for year
                listOwnFinan = listOwnFinan.Where(f => f.Date.Month == month).ToList();
                 
            }
            else if (month != 0 && year != 0)
            {//No sorting  for year
                listOwnFinan = listOwnFinan.Where(f => f.Date.Month == month &&   f.Date.Year == year).ToList();
                 
            }

            // i send the Financialstatuses  as seperad on user , because i need to make some sort on it
            var vm = new EditUserViewModel()
            {
                 User = editUser,
                 Financialstatuses = listOwnFinan.ToList()
            };
            ViewBag.AllRoles = ApplicationDbContext.Create().Roles.ToList();


            ViewBag.m = month;
            ViewBag.y = year;

            return View(vm);
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
                var deleteUser = _unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRolesAndFilesAndFinanicalWithFiles(id);
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
