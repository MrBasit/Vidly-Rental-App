using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace vidly.Models
{
    public class Customers
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public int Id { get; set; }

        [Display(Name = "Subscribe to newsletter")]
        public bool IsSubscribed { get; set; }

        [Required]
        [Display(Name = "Select Membership Type")]
        public int MembershipTypeId { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        //[Min18YearsIfMember]
        public DateTime? DateOfBirth { get; set; }

        public MembershipType MembershipType { get; set; }

        public static readonly int unknown = 0;
        public static readonly int PayAsYouGo = 1;
        public static readonly int requiredAge = 18;

        public Customers()
        {
            
        }
    }
}