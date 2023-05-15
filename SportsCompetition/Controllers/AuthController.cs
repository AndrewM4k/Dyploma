using AutorisationApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsCompetition.Dtos;
using SportsCompetition.Enums;
using SportsCompetition.Filters;
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
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;
        private readonly RefreshTokenService _refreshTokenService;
        private readonly SportCompetitionDbContext _context;

        public AuthController(
            TokenService tokenService, 
            UserManager<IdentityUser<Guid>> userManager, 
            SignInManager<IdentityUser<Guid>> signInManager, 
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
            var user = new IdentityUser<Guid>()
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
                .SingleOrDefaultAsync(u => u.NormalizedUserName == dto.Username.ToUpper());

            if (user == null) { return Unauthorized(); }

            var signInResult = await _signInManager
                .CheckPasswordSignInAsync(user, dto.Password, false);

            if (!signInResult.Succeeded)
            {
                return Unauthorized();
            }
            var token = await _tokenService.CreateTokenAsync(user);

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
                AccessToken = await _tokenService.CreateTokenAsync(token.User),
                RefreshToken = await _refreshTokenService.CreateRefreshTokenAsync(token.User)
            };

            return result;
        }
    }
}