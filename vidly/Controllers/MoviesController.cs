using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidly.ViewModel;
using vidly.Models;
using System.Data.SqlClient;
using System.Data.Entity;

namespace vidly.Controllers
{
    public class MoviesController : Controller
    {
        ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(c => c.MoviesGeneres).ToList();
            if (User.IsInRole(RoleNames.canManageMovies))
                return View("List", movies);
            return View("ReadOnlyList", movies);
        }
        [Authorize(Roles = RoleNames.canManageMovies)]
        public ActionResult Edit(int Id)
        {
            Movies movie = _context.Movies.SingleOrDefault(m => m.Id == Id);
            NewMovieViewModel newmovieviewmodel = new NewMovieViewModel();
            newmovieviewmodel.Movies = movie;
            newmovieviewmodel.MoviesGenres = _context.MoviesGenere.ToList();

            return View("New",newmovieviewmodel);
        }

        [Authorize(Roles = RoleNames.canManageMovies)]
        public ActionResult New()
        {
            NewMovieViewModel moviewViewModel = new NewMovieViewModel() {
                MoviesGenres = _context.MoviesGenere.ToList(),
                Movies=new Movies()
            };
            
            
            return View(moviewViewModel);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(NewMovieViewModel newmoviemodel)
        
        {
            if (!ModelState.IsValid)
            {
                var moviesmodel = new NewMovieViewModel
                {
                    Movies = newmoviemodel.Movies,
                    MoviesGenres = _context.MoviesGenere.ToList()
                };
                return View("New", moviesmodel);
            }


            if (newmoviemodel.Movies.Id == 0)
            {
                //add new movie
                _context.Movies.Add(newmoviemodel.Movies);
                _context.SaveChanges();
            }

            if (newmoviemodel.Movies.Id != 0)
            {
                //update existing movie
                var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == newmoviemodel.Movies.Id);
                movieInDb.Name = newmoviemodel.Movies.Name;
                movieInDb.AddedDate = newmoviemodel.Movies.AddedDate;
                movieInDb.ReleaseDate = newmoviemodel.Movies.ReleaseDate;
                movieInDb.NumberInStock = newmoviemodel.Movies.NumberInStock;
                movieInDb.MoviesGenereId = newmoviemodel.Movies.MoviesGenereId;
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Movies");
        }

    }
}