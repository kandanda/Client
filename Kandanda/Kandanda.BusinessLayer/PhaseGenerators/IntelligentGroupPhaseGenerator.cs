using System;
using System.Collections.Generic;
using System.Linq;
using Kandanda.Dal.Entities;

namespace Kandanda.BusinessLayer.PhaseGenerators
{
    public sealed class IntelligentGroupPhaseGenerator : IPhaseGenerator
    {
        public DateTime GroupPhaseStart { get; set; }

        public DateTime GroupPhaseEnd { get; set; }

        public TimeSpan BreakBetweenGames { get; set; }

        public TimeSpan LunchBreakStart { get; set; }

        public TimeSpan LunchBreakEnd { get; set; }
        
        public TimeSpan PlayTimeStart { get; set; }

        public TimeSpan PlayTimeEnd { get; set; }

        public TimeSpan GameDuration { get; set; }

        public int GroupSize { get; set; }

        public int NumberOfTeams { get; set; }

        public IEnumerable<Match> GenerateMatches()
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

            var morningMatches = GenerateMatchesBetweenTimes(playTimeStart, lunchBreakStart);
            var eveningMatches = GenerateMatchesBetweenTimes(lunchBreakEnd, playTimeEnd);

            return morningMatches.Concat(eveningMatches);
        }

        private IEnumerable<Match> GenerateMatchesBetweenTimes(DateTime startTime, DateTime endTime)
        {
            var time = startTime;

            while (time + GameDuration + BreakBetweenGames <= endTime)
            {
                yield return CreateMatch(time);
                time = time.Add(GameDuration + BreakBetweenGames);
            }
        }

        private Match CreateMatch(DateTime startTime)
        {
            return new Match
            {
                From = startTime,
                Until = startTime + GameDuration
            };
        }
    }
}
