﻿using Kandanda.Dal.Entities;

namespace Kandanda.BusinessLayer.ServiceInterfaces
{
    public interface IPublishTournamentRequestBuilder {
        string BuildJsonRequest(Tournament tournament);
    }
}