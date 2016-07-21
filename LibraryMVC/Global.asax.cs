
using Autofac;
using Autofac.Integration.Mvc;
using EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebModelServices.BookModel;
using WebModelServices.BorrowModel;
using WebModelServices.ReportsModel;
using WebModelServices.ReportsModel.NewFolder1;
using WebModelServices.UserModel.contracts.DTO;
using WebModelServices.UserModel.contracts.Interface;

namespace LibraryMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerDependency();
            builder.RegisterType<BookLibraryEF>()
                .As<BookLibraryEF>();
            builder.RegisterType<BookService>()
                .As<IBookService>()
                .InstancePerDependency();
            builder.RegisterType<BorrowService>()
                .As<IBorrowService>()
                .InstancePerDependency();
            builder.RegisterType<ReportService>()
                .As<IReportService>()
                .InstancePerDependency();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
