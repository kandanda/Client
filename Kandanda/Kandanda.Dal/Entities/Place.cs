using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kandanda.Dal.Entities
{
    [ExcludeFromCodeCoverage]
    public class Place : IEntity
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
