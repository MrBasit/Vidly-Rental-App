using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace vidly.Dtos
{
    public class CustomerDto
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }


        public int Id { get; set; }

        
        public bool IsSubscribed { get; set; }

        
        [Required]
        public int MembershipTypeId { get; set; }

        
        [Required]
        //[Min18YearsIfMember]
        public DateTime? DateOfBirth { get; set; }
        public MembershipTypeDto MembershipType { get; set; }
    }
}