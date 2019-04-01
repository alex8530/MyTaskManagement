using MyTaskManagement.Models;
using MyTaskManagement.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
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
          
                //No sorting , show all tasks
                var taskss = _unitOfWork.TTaskRepositry.GetAllTasksWithUserAndUserAndProject();
       


                return View(taskss);
 


            //var grouped = from T in tasks 
            //              group T by new { month = T.StartTime.Month, year = T.StartTime.Year } into d
            //    select new { dt = string.Format("{0}/{1}", d.Key.month, d.Key.year), count = d.Count() };


            //Debug.WriteLine( "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz"); 
            //Debug.WriteLine( grouped.ToList());
            //Debug.WriteLine("zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz");

        }

        // GET: TTask/ShowTaskForEmployee 
        public ActionResult ShowTaskForEmployee( )
        {
            var currentUserID = User.Identity.GetUserId();
            var tasks = _unitOfWork.TTaskRepositry.GetAllTasksWithUserAndUserAndProject( ) .Where(task => task.ApplicationUser.Id== currentUserID) ;

            return View(tasks);
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
                         
                        StartTime = DateTime.Now, //this is defult time
                       
                    },
                    Users = _unitOfWork.UserRepositry.GetAll().ToList(),
                    Projects = _unitOfWork.ProjectRepositry.GetAll().ToList() 
                    
                };



                if (User.IsInRole("Admin"))
                {
                return View(newViewModel);

                }
                else
                {
                return View("CreateByManager", newViewModel);

                }

            }
            else
            {
                //come from project , the only diff here , to show list users that work on this project to avoid show list of user
                ViewBag.fromTask = "no";
 
                var newViewModel = new CreateTaskViewModel()
                {
                    Task = new TTask()
                    {
                        ProjectId = id_current_project,//this is must neot change !!!
                        StartTime = DateTime.Now,//this is defult time,
                        

                    },
                    Users = _unitOfWork.UserRepositry.GetAll().ToList()
                };

                if (User.IsInRole("Admin"))
                {
                    return View(newViewModel);

                }
                else
                {
                    return View("CreateByManager", newViewModel);

                }
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
                int type = Int32.Parse(Request.Form["type"]);
                int pri = Int32.Parse(Request.Form["priority"]);
                var ui = Request.Form["__UserId__"];
                var ri = Request.Form["__ReviewerId__"];
                var pi = Request.Form["__ProjectId__"];
                 //var id_current_project = Request.Form["id_current_project"];

                
                var user = _unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRolesAndFilesAndFinanicalWithFiles( ui);
                var reviewer = _unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRolesAndFilesAndFinanicalWithFiles(ri);

                /*
                 * Very important here
                 *when you add task to project .. the task must assgin   to user ..
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
                        
                        StartTime = task.StartTime,
                        Title = task.Title,
                        Status = (StatusEnum)Enum.ToObject(typeof(StatusEnum), stat),
                        TypeTask = (TypeTaskEnum)Enum.ToObject(typeof(TypeTaskEnum), type),
                        Priority = (PriorityEnum)Enum.ToObject(typeof(PriorityEnum), pri),
                        EstimatedTime = task.EstimatedTime,
                         EffortHours = task.EffortHours,
                        Ticket= task.Ticket,
                         Notes= task.Notes,
                        Creator = User.Identity.Name,
                        Owner = task.Owner,
                        ReviewerName = reviewer.FirstName,
                        ReviewerId = reviewer.Id
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
                     
                        StartTime = task.StartTime,
                        Title = task.Title,
                        Status = (StatusEnum)Enum.ToObject(typeof(StatusEnum), stat),
                        TypeTask = (TypeTaskEnum)Enum.ToObject(typeof(TypeTaskEnum), type),
                        Creator = User.Identity.Name,
                        Priority = (PriorityEnum)Enum.ToObject(typeof(PriorityEnum), pri),
                        EstimatedTime = task.EstimatedTime,
                        EffortHours = task.EffortHours,
                        Ticket = task.Ticket,
                        Notes = task.Notes,
                        Owner = task.Owner,
                        ReviewerName = reviewer.FirstName,
                        ReviewerId = reviewer.Id
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
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");


                }
                else
                {
                    //redirect to his project
                    return RedirectToAction("ShowProjectsForManager","Project");


                }

            }
            catch (Exception exception)
            {
                return Content(exception.Message);
            }
        }

        
        /// <summary>
        /// //////////////////////////////////////////this is for employee
        /// </summary>
        /// <param name="id_current_project"></param>
        /// <returns></returns>

        //// GET: TTask/Create
        ////this is comming from project controller 
        ////if this action is called from project  Edit View .. this id_current_project will not null 
        ////if this action is called from Task  View .. this id_current_project will be null 
        //public ActionResult CreateTaskFromEmployee(string id_current_project)
        //{
        //    //come from task page

        //    if (id_current_project.IsNullOrWhiteSpace())
        //    {
        //        ViewBag.fromTask = "yes";
        //        var newViewModel = new CreateTaskViewModel()
        //        {
        //            Task = new TTask()
        //            {

        //                StartTime = DateTime.Now, //this is defult time

        //            },
        //            Users = _unitOfWork.UserRepositry.GetAll().ToList(),
        //            Projects = _unitOfWork.ProjectRepositry.GetAll().ToList()

        //        };
        //        return View(newViewModel);

        //    }
        //    else
        //    {
        //        //come from project , the only diff here , to show list users that work on this project to avoid show list of user
        //        ViewBag.fromTask = "no";

        //        var newViewModel = new CreateTaskViewModel()
        //        {
        //            Task = new TTask()
        //            {
        //                ProjectId = id_current_project,//this is must neot change !!!
        //                StartTime = DateTime.Now,//this is defult time,


        //            },
        //            Users = _unitOfWork.UserRepositry.GetAll().ToList()
        //        };

        //        return View(newViewModel);
        //    }

        //}

        //// POST: TTask/Create
        //[HttpPost]
        //public ActionResult CreateTaskFromEmployee(TTask task, string id_current_project)
        //{

        //    // you should note that id_current_project comming from viewmodel !! not from form 

        //    try
        //    {
        //        int stat = Int32.Parse(Request.Form["status"]);
        //        int pri = Int32.Parse(Request.Form["priority"]);
        //        var ui = Request.Form["__UserId__"];
        //        var pi = Request.Form["__ProjectId__"];
        //        //var id_current_project = Request.Form["id_current_project"];


        //        var user = _unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRolesAndFilesAndFinanicalWithFiles(ui);

        //        /*
        //         * Very important here
        //         *when you add task to project .. the task must assgin   to user ..
        //         * and then when you add task to poject ..you must add this project to user !!
        //         *
        //         *
        //         *
        //         */
        //        //var project = _unitOfWork.ProjectRepositry.SingleOrDefault(pp => pp.Id == id_current_project);
        //        var dbContext = new ApplicationDbContext();

        //        if (id_current_project.IsNullOrWhiteSpace())
        //        {
        //            //thats mean we will create new task from out side project , no will be project id as forign_key



        //            var newTask = new TTask()
        //            {
        //                ProjectId = pi,
        //                ApplicationUser = user,
        //                Description = task.Description,

        //                StartTime = task.StartTime,
        //                Name = task.Name,
        //                Status = (StatusEnum)Enum.ToObject(typeof(StatusEnum), stat),
        //                Priority = (PriorityEnum)Enum.ToObject(typeof(PriorityEnum), pri),
        //                EstimatedTime = task.EstimatedTime,
        //                EffortHours = task.EffortHours,
        //                Ticket = task.Ticket,
        //                Notes = task.Notes,
        //                Creator = User.Identity.Name
        //                //Project =new Project() // here no need to add project object , just add his forign key
        //                //, but if you need to add project object , you must init it

        //            };


        //            //will add project to user to increse number of projects thats user work on
        //            var pro = _unitOfWork.ProjectRepositry.SingleOrDefault(p => p.Id == pi);

        //            if (!user.Projects.Contains(pro))
        //            {
        //                user.Projects.Add(pro);

        //            }
        //            _unitOfWork.TTaskRepositry.Add(newTask);
        //            _unitOfWork.Complete();

        //        }
        //        else
        //        {


        //            var newTask = new TTask()
        //            {
        //                ProjectId = id_current_project,
        //                ApplicationUser = user,
        //                Description = task.Description,

        //                StartTime = task.StartTime,
        //                Name = task.Name,
        //                Status = (StatusEnum)Enum.ToObject(typeof(StatusEnum), stat),
        //                Priority = (PriorityEnum)Enum.ToObject(typeof(PriorityEnum), pri),
        //                EstimatedTime = task.EstimatedTime,
        //                EffortHours = task.EffortHours,
        //                Ticket = task.Ticket,
        //                Notes = task.Notes
        //                //Project =new Project() // here no need to add project object , just add his forign key
        //                //, but if you need to add project object , you must init it

        //            };


        //            var pro = _unitOfWork.ProjectRepositry.SingleOrDefault(p => p.Id == id_current_project);

        //            if (!user.Projects.Contains(pro))
        //            {
        //                user.Projects.Add(pro);

        //            }


        //            _unitOfWork.TTaskRepositry.Add(newTask);
        //            _unitOfWork.Complete();

        //        }


        //        return RedirectToAction("CreateTaskFromEmployee");
        //    }
        //    catch (Exception exception)
        //    {
        //        return Content(exception.Message);
        //    }
        //}
        // GET: TTask/Edit/5
        public ActionResult Edit(int id)
        {
           

            var vm = new EditTaskViewModel()
            {
                Users = _unitOfWork.UserRepositry.GetAll().ToList(),
                Task = _unitOfWork.TTaskRepositry.GetTasksWithUserAndUserAndProject(id)
            };
            if (User.IsInRole("Admin"))
            {
                
                return View(vm);

            }
            else
            {
              
                return View("EditByManager", vm);

            }
        }

        // POST: TTask/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TTask task)
        {

            //var ApplicationUserId = Request.Form["ApplicationUserId"];
            //var ProjectId = Request.Form["ProjectId"];
            var  reviewerId  = Request.Form["__ReviewerId__"];
            try
            {
                //get new reviewer 
                var newReviewer =
                   _unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRolesAndFilesAndFinanicalWithFiles(
                        reviewerId);
                 
                var oldTask = _unitOfWork.TTaskRepositry.GetTasksWithUserAndUserAndProject(id);
                //very important
                /*
                 * if the manager approve the task and go to finanical and then , the employee or
                 * manager change/update the hour of this task .. then the task must be re approve ,
                 * so i will show the button approve agian , and to do that ,i will check if there  is
                 * any change or edit on hour!!
                 */

                if (oldTask.EffortHours != task.EffortHours || oldTask.EstimatedTime != task.EstimatedTime)
                {
                    //there is a change
                    oldTask.IsApproveByManager = false;
                    // oldTask.IsUpdate = true;

                }
                else
                {
                    oldTask.IsApproveByManager = true;

                }

                oldTask.Title = task.Title;
                oldTask.Priority = task.Priority;
                oldTask.Status = task.Status;
                oldTask. TypeTask = task. TypeTask;
                oldTask.StartTime = task.StartTime;
                 oldTask.Description = task.Description;
                oldTask.EstimatedTime = task.EstimatedTime;
                oldTask.EffortHours = task.EffortHours;
                oldTask.Ticket = task.Ticket;
                oldTask.Notes = task.Notes;
                oldTask.Owner = task.Owner;
                oldTask.IsApproveByManager = task.IsApproveByManager;
                oldTask.ReviewerName = newReviewer.FirstName;
                oldTask.ReviewerId = newReviewer.Id;

                 //check if task after update complete and if task has a reviewer , then make clone
                if (oldTask.Status.Equals(StatusEnum.Ended) && !oldTask.ReviewerId.IsNullOrWhiteSpace())
                {
                    var newTaskToClone = new TTask();

                    newTaskToClone.Title = oldTask.Title;
                    newTaskToClone.Priority = oldTask.Priority;
                    newTaskToClone.Status = StatusEnum.Not_Start;//this will be begin from not start
                    newTaskToClone.TypeTask =TypeTaskEnum.Review; //now this task is review 
                    newTaskToClone.StartTime = DateTime.Now;//date now
                    newTaskToClone.Description = oldTask.Description;
                    newTaskToClone.EstimatedTime = 0; //0 because this is new task

                    newTaskToClone.EffortHours = 0;//0 because this is new task
                    newTaskToClone.Ticket = oldTask.Ticket;
                    newTaskToClone.Notes = oldTask.Notes;
                    newTaskToClone.Owner = oldTask.Owner;
                    newTaskToClone.Creator = oldTask.Creator;
                    newTaskToClone.ProjectId = oldTask.ProjectId;
                    newTaskToClone.ReviewerId = oldTask.ReviewerId;



                    _unitOfWork.TTaskRepositry.Add(newTaskToClone);

                }
                _unitOfWork.Complete();

                if (User.IsInRole("Admin"))
                {

                    return RedirectToAction("Index");

                }
                else
                {
                    //redirect to his project
                    return RedirectToAction("ShowProjectsForManager", "Project");


                }
               
            }
            catch (Exception e)
            {
                return View();
            }
        }

       
        // GET: TTask/Delete/5
        public ActionResult Delete(int id)
        {
            var task = _unitOfWork.TTaskRepositry.GetTasksWithUserAndUserAndProject(id);

            if (User.IsInRole("Admin"))
            {

                return View(task);

            }
            else
            {
                return View("DeleteByManager",task);

            }
           
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

                if (User.IsInRole("Admin"))
                {

                    return RedirectToAction("Index");

                }
                else
                {
                    //redirect to his project
                    return RedirectToAction("ShowProjectsForManager", "Project");


                }

            }
            catch (Exception exception)
            {
                return View();
            }
        }

        // GET: TTask/Approve/5
        public ActionResult Approve(int id)
        {
            var task = _unitOfWork.TTaskRepositry.GetTasksWithUserAndUserAndProject(id);

            if (User.IsInRole("Admin"))
            {

                return View(task);

            }
            else
            {
                return View("ApproveByManager", task);

            }
           
        }

        // POST: TTask/Approve/5
        [HttpPost]
        public ActionResult Approve(int id, string taskId)
        {
            try
            {
                // TODO: Add delete logic here
                var ApplicationUserId = Request.Form["ApplicationUserId"];
                var ProjectId = Request.Form["ProjectId"];
                var task = _unitOfWork.TTaskRepositry.GetTasksWithUserAndUserAndProject(id);
                var user = _unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRolesAndFilesAndFinanicalWithFiles(ApplicationUserId);


                //update task to approve 
                task.IsApproveByManager = true;

                _unitOfWork.TTaskRepositry.Add(task);

                //now copy it to finanical status
                ////check if status change to end
                if (task.Status == StatusEnum.Ended)
                {
                    //add this  to financail status ,ok now do the real code !!

                    double payment = Math.Round(user.HourlyRate * task.EffortHours * .80, 2);
                    double totalEquation = user.HourlyRate * task.EffortHours;

                    var financial = new Financialstatus()
                    {
                        Id = task.Id.ToString(),
                        Date = task.StartTime, //must change, but for now for testing  !!
                        EstimatedHours = task.EstimatedTime,
                        EffortHours = task.EffortHours,

                        Pro__id = ProjectId,
                        Task__id = task.Id.ToString(),
                        User__id = ApplicationUserId,
                        Payment = payment,
                        Remain = Math.Round(totalEquation - payment, 2),
                        IsApproveByManager = true
                    };
                    try
                    {
                        _unitOfWork.FinancialRepositry.Add(financial);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
                _unitOfWork.Complete();

                

                if (User.IsInRole("Admin"))
                {

                    return RedirectToAction("Index");

                }
                else
                {
                    //redirect to his project
                    return RedirectToAction("ShowProjectsForManager", "Project");


                }

            }
            catch (Exception exception)
            {
                return View();
            }
        }



        // GET: TTask/CloneToReviewer/5
        public ActionResult CloneToReviewer(int id)
        {
            var newViewModel = new CreateTaskViewModel()
            {
                Task = _unitOfWork.TTaskRepositry.GetTasksWithUserAndUserAndProject(id),
                Users = _unitOfWork.UserRepositry.GetAll().ToList(),
                Projects = _unitOfWork.ProjectRepositry.GetAll().ToList()

            };

            if (User.IsInRole("Admin"))
            {

                return View(newViewModel);

            }
            else
            {
                return View("CloneToReviewerByEmployee", newViewModel);


            }

           
        }

        // POST: TTask/CloneToReviewer/5
        [HttpPost]
        public ActionResult CloneToReviewer(int id,   CreateTaskViewModel model)
        {

            
            try
            {
                var ApplicationUserId = Request.Form["__UserId__"];
                var __ProjectId__ = Request.Form["__ProjectId__"];

                var user = _unitOfWork.UserRepositry.GetUserWithProjectsAndTasksAndRolesAndFilesAndFinanicalWithFiles(ApplicationUserId);


                var newTask = new TTask();
                
                newTask.Title = model.Task.Title;
                newTask.Priority = model.Task.Priority;
                newTask.Status = model.Task.Status;
                 //newTask.TypeTask = model.Task.TypeTask; //to take inital value ((Coding))
                newTask.StartTime = model.Task.StartTime;
                newTask.Description = model.Task.Description;
                newTask.EstimatedTime = model.Task.EstimatedTime;
                
                newTask.EffortHours = model.Task.EffortHours;
                newTask.Ticket = model.Task.Ticket;
                newTask.Notes = model.Task.Notes;
                newTask.Owner = model.Task.Owner;
                newTask.Creator = model.Task.Creator;
                newTask.ProjectId = __ProjectId__;
                newTask.ApplicationUser= new ApplicationUser();
                newTask.ApplicationUser = user;

                 

                _unitOfWork.TTaskRepositry.Add(newTask);
                _unitOfWork.Complete();


                if (User.IsInRole("Admin"))
                {

                    return RedirectToAction("Index");


                }
                else
                {
                    return RedirectToAction("ShowTaskForEmployee");



                }


            }
            catch (Exception e )
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
