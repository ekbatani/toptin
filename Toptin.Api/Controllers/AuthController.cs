using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Toptin.Api.Data.Dtos;
using Toptin.Api.Models;

namespace Toptin.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var userToCreate = _mapper.Map<User>(userForRegisterDto);
            userToCreate.UserName = userForRegisterDto.Phone;

            // string generatedPass = GenerateRandomPassword();
            string generatedPass = "123456";
            IList<string> role = new[] { "Customer" };


            var result = await _userManager.CreateAsync(userToCreate, generatedPass);

            if (result.Succeeded)
            {
                result = await _userManager.AddToRolesAsync(userToCreate, role);

                var userToReturn = _mapper.Map<UserForDetailedDto>(userToCreate);

                if (result.Succeeded)
                {
                    // send sms
                    string res =  Sendsms(userToReturn.Phone, generatedPass);

                    if (res != null)
                    {
                        return CreatedAtRoute("GetUser", 
                            new { controller = "Users", id = userToCreate.Id }, userToReturn);
                    }
                }
            }
            
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userForLoginDto.Phone);

            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.PhoneNumber == userForLoginDto.Phone);
                
                var userToReturn = _mapper.Map<UserForDetailedDto>(appUser);

                return Ok(new
                {
                    token = GenerateJwtToken(appUser).Result,
                    user = userToReturn
                });                    
            }

            return Unauthorized();
        }

        [HttpPost("SendSms")]
        [AllowAnonymous]
        public async Task<IActionResult> SendSms(string userPhone)
        {
            var user = await _userManager.FindByNameAsync(userPhone);

            if ( user == null )
                return NotFound();
            if (user.SmsSentNumber > 5)
            {
                return Forbid();
            }

            string generatedPass = GenerateRandomPassword();
            user.SmsSentNumber += 1;
            await _userManager.UpdateAsync(user);            

            var result = await _userManager.RemovePasswordAsync(user);
            result = await _userManager.AddPasswordAsync(user, generatedPass);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            string res = Sendsms(userPhone, generatedPass);
            return Ok();
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
           {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSetings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        
        private static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 6,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = false,
                RequireNonAlphanumeric = false,
                RequireUppercase = false
            };

            string[] randomChars = new[] {
                //"ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                //"abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                //"!@$?_-"                        // non-alphanumeric
            };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
            {
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);
            }

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                                      || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        private string Sendsms(string to, string pass)
        {
            string url = "http://tsms.ir/url/tsmshttp.php";
            string from = "30007227002559";
            string username = "pasd_inter";
            string password = "@Inter65";
            string message = "به فروشگاه خوش آمدید    نام کاربری: " + to + "   رمزعبور: " + pass;
            string createdUrl = url + "?" +
                                "from=" + from +
                                "&to=" + to +
                                "&username=" + username +
                                "&password=" + password +
                                "&message=" + message;

            try
            {
                //Create the request and send data to the SMS Gateway Server by HTTP connection
                WebRequest request = WebRequest.Create(createdUrl);
                request.Method = "GET";

                WebResponse wr = request.GetResponse();

                return wr.ToString();
            }
            catch
            {
            }
            return null;
        }

    }
}