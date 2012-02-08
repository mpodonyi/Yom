using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using Yom.Web.Services;

namespace Yom.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            ); 

        }

        protected void Application_Start()
        {
            ContainerSetup();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }



        private void ContainerSetup()
        {
            var builder = new ContainerBuilder();

            RegisterObjects(builder);

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }

        private void RegisterObjects(ContainerBuilder builder)
        {
            //builder.RegisterAssemblyTypes(typeof(IAuthenticationService).Assembly)
            //                .Where(t => t.Name.EndsWith("Service"))
            //                .AsImplementedInterfaces()
            //                .InstancePerHttpRequest();


            builder.RegisterType<UserService>().As<IUserService>().InstancePerHttpRequest();

            builder.RegisterType<ReferenceUserService>().As<IReferenceUserService>().InstancePerHttpRequest();

            builder.RegisterType<DebtService>().As<IDebtService>().InstancePerHttpRequest();
        }

    }
}