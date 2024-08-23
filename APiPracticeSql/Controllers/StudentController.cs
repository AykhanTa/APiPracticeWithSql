using ApiPractice.DAL.Data;
using APiPracticeSql.Dtos.StudentDtos;
using APiPracticeSql.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APiPracticeSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApiPracticeContext _context;
        private readonly IMapper _mapper;

        public StudentController(ApiPracticeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get(int page=1,string search=null)
        {
            var query = _context.Students.AsQueryable();
            if (search!=null)
                query=query.Where(s=>s.Name.Contains(search));
            var datas=await query
                .Include(s=>s.Group)
                .Skip((page-1)*2)
                .Take(2)
                .ToListAsync();
            var totalCount = await query.CountAsync();

            StudentListDto studentListDto = new();
            studentListDto.TotalCount = totalCount;
            studentListDto.CurrentPage= page;
            studentListDto.Students = _mapper.Map<List<StudentReturnDto>>(datas);


            return Ok(studentListDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var existStudent = await _context.Students
                .Include(s=>s.Group)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (existStudent == null) return NotFound();
            return Ok(_mapper.Map<StudentReturnDto>(existStudent));
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(StudentCreateDto studentCreateDto)
        {
            var existGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == studentCreateDto.GroupId);
            if (existGroup == null) return NotFound("Group not found");
            if (existGroup.Students.Count >= existGroup.Limit)
                return Conflict("Group is full");

            Student student = _mapper.Map<Student>(studentCreateDto);

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,StudentUpdateDto studentUpdateDto)
        {
            var existStudent = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (existStudent == null) return NotFound();

            if (existStudent.GroupId!=studentUpdateDto.GroupId)
            {
                var existGroup = await _context.Groups
                    .Include(g=>g.Students)
                    .FirstOrDefaultAsync(g => g.Id == studentUpdateDto.GroupId);
                if (existGroup == null) return NotFound("Group not found");
                if (existGroup.Students.Count >= existGroup.Limit)
                    return Conflict("Group is full");
            }

            _mapper.Map(studentUpdateDto, existStudent);
            existStudent.UpdateDate=DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<StudentReturnDto>(existStudent));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existStudent = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (existStudent == null) return NotFound();
            _context.Students.Remove(existStudent);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
