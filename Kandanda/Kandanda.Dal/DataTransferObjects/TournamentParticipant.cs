using System.ComponentModel.DataAnnotations;

namespace Kandanda.Dal.DataTransferObjects
{
    public class TournamentParticipant : IEntry
    {
        [Key]
        public int Id { get; set; }

        public int TournamentId { get; set; }

        public int ParticipantId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
