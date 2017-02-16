﻿using System;
using System.IO;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Routing;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Nirvana.Configuration;
using Nirvana.Web.Generation;
using Nirvana.Web.Startup;
using Owin;
using TechFu.Nirvana.EventStoreSample.Infrastructure.IoC;
using TechFu.Nirvana.EventStoreSample.WebAPI.CommandProcessor.Configuration;

[assembly: OwinStartup(typeof(Startup))]

namespace TechFu.Nirvana.EventStoreSample.WebAPI.CommandProcessor.Configuration
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configure(x => WebApiConfig.Register(x, a => { }));
            RouteConfig.RegisterRoutes(RouteTable.Routes, x => { });


            StructureMapAspNet.Configure(Assembly.GetExecutingAssembly()).ForWebApi();
            var config = new NirvanaCommandProcessorEndpointConfiguration();

            NirvanaSetup.Configure()
                .UsingControllerName(config.ControllerAssemblyName, config.RootNamespace)
                .WithAssembliesFromFolder(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"))
                .SetAdditionalAssemblyNameReferences(config.AssemblyNameReferences)
                .SetRootTypeAssembly(typeof(Services.Shared.InfrastructureRoot).Assembly)
                .SetAttributeMatchingFunction(config.AttributeMatchingFunction)
                .SetDependencyResolver(config.GetService, config.GetAllServices)
                .ForCommands(MediationStrategy.InProcess, MediationStrategy.ForwardToQueue, MediationStrategy.None)
                .ForInternalEvents(MediationStrategy.InProcess, MediationStrategy.ForwardToQueue, MediationStrategy.None)
                .ForUiNotifications(MediationStrategy.ForwardToWeb, MediationStrategy.ForwardToWeb, MediationStrategy.None)
                .ForQueries(MediationStrategy.InProcess, MediationStrategy.InProcess)
                .BuildConfiguration()
                ;

            app.UseCors(CorsOptions.AllowAll);


            var thirdPartyReferences = new Assembly[0];
            new CqrsApiGenerator().LoadAssembly(thirdPartyReferences);

            var dynamicApiSelector = new DynamicApiSelector(GlobalConfiguration.Configuration, config.InlineControllerTypes);
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector), dynamicApiSelector);

        }
    }
}