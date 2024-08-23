using APiPracticeSql.Entities;

namespace APiPracticeSql.Dtos.StudentDtos
{
    public class StudentListDto
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public List<StudentReturnDto> Students { get; set; }
    }
}
