using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;

namespace API.Controllers
{
    public class AccountController : BaseAPiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public readonly IOptions<StripeOptions> options;
        private readonly IStripeClient client;
        public UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper, IOptions<StripeOptions> options)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
            
            this.options = options;
            this.client = new StripeClient(this.options.Value.SecretKey);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {   //check if username is taken
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            
            //Map incoming request Generate Hash for password
            var user = _mapper.Map<AppUser>(registerDto);


           
            user.UserName = registerDto.Username.ToLower();
   
            //Create User in Stripe
            //put logic in here to determine if the customer already exsists 
            StripeConfiguration.ApiKey = this.options.Value.SecretKey;
            var options = new CustomerCreateOptions
            {   
                Name = user.KnownAs,
                Email = user.EmailAddress
            };
            var service = new CustomerService();
            var stripeid = service.Create(options);
            user.StripeId = stripeid.Id;
            //stage and commit to database
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            
            if (!result.Succeeded) return BadRequest(result.Errors);
            
            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("Invalid username");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if(!result) return Unauthorized("invalid password");
            
            // Implicity Add every user to the role of Member
            

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}