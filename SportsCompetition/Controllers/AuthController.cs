using AutorisationApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Dtos;
using SportsCompetition.Models;
using SportsCompetition.Persistance;
using SportsCompetition.Services;

namespace AutorisationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RefreshTokenService _refreshTokenService;
        private readonly SportCompetitionDbContext _context;

        public AuthController(
            TokenService tokenService, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            RefreshTokenService refreshTokenService, 
            SportCompetitionDbContext context)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _refreshTokenService = refreshTokenService;
            _context = context;
        }

        [HttpPost("singUp")]
        public async Task<IActionResult> SignUnAsync(SingUpDto dto)
        {
            var user = new User()
            {
                UserName = dto.Username,
            };

            var signUpResult = await _userManager.CreateAsync(user, dto.Password);
            if (!signUpResult.Succeeded)
            {
                return BadRequest(signUpResult.Errors);
            }

            return Ok();
        }

        [HttpPost("singIn")]
        public async Task<ActionResult<SignInResultDto>> SignInAsync(SingInDto dto)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(u => u.NormalizedUserName == dto.Username.ToUpper() ||
                                             u.NormalizedEmail == dto.Email.ToUpper());

            if (user == null) { return Unauthorized(); }

            var signInResult = await _signInManager
                .CheckPasswordSignInAsync(user, dto.Password, false);

            if (!signInResult.Succeeded)
            {
                return Unauthorized();
            }
            var token = _tokenService.CreateToken(user);

            var result = new SignInResultDto
            {
                AccessToken = token,
                RefreshToken = await _refreshTokenService.CreateRefreshTokenAsync(user)
            };

            return result;
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<RefreshTokenResultDto>> RefreshTokenAsync(RefreshTokenDto dto)
        {
            var token = await _context.RefreshTokens
                .Include(t =>t.User)
                .FirstOrDefaultAsync(t => t.Token == dto.RefreshToken);
            if (token == null) 
            {
                return BadRequest();
            }

            var result = new RefreshTokenResultDto
            {
                AccessToken = _tokenService.CreateToken(token.User),
                RefreshToken = await _refreshTokenService.CreateRefreshTokenAsync(token.User)
            };

            return result;
        }
    }
}