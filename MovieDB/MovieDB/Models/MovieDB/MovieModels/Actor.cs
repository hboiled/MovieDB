using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieDB.Models.MovieDB.MovieModels
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Status Status { get; set; }
        public byte[] Photo { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }

    public enum Status
    {
        Active,        
        Inactive,
        Retired
    }
}