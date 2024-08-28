using FluentValidation;

namespace APiPracticeSql.Dtos.UserDtos
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(d => d.FullName)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(30);

            RuleFor(d => d.UserName)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(30);

            RuleFor(d => d.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(d => d.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20);

            RuleFor(d => d.RePassword)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20);

            //RuleFor(r => r)
            //    .Custom((r, context) =>
            //    {
            //        if (r.Password != r.RePassword)
            //        {
            //            context.AddFailure("Password", "doesn't match..");
            //        }
            //    });
            RuleFor(r => r.Password).Equal(r => r.RePassword);
        }
    }
}
