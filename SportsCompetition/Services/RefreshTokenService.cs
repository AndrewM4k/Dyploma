using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration.UserSecrets;
using SportsCompetition.Models;
using SportsCompetition.Persistance;

namespace SportsCompetition.Services
{
    public class RefreshTokenService
    {
        private readonly SportCompetitionDbContext _context;

        public RefreshTokenService(SportCompetitionDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateRefreshTokenAsync(User user)
        {
            var token = $"{Guid.NewGuid()}{Guid.NewGuid()}".Replace("-", "");


            var existentToken = await _context.RefreshTokens.SingleOrDefaultAsync(t => t.UserId == user.Id);
            if (existentToken != null)
            {
                _context.Remove(existentToken);
            }

            var newToken = new RefreshToken()
            {
                Token = token,
                UserId = user.Id
            };
            await _context.AddAsync(newToken);

            await _context.SaveChangesAsync();

            return token;
        }
    }
}
