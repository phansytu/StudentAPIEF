using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using StudentAPIw6.Context;
using StudentAPIw6.handler;
using StudentAPIw6.Services;
using StudentAPIw6.validator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<LopHocValidator>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


builder.Services.AddScoped<SinhVienBusinessValidator>();
builder.Services.AddScoped<LopHocBusinessValidator>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISinhVienService, SinhVienService>();
builder.Services.AddScoped<ILopHocService, LopHocService>();


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