using MyTaskManagement.Models;
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
using Microsoft.AspNet.Identity;
using MyTaskManagement.Core.Domain;
using Project = MyTaskManagement.Models.Project;

namespace MyTaskManagement.Controllers
{
   
    public class ProjectController : Controller
    {
        private UnitOfWork _unitOfWork = new UnitOfWork(new ApplicationDbContext());


        [Authorize(Roles = "Admin")]
        // GET: Project
        public ActionResult Index()
        {

            //get all projects
          var ProjectsWithClientAndUsers = _unitOfWork.ProjectRepositry.GetAllProjectsWithClientAndUsersAndTasksWithFiles();

            var vm = new IndexProjectViewModel();
            vm.Projects = new List<Project>();
            vm.Managers=  new List<ApplicationUser>();


            foreach (var project in ProjectsWithClientAndUsers)
            {   
                //add project to list
                vm.Projects.Add(project);
                //then add thier manager
                 
              vm.Managers.Add(GetManagerForProject(project.Id));  

            }
             return View(vm);
             

        }


        public ActionResult TaskOfProject(string id)
        {
            var pro = _unitOfWork.ProjectRepositry.GetProjectsWithClientAndUsersAndTasksWithFiles(id);


            return View(pro);
        }

        public ActionResult TaskOfProjectManager(string id)
        {
            var pro = _unitOfWork.ProjectRepositry.GetProjectsWithClientAndUsersAndTasksWithFiles(id);


            return View(pro);
        }

        // GET: Project/Details/5
        public ActionResult Details(string id)
        {
            return View(_unitOfWork.ProjectRepositry.GetProjectsWithClientAndUsersAndTasksWithFiles(id));
        } 
        
        // GET: Project/ShowProjectsForEmployee 

        public ActionResult ShowProjectsForEmployee( )
        {
            //get all projects
            var ProjectsWithClientAndUsers = _unitOfWork.ProjectRepositry.GetAllProjectsWithClientAndUsersAndTasksWithFiles() ;

            var vm = new IndexProjectViewModel();
            vm.Projects = new List<Project>();
            vm.Managers = new List<ApplicationUser>();


            foreach (var project in ProjectsWithClientAndUsers)
            {
                foreach (var user in project.Users)
                {
                    if (user.Id == User.Identity.GetUserId())
                    {
                        //add project to list
                        vm.Projects.Add(project);   
                        //then add thier manager

                        vm.Managers.Add(GetManagerForProject(project.Id));
                    }

                }
              

            }
            return View(vm);
        }
        // GET: Project/ShowProjectsForManager 
        public ActionResult ShowProjectsForManager( )
        {
            //get all projects
            var ProjectsWithClientAndUsers = _unitOfWork.ProjectRepositry.GetAllProjectsWithClientAndUsersAndTasksWithFiles() ;

            var vm = new IndexProjectViewModel
            {
                Projects = new List<Project>(),
                Managers = new List<ApplicationUser>()
            };


            foreach (var project in ProjectsWithClientAndUsers)
            {
                 
                    //then check if this user is manager
                var i = User.Identity.GetUserId();
                var e = GetManagerForProject(project.Id).Id;
                    if ( User.Identity.GetUserId()  == GetManagerForProject(project.Id).Id)
                    {
                        //add project to list
                        vm.Projects.Add(project);   
                        //then add thier manager

                        vm.Managers.Add(GetManagerForProject(project.Id));
                

                    }
              

            }
            return View(vm);
        }




        [Authorize(Roles = "Admin")]
        // GET: Project/Create
        public ActionResult Create()
        {

            var viewmodel = new CreateProjectViewModels()
            {
                 Project = new Project()
                 {
                     StartTime = DateTime.Now , DeadTime = DateTime.Now.AddDays(66)
                 },
                 Users = _unitOfWork.UserRepositry.GetAll().ToList(),
              

            };
            return View(viewmodel);
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProjectViewModels viewmodel, string __UserId__,  int status, HttpPostedFileBase upload)
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

                //var client = _unitOfWork.ClientRepositry.SingleOrDefault(c => c.Name == ClientId);


            

                //only corspond data will be set ,so users,clinet will be null
                var newProject = new Project()
                {
                    Id = viewmodel.Project.Id,
                    Name = viewmodel.Project.Name,
                    DeadTime = viewmodel.Project.DeadTime,
                    StartTime = viewmodel.Project.StartTime,
                    Status = (StatusEnum)Enum.ToObject(typeof(StatusEnum), status),
                    Description = viewmodel.Project.Description,
                    ClientId = 1,
                     Users = new List<ApplicationUser>(),//this is must
                     ProjectFiles = new List<MyProjectFile>()
                    
                };

