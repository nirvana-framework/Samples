﻿using System;
using System.Linq;
using Nirvana.Domain;
using Nirvana.Web.Controllers;
using TechFu.Nirvana.EventStoreSample.Infrastructure.IoC;
using TechFu.Nirvana.EventStoreSample.Services.Shared;

namespace TechFu.Nirvana.EventStoreSample.WebAPI.CommandProcessor.Configuration
{
    public class NirvanaCommandProcessorEndpointConfiguration
    {

        public string RootNamespace => "TechFu.Nirvana.EventStoreSample.WebAPI.CommandProcessor";
        public string ControllerAssemblyName => RootNamespace + ".dll";

        public  Type RootType => typeof(RootType);
        public  Type AggregateAttributeType => typeof(AggregateRootAttribute);

        public  Func<string, object, bool> AttributeMatchingFunction
            => (x, y) => x == ((AggregateRootAttribute) y).RootType.ToString();

        public  string[] AssemblyNameReferences => new[]
        {
            "TechFu.Nirvana.EventStoreSample.Domain.dll",
            "TechFu.Nirvana.EventStoreSample.Infrastructure.dll",
            "TechFu.Nirvana.EventStoreSample.Services.Shared.dll",
            ControllerAssemblyName
        };

        public Type[] InlineControllerTypes => new[] { typeof(ApiUpdatesController) };


        public object GetService(Type serviceType) => InternalDependencyResolver.GetInstance(serviceType);
        public object[] GetAllServices(Type serviceType) => InternalDependencyResolver.GetAllInstances(serviceType).ToArray();
    }
}