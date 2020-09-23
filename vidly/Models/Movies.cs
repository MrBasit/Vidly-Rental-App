using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace vidly.Models
{
    public class Movies
    {
        public Movies()
        {

        }

        [Required]
        public string Name { get; set; }
        
        public int Id { get; set; }

        [Required]
        public int MoviesGenereId { get; set; }

        [Required]
        [Display(Name="Date Of Release")]
        public DateTime? ReleaseDate { get; set; }
        
        [Required]
        [DateOfReleaseEarlierThanDateOfAddition]
        [Display(Name = "Date Of Addition")]
        public DateTime? AddedDate { get; set; }
        
        [Required]
        [Range(1,20)]
        [Display(Name="Movies In Stock")]
        public int? NumberInStock { get; set; }
        
        public MoviesGenere MoviesGeneres { get; set; }
        
    }
}