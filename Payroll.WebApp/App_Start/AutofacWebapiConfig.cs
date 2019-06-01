using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Payroll.Data;
using Payroll.Data.Infrastructure;
using Payroll.Data.Repositories;
using System.Data.Entity;
using Payroll.Services;
using Payroll.Services.Abstract;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Reflection;
using Payroll.WebApp.Infrastructure.Core;
//using Payroll.Web.Infrastructure.Extensions;

namespace Payroll.WebApp.App_Start
{

    //*****  SEE http://autofac.readthedocs.io/en/latest/getting-started/index.html
    public class AutofacWebapiConfig
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // EF PayrollContext
            builder.RegisterType<PayrollContext>()
                   .As<DbContext>()
                   .InstancePerRequest();

            builder.RegisterType<DbFactory>()
                .As<IDbFactory>()
                .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            //****************************************************************************

            //builder.RegisterType<EmployeeRepository>()
            //    .As<IEmployeeRepository>()
            //    .InstancePerRequest();

            builder.RegisterGeneric(typeof(EntityBaseRepository<>))
                     .As(typeof(IEntityBaseRepository<>))
                     .InstancePerRequest();

            // Services
            builder.RegisterType<EncryptionService>()
                .As<IEncryptionService>()
                .InstancePerRequest();

            builder.RegisterType<MembershipService>()
                .As<IMembershipService>()
                .InstancePerRequest();

            //// Generic Data Repository Factory
            builder.RegisterType<DataRepositoryFactory>()
                .As<IDataRepositoryFactory>().InstancePerRequest();


            Container = builder.Build();

            return Container;
        }
    }
}
