using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kandanda.Dal.DataTransferObjects
{
    public class Participant : IEntry
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("Participants")]
        public virtual ICollection<Tournament> Tournaments { get; set; } = new HashSet<Tournament>();

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}