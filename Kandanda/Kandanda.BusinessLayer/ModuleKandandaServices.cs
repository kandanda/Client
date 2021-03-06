﻿using System;
using System.Diagnostics.CodeAnalysis;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace Kandanda.BusinessLayer
{
    [ExcludeFromCodeCoverage]
    public class ModuleKandandaServices : IModule
    {
        private readonly IUnityContainer _container;

        public ModuleKandandaServices(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container
                .RegisterInstance(new KandandaDbContextLocator())
                .RegisterType<IMatchService, MatchService>(new ContainerControlledLifetimeManager())
                .RegisterType<ITournamentService, TournamentService>(new ContainerControlledLifetimeManager())
                .RegisterType<IParticipantService, ParticipantService>(new ContainerControlledLifetimeManager())
                .RegisterType<IPhaseService, PhaseService>(new ContainerControlledLifetimeManager())
                .RegisterType<IPublishTournamentService, PublishTournamentService>(new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(new Uri("https://www.kandanda.ch/"), typeof(IPublishTournamentRequestBuilder)))
                .RegisterType<IPublishTournamentRequestBuilder, PublishTournamentRequestBuilder>(
                    new ContainerControlledLifetimeManager())
                .RegisterType<IMenubarService, MenubarService>( new ContainerControlledLifetimeManager());
        }
    }
}
