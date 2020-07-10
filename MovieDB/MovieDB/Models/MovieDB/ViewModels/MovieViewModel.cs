using MovieDB.Models.MovieDB.MovieModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace MovieDB.Models.MovieDB.ViewModels
{
    public class MovieViewModel
    {        
        public string Title { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Run Time")]
        public TimeSpan RunTime { get; set; }
        [Display(Name = "Director")]
        public int DirectorId { get; set; }
        public Genre Genre { get; set; }
        public HttpPostedFileBase Poster { get; set; } 
        public ICollection<Director> Directors { get; set; }
        //public ICollection<Actor> Actors { get; set; }
    }
}