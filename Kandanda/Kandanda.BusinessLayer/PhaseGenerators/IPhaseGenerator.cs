using System.Collections.Generic;
using Kandanda.Dal.Entities;

namespace Kandanda.BusinessLayer.PhaseGenerators
{
    internal interface IPhaseGenerator
    {
        IEnumerable<Match> GenerateMatches();
    }
}
