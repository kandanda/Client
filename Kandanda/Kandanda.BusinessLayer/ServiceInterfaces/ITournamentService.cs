﻿using System.Collections.Generic;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer.ServiceInterfaces
{
    public interface ITournamentService
    {
        Tournament CreateEmpty(string name);
        Phase GeneratePhase(Tournament tournament, int groupSize);
        void EnrolParticipant(Tournament tournament, Participant participant);
        void DeregisterParticipant(Tournament tournament, Participant participant);
        List<Participant> GetParticipantsByTournament(Tournament tournament);
        List<Tournament> GetAllTournaments();
        List<Phase> GetPhasesByTournament(Tournament tournament);
        List<Match> GetMatchesByPhase(Phase phase);
        void DeleteTournament(Tournament tournament);
    }
}