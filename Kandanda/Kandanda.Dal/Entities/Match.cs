using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kandanda.Dal.Entities
{
    public class Match : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime From { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Until { get; set; }

        public int FirstParticipantId { get; set; }

        public int SecondParticipantId { get; set; }

        public int PhaseId { get; set; }

        public int PlaceId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}