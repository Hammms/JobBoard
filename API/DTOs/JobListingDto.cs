using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class JobListingDto
    {   public int Id { get; set; }
        public string username { get; set; }
        public string CompanyName { get; set; }
        public string ListingImage { get; set; }
        public string JobTitle { get; set; }
        public string Location { get; set; }
        public string JobType { get; set; }
        public string Description { get; set; }
        public string Qualifications { get; set; }
        
    }
}