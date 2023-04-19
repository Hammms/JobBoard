using System;
using API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {

        public string StripeId { get; set; }
        public string EmailAddress { get; set; }
        public DateOnly DateOfBirth {get; set;}
        public string KnownAs {get; set;}
        public DateTime Created {get; set;} = DateTime.Now;
        public DateTime SubscriptionExperation {get; set;}
        public DateTime LastActive {get; set;} = DateTime.Now;
        public string Gender {get; set;}
        public string Introduction {get;set;}
        public string LookingFor {get;set;}
        public string Interests {get; set;}
        public string City {get; set;}
        public string Country {get; set;}
        public ICollection<Resume> Resume {get; set;}
        public ICollection<Photo> Photos {get; set;}
        public List<UserSave> SavedUsers { get; set; }
        public List<UserSave> SavedByUsers { get; set; }
        public List<Message> MessagesSent { get; set; }
        public List<Message> MessagesReceived { get; set; }
        public ICollection<JobListings> JobListingsId { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }

    }
}

