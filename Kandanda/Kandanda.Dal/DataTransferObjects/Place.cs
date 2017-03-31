using System.ComponentModel.DataAnnotations;

namespace Kandanda.Dal.DataTransferObjects
{
    public class Place : IEntry
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Comment { get; set; }

        public int TournamentId { get; set; }
        
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
