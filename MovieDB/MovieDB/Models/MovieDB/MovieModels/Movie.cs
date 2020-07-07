using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieDB.Models.MovieDB.MovieModels
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TimeSpan RunTime { get; set; }
        public Director Director { get; set; }
        public Genre Genre { get; set; }

        public virtual int DirectorId { get; set; }
        
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