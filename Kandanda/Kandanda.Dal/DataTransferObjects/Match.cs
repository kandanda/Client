using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kandanda.Dal.DataTransferObjects
{
    public class Match : IEntry
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime From { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Until { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}