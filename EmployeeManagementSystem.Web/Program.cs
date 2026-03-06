using EmployeeManagementSystem.Application.Interfaces;
using EmployeeManagementSystem.Infrastructure;
using EmployeeManagementSystem.Infrastructure.Middlewares;
using EmployeeManagementSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
        b => b.MigrationsAssembly("EmployeeManagementSystem.Infrastructure")));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors("AllowOrigin");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.MapGet("/", context => {
    context.Response.Redirect("/index.html");
    return Task.CompletedTask;
});

app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();