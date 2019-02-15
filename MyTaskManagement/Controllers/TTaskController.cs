﻿using MyTaskManagement.Models;
using MyTaskManagement.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MyTaskManagement.Core.Domain;
using MyTaskManagement.Core.ViewModel;

namespace MyTaskManagement.Controllers
{
    public class TTaskController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());

        // GET: TTask
        public ActionResult Index()
        {
            var tasks = _unitOfWork.TTaskRepositry.GetAllTasksWithUserAndUserAndProject();
            return View( tasks);
        }

        // GET: TTask/Details/5
        public ActionResult Details(int id)
        {
            var task = _unitOfWork.TTaskRepositry.GetTasksWithUserAndUserAndProject(id);

            return View(task);
        }



        // GET: TTask/Create
        //this is comming from project controller 
        //if this action is called from project  Edit View .. this id_current_project will not null 
        //if this action is called from Task  View .. this id_current_project will be null 
        public ActionResult Create(string id_current_project)
        {
            //come from task page

            if (id_current_project.IsNullOrWhiteSpace())
            {
                ViewBag.fromTask = "yes";
                var newViewModel = new CreateTaskViewModel()
                {
                    Task = new TTask()
                    {
                         
                        StartTime = DateTime.Now //this is defult time

                    },
                    Users = _unitOfWork.UserRepositry.GetAll().ToList(),
                    Projects = _unitOfWork.ProjectRepositry.GetAll().ToList() 
                    
                };
                return View(newViewModel);

            }
            else
            {
                //come from project
                ViewBag.fromTask = "no";

                var newViewModel = new CreateTaskViewModel()
                {
                    Task = new TTask()
                    {
                        ProjectId = id_current_project,//this is must neot change !!!
                        StartTime = DateTime.Now //this is defult time

                    },
                    Users = _unitOfWork.UserRepositry.GetAll().ToList()
                };

                return View(newViewModel);
            }
           
        }

        // POST: TTask/Create
        [HttpPost]
        public ActionResult Create(TTask  task , string id_current_project)
        {

            // you should note that id_current_project comming from viewmodel !! not from form 

            try
            {
                int stat = Int32.Parse(Request.Form["status"]);
                int pri = Int32.Parse(Request.Form["priority"]);
                var ui = Request.Form["__UserId__"];
                var pi = Request.Form["__ProjectId__"];
                 //var id_current_project = Request.Form["id_current_project"];

                
                //var user = _unitOfWork.UserRepositry.SingleOrDefault(u => u.Id == ui);
                var user = _unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRoles( ui);

                /*
                 * Very important here
                 *when you add task to project .. the task must be have user ..
                 * and then when you add task to poject ..you must add this project to user !!
                 *
                 *
                 *
                 */
                //var project = _unitOfWork.ProjectRepositry.SingleOrDefault(pp => pp.Id == id_current_project);
                var dbContext = new ApplicationDbContext();

                if (id_current_project.IsNullOrWhiteSpace())
                {
                    //thats mean we will create new task from out side project , no will be project id as forign_key

                     

                    var newTask = new TTask()
                    {
                        ProjectId = pi,
                        ApplicationUser = user,
                        Description = task.Description,
                        DeadTime = task.DeadTime,
                        StartTime = task.StartTime,
                        Name = task.Name,
                        Status = (StatusEnum)Enum.ToObject(typeof(StatusEnum), stat),
                        Priority = (PriorityEnum)Enum.ToObject(typeof(PriorityEnum), pri),
                        OverTime = task.OverTime,
                        WorkingHours = task.WorkingHours
                        //Project =new Project() // here no need to add project object , just add his forign key
                        //, but if you need to add project object , you must init it

                    };


                    //will add project to user to increse number of projects thats user work on
                    var pro = _unitOfWork.ProjectRepositry.SingleOrDefault(p => p.Id == pi);

                    if (!user.Projects.Contains(pro))
                    {
                        user.Projects.Add(pro);

                    }
                    _unitOfWork.TTaskRepositry.Add(newTask);
                    _unitOfWork.Complete();

                }
                else
                {


                    var newTask = new TTask()
                    {
                        ProjectId = id_current_project,
                        ApplicationUser = user,
                        Description = task.Description,
                        DeadTime = task.DeadTime,
                        StartTime = task.StartTime,
                        Name = task.Name,
                        Status = (StatusEnum)Enum.ToObject(typeof(StatusEnum), stat),
                        Priority = (PriorityEnum)Enum.ToObject(typeof(PriorityEnum), pri),
                        OverTime = task.OverTime,
                        WorkingHours = task.WorkingHours
                        //Project =new Project() // here no need to add project object , just add his forign key
                        //, but if you need to add project object , you must init it

                    };


                    var pro = _unitOfWork.ProjectRepositry.SingleOrDefault(p => p.Id == id_current_project);
                   
                        if (!user.Projects.Contains(pro))
                        {
                          user.Projects.Add(pro);

                        }


                    _unitOfWork.TTaskRepositry.Add(newTask);
                    _unitOfWork.Complete();

                }


                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return Content(exception.Message);
            }
        }

        // GET: TTask/Edit/5
        public ActionResult Edit(int id)
        {
           

            var vm = new EditTaskViewModel()
            {
                Users = _unitOfWork.UserRepositry.GetAll().ToList(),
                Task = _unitOfWork.TTaskRepositry.GetTasksWithUserAndUserAndProject(id)
            };
            return View(vm);
        }

        // POST: TTask/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TTask  task)
        {
            try
            {
                // TODO: Add update logic here
                var oldTask = _unitOfWork.TTaskRepositry.GetTasksWithUserAndUserAndProject(id);
                oldTask.Priority = task.Priority;
                oldTask.Status= task.Status;
                oldTask.StartTime = task.StartTime;
                oldTask.DeadTime= task.DeadTime;
                oldTask.Description = task.Description;


                _unitOfWork.Complete();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TTask/Delete/5
        public ActionResult Delete(int id)
        {
            var task = _unitOfWork.TTaskRepositry.GetTasksWithUserAndUserAndProject(id);

            return View(task);
        }

        // POST: TTask/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var task = _unitOfWork.TTaskRepositry.GetTasksWithUserAndUserAndProject(id);
                _unitOfWork.TTaskRepositry.Remove(task);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return View();
            }
        }



        // GET: TTask/DeleteUserFromTask/idUser/
        // GET: TTask/DeleteUserFromTask/idUser/idTask
        public ActionResult DeleteUserFromTask(string idUser)
        {

            var deletedUser = _unitOfWork.UserRepositry.SingleOrDefault(user => user.Id == idUser);

            return View(deletedUser);
        }

        // POST: TTask/DeleteUserFromTask/idUser/idTask
        [HttpPost]
        public ActionResult DeleteUserFromTask(string idUser, int idTask)
        {
            try
            {
                // TODO: Add delete logic here
                var deletedUser = _unitOfWork.UserRepositry.SingleOrDefault(user => user.Id == idUser);
                var task = _unitOfWork.TTaskRepositry.GetTasksWithUserAndUserAndProject(idTask);
                //Note ,,, You must delete user from project table, not  from user table...

                //task.ApplicationUser.Remove(deletedUser);
               
                //now , because we have one user , i will do that
                task.ApplicationUser = null;
                _unitOfWork.Complete();
                return RedirectToAction("Edit", new { id = idTask });
            }
            catch (Exception exception)
            {
                return View();
            }
        }


        // GET: TTask/AddUserToTask/idUser/
        // GET: TTask/AddUserToTask/idUser/idTask
        public ActionResult AddUserToTask(string idUser)
        {

            var addUser = _unitOfWork.UserRepositry.SingleOrDefault(user => user.Id == idUser);

            return View(addUser);
        }

        // POST: TTask/AddUserToTask/idUser/idTask
        [HttpPost]
        public ActionResult AddUserToTask(string idUser, int idTask)
        {
            try
            {
                // TODO: Add delete logic here
                var addedUser = _unitOfWork.UserRepositry.SingleOrDefault(user => user.Id == idUser);
                var task = _unitOfWork.TTaskRepositry.GetTasksWithUserAndUserAndProject(idTask);
               
                task.ApplicationUser = new ApplicationUser();
                task.ApplicationUser = addedUser;

                _unitOfWork.Complete();
                return RedirectToAction("Edit", new { id = idTask });
            }
            catch (Exception exception)
            {
                return View();
            }
        }


 
    }

}