                //get user manager
                if (!__UserId__.IsEmpty())
                {
                    var userManager = _unitOfWork.UserRepositry.SingleOrDefault(u => u.Id == __UserId__);
                    //Add this manger 
                    //newProject.Users.Add(user);
                    var pm = new ProjectManagerTable()
                    {
                        ManagerID = userManager.Id,
                        ProjectID = viewmodel.Project.Id


                    };

                    _unitOfWork.ProjectMangerRepositry.Add(pm);

                }

                //Guid is used as a file name on the basis that it can pretty much guarantee uniqueness
                if (upload != null && upload.ContentLength > 0)
                {
                    var photo = new MyProjectFile()
                    {
                        FileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName),
                        MyFileType = MyFileType.Photo
                    };


                    string physicalPath = Server.MapPath("~/images/" + photo.FileName);

                    // save image in folder
                    upload.SaveAs(physicalPath);
                    newProject.ProjectFiles.Add(photo);

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
            //All users
            var users = _unitOfWork.UserRepositry.GetAll().ToList();

            //current project
            var currentProject = _unitOfWork.ProjectRepositry.GetProjectsWithClientAndUsersAndTasksWithFiles(id);

            var currentManager = GetManagerForProject(currentProject.Id);
            //get current project

            var viewmodel = new CreateProjectViewModels()
            {
                Project = currentProject,
                Users = users,
                Clients = _unitOfWork.ClientRepositry.GetAll().ToList(),
                Manager = currentManager


            };


            if (User.IsInRole("Admin"))
            {

                return View(viewmodel);

            }
            else
            {

                return View("EditByManager", viewmodel);


            }
        }

        // POST: Project/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Project  project,    string ManagerId, HttpPostedFileBase upload)
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
                    
                  


                    //update manager in that table
                    var newProjectManager= new ProjectManagerTable()
                    {
                        ProjectID = id,
                        ManagerID = ManagerId
                    }; 
                    
                    //Guid is used as a file name on the basis that it can pretty much guarantee uniqueness
                    if (upload != null && upload.ContentLength > 0)
                    {
                        var photo = new MyProjectFile()
                        {
                            FileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName),
                            MyFileType = MyFileType.Photo
                        };


                        string physicalPath = Server.MapPath("~/images/" + photo.FileName);

                        // save image in folder
                        upload.SaveAs(physicalPath);
                        oldProject.ProjectFiles= new List<MyProjectFile>();
                       

                        oldProject.ProjectFiles.Add(photo);

                    }
                    _unitOfWork.ProjectMangerRepositry.Add(newProjectManager);//this is for add or update

                    _unitOfWork.Complete();

                }

                if (User.IsInRole("Admin"))
                {

                    return RedirectToAction("Index");


                }
                else
                {

                    return RedirectToAction("ShowProjectsForManager");


                }
            }
            catch (Exception exception)
            {
                return View();
            }
        }


        
        // GET: Project/DeleteUser/idUser/idProject
        public ActionResult DeleteUser(string idUser)
        {
            var deletedUser = _unitOfWork.UserRepositry.SingleOrDefault(user => user.Id == idUser);
            return View(deletedUser);
        }


        // POST: Project/DeleteUser/idUser/idProject
        [HttpPost]
        public ActionResult DeleteUser(string idUser, string idProject)
        {
            try
            {
                // TODO: Add delete logic here
                var deletedUser = _unitOfWork.UserRepositry.SingleOrDefault(user => user.Id == idUser);
                var project = _unitOfWork.ProjectRepositry.GetProjectsWithClientAndUsersAndTasksWithFiles(idProject);
                //Note ,,, You must delete user from project table, not  from user table...

                project.Users.Remove(deletedUser);
                _unitOfWork.Complete();
                return RedirectToAction("Edit" , new {id= idProject });
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        [Authorize(Roles = "Admin")]
        // GET: Project/DeleteProject/asdasd
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
                var project = _unitOfWork.ProjectRepositry.GetProjectsWithClientAndUsersAndTasksWithFiles(idProject);
                project.Users.Add(addUser);
                 _unitOfWork.ProjectRepositry.Add(project);
                 _unitOfWork.Complete();
                return RedirectToAction("Edit",new {id=idProject});
            }
            catch(Exception exception)
            {
                return View();
            }
        }


        private ApplicationUser GetManagerForProject(string projectId)
        {
            var result = _unitOfWork.ProjectMangerRepositry.SingleOrDefault(s => s.ProjectID == projectId);
            var managerUser = _unitOfWork.UserRepositry.SingleOrDefault(user => user.Id == result.ManagerID);
            return managerUser;
        }
    }
    


}
