using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using vidly.Controllers;
using vidly.Models;
using System.Data.SqlClient;
using AutoMapper;
using vidly.Dtos;

namespace vidly.Controllers.Api
{
    public class MovieController : ApiController
    {
        IMapper mapper;
        ApplicationDbContext _context;
        public MovieController()
        {
            var config = new MapperConfiguration(
                 c => {
                     c.CreateMap<Movies, MovieDto>();
                     c.CreateMap<MovieDto, Movies>();
                     c.CreateMap<MoviesGenere, MoviesGenereDto>();
                 }
                );
            mapper = config.CreateMapper();
            _context = new ApplicationDbContext();
           
        }
        public IHttpActionResult GetMovies()
        {
            IEnumerable<MovieDto> moviedtoslist = _context.Movies.Include("MoviesGeneres").Select(mapper.Map<Movies, MovieDto>);
            return Ok(moviedtoslist);
        }
        public IHttpActionResult GetMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movieInDb == null)
                return NotFound();
            return Ok(mapper.Map<MovieDto>(movieInDb));
        }

        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto moviedto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _context.Movies.Add(mapper.Map<Movies>(moviedto));
            _context.SaveChanges();
            return Created(new Uri(Request.RequestUri + "/" + "id"), moviedto);
        }

        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto moviedto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                return NotFound();
            mapper.Map<MovieDto, Movies>(moviedto, movieInDb);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return Ok();
        }

    }
}
