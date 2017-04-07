using System.ComponentModel.DataAnnotations;

namespace Kandanda.Dal.Entities
{
    public class SportDescription : IEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
