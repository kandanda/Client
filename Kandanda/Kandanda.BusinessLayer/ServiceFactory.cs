using System;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;

namespace Kandanda.BusinessLayer
{
    [Obsolete("Use Dependency Injection with Unity Constructors instead.")]
    public sealed class ServiceFactory
    {
        private readonly KandandaDbContext _context;

        public ServiceFactory()
        {
            _context = new KandandaDbContext(new SampleDataDbInitializer());
        }

        public IMatchService CreateMatchService()
        {
            return new MatchService(_context);
        }

        public ITournamentService CreateTournamentService()
        {
            return new TournamentService(_context);
        }

        public IParticipantService CreateParticipantService()
        {
            return new ParticipantService(_context);
        }

        /*
        public IPublishTournamentService CreatePublishTournamentService()
        {
            return new PublishTournamentService(_context);
        }
        */

        public IPublishTournamentRequestBuilder CreatePublishTournamentRequestBuilder()
        {
            return new PublishTournamentRequestBuilder(_context);   
        } 
    }
}
