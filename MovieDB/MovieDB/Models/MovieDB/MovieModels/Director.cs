using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieDB.Models.MovieDB.MovieModels
{
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        [Display(Name = "Directing Credits")]
        public virtual ICollection<Movie> Movies { get; set; }
    }
}