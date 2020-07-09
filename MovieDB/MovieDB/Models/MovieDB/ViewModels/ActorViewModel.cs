﻿using MovieDB.Models.MovieDB.MovieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieDB.Models.MovieDB.ViewModels
{
    public class ActorViewModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Status Status { get; set; }
        public int MovieId { get; set; }
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}