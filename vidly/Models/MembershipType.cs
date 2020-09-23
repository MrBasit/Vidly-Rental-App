using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vidly.Models
{
    public class MembershipType
    {
        public int Id { get; set; }
        public int SignupFee { get; set; }
        public string Name { get; set; }        
        public int DurationInMonth { get; set; }
        public int DiscountRate { get; set; }
    }
}