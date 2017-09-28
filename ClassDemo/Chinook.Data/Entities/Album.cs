using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Chinook.Data.Entities
{
    [Table("Albums")]
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }

        [Required(ErrorMessage ="Title is a required field.")]
        [StringLength(160,ErrorMessage ="Title is limited to 160 characters.")]
        public string Title { get; set; }

        public int ArtistId { get; set; }

        public int ReleaseYear { get; set; }

        [StringLength(50,ErrorMessage ="Release Label is limited to 50 characters.")]
        public string ReleaseLabel { get; set; }

        //Navigation Properties
        //album (child) points to artist (parent)
        //singular relationship
        public virtual Artist Artist { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
