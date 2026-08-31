using Microsoft.EntityFrameworkCore; 
using learn_asp_clean_structure.Services;
using learn_asp_clean_structure.Data;
using learn_asp_clean_structure.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionHandelingMiddleware>();
app.MapControllers();

app.Run();