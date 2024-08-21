using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.Identity;
using Shaghaf.Core.Services.Contract;
using System.Security.Claims;
using Talabat.APIs.Controllers;

namespace Shaghaf.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthService _authService;
   

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAuthService authService
            
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var userByPhoneNumber = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == model.PhoneNumber);

            if (userByPhoneNumber is null)
            {
                return NotFound("User not found.");
            }


            var userName = userByPhoneNumber.UserName;


            var user = await _userManager.FindByNameAsync(userName);

            if (user is null)
            {
                return NotFound("User not found.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return NotFound("User not found.");

            return Ok(new UserDto()
            {
                UserName = user.UserName,

                Token = await _authService.CreateTokenAsync(user, _userManager)
            });




        }



        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (model.ConfirmPassword != model.Password)
            {
                return BadRequest("Confirm Password does not match Password");
            }

            var user = new ApplicationUser()
            {
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber
            };


            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest("Invalid Sign Up");
            var phoneNumberClaim = new Claim(ClaimTypes.MobilePhone, user.PhoneNumber);
            await _userManager.AddClaimAsync(user, phoneNumberClaim);
            return Ok(new UserDto()
            {
                UserName = user.UserName,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }




    }
}
