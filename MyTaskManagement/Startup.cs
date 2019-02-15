using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using MyTaskManagement.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyTaskManagement.Startup))]
namespace MyTaskManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           
        }

        
    }
}
