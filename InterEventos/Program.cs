using Data.Commands;
using Data.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Models.Interfaces;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Bussiness.Logic;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins", policy =>
            {
                policy.WithOrigins("http://localhost:4200") // Reemplaza con el dominio de tu frontend
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddHttpContextAccessor()
        .AddAuthorization().AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });

        builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
        //  Inject dependency
        builder.Services.AddScoped<IEventServices, EventLogic>();
        builder.Services.AddScoped<IEventCommands, EventCommands>();
        builder.Services.AddScoped<IUserServices, UserLogic>();
        builder.Services.AddScoped<IUserCommands, UserCommands>();
        builder.Services.AddScoped<IAttendantServices, AttendantLogic>();
        builder.Services.AddScoped<IAttendantCommands, AttendatCommands>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors("AllowSpecificOrigins");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}