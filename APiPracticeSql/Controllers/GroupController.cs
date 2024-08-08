﻿using ApiPractice.DAL.Data;
using ApiPractice.DAL.Extensions;
using APiPracticeSql.Dtos.GroupDtos;
using APiPracticeSql.Entities;
using APiPracticeSql.Mappers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APiPracticeSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly ApiPracticeContext _context;
        private readonly IMapper _mapper;
        public GroupController(ApiPracticeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                list.Add(_mapper.Map<GroupReturnDto>(group));
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
            return Ok(_mapper.Map<GroupReturnDto>(existGroup));
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm]GroupCreateDto groupCreateDto)
        {
            if (await _context.Groups.AnyAsync(g => g.Name.ToLower() == groupCreateDto.Name.ToLower()))
                return BadRequest("group name already exist...");

            var file = groupCreateDto.File;


            Group group = new();
            group.Name=groupCreateDto.Name;
            group.Limit=groupCreateDto.Limit;
            group.Image = file.Save(Directory.GetCurrentDirectory(), "images");
            await _context.Groups.AddAsync(group);
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
