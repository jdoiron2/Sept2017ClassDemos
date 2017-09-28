
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chinook.Data.Entities
{
    [Table("Tracks")]
    public class Track
    {
        [Key]

        public int TrackId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public int? AlbumId { get; set; }

        public int MediaTypeId { get; set; }

        public int? GenreId { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        public int Milliseconds { get; set; }

        public int? Bytes { get; set; }

        [Column(TypeName = "numeric")]
        public decimal UnitPrice { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Album Album { get; set; }
        public virtual MediaType MediaType { get; set; }
        public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; }
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }
    }
}
