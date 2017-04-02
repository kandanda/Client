using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer.ServiceInterfaces
{
    public interface IPublishTournamentRequestBuilder {
        string BuildJsonRequest(Tournament tournament);
    }
}