using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SmartBank.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public ApplicationUserController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        [HttpPost]
        [Route("/register/user")]
        public async Task<IActionResult> RegisterUser(ApplicationUserModel user)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName

            };


            try
            {
                var result = await _userManager.CreateAsync(applicationUser, user.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }




        }


        [HttpPost]
        [Route("/login/user")]
        public async Task<IActionResult> LoginUser(LoginModel user)
        {
            try
            {
                string token = string.Empty;
                if(ModelState.IsValid)
                {
                    var userDb = await _userManager.FindByEmailAsync(user.Email);
                    var key = _configuration.GetValue<string>("ApplicationSettings:JWT_Secret");
                    if (userDb != null && await _userManager.CheckPasswordAsync(userDb, user.Password))
                    {
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim("UserID", userDb.Id.ToString())
                            }),
                            Expires = DateTime.UtcNow.AddMinutes(20),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),SecurityAlgorithms.HmacSha256Signature) 

                        };

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                         token = tokenHandler.WriteToken(securityToken);
                       
                    }
                    else
                        return BadRequest(new { message = " Email or password is wrong" });
                }

                return Ok(new { token });
            }
            catch (Exception)
            {

                throw;
            }
        }














    }



}
