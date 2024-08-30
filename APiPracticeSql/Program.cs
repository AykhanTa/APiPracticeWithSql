using ApiPractice.DAL.Data;
using ApiPractice.DAL.Entities;
using APiPracticeSql;
using APiPracticeSql.Dtos.GroupDtos;
using APiPracticeSql.Profiles;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); 
var config = builder.Configuration;
// Add services to the container.

builder.Services.Register(config);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
