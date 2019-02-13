using MyTaskManagement.Models;
using MyTaskManagement.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return View(tasks);
        }

        // GET: TTask/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TTask/Create
        //this is comming from project controller 
        public ActionResult Create(string id_current_project)
        {
            var newTaskViewModel = new TaskViewModel()  
            {
                Task = new TTask()
                {
                    ProjectId = id_current_project,//this is must neot change !!!
                    StartTime = DateTime.Now //this is defult time
                  
                },
                Users = _unitOfWork.UserRepositry.GetAll().ToList() 
            };

            return View(newTaskViewModel);
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
                 //var id_current_project = Request.Form["id_current_project"];

                
                var user = _unitOfWork.UserRepositry.SingleOrDefault(u => u.Id == ui);
               //var project = _unitOfWork.ProjectRepositry.SingleOrDefault(pp => pp.Id == id_current_project);
                var dbContext= new ApplicationDbContext();
          
              
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
                //newTask.Project = project;


                _unitOfWork.TTaskRepositry.Add(newTask);
                _unitOfWork.Complete();

               
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
            return View();
        }

        // POST: TTask/Edit/5
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

        // GET: TTask/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TTask/Delete/5
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
