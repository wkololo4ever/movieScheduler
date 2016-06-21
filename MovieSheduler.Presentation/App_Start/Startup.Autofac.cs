﻿using System.Data.Entity;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using MovieSheduler.Application.Infrastructure;
using MovieSheduler.Application.SheduleRecord;
using MovieSheduler.Domain.Infrastructure;
using MovieSheduler.EntityFramework;
using MovieSheduler.EntityFramework.Infrastructure;
using MovieSheduler.EntityFramework.Repositories.SheduleRecord;

namespace MovieSheduler.Presentation
{
	public partial class Startup
	{
	    public IContainer ConfigureAutofac()
	    {
	        var builder=new ContainerBuilder();
	        builder.RegisterControllers(typeof (MvcApplication).Assembly);

            //TODO коммент
	        builder.RegisterAssemblyTypes(typeof (SheduleRecordService).Assembly)
	            .Where(t => t.Name.EndsWith("Service"))
	            .AsImplementedInterfaces();

            //TODO коммент
            builder.RegisterAssemblyTypes(typeof(SheduleRecordRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

	        builder.RegisterType<MovieShedulerContext>().InstancePerLifetimeScope().As<DbContext>();
	        builder.RegisterType<MovieShedulerContext>().InstancePerLifetimeScope();
	        builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>();
	        builder.RegisterType<ValidationDictionary>().As<IValidationDictionary>();

	        IContainer container = builder.Build();
	        DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
	        return container;
	    }
	}
}