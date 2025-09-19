using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddScoped<IWorkoutService, WorkoutService>();  
builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddControllers();

// CORS Policy when we have a frontend and booking API
//builder.Services.AddCors(o =>
//{
//    o.AddPolicy("CorsPolicy", p =>
//    {
//        if (builder.Environment.IsDevelopment())
//            p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
//        else
//            p.WithOrigins("frontend API-link here", "booking API-link here")
//             .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
//    });
//});

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Workout Service API");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();


app.Run();
