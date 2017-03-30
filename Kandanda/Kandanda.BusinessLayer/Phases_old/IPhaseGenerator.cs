using System.Collections.Generic;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer.Phases
{
    internal interface IPhaseGenerator
    {
        IEnumerable<Match> GenerateMatches();
    }
}
