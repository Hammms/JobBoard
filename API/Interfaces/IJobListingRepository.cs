using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IJobListingRepository
    {
        void AddListing(JobListings listings);
        void DeleteListing(JobListings listings);
        Task<JobListings> GetListingByIdAsync(int id);
        Task<PagedList<JobListingDto>>GetListingsAsync(PaginationParams pageParams);
        Task<bool> SaveAllAsync();
    }
}