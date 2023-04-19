using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using API.Entities;
using API.Interfaces;
using API.Helpers;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class JobListingRepository : IJobListingRepository
    {
        public DataContext _context { get; }
        private readonly IMapper _mapper;

        public JobListingRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async void AddListing(JobListings listings)
        {
            _context.Add(listings);
        }

        public async void DeleteListing(JobListings listings)
        {
            _context.Remove(listings);
        }

        public async Task<JobListings> GetListingByIdAsync(int id)
        {
            return await _context.Listings.FindAsync(id);
        }

        //possibly create a Helper method for passing in Listings params from frontend?
        public async Task<PagedList<JobListingDto>> GetListingsAsync(PaginationParams pageParams)
        {
            // This is unfinished code placed here to setup the the repository
            // Reference 
            var query = _context.Listings.AsQueryable();

            
             return await PagedList<JobListingDto>.CreateAsync(
                query.AsNoTracking().ProjectTo<JobListingDto>(_mapper.ConfigurationProvider), 
                pageParams.PageNumber, 
                pageParams.PageSize);
        }

         public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

       

      
    }
}