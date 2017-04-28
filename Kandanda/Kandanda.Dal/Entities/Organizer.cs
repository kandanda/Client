using System.ComponentModel.DataAnnotations;

namespace Kandanda.Dal.Entities
{
    public class Organizer : IEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(32)]
        public string Password { get; set; }

        [StringLength(255)]
        public string LoginToken { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
