using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace vidly.Models
{
    public class DateOfReleaseEarlierThanDateOfAddition:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
 	        var movie = (Movies)validationContext.ObjectInstance;
            
            if(movie.AddedDate==null)
                return ValidationResult.Success;

            if(movie.AddedDate<movie.ReleaseDate)
                return new ValidationResult("Added date should be later than release date");

            return ValidationResult.Success;
        }
    }
}