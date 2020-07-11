using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieDB.Data;
using MovieDB.Models.MovieDB.MovieModels;
using MovieDB.Models.MovieDB.ViewModels;
using MovieDB.UtilMethods;

namespace MovieDB.Controllers.MovieData
{
    public class MoviesController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: Movies
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index"); //new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            MovieViewModel movieViewModel = new MovieViewModel
            {
                Directors = db.Directors.ToList()
            };

            return View(movieViewModel);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieViewModel movie, HttpPostedFileBase file)
        {
            try 
            {
                if (ModelState.IsValid) 
                { 
                    Movie newMovie = new Movie
                    {
                        Title = movie.Title,
                        ReleaseDate = movie.ReleaseDate,
                        RunTime = movie.RunTime,
                        DirectorId = movie.DirectorId,
                        Genre = movie.Genre                        
                    };

                    if (file != null && file.ContentLength > 0)
                    {
                        using (var reader = new System.IO.BinaryReader(file.InputStream))
                        {
                            newMovie.Poster = reader.ReadBytes(file.ContentLength);
                        }

                    }

                    db.Movies.Add(newMovie);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException de)
            {
                ModelState.AddModelError("Error", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index"); //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            
            MovieViewModel movieViewModel = new MovieViewModel
            {
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate.Date,
                RunTime = movie.RunTime,
                Genre = movie.Genre,
                DirectorId = movie.DirectorId,
                Directors = db.Directors.ToList()
            };

            return View(movieViewModel);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ReleaseDate,RunTime,Genre,DirectorId")] Movie movie,
            HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(file.InputStream))
                    {
                        movie.Poster = reader.ReadBytes(file.ContentLength);
                    }

                }

                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index"); 
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //[ETag, OutputCache(Duration = 3600, VaryByParam = "filename")]
        public ActionResult Thumbnail(int? id)
        {
            //Image image = ImgProc.ByteArrayToImage(img);
            //return new ImageResult(image.BestFit(291, 285));
            var poster = db.Movies.Find(id).Poster;
            Image image;

            if (poster != null)
            {
                image = ImgProc.ByteArrayToImage(poster);   
            } else
            {
                //image = Image.FromFile(@"C:\Users\61406\Pictures\posters\image-placeholder.png");
                string path = Server.MapPath(@"~\Content\image-placeholder.png");
                image = Image.FromFile(
                        path
                    );
            }

            return new ImageResult(image.BestFit(150, 150));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
