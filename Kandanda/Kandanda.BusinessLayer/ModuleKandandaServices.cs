using System.Data.Entity;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace Kandanda.BusinessLayer
{
    public class ModuleKandandaServices : IModule
    {
        private readonly IUnityContainer _container;

        public ModuleKandandaServices(IUnityContainer container)
        {
            _container = container;
        }

        public KandandaDbContext BuildKandandaContext()
        {
            return new KandandaDbContext(new SampleDataDbInitializer());
        }

        public void Initialize()
        {
            _container
                .RegisterInstance(typeof(KandandaDbContext), BuildKandandaContext())
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
