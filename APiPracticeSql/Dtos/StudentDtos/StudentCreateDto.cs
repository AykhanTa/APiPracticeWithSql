using FluentValidation;

namespace APiPracticeSql.Dtos.StudentDtos
{
    public class StudentCreateDto
    {
        public string Name { get; set; }
        public double Point { get; set; }
        public int GroupId { get; set; }
    }
    public class StudentCreateDtoValidator : AbstractValidator<StudentCreateDto>
    {
        public StudentCreateDtoValidator()
        {
            RuleFor(s=>s.Name)
                .MinimumLength(3)
                .MaximumLength(20)
                .NotEmpty();
            RuleFor(s => s.Point)
                .NotEmpty()
                .InclusiveBetween(0, 100);

        }
    }
}
