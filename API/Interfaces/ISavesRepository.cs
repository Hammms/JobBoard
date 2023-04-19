using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface ISavesRepository
    {
        Task<UserSave> GetUserSave(int sourceUserId, int TargetUserId);
        Task<AppUser> GetUserWithSaves(int userId);
        Task<PagedList<SaveDto>> GetUsersSaves(SavesParams SavesParams);
    }
}