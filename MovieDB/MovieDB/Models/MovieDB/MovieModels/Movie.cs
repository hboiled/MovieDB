using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieDB.Models.MovieDB.MovieModels
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Run Time")]
        public TimeSpan RunTime { get; set; }
        public int DirectorId { get; set; }
        public Genre Genre { get; set; }

        
        public virtual Director Director { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }
    }

    public enum Genre
    {
        Action,
        Romance,
        Comedy,
        Drama,
        Period
    }
}