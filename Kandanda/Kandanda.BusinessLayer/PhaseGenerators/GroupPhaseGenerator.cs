using System;
using System.Collections.Generic;
using System.Linq;
using Kandanda.Dal.Entities;

namespace Kandanda.BusinessLayer.PhaseGenerators
{
    public sealed class GroupPhaseGenerator : IPhaseGenerator
    {
        private const int ShuffleIterationCount = 5000;
        private readonly List<Participant> _participants;
        private readonly int _groupSize;
        
        public GroupPhaseGenerator(IEnumerable<Participant> participants, int groupSize)
        {
            var participantList = participants.ToList();

            if (participants == null || participantList.Count == 0)
                throw new ArgumentException($"{nameof(participants)} must not be empty or null");

            if (groupSize < 1)
                throw new ArgumentException($"{nameof(groupSize)} must not be null");

            _participants = participants.ToList();
            _groupSize = groupSize;
        }
        
        public IEnumerable<Match> GenerateMatches()
        {
            var participantCount = _participants.Count;
            var groupCount = (participantCount - 1) / _groupSize + 1;
            var shuffledParticipants = GetShuffledParticipants();
            var matches = new List<Match>();

            for (var groupIndex = 0; groupIndex < groupCount; ++groupIndex)
            {
                GenerateAllPairs(shuffledParticipants, groupIndex, matches);
            }

            return matches;
        }

        private void GenerateAllPairs(Participant[] participants, int groupIndex, List<Match> matches)
        {
            var startGroupIndex = groupIndex * _groupSize;
            var endGroupIndex = Math.Min(startGroupIndex + _groupSize, participants.Length);

            for (var firstIndex = startGroupIndex; firstIndex < endGroupIndex - 1; ++firstIndex)
            {
                for (var secondIndex = firstIndex + 1; secondIndex < endGroupIndex; ++secondIndex)
                {
                    var firstParticipant = participants[firstIndex];
                    var secondParticipant = participants[secondIndex];

                    var match = new Match
                    {
                        FirstParticipantId = firstParticipant.Id,
                        SecondParticipantId = secondParticipant.Id
                    };

                    matches.Add(match);
                }
            }
        }

        private Participant[] GetShuffledParticipants()
        {
            var shuffledParticipants = new Participant[_participants.Count];
            _participants.CopyTo(shuffledParticipants);

            var random = new Random();
            var participantCount = _participants.Count;

            for (var iteration = 0; iteration < ShuffleIterationCount; ++iteration)
            {
                var firstRandomIndex = random.Next(participantCount);
                var secondRandomIndex = random.Next(participantCount);

                if (firstRandomIndex != secondRandomIndex)
                {
                    var swapParticipant = shuffledParticipants[firstRandomIndex];
                    shuffledParticipants[firstRandomIndex] = shuffledParticipants[secondRandomIndex];
                    shuffledParticipants[secondRandomIndex] = swapParticipant;
                }
            }

            return shuffledParticipants;
        }
    }
}
