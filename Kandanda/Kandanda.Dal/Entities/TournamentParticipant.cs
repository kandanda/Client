using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kandanda.Dal.Entities
{
    [ExcludeFromCodeCoverage]
    public class TournamentParticipant : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int TournamentId { get; set; }

        public int ParticipantId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
