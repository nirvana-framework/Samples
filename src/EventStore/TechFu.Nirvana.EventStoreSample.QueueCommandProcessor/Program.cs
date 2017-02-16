﻿using System;
using System.Reflection;
using Nirvana.Configuration;
using Nirvana.CQRS.Queue;
using TechFu.Nirvana.EventStoreSample.Infrastructure.IoC;
using Topshelf;

namespace TechFu.Nirvana.EventStoreSample.QueueCommandProcessor
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            HostFactory.Run(x =>                                 //1
            {
                x.Service<QueueService>(s =>                     
                {
                    s.ConstructUsing(name => new QueueService());     //3
                    s.WhenStarted(tc => tc.Start());              //4
                    s.WhenStopped(tc => tc.Stop());               //5
                });
                x.RunAsLocalSystem();                            //6

                x.SetDescription("Sample Topshelf Host");        //7
                x.SetDisplayName("Stuff");                       //8
                x.SetServiceName("Stuff");                       //9
            });



          





        }

        
    }

    public class QueueService
    {
        readonly IQueueController _queueController;
        public QueueService()
        {
            StructureMapAspNet.Configure(Assembly.GetExecutingAssembly());
            var config = new CommandProcessorNirvanaConfig();
            NirvanaSetup.Configure()
                .SetAdditionalAssemblyNameReferences(config.AssemblyNameReferences)
                .SetRootTypeAssembly( typeof(Services.Shared.InfrastructureRoot).Assembly)
                .SetAttributeMatchingFunction(config.AttributeMatchingFunction)
                .SetDependencyResolver(config.GetService,config.GetAllServices)
                .ForCommands(MediationStrategy.ForwardToWeb, MediationStrategy.ForwardToQueue, MediationStrategy.ForwardToQueue)
                .ForInternalEvents(MediationStrategy.ForwardToWeb, MediationStrategy.ForwardToQueue, MediationStrategy.ForwardToQueue)
                .ForUiNotifications(MediationStrategy.ForwardToWeb, MediationStrategy.ForwardToQueue, MediationStrategy.ForwardToQueue)
                .BuildConfiguration()
                ;

            _queueController = InternalDependencyResolver.GetInstance<IQueueController>();



        }

        public void Start()
        {
            _queueController.StartAll();
        }

        public void Stop()
        {
            _queueController.StopAll();
            Console.WriteLine("Press Any Key to continue");
            Console.ReadLine();
        }

    }
}