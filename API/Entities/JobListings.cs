using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{   
    [Table("JobListings")]
    public class JobListings
    {
        public int Id { get; set; }
        public DateTime Experation { get; set; }
        public string CompanyName { get; set; }
        public string ListingImage { get; set; }
        public string JobTitle { get; set; }
        public string Location { get; set; }
        public string JobType { get; set; }
        public string Description { get; set; }
        public string Qualifications { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId {get; set;}
    }
}