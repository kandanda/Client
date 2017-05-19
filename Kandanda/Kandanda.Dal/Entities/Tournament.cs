using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Kandanda.Dal.Entities
{
    [ExcludeFromCodeCoverage]
    public class Tournament : IEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        
        public int GroupSize { get; set; }

        public int KoType { get; set; }

        public bool DetermineThird { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime From { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Until { get; set; }

        public int PublicId { get; set; }

        [StringLength(255)]
        public string SharedLink { get; set; }
        
        public TimeSpan PlayTimeStart { get; set; }

        public TimeSpan PlayTimeEnd { get; set; }
        
        public TimeSpan GameDuration { get; set; }

        public TimeSpan BreakBetweenGames { get; set; }

        public TimeSpan LunchBreakStart { get; set; }

        public TimeSpan LunchBreakEnd { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime FinalsFrom { get; set; }

        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday { get; set; }

        public bool Saturday { get; set; }

        public bool Sunday { get; set; }

        public int OrganizerId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public bool IsActive { get; set; }
    }
}