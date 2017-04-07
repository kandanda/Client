﻿using System.ComponentModel.DataAnnotations;

namespace Kandanda.Dal.Entities
{
    public class Participant : IEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Captain { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Email { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}