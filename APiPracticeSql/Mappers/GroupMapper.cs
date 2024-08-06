using APiPracticeSql.Dtos.GroupDtos;
using APiPracticeSql.Entities;

namespace APiPracticeSql.Mappers
{
    public class GroupMapper
    {
        public static Group GroupCreateDtoToGroup(GroupCreateDto dto) => new Group
        {
            Name= dto.Name,
            Limit= dto.Limit,
        };
        public static GroupReturnDto GroupToGroupReturnDto(Group group) => new GroupReturnDto
        {
            Id= group.Id,
            Name= group.Name,
            Limit = group.Limit,
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now,
            Students = group.Students.Select(s=>new StudentInGroupReturnDto
            {
                Name= s.Name,
                Point=s.Point,
            }).ToList(),

        };
    }
}
