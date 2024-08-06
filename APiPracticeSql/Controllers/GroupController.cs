using ApiPractice.DAL.Data;
using APiPracticeSql.Dtos.GroupDtos;
using APiPracticeSql.Entities;
using APiPracticeSql.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APiPracticeSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly ApiPracticeContext _context;

        public GroupController(ApiPracticeContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var groups= await _context.Groups
                .Include(g=>g.Students)
                .ToListAsync();
            List<GroupReturnDto> list = new();
            foreach (var group in groups)
            {
                list.Add(GroupMapper.GroupToGroupReturnDto(group));
            }
            return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var existGroup = await _context.Groups
                .Include(g=>g.Students) 
                .FirstOrDefaultAsync(g => g.Id == id);
            if (existGroup is null) return NotFound();
            return Ok(GroupMapper.GroupToGroupReturnDto(existGroup));
        }

        [HttpPost("")]
        public async Task<IActionResult> Create(GroupCreateDto groupCreateDto)
        {
            if (await _context.Groups.AnyAsync(g => g.Name.ToLower() == groupCreateDto.Name.ToLower()))
                return BadRequest("group name already exist...");
            
            await _context.Groups.AddAsync(GroupMapper.GroupCreateDtoToGroup(groupCreateDto));
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,GroupUpdateDto groupUpdateDto)
        {
            var existGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (existGroup is null) return NotFound();
            if (existGroup.Name.ToLower()!= groupUpdateDto.Name.ToLower() && await _context.Groups.AnyAsync(g =>g.Id!=id && g.Name.ToLower() == groupUpdateDto.Name.ToLower()))
                return BadRequest("group name already exist..."); 
            existGroup.Name= groupUpdateDto.Name;   
            existGroup.Limit= groupUpdateDto.Limit;
            await _context.SaveChangesAsync();
            return Ok(existGroup);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (existGroup is null) return NotFound();
            _context.Groups.Remove(existGroup);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
