using SwingFilms.Infrastructure;
using SwingFilms.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories();
builder.Services.AddDatabase(builder.Configuration.GetConnectionString("DefaultConnection")!);

builder.Services.AddServiceMemoryCache();
builder.Services.AddServiceMediatR();
builder.Services.AddServices();
builder.Services.AddServiceLocalization();
builder.Services.AddServiceMapper();
builder.Services.AddServiceFluentValidation();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Product"))
{
    app.UseSwagger().UseSwaggerUI();
}

app.MapControllers();
app.Run();