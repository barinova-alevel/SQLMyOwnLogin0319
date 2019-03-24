using Autofac;
using Autofac.Integration.Mvc;
using MyOwnLogin.BusinesLogic.Interfaces;
using MyOwnLogin.BusinesLogic.Managers;
using MyOwnLoginSqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace MyOwnLogin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<ContactManager>().As<IContactManager>();
            builder.RegisterType<AccountManager>().As<IAccountManager>();
            builder.RegisterInstance<IContactRepository>(new ContactRepositories());

            builder.RegisterInstance<IContactRepository>(new ContactRepositories());
            // builder.RegisterInstance<IAccountRepository>(new AccountRepositories());
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
