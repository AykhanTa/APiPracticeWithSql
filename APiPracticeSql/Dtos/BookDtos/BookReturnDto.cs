namespace APiPracticeSql.Dtos.BookDtos
{
    public class BookReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PageCount { get; set; }
        public List<AuthorInBookReturnDto> BookAuthors { get; set; }

    }
    public class AuthorInBookReturnDto
    {
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
    }
}
