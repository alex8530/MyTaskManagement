﻿using MyTaskManagement.Models;
using MyTaskManagement.Persistence;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using MyTaskManagement.Core.ViewModel;
using System.Data.Entity;

namespace MyTaskManagement.Controllers
{
    public class ProjectController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        
 
        // GET: Project
        public ActionResult Index()
        {
             

            //var projects = _unitOfWork.ProjectRepositry.GetAll();
            var ProjectsWithClientAndUsers = _unitOfWork.ProjectRepositry.GetAllProjectsWithClientAndUsersAndTasks() ;
            return View(ProjectsWithClientAndUsers);
        }

        // GET: Project/Details/5
        public ActionResult Details(string id)
        {
           

            return View(_unitOfWork.ProjectRepositry.SingleOrDefault( project => project.Id == id));
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
        public ActionResult Create(IndexViewModels  viewmodel, string __UserId__,string ClientId, int status )
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
                //Note , here i send Name of client but i name ClientId in that form

                var client = _unitOfWork.ClientRepositry.SingleOrDefault(c => c.Name == ClientId);


            

                //only corspond data will be set ,so users,clinet will be null
                var newProject = new Project()
                {
                    Id = viewmodel.Project.Id,
                    Name = viewmodel.Project.Name,
                    DeadTime = viewmodel.Project.DeadTime,
                    StartTime = viewmodel.Project.StartTime,
                    Status = (StatusEnum)Enum.ToObject(typeof(StatusEnum), status),
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
        public ActionResult Edit(string id)
        {
            var viewmodel = new EditViewModel()
            {
                Project = _unitOfWork.ProjectRepositry.GetProjectsWithClientAndUsers(id),
                Users = _unitOfWork.UserRepositry.GetAll().ToList(),
                Clients = _unitOfWork.ClientRepositry.GetAll().ToList()
                 
            };
             
            

            return View(viewmodel);
        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Project  project,  int ClientId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    var oldProject = _unitOfWork.ProjectRepositry.SingleOrDefault(p => p.Id == id);
                    oldProject.Name = project.Name;
                    oldProject.StartTime = project.StartTime;
                    oldProject.DeadTime = project.DeadTime;
                    oldProject.Description = project.Description;
                    
                    oldProject.ClientId =  ClientId;

                    _unitOfWork.Complete();

                }
               

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return View();
            }
        }

        // GET: Project/DeleteUser/5
        public ActionResult DeleteUser(string idUser)
        {
            var deletedUser = _unitOfWork.UserRepositry.SingleOrDefault(user => user.Id == idUser);
            return View(deletedUser);
        }
      

        // POST: Project/DeleteUser/5
        [HttpPost]
        public ActionResult DeleteUser(string idUser, string idProject)
        {
            try
            {
                // TODO: Add delete logic here
                var deletedUser = _unitOfWork.UserRepositry.SingleOrDefault(user => user.Id == idUser);
                var project = _unitOfWork.ProjectRepositry.GetProjectsWithClientAndUsers(idProject);
                //Note ,,, You must delete user from project table, not  from user table...

                project.Users.Remove(deletedUser);
                _unitOfWork.Complete();
                return RedirectToAction("Edit" , new {id= idProject });
            }
            catch (Exception exception)
            {
                return View();
            }
        }

        // GET: Project/DeleteProject/5
        public ActionResult DeleteProject(string id)
        {
            var deletedProject = _unitOfWork.ProjectRepositry.SingleOrDefault(project => project.Id == id);
            return View(deletedProject);
        }


        // POST: Project/DeleteProject/5
        [HttpPost]
        public ActionResult DeleteProject(string id ,Project project )
        {
            try
            {
                // TODO: Add delete logic here
                var deletedProject = _unitOfWork.ProjectRepositry.SingleOrDefault(p => p.Id == id);
 
                _unitOfWork.ProjectRepositry.Remove(deletedProject);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return View();
            }
        }

        public ActionResult AddUserToProject(string idUser)
        {
            var addUser  = _unitOfWork.UserRepositry.SingleOrDefault(user => user.Id == idUser);


            return View(addUser);

        }

        [HttpPost]
        public ActionResult AddUserToProject(string idUser, string idProject)
        {
            try
            {
                
                var addUser = _unitOfWork.UserRepositry.SingleOrDefault(user => user.Id == idUser);

                /// Note we must include the oject with thier navigation proparity
                var project = _unitOfWork.ProjectRepositry.GetProjectsWithClientAndUsers(idProject);
                project.Users.Add(addUser);
                 _unitOfWork.ProjectRepositry.Add(project);
                 _unitOfWork.Complete();
                return RedirectToAction("Edit",new {id=idProject});
            }
            catch(Exception e)
            {
                return View();
            }
        }
    }
    


}
