using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Extensions;
using API.Helpers;

namespace API.Data
{
    public class SavesRepository : ISavesRepository
    {
        private readonly DataContext _context;
        public SavesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserSave> GetUserSave(int sourceUserId, int TargetUserId)
        {
            return await _context.Save.FindAsync(sourceUserId, TargetUserId);
        }

        public async Task<PagedList<SaveDto>> GetUsersSaves(SavesParams savesParams)
        {
            var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
            var saves = _context.Save.AsQueryable();


            if (savesParams.predicate == "saved")
            {
                saves = saves.Where(save => save.SourceUserId == savesParams.userId);
                users = saves.Select(save => save.TargetUser);
            }

            if (savesParams.predicate == "savedBy")
            {
                saves = saves.Where(save => save.SourceUserId == savesParams.userId);
                users = saves.Select(save => save.SourceUser);
            }

            var savedUsers =  users.Select(user => new SaveDto{
                UserName = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
                City = user.City,
                Id = user.Id
            });

            return await PagedList<SaveDto>.CreateAsync(savedUsers,savesParams.PageNumber, savesParams.PageSize );
        }

        public async Task<AppUser> GetUserWithSaves(int userId)
        {
            return await _context.Users.Include(x => x.SavedUsers).FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}