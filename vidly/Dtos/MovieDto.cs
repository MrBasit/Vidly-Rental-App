using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace vidly.Dtos
{
    public class MovieDto
    {
        [Required]
        public string Name { get; set; }

        public int Id { get; set; }

        [Required]
        public int MoviesGenereId { get; set; }

        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Required]
       // [DateOfReleaseEarlierThanDateOfAddition]
        public DateTime? AddedDate { get; set; }

        [Required]
        [Range(1, 20)]
        public int? NumberInStock { get; set; }

        public MoviesGenereDto MoviesGeneres { get; set; }

    }
}