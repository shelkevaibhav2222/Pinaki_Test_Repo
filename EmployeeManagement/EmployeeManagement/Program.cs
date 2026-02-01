using EmployeeManagement.Application.Mapping;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// HTTP request/response logging
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields =
        HttpLoggingFields.RequestMethod
        | HttpLoggingFields.RequestPath
        | HttpLoggingFields.RequestQuery
        | HttpLoggingFields.ResponseStatusCode
        | HttpLoggingFields.Duration;
});

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDB"))
);

// Dependency Injection
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeSalaryRepository, EmployeeSalaryRepository>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<EmployeeSalaryService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler("/Error");

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Handle non-exception status codes (404/403/etc.) in one place
app.UseStatusCodePagesWithReExecute("/Error", "?statusCode={0}");

app.UseHttpLogging();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
