using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chinook.Data.Entities
{
    [Table("Artists")]
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }

        [StringLength(120, ErrorMessage ="Name is limited to 120 characters")]
        public string Name { get; set; }
        //Navigation Properties
        //Artist (parent) points to many Albums (children)
        //collection of records
        public virtual ICollection<Album> Albums { get; set; }
    }
}
