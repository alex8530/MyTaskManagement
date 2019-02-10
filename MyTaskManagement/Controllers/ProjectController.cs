using MyTaskManagement.Models;
using MyTaskManagement.Persistence;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using MyTaskManagement.Core.ViewModel;

namespace MyTaskManagement.Controllers
{
    public class ProjectController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());

 
        // GET: Project
        public ActionResult Index()
        {
            //var projects = _unitOfWork.ProjectRepositry.GetAll();
            var ProjectsWithClientAndUsers = _unitOfWork.ProjectRepositry.GetAllProjectsWithClientAndUsers();
            return View(ProjectsWithClientAndUsers);
        }

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Project/Create
        public ActionResult Create()
        {

            var viewmodel = new IndexViewModels()
            {
                 Project = new Project(),
                 Users = _unitOfWork.UserRepositry.GetAll().ToList(),
                Clients = _unitOfWork.ClientRepositry.GetAll().ToList() 
             

            };
            return View(viewmodel);
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IndexViewModels  viewmodel, string __UserId__,string ClientId)
        {

           

            try
            {
                if (viewmodel == null)
                    return HttpNotFound();

                //if (!ModelState.IsValid)
                //{
                //    var errors = ModelState.Values.SelectMany(vr => vr.Errors);
                //    var s = ModelState.Values;
                //    var v = new IndexViewModels()
                //    {
                //        Project = new Project(),
                //        Users = _unitOfWork.UserRepositry.GetAll().ToList(),
                //        Clients = _unitOfWork.ClientRepositry.GetAll().ToList()

                //    };

                //    return View(v);
                //}

                //Add To Projects..we need user,client,to add them to project   

 

                //get client

                var client = _unitOfWork.ClientRepositry.SingleOrDefault(c => c.Name == ClientId);



                //only corspond data will be set ,so users,clinet will be null
                var newProject = new Project()
                {
                    Id = viewmodel.Project.Id,
                    Name = viewmodel.Project.Name,
                    DeadTime = viewmodel.Project.DeadTime,
                    StartTime = viewmodel.Project.StartTime,
                    Status = StatusEnum.Not_Start,
                    Description = viewmodel.Project.Description,
                    Client = client,
                    Users = new List<ApplicationUser>()//this is must
                };

                //get user //it may to add list of users not only list
                if (!__UserId__.IsEmpty())
                {
                    var user = _unitOfWork.UserRepositry.SingleOrDefault(u => u.Id == __UserId__);
                    newProject.Users.Add(user);

                }


                _unitOfWork.ProjectRepositry.Add(newProject);
                _unitOfWork.Complete();



                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Project/Edit/5
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

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Project/Delete/5
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
