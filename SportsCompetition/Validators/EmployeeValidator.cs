using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SportsCompetition.Dtos;
using SportsCompetition.Persistance;

namespace SportsCompetition.Validators
{
    public class EmployeeValidator : AbstractValidator<AddEmployeeDto>
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        public EmployeeValidator(SportCompetitionDbContext context, UserManager<IdentityUser<Guid>> userManager)
        {
            RuleFor(e => e.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!#$%&? \"]).*$");

            RuleFor(e => e.Name)
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
