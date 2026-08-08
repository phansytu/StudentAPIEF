using FluentValidation;
using FluentValidation.AspNetCore;
using StudentAPIw5.data;
using StudentAPIw5.handler;
using StudentAPIw5.service;
using StudentAPIw5.validator;

var builder = WebApplication.CreateBuilder(args);


// =====================================================
// 1. CONTROLLER
// =====================================================

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<LopHocValidator>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddSingleton<DataSinhVien>();
builder.Services.AddSingleton<DataLopHoc>();


builder.Services.AddScoped<SinhVienBusinessValidator>();
builder.Services.AddScoped<LopHocBusinessValidator>();

builder.Services.AddScoped<ISinhVienService, SinhVienService>();
builder.Services.AddScoped<ILopHocService, LopHocService>();


var app = builder.Build();


// =====================================================
// 8. HTTP REQUEST PIPELINE
// =====================================================

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