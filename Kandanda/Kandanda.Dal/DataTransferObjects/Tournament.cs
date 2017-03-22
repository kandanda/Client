using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kandanda.Dal.DataTransferObjects
{
    public class Tournament : IEntry
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Phase> Phases { get; set; }

        public virtual ICollection<Place> Places { get; set; }

        [InverseProperty("Tournaments")]
        public virtual ICollection<Participant> Participants { get; set; } = new HashSet<Participant>();

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}