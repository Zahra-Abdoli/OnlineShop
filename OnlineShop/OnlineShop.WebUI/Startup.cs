using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using OnlineShop.WebUI.Models;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Hosting;

[assembly: OwinStartupAttribute(typeof(OnlineShop.WebUI.Startup))]
namespace OnlineShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          
            ConfigureAuth(app);
      

        }
        
    }
    }

