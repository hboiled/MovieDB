using MovieDB.Data;
using MovieDB.UtilMethods;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieDB.Controllers.MovieData
{
    public class ThumbnailController : Controller
    {
        private MovieContext db = new MovieContext();

        public ActionResult MoviesThumbnail(int? id)
        {
            var poster = db.Movies.Find(id).Poster;
            Image image;

            if (poster != null)
            {
                image = ImgProc.ByteArrayToImage(poster);
            }
            else
            {
                string path = Server.MapPath(@"~\Content\image-placeholder.png");
                image = Image.FromFile(
                        path
                    );
            }

            return new ImageResult(image.BestFit(150, 150));
        }

        public ActionResult ActorsThumbnail(int? id)
        {
            var photo = db.Actors.Find(id).Photo;
            Image image;

            if (photo != null)
            {
                image = ImgProc.ByteArrayToImage(photo);
            }
            else
            {
                string path = Server.MapPath(@"~\Content\image-placeholder.png");
                image = Image.FromFile(
                        path
                    );
            }

            return new ImageResult(image.BestFit(150, 150));
        }

        public ActionResult DirectorsThumbnail(int? id)
        {
            var photo = db.Directors.Find(id).Photo;
            Image image;

            if (photo != null)
            {
                image = ImgProc.ByteArrayToImage(photo);
            }
            else
            {
                string path = Server.MapPath(@"~\Content\image-placeholder.png");
                image = Image.FromFile(
                        path
                    );
            }

            return new ImageResult(image.BestFit(150, 150));
        }
    }
}