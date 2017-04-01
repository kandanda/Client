using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace Kandanda.BusinessLayer
{
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
                .RegisterType<KandandaDbContext>(new ContainerControlledLifetimeManager())
                .RegisterType<IMatchService, MatchService>(new ContainerControlledLifetimeManager())
                .RegisterType<ITournamentService, TournamentService>(new ContainerControlledLifetimeManager())
                .RegisterType<IParticipantService, ParticipantService>(new ContainerControlledLifetimeManager())
                .RegisterType<IPublishTournamentService, PublishTournamentService>(
                    new ContainerControlledLifetimeManager())
                .RegisterType<IPublishTournamentRequestBuilder, PublishTournamentRequestBuilder>(
                    new ContainerControlledLifetimeManager());
        }
    }
}
