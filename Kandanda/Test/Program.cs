using System;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.Dal;
using Kandanda.Dal.Entities;

namespace Test
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            var ts = new TournamentService(new KandandaDbContextLocator());
            var ps = new ParticipantService(new KandandaDbContextLocator());

            var t = ts.CreateEmpty("ABC");
            t.GroupSize = 4;

            Participant p = null;

            for (var i = 'h'; i < 'h' + 16; ++i)
            {
                p = ps.CreateEmpty(((char) i).ToString());
                ts.EnrolParticipant(t, p);
            }

            ts.GenerateGroups(t);
            
            
            string gn = ts.GetGroupByParticipant(t, p);

            Console.WriteLine();
            Console.WriteLine(gn);

            Console.ReadKey(true);
        }
    }
}

