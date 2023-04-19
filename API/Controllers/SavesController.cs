using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using API.Entities;
using API.Interfaces;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using API.Helpers;

namespace API.Controllers
{
    
    public class SavesController : BaseAPiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ISavesRepository _savesRepository;
        public SavesController(IUserRepository userRepository, ISavesRepository savesRepository) 
        {
            _savesRepository = savesRepository;
            _userRepository = userRepository;     
        }

            [HttpPost("{username}")]
            public async Task<ActionResult> AddSave(string username)
            {
                var sourceUserId = User.GetUserId();
                var SavedUser = await _userRepository.GetUserByUsernameAsync(username);
                var SourceUser = await _savesRepository.GetUserWithSaves(sourceUserId);
                
                if (SavedUser == null ) return NotFound();

                if (SourceUser.UserName == username) return BadRequest("You cannot Save yourself");

                var UserSave = await _savesRepository.GetUserSave(sourceUserId, SavedUser.Id);

                if (UserSave != null) return BadRequest("You already saved this user");

                UserSave = new UserSave
                {
                    SourceUserId = sourceUserId,
                    TargetUserId = SavedUser.Id
                };

                SourceUser.SavedUsers.Add(UserSave);

                if(await _userRepository.SaveAllAsync()) return Ok();

                return BadRequest("Failed to save user");
            }

            [HttpGet]
            public async Task<ActionResult<PagedList<SaveDto>>> GetUserSaves([FromQuery]SavesParams savesParams)
            {
                savesParams.userId = User.GetUserId();
                var users = await _savesRepository.GetUsersSaves(savesParams);
                Response.AddPageinationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));

                return Ok(users);
            }
    }
}