using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vidly.Models;

namespace vidly.ViewModel
{
    public class NewMovieViewModel
    {
        public IEnumerable<MoviesGenere> MoviesGenres { get; set; }
        public Movies Movies { get; set; }
    }
}