using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using StudentAPIw6.API.Module;
using StudentAPIw6.API.Validators.InputValidators;
using StudentAPIw6.Context;
using StudentAPIw6.API.Middlewares;
using StudentAPIw6.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services
    .AddSinhVienModule()
    .AddLopHocModule()
    .AddBoMonModule();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Global Exception Handler
app.UseExceptionHandler();


// HTTPS
app.UseHttpsRedirection();


// Controller
app.MapControllers();


app.Run();