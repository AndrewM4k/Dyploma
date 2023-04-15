using FluentValidation;
using SportsCompetition.Dtos;
using SportsCompetition.Persistance;

namespace SportsCompetition.Validators
{
    public class SportsmanvValidator : AbstractValidator<AddSportsmanDto>
    {

        private readonly ComposeApiDbContext _context;
        public SportsmanvValidator(ComposeApiDbContext context)
        {
            RuleFor(s => s.Gender)
                .Must(CheckValueGender);

            RuleFor(s => s.Password)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(s => s.Email)
                .EmailAddress();

            RuleFor(s => s.Username)
                .NotEmpty()
                .Must(CheckValueUsername)
                .MinimumLength(5);
            _context = context;
        }

        public bool CheckValueUsername(string v)
        {
            foreach (string item in _context.Sportsmans.Select(u => u.Username).ToList())
            {
                if (v == item)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckValueGender(string v)
        {
            var genders = new List<string>() { "male", "female" };

            foreach (string item in genders)
            {
                if (v == item)
                { 
                return true;
                }
            }
            return false;
        }
    }
}
