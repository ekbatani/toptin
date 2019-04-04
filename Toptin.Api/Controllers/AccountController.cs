using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Toptin.Api.Data.Dtos;
using Toptin.Api.Models;

namespace Toptin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager, 
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }


        //---------------------------- Login and Register Section ----------------------

        // [HttpPost("Register")]
        // [AllowAnonymous]
        // public async Task<IActionResult> Register([FromBody] AccountViewModel credentialsViewModel)
        // {
        //     //string generatedPass = GenerateRandomPassword();
        //     string generatedPass = "123456";

        //     IList<string> role = new[] { "Customer" };

        //     var user = new User
        //     {
        //         UserName = credentialsViewModel.Phone,
        //         PhoneNumber = credentialsViewModel.Phone
        //     };

        //     var result = await _userManager.CreateAsync(user, generatedPass);

        //     if (!result.Succeeded)
        //         return BadRequest(result.Errors);

        //     result = await _userManager.AddToRolesAsync(user, role);

        //     if (!result.Succeeded)
        //         return BadRequest(result.Errors);

        //     //send sms
        //     //string res =  Sendsms(credentialsViewModel.Phone, generatedPass);

        //     return Ok();
        // }

        // [HttpPost("Login")]
        // [AllowAnonymous]
        // public async Task<IActionResult> Login([FromBody] AccountViewModel credentialsViewModel)
        // {
        //    var result = await _signInManager.PasswordSignInAsync(credentialsViewModel.Phone, credentialsViewModel.Password , false, false );

        //     if (!result.Succeeded)
        //         return BadRequest();

        //     var user = await _userManager.FindByNameAsync(credentialsViewModel.Phone);
           
        //     if(user == null)
        //         return BadRequest();

        //     IList<string> role = _userManager.GetRolesAsync(user).Result;

        //     return Ok(CreateToken(user , role));
        // }

        // [HttpPost("SendSms")]
        // [AllowAnonymous]
        // public async Task<IActionResult> SendSms(string userPhone)
        // {
        //     var user = await _userManager.FindByNameAsync(userPhone);

        //     if ( user == null )
        //         return NotFound();
        //     if (user.SmsSentNumber > 5)
        //     {
        //         return Forbid();
        //     }

        //     string generatedPass = GenerateRandomPassword();
        //     user.SmsSentNumber += 1;
        //     await _userManager.UpdateAsync(user);

            

        //     var result = await _userManager.RemovePasswordAsync(user);
        //     result = await _userManager.AddPasswordAsync(user, generatedPass);

        //     if (!result.Succeeded)
        //         return BadRequest(result.Errors);

        //     string res = Sendsms(userPhone, generatedPass);
        //     return Ok();
        // }
        //---------------------------- End of Login and Register Section ----------------------


        //---------------------------- Users Section ----------------------
        [HttpGet("GetUser")]
        [Authorize(Roles = "Admin")]
        // GET: api/Account/GetUser
        public IEnumerable<UserForLoginDto> GetUser()
        {
            string[] roles = new[] {"Admin", "Customer"};
            return _userManager.Users.Select( u=>  new UserForLoginDto
            {
                Phone = u.UserName,
            });
        }

        [HttpGet("GetUser/{id}")]
        [Authorize(Roles = "Admin")]
        // GET: api/Account/GetUser/id
        public async Task<User> GetUser(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        // GET: api/Account/PutUser
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.Id)
                return BadRequest();
            try
            {
                await _userManager.UpdateAsync(user);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        // GET: api/Account/DeleteUser
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }
        //---------------------------- End of Users Section ----------------------


        //---------------------------- Roles Section ----------------------

        [HttpGet("GetRole")]
        [Authorize(Roles = "Admin")]
        // GET: api/Account/GetRole
        public IEnumerable<IdentityRole> GetRole()
        {
            return _roleManager.Roles;
        }

        [HttpGet("GetRole/{id}")]
        [Authorize(Roles = "Admin")]
        // GET: api/Account/GetRole/id
        public async Task<IdentityRole> GetRole(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        // GET: api/Account/PutRole
        public async Task<IActionResult> PutRole(string id, string newRole)
        {
            if (id == null | newRole == null)
            {
                return BadRequest();
            }

            try
            {
                IdentityRole role = await _roleManager.FindByIdAsync(id);
                role.Name = newRole;
                await _roleManager.UpdateAsync(role);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(newRole);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        // GET: api/Account/PostRole
        public async Task<IActionResult> PostRole(string role)
        {
            if (role == null)
                return BadRequest();

            if (!await _roleManager.RoleExistsAsync(role))
            {
                var r = new IdentityRole {Name = role};
                await _roleManager.CreateAsync(r);
            }
            else
            {
                return BadRequest();
            }

            return Ok(role);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        // GET: api/Account/DeleteRole
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (id == null)
                return BadRequest();

            var r = await _roleManager.FindByIdAsync(id);

            if ( r != null )
            {
                await _roleManager.DeleteAsync(r);
            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }

        // [HttpPost]
        // [Authorize(Roles = "Admin")]
        // public async Task<IActionResult> AssignRole([FromBody]RoleAsignViewModel roleAsignViewModel)
        // {
        //     User user = await _userManager.FindByIdAsync(roleAsignViewModel.Id);

        //     if (user == null)
        //         return BadRequest();

        //     if (roleAsignViewModel.Roles == null)
        //     {
        //         var userRoles = _userManager.GetRolesAsync(user).Result;
        //         await _userManager.RemoveFromRolesAsync(user, userRoles);
        //     }
        //     else
        //     {
        //         await _userManager.AddToRolesAsync(user, roleAsignViewModel.Roles);
        //     }

        //     return Ok();
        // }

        //---------------------------- End of Roles Section ----------------------


        //---------------------------- Token and password Section ----------------------
        // private string CreateToken(User user, IList<string> role)
        // {
        //     var claim = new List<Claim>
        //     {
        //         new Claim(JwtRegisteredClaimNames.Sub, user.Id)
        //     };

        //     foreach (string r in role)
        //     {
        //         claim.Add(new Claim(ClaimTypes.Role, r));
        //     }

        //     var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("CahdseV265@2fl]xd7"));
        //     var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        //     var jwt = new JwtSecurityToken(
        //         signingCredentials: signingCredentials,
        //         claims: claim
        //     );
        //     return new JwtSecurityTokenHandler().WriteToken(jwt);
        // }

        //---------------------------- End of Token and password Section ----------------------

    }
}