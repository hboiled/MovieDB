using MovieDB.Models.MovieDB.MovieModels;
using MovieDB.UtilMethods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace MovieDB.Data
{
    public class SeedData : System.Data.Entity.DropCreateDatabaseIfModelChanges<MovieContext>
    {
        protected override void Seed(MovieContext context)
        {
            var directors = new List<Director>
            {
                new Director
                {
                    Name="D1",
                    Age=29,
                    Movies=new List<Movie>(),
                },
                new Director
                {
                    Name="D5",
                    Age=46,
                    Movies=new List<Movie>(),
                },
                new Director
                {
                    Name="D2",
                    Age=34,
                    Movies=new List<Movie>(),
                }
            };

            directors.ForEach(d => context.Directors.Add(d));
            context.SaveChanges();

            var actors = new List<Actor>
            {
                new Actor
                {
                    Name="A1",
                    Age=19,
                    Status = Status.Active,
                    Movies=new List<Movie>(),
                },
                new Actor
                {
                    Name="A2",
                    Age=26,
                    Status = Status.Inactive,
                    Movies=new List<Movie>(),
                },
                new Actor
                {
                    Name="A3",
                    Age=30,
                    Status = Status.Active,
                    Movies=new List<Movie>(),
                },
                new Actor
                {
                    Name="A4",
                    Age=70,
                    Status = Status.Retired,
                    Movies=new List<Movie>(),
                }
            };

            actors.ForEach(a => context.Actors.Add(a));
            context.SaveChanges();

            var m1poster = ImgProc.ImageToByteArray(Image.FromFile(@"C:\Users\61406\Pictures\posters\goodfellas.jpg"));

            var movies = new List<Movie>
            {
                new Movie
                {
                    Title="M1",
                    ReleaseDate=DateTime.Parse("2000-09-01"),
                    RunTime=new TimeSpan(1,30,0),
                    Genre=Genre.Action,
                    DirectorId = 1,
                    Actors=new List<Actor> {},
                    Poster=m1poster
                },
                new Movie
                {
                    Title="M2",
                    ReleaseDate=DateTime.Parse("1990-09-01"),
                    RunTime=new TimeSpan(2,39,0),
                    Genre=Genre.Drama,
                    DirectorId = 1,
                    Actors=new List<Actor> {}
                },
                new Movie
                {
                    Title="M3",
                    ReleaseDate=DateTime.Parse("1988-02-21"),
                    RunTime=new TimeSpan(1,45,0),
                    Genre=Genre.Period,
                    DirectorId = 2,
                    Actors=new List<Actor> {}
                },
                new Movie
                {
                    Title="M4",
                    ReleaseDate=DateTime.Parse("2006-02-11"),
                    RunTime=new TimeSpan(1,35,0),
                    Genre=Genre.Romance,
                    DirectorId = 3,
                    Actors=new List<Actor> {}
                }
            };

            movies.ForEach(m => context.Movies.Add(m));
            context.SaveChanges();

            UpdateActors(context, "A1", "M1");
            UpdateActors(context, "A2", "M2");
            UpdateActors(context, "A3", "M3");
            UpdateActors(context, "A4", "M3");
            UpdateActors(context, "A4", "M1");
            UpdateActors(context, "A2", "M4");
            UpdateActors(context, "A1", "M4");
        }

        public void UpdateActors(MovieContext context, string actorName, string movieName)
        {
            var actor = context.Actors.SingleOrDefault(a => a.Name.Equals(actorName));
            var movie = context.Movies.SingleOrDefault(m => m.Title.Equals(movieName));

            var actorHasMovie = actor.Movies.SingleOrDefault(m => m.Title.Equals(movieName)); // Find movie in actor's movies
            var movieHasActor = movie.Actors.SingleOrDefault(a => a.Name.Equals(actorName)); // Find actor in movie's actors

            if (actorHasMovie == null && movieHasActor == null)
            {
                actor.Movies.Add(movie);
                movie.Actors.Add(actor);
            }
        }

    }
}