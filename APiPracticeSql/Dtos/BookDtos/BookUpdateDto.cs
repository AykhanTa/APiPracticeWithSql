using FluentValidation;

namespace APiPracticeSql.Dtos.BookDtos
{
    public class BookUpdateDto
    {
        public string Name { get; set; }
        public int PageCount { get; set; }
        public int[] AuthorIds { get; set; }
    }
    public class BookUpdateDtoValidator : AbstractValidator<BookUpdateDto>
    {
        public BookUpdateDtoValidator()
        {
            RuleFor(b => b.Name)
                .MaximumLength(100)
                .MinimumLength(3)
                .NotEmpty();
            RuleFor(b => b.PageCount)
                .InclusiveBetween(1, 1000)
                .NotEmpty();
            RuleFor(b => b.AuthorIds)
                .NotEmpty();
        }
    }
}
