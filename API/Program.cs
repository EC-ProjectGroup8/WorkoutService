using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddScoped<IWorkOutService, WorkoutService>();  
builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddControllers();
builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy", p =>
    {
        if (builder.Environment.IsDevelopment())
            p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        else
            p.WithOrigins("frontend link here", "booking link here")
             .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();


//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();


app.Run();
