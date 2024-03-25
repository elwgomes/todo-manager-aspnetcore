using Application.Common.Interfaces;
using Application.Users.Command.CreateUser;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // db config
        builder.Services.AddEntityFrameworkMySql()
            .AddDbContext<ApplicationDbContext>(
                options => options.UseMySql(
                    builder.Configuration.GetConnectionString("Database"),
                    new MySqlServerVersion(new Version(6, 0, 0))
                )
            );
        
        // mediatR
        builder.Services.AddMediatR(
            config => config.RegisterServicesFromAssemblyContaining<CreateUserCommand>());
        
        // Add services to the container.
        builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}