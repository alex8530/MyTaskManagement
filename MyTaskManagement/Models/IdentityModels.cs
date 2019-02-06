﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyTaskManagement.Core.Domain;

namespace MyTaskManagement.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public bool IsAcceptedOnCondition { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        

        public ICollection<Project> Projects { get; set; }
        public   ICollection<TTask> Tasks { get; set; }
        public ICollection<Financialstatus> TaFinancialstatussks { get; set; }


        public Byte[] Image { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<TTask>  Tasks { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Financialstatus> Financialstatuses { get; set; }
        //public DbSet<ApplicationUser> ApplicationUser { get; set; }
       

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

       
      

    }
}