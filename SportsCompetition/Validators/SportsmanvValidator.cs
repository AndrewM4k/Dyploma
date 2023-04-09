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
                ;

            RuleFor(s => s.Password)
                .NotEmpty();

            RuleFor(s => s.Email)
                .NotEmpty()
                .Must(CheckValue);
            _context = context;
        }

        public bool CheckValue(string v)
        {
            foreach (string item in _context.Sportsmans.Select(u => u.Email).ToList())
            {
                if (v == item)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
