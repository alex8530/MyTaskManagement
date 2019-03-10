using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using MyTaskManagement.Core.Domain;
using MyTaskManagement.Models;

namespace MyTaskManagement.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web;

    internal sealed class Configuration : DbMigrationsConfiguration< ApplicationDbContext>
    {
        List<ApplicationUser> _users = new List<ApplicationUser>();


        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed( ApplicationDbContext context)
        {

            //Add Test User
            InitUsers(context);
            context.SaveChanges();

            //Add Clients
            var clientList = new List<Client>
            {
                new Client()
                {
                    Name = "Client 1" ,
                    Email = "Client1@gmail.com",
                    Address = "Alnasser Street",
                    AdditionInformation = "I want many things",

                },
                new Client()
                {
                    Name = "Client 2" ,
                    Email = "Client2@gmail.com",
                    Address = "Alnasser Street",
                    AdditionInformation = "I want many things2",

                },
                new Client()
                {
                    Name = "Client 3" ,
                    Email = "Client3@gmail.com",
                    Address = "Alnasser Street",
                    AdditionInformation = "I want many things3",

                }

            };
            clientList.ForEach(c => context.Clients.AddOrUpdate(client => client.Id, c));
            context.SaveChanges();

            //Add projects
            var projects = new List<Project>
            {
                new Project()
                {
                    Id = "Project0",
                    Name = "WallPaper App ",
                    StartTime = DateTime.Now,
                    DeadTime = DateTime.Now.AddDays(20),
                    Status = StatusEnum.Not_Start,
                    Description = "Description 0",
                    ClientId = clientList[0].Id,
                    Users = _users,
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                               FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project1",
                    Name = "Family App ",
                    StartTime = DateTime.Now,
                    DeadTime = DateTime.Now.AddDays(60),
                    Status = StatusEnum.InProgress,
                    Description = "Description 1",

                    ClientId = clientList[0].Id,
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                              FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                } ,
                new Project()
                {
                    Id = "Project2",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description 2",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    ClientId = clientList[2].Id,
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                             FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project3",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description 3",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    ClientId = clientList[2].Id,
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                              FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project4",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description 4",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    ClientId = clientList[2].Id,
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                               FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project5",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description 5",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    ClientId = clientList[2].Id,
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                             FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project6",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description 6",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    ClientId = clientList[2].Id,
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                            FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project7",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description7",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    Client = clientList[2],
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                    FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project8",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description 8",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    Client = clientList[2],
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                               FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project9",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description9",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    Client = clientList[0],
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                              FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project10",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description 10",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    Client = clientList[0],
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                              FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project11",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description 11",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    Client = clientList[0],
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                              FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project12",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description 12",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    Client = clientList[1],
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                          FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project13",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description 13",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    Client = clientList[1],
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                              FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project 14",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description 14",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    Client = clientList[1],
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                               FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project15",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description 15",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    Client = clientList[1],
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                              FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project16",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description 16",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    Client = clientList[1],
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                             FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project17",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description 2",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    Client = clientList[1],
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                             FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                },
                new Project()
                {
                    Id = "Project18",
                    Name = "Movies ",
                    StartTime = DateTime.Now,
                    Description = "Description 2",

                    DeadTime = DateTime.Now.AddDays(555),
                    Status = StatusEnum.Not_Start,
                    Client = clientList[1],
                      ProjectFiles= new List<MyProjectFile>
                      {
                          new MyProjectFile()
                          {
                              FileName = "~/images/ccad8040-b454-4fa0-8c51-d66984dda0dd.png",
                              MyFileType = MyFileType.Photo
                          }
                      }

                }
            };
            projects.ForEach(p => context.Projects.AddOrUpdate(project => project.Id, p));
            context.SaveChanges();



            //add manager for each project
            foreach (var   project in projects)
            {
                var pm = new ProjectManagerTable()
                {
                    //one manager for all project to test only !!
                    ManagerID = _users[0].Id,
                    ProjectID = project.Id


                };
                context.ProjectManagerTable.Add(pm);

            }


         

        }

        private void InitUsers( ApplicationDbContext context)
        {
 
            //Here we create a Admin super user who will maintain the website                 
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //////////////////////////////////Create Default Roles////////////////////////////////////////
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin role   
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);


            }
            //   Creating ProjectManager role   
            if (!roleManager.RoleExists("ProjectManager"))
            {
                var role = new IdentityRole();
                role.Name = "ProjectManager";
                roleManager.Create(role);

            }

            //   Creating Employee role   
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

            }
            //////////////////////////////////Create Default Users////////////////////////////////////////

            var user1 = new ApplicationUser
            {
                UserName = "Admin",
                Email = "Admin@Admin.com",
                FirstName = "Alex Abu Harb",
                LastName = "Abu Admin",
                IsAcceptedOnCondition = true,
                JopTitle = "SuperAdmin , Co-Founder & CTO",
                O_T_H_Rate = 4,
                HourlyRate = 7.9
                
            };
           var chkUser1= userManager.Create(user1, "123123");

            //Add default User to Role Admin  
            if (chkUser1.Succeeded)
            {
                var result1 = userManager.AddToRole(user1.Id, "Admin");

            }

            var user2 = new ApplicationUser
            {
                UserName = "Alex",
                Email = "Alex@Alex.com",
                FirstName = "Alex",
                LastName = "Abu Alex",
                IsAcceptedOnCondition = true,
                JopTitle = "ProjectManager , Android Developer",
                O_T_H_Rate = 2.6,
                HourlyRate = 5.9
            };
            var chkUser2  = userManager.Create(user2, "123123");
            //Add default User to Role ProjectManager  
            if (chkUser2.Succeeded)
            {
                var result1 = userManager.AddToRole(user2.Id, "ProjectManager");

            }



            var user3 = new ApplicationUser
            {
                UserName = "Ahmed",
                Email = "Ahmed@Ahmed.com",
                FirstName = "Ahmed",
                LastName = "Abu Ahmed",
                IsAcceptedOnCondition = true,
                JopTitle = " Front-end Developer",
                O_T_H_Rate =8,
                HourlyRate = 2.9

            };
 
            var chkUser3 = userManager.Create(user3, "123123");
            //Add default User to Role Employee  
            if (chkUser3.Succeeded)
            {
                var result1 = userManager.AddToRole(user3.Id, "Employee");

            }


            var user4 = new ApplicationUser
            {
                UserName = "Sameer",
                Email = "Sameer@Sameer.com",
                FirstName = "Sameer",
                LastName = "Abu Sameer",
                IsAcceptedOnCondition = true,
                JopTitle = " Back-end Developer",
                O_T_H_Rate = 44,
                HourlyRate = 29.6


            };

            var chkUser4 = userManager.Create(user4, "123123");
            //Add default User to Role Employee  
            if (chkUser4.Succeeded)
            {
                var result1 = userManager.AddToRole(user4.Id, "Employee");

            }

             
            var user5 = new ApplicationUser
            {
                UserName = "FoFo",
                Email = "FoFo@FoFo.com",
                FirstName = "FoFo",
                LastName = "Abu FoFo",
                IsAcceptedOnCondition = true,
                JopTitle = " Graghic Designer",
                O_T_H_Rate = 0.9,
                HourlyRate = 5.8


            };
 
            var chkUser5 = userManager.Create(user5, "123123");
            //Add default User to Role Employee  
            if (chkUser5.Succeeded)
            {
                var result1 = userManager.AddToRole(user5.Id, "Employee");

            }



            _users.Add (user1);
            _users.Add (user2);
            _users.Add (user3);
            _users.Add (user4);
            _users.Add (user5);
        }

    }


   
    }
