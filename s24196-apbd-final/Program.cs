using Microsoft.EntityFrameworkCore;
using s24196_apbd_final.Data;
using s24196_apbd_final.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<IDbService, DbService>();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();