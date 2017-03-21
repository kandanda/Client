﻿using System.Data.Entity;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.Dal
{
    public class KandandaDbContext : DbContext
    {
        public KandandaDbContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<KandandaDbContext>());
        }

        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<Participant> Participants { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Phase> Phases { get; set; }
    }
}