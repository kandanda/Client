using System.ComponentModel.DataAnnotations;

namespace Kandanda.Dal.DataTransferObjects
{
    public class Organizer : IEntry
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(30)]
        public string Password { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
