using FluentValidation;

namespace APiPracticeSql.Dtos.GroupDtos
{
    public class GroupUpdateDto
    {
        public string Name { get; set; }
        public int Limit { get; set; }
        public IFormFile File { get; set; }
    }
    public class GroupUpdateDtoValidator : AbstractValidator<GroupUpdateDto>
    {
        public GroupUpdateDtoValidator()
        {
            RuleFor(g => g.Name)
                .MaximumLength(10)
                .MinimumLength(3);
            RuleFor(g => g.Limit)
                .NotEmpty()
                .InclusiveBetween(5, 10); 
            RuleFor(g => g)
                .Custom((g, context) =>
                {
                    if (g.File != null)
                    {
                        if (g.File.Length / 1024 > 500)
                        {
                            context.AddFailure("File", "size is too large");
                        }
                        if (!g.File.ContentType.Contains("image/"))
                        {
                            context.AddFailure("File", "file only imagee");
                        }
                    }
                    
                });

        }
    }
}
