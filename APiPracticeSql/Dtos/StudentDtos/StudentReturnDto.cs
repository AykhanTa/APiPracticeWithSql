namespace APiPracticeSql.Dtos.StudentDtos
{
    public class StudentReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Point { get; set; }
        public int GroupId { get; set; }
        public GroupInStudentReturnDto Group { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
    public class GroupInStudentReturnDto
    {
        public string Name { get; set; }
        public int StudentsCount { get; set; }
    }
}
