using FluentValidation;
using SportsCompetition.Dtos;
using SportsCompetition.Enums;
using SportsCompetition.Persistance;

namespace SportsCompetition.Validators
{
    public class SportsmanvValidator : AbstractValidator<AddSportsmanDto>
    {

        private readonly SportCompetitionDbContext _context;
        public SportsmanvValidator(SportCompetitionDbContext context)
        {
            RuleFor(s => s.Gender)
                .IsInEnum();

            RuleFor(s => s.Password)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(s => s.Email)
                .EmailAddress();

            RuleFor(s => s.Name)
                .NotEmpty()
                .MinimumLength(2);
            _context = context;
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
