using FluentValidation;

namespace APiPracticeSql.Dtos.UserDtos
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(d => d.UserName)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(30);

            RuleFor(d => d.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20);
        }
    }
}
