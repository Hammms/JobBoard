using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{   [Authorize]
    public class JobListingsController : BaseAPiController
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IPhotoService _photoService;

        private readonly IJobListingRepository _ListingRepository; 

        public JobListingsController(IPhotoService photoService, IUserRepository userRepository,DataContext context, IJobListingRepository listingRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _photoService = photoService;
            _ListingRepository = listingRepository;
        }

       // [Authorize(Roles = "Member")]
        [HttpPost("CreateListing")]
        public async Task<ActionResult<JobListingDto>> CreateListing(JobListingDto jobListingDto)
        {   // Find the current user
            var user = await _userRepository.GetUserByUsernameAsync(jobListingDto.username);
            if (user == null ) return NotFound("user not found");
            Console.WriteLine(jobListingDto);
            Console.WriteLine(jobListingDto);Console.WriteLine(jobListingDto);Console.WriteLine(jobListingDto);Console.WriteLine(jobListingDto);Console.WriteLine(jobListingDto);

            // compile job listing to be saved to database
            var joblisting = new JobListings
            {
                Experation = DateTime.UtcNow.AddDays(30),
                ListingImage = null,
                CompanyName = jobListingDto.CompanyName,
                JobTitle = jobListingDto.JobTitle,
                Location = jobListingDto.Location,
                JobType = jobListingDto.JobType,
                Description = jobListingDto.Description,
                Qualifications = jobListingDto.Qualifications,
                AppUserId = user.Id
            };

            _ListingRepository.AddListing(joblisting);
            await _ListingRepository.SaveAllAsync();
            return Ok();

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobListingDto>>> GetListings([FromQuery]PaginationParams pageParams)
        {
            var Listings = await _ListingRepository.GetListingsAsync(pageParams);
            if (Listings == null) return BadRequest();
            Response.AddPageinationHeader(new PaginationHeader(Listings.CurrentPage, Listings.PageSize, Listings.TotalCount, Listings.TotalPages));
            return Ok(Listings);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<JobListingDto>> GetListing(int id)
        {
            var Listing = await _ListingRepository.GetListingByIdAsync(id);
            if (Listing == null) return BadRequest();
            return Ok(Listing);
        }

    }
}