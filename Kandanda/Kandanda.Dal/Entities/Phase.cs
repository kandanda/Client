using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Kandanda.Dal.Entities
{
    [ExcludeFromCodeCoverage]
    public class Phase : IEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime From { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Until { get; set; }

        public int TournamentId { get; set; }
        
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
