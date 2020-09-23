using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace vidly.Models
{
    public class Min18YearsIfMember:ValidationAttribute
    {
        protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            var customer = (Customers)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == Customers.PayAsYouGo || customer.MembershipTypeId == Customers.unknown)
                return ValidationResult.Success;

            //if (customer.MembershipTypeId == 0)
            //    return new ValidationResult("Membership Type is required!!");

            int age = DateTime.Now.Year - ((DateTime)customer.DateOfBirth).Year;
            if (age >= Customers.requiredAge)
                return ValidationResult.Success;
            else
                return new ValidationResult("Your Age should must be 18");
        }
    }
}