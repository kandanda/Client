using System.Collections.Generic;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer.PhaseGenerators
{
    internal interface IPhaseGenerator
    {
        IEnumerable<Match> GenerateMatches();
    }
}
