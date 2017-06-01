using System;
using System.Collections.Generic;
using System.Linq;
using Kandanda.Dal.Entities;

namespace Kandanda.BusinessLayer.PhaseGenerators
{
    public sealed class IntelligentGroupPhaseGenerator
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

        public void AddParticipants(IEnumerable<Participant> participants)
        {
            _participants.AddRange(participants);
        }

        public Dictionary<string, List<Participant>> GenerateGroups()
        {
            int groupCount;

            if (!CalculateGroupCount(out groupCount))
            {
                throw new ArgumentException($"Cannot generate schedule with group size {GroupSize} and number of teams {_participants.Count}");
            }

            var groups = GenerateGroups(groupCount).ToList();

            var groupMap = new Dictionary<string, List<Participant>>();

            for (var groupIndex = 0; groupIndex < groups.Count; ++groupIndex)
            {
                var groupName = Convert.ToChar(groupIndex + 'A').ToString();

                groupMap[groupName] = new List<Participant>();

                foreach (var participant in groups[groupIndex])
                {
                    groupMap[groupName].Add(participant);
                }
            }

            return groupMap;
        }
        
        public IEnumerable<Match> GenerateMatches(Dictionary<string, List<Participant>> groupMap)
        {
            CheckGameDuration();
            CheckGroupSize();

            var groupes = groupMap.Select(group => group.Value).ToList();

            var timeslots = GenerateTimeslots().ToList();
            var matches = GetAllMatches(groupes).ToList();

            if (timeslots.Count < matches.Count)
            {
                throw new ArgumentException($"There are only {timeslots.Count} timeslots but {matches.Count} are needed.");
            }

            for (var matchIndex = 0; matchIndex < matches.Count; ++matchIndex)
            {
                matches[matchIndex].From = timeslots[matchIndex];
                matches[matchIndex].Until = matches[matchIndex].From + GameDuration;
            }

            return matches;
        }

        private IEnumerable<Match> GetAllMatches(List<List<Participant>> groups)
        {
            return groups.SelectMany(GetAllMatchesByGroup);
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

        private int GetMatchCountByParticipant(Dictionary<Participant, int> groupMatchCount, Participant participant)
        {
            if (participant == null)
            {
                return 0;
            }

            if (!groupMatchCount.ContainsKey(participant))
            {
                groupMatchCount[participant] = 0;
                return 0;
            }

            return groupMatchCount[participant];
        }

        private Tuple<int, int> LeastPlayedParticipantsPair(Dictionary<Participant, int> groupMatchCount, List<Participant> participants)
        {
            var leastPlayedIndex = -1;
            var secondLeastPlayedIndex = -1;
            
            for (var participantIndex = 0; participantIndex < participants.Count; ++participantIndex)
            {
                var currentParticipant = participants[participantIndex];

                var leastPlayedParticipant = leastPlayedIndex != -1 ? 
                    participants[leastPlayedIndex] : null;

                var secondLeastPlayedParticipant = secondLeastPlayedIndex != -1
                    ? participants[secondLeastPlayedIndex] : null;

                var matchCount = GetMatchCountByParticipant(groupMatchCount, currentParticipant);
                var leastMatchCount = GetMatchCountByParticipant(groupMatchCount, leastPlayedParticipant);
                var secondLeastMatchCount = GetMatchCountByParticipant(groupMatchCount, secondLeastPlayedParticipant);

                if (leastPlayedIndex == -1 || matchCount < leastMatchCount)
                {
                    secondLeastPlayedIndex = leastPlayedIndex;
                    leastPlayedIndex = participantIndex;
                }
                else if (secondLeastPlayedIndex == -1 || matchCount < secondLeastMatchCount)
                {
                    secondLeastPlayedIndex = participantIndex;
                }
            }

            return Tuple.Create(leastPlayedIndex, secondLeastPlayedIndex);
        }
        
        private IEnumerable<Match> GetAllMatchesByGroup(List<Participant> group)
        {
            var teamCount = group.Count;
            var matchCount = teamCount * (teamCount - 1) / 2;

            var groupMatchCount = new Dictionary<Participant, int>();
            var groupPairList = new List<Tuple<int, int>>();

            for (var matchIndex = 0; matchIndex < matchCount; ++matchIndex)
            {
                var leastPlayedPair = LeastPlayedParticipantsPair(groupMatchCount, group);

                while (groupPairList.Contains(leastPlayedPair))
                {
                    var secondIndex = (leastPlayedPair.Item2 + 1) % group.Count;
                    leastPlayedPair = Tuple.Create(leastPlayedPair.Item1, secondIndex);
                }
                
                var leastParticipant = group[leastPlayedPair.Item1];
                var secondLeastParticipant = group[leastPlayedPair.Item2];

                ++groupMatchCount[leastParticipant];
                ++groupMatchCount[secondLeastParticipant];

                groupPairList.Add(leastPlayedPair);
                
                var match = CreateMatch(DateTime.Now, leastParticipant.Id, secondLeastParticipant.Id);

                yield return match;
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

        private IEnumerable<DateTime> GenerateTimeslots()
        {
            for (var date = GroupPhaseStart.Date; date <= GroupPhaseEnd.Date; date = date.AddDays(1))
            {
                foreach (var timeslot in GenerateTimeslotsByDay(date))
                {
                    yield return timeslot;
                }
            }
        }

        private IEnumerable<DateTime> GenerateTimeslotsByDay(DateTime date)
        {
            var day = date.Date;
            
            var playTimeStart = day.Add(PlayTimeStart);
            var playTimeEnd = day.Add(PlayTimeEnd);

            var lunchBreakStart = day.Add(LunchBreakStart);
            var lunchBreakEnd = day.Add(LunchBreakEnd);

            var morningMatches = GenerateTimeslotsBetween(playTimeStart, lunchBreakStart);
            var eveningMatches = GenerateTimeslotsBetween(lunchBreakEnd, playTimeEnd);
            
            return morningMatches.Concat(eveningMatches);
        }

        private IEnumerable<DateTime> GenerateTimeslotsBetween(DateTime startTime, DateTime endTime)
        {
            var time = startTime;

            while (time + GameDuration <= endTime)
            {
                yield return time;
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

        private void CheckGameDuration()
        {
            if (GameDuration == TimeSpan.Zero)
            {
                throw new ArgumentException("Game duration must be greater than zero");
            }
        }

        private void CheckGroupSize()
        {
            if (GroupSize < MinimumGroupSize || GroupSize > MaximumGroupSize)
            {
                throw new ArgumentException($"Group size must be between {MinimumGroupSize} and {MaximumGroupSize}");
            }
        }
    }
}
