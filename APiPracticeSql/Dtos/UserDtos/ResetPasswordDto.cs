using FluentValidation;

namespace APiPracticeSql.Dtos.UserDtos
{
    public class ResetPasswordDto
    {
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(d => d.Password)
               .NotEmpty()
               .MinimumLength(6)
               .MaximumLength(20);

            RuleFor(d => d.RePassword)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20);

            RuleFor(r => r.Password).Equal(r => r.RePassword);

        }
    }
}