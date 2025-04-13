using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using weblearn.Models.DTO;
using weblearn.Repository;

namespace weblearn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly  UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository) {
            this._userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email =registerRequestDto.Username
            };
            var identityResult =await _userManager.CreateAsync(identityUser,registerRequestDto.Password);
            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult= await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded) {
                        return Ok(new
                        {
                            data = identityResult.Succeeded,
                        });
                    }
                }
            }
            return BadRequest("Something went wrong");
           
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody ] LoginRequestDto loginRequestDto)
        {
            var user =await  _userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user,loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    var  roles = await _userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        var token =tokenRepository.CreateJWTToken(user, roles.ToList());
                        var reponse = new LoginResponseDto
                        {
                            JwtToken = token,
                        };
                        return Ok(reponse);
                    }
                 
                }
            }
            return BadRequest("Username or password incorrect");
        }
    }
}
