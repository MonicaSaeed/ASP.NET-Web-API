using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mobile45API.DTOs;
using StudentAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManger;

        public AccountController(UserManager<ApplicationUser> userManger)
        {
            _userManger = userManger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO userDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = userDto.UserName;
                user.Email = userDto.Email;

                IdentityResult res = await _userManger.CreateAsync(user, userDto.Password);
                if (res.Succeeded)
                {
                    return Ok("User Created");
                }
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

            }
            return BadRequest(ModelState);

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO userDTo)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManger.FindByNameAsync(userDTo.UserName);
                bool found = await _userManger.CheckPasswordAsync(user, userDTo.Password);
                if (found)
                {
                    List<Claim> userCliams = new List<Claim>();
                    userCliams.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    userCliams.Add(new Claim(ClaimTypes.Name, user.UserName));
                    userCliams.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); // for unique token 

                    var userRoles = await _userManger.GetRolesAsync(user);
                    foreach (var r in userRoles)
                    {
                        userCliams.Add(new Claim(ClaimTypes.Role, r));
                    }

                    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("e1045a50607bcbaadf16e640fba1a9bea8ab47d8706e30e4f8fa6529385d48b8"));

                    SigningCredentials signCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


                    JwtSecurityToken myToken = new JwtSecurityToken(
                        issuer: "http://localhost:5028/", // from
                        audience: "http://localhost:5127",
                        expires: DateTime.Now.AddDays(1),
                        claims: userCliams,
                        signingCredentials: signCred
                        );

                    return Ok(
                          new
                          {
                              token = new JwtSecurityTokenHandler().WriteToken(myToken),
                              Expire = DateTime.Now.AddDays(1)
                          }
                        );
                }
                ModelState.AddModelError("", "Invalid UserName Or Password");
            }
            return BadRequest(ModelState);

        }
    }
}
