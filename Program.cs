using learn_asp_clean_structure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();

app.MapControllers();

app.Run();