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


// Apply new CORS policy here later
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

builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p =>
        p.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod());
});

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
app.UseCors();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();


app.Run();
