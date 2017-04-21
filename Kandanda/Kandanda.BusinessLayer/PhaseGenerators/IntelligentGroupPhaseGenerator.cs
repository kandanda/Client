using System;
using System.Collections.Generic;
using System.Linq;
using Kandanda.Dal.Entities;

namespace Kandanda.BusinessLayer.PhaseGenerators
{
    public sealed class IntelligentGroupPhaseGenerator : IPhaseGenerator
    {
        private const int MinimumGroupSize = 3;
        private const int MaximumGroupSize = 7;

        private readonly List<Participant> _participants;

        public IntelligentGroupPhaseGenerator()
        {
            _participants = new List<Participant>();
        }
        
        public DateTime GroupPhaseStart { get; set; }

        public DateTime GroupPhaseEnd { get; set; }

        public TimeSpan BreakBetweenGames { get; set; }

        public TimeSpan LunchBreakStart { get; set; }

        public TimeSpan LunchBreakEnd { get; set; }
        
        public TimeSpan PlayTimeStart { get; set; }

        public TimeSpan PlayTimeEnd { get; set; }

        public TimeSpan GameDuration { get; set; }

        public int GroupSize { get; set; }

        public void AddParticipant(Participant participant)
        {
            _participants.Add(participant);
        }

        public IEnumerable<Match> GenerateMatches()
        {
            CheckGroupSize(GroupSize);

            int groupCount;

            if (!CalculateGroupCount(out groupCount))
            {
                throw new ArgumentException($"Cannot generate schedule with group size {GroupSize} and number of teams {_participants.Count}");
            }
            
            var groups = GenerateGroups(groupCount);

            foreach (var group in groups)
            {
                foreach (var match in GetAllMatchesByGroup(group))
                {
                    Console.WriteLine(match.FirstParticipantId + ", " + match.SecondParticipantId);
                }
            }

            return null;
        }

        private List<List<Participant>> GenerateGroups(int groupCount)
        {
            var differenceOptimalNumberOfTeams = _participants.Count - GroupSize * groupCount;
            var groupDelta = Math.Sign(differenceOptimalNumberOfTeams);
            var groups = new List<List<Participant>>();

            var shuffledParticipants = GetShuffledParticipants();
            var startGroupIndex = 0;

            for (var index = 0; index < groupCount; ++index)
            {
                var groupSize = GroupSize;
                
                if (index < Math.Abs(differenceOptimalNumberOfTeams))
                {
                    groupSize += groupDelta;
                }
                
                var groupArray = new Participant[groupSize];
                Array.Copy(shuffledParticipants, startGroupIndex, groupArray, 0, groupSize);

                var group = groupArray.ToList();

                startGroupIndex += groupSize;

                groups.Add(group);
            }

            return groups;
        }
        
        private IEnumerable<Match> GetAllMatchesByGroup(List<Participant> group)
        {
            var teamCount = group.Count;
            
            for (var firstIndex = 0; firstIndex < teamCount; ++firstIndex)
            {
                for (var secondIndex = firstIndex + 1; secondIndex < teamCount; ++secondIndex)
                {
                    yield return CreateMatch(DateTime.Now, firstIndex, secondIndex);
                }
            }
        }
        
        private bool CalculateGroupCount(out int groupCount)
        {
            var allowedGroupCountList = new List<int> { 4, 8, 16 };

            foreach (var currentGroupCount in allowedGroupCountList)
            {
                if (CheckGroupRange(currentGroupCount))
                {
                    groupCount = currentGroupCount;
                    return true;
                }
            }

            groupCount = 0;
            return false;
        }

        private bool CheckGroupRange(int groupCount)
        {
            var standardGroupCount = GroupSize * groupCount;

            var minAllowedTeamCount = standardGroupCount - GroupSize + 1;
            var maxAllowedTeamCount = standardGroupCount + GroupSize - 1;

            return _participants.Count >= minAllowedTeamCount &&
                _participants.Count <= maxAllowedTeamCount;
        }

        private IEnumerable<Match> GenerateTimeslots()
        {
            for (var date = GroupPhaseStart.Date; date <= GroupPhaseEnd.Date; date = date.AddDays(1))
            {
                foreach (var match in GenerateMatchesByDay(date))
                {
                    yield return match;
                }
            }
        }

        private IEnumerable<Match> GenerateMatchesByDay(DateTime date)
        {
            var day = date.Date;
            
            var playTimeStart = day.Add(PlayTimeStart);
            var playTimeEnd = day.Add(PlayTimeEnd);

            var lunchBreakStart = day.Add(LunchBreakStart);
            var lunchBreakEnd = day.Add(LunchBreakEnd);

            var morningMatches = GenerateMatchesBetween(playTimeStart, lunchBreakStart);
            var eveningMatches = GenerateMatchesBetween(lunchBreakEnd, playTimeEnd);
            
            return morningMatches.Concat(eveningMatches);
        }

        private IEnumerable<Match> GenerateMatchesBetween(DateTime startTime, DateTime endTime)
        {
            var time = startTime;

            while (time + GameDuration <= endTime)
            {
                yield return CreateMatch(time, 0, 0);
                time = time.Add(GameDuration + BreakBetweenGames);
            }
        }

        private Match CreateMatch(DateTime startTime, int firstParticipantId, int secondParticipantId)
        {
            return new Match
            {
                From = startTime,
                Until = startTime + GameDuration,
                FirstParticipantId = firstParticipantId,
                SecondParticipantId = secondParticipantId
            };
        }

        private Participant[] GetShuffledParticipants()
        {
            const int shuffleIterationsPerTeam = 555;

            var shuffledParticipants = new Participant[_participants.Count];
            _participants.CopyTo(shuffledParticipants);

            var random = new Random();
            var participantCount = _participants.Count;

            var shuffleIterations = shuffleIterationsPerTeam * participantCount;

            for (var iteration = 0; iteration < shuffleIterations; ++iteration)
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

        private static void CheckGroupSize(int groupSize)
        {
            if (groupSize < MinimumGroupSize || groupSize > MaximumGroupSize)
            {
                throw new ArgumentException($"Group size must be between {MinimumGroupSize} and {MaximumGroupSize}");
            }
        }
    }
}
