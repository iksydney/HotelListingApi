using AutoMapper;
using HotelListing.Data;
using HotelListing.Models;
using HotelListing.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;
        private readonly IAuthManager _authmanager;

        public AccountController(UserManager<User> usermanager,
            IMapper mapper, 
            ILogger<AccountController> logger,
            IAuthManager authmanager)
        {
            _userManager = usermanager;            
            _mapper = mapper;
            _logger = logger;
            _authmanager = authmanager;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation($"Registration attempt from{userDTO.Email}");
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _mapper.Map<User>(userDTO);
                user.UserName = userDTO.Email;
                var result  = await _userManager.CreateAsync(user, userDTO.Password);

                if (!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                await _userManager.AddToRolesAsync(user, userDTO.Roles);
                return Accepted();

            }catch(Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }

        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            _logger.LogInformation($"Login attempt from{userDTO.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if(!await _authmanager.ValidateUser(userDTO))
                {
                    return Unauthorized();
                }
                return Accepted(new {Token = await _authmanager.CreateToken()});

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }
    }
}
