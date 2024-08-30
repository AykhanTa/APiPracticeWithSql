namespace APiPracticeSql.Dtos.UserDtos
{
    public class UserReturnDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
