namespace APiPracticeSql.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Point { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
