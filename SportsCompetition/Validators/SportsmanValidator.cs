using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SportsCompetition.Dtos;
using SportsCompetition.Enums;
using SportsCompetition.Persistance;

namespace SportsCompetition.Validators
{
    public class SportsmanValidator : AbstractValidator<AddSportsmanDto>
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        public SportsmanValidator(SportCompetitionDbContext context, UserManager<IdentityUser<Guid>> userManager)
        {
            RuleFor(s => s.Gender)
                .IsInEnum();

            RuleFor(s => s.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!#$%&? \"]).*$");

            RuleFor(s => s.Email)
                .EmailAddress();

            RuleFor(s => s.Name)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(s => s.Username)
                .NotEmpty()
                .MustAsync(CheckValue);

            _userManager = userManager;
        }

        public async Task<bool> CheckValue(string v, CancellationToken token)
        {
            var user = await _userManager.FindByNameAsync(v);
            if (user == null) { return true; };
            return false;
        }
    }
}
