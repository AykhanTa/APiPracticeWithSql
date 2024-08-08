using APiPracticeSql.Entities;

namespace APiPracticeSql.Dtos.GroupDtos
{
    public class GroupReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Limit { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Image { get; set; }
        public List<StudentInGroupReturnDto> Students { get; set; }
    }
    public class StudentInGroupReturnDto
    {
        public string Name { get; set; }
        public double Point { get; set; }
    }
}
