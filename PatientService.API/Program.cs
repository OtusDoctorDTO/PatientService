using MassTransit;
using MassTransit.NewIdProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PatientService.API.Consumers;
using PatientService.API.Settings;
using PatientService.Data.Context;
using PatientService.Data.Repositories;
using PatientService.Domain.Repositories;
using PatientService.Domain.Services;
using System.Transactions;

namespace PatientService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            if (configuration.Get<ApplicationSettings>() is not ApplicationSettings receptionConfig)
                throw new ConfigurationException("Error connecting to ApplicationSettings");

            var rabbitMqSettings = configuration.GetSection("RabbitMQ").Get<RabbitMqSetting>();
            ArgumentNullException.ThrowIfNull(rabbitMqSettings);

            builder.Services.AddTransient<IPatientRepository, PatientRepository>();
            builder.Services.AddTransient<IPatientService, Domain.Services.PatientService>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<PatientDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Patient Service", Version = "v1" });
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<PatientConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqSettings.Host, rabbitMqSettings.Port, "/", h =>
                    {
                        h.Username(rabbitMqSettings.Username);
                        h.Password(rabbitMqSettings.Password);
                    });

                    cfg.UseTransaction(_ =>
                    {
                        _.Timeout = TimeSpan.FromSeconds(60);
                        _.IsolationLevel = IsolationLevel.ReadCommitted;
                    });

                    cfg.ReceiveEndpoint(new TemporaryEndpointDefinition(), e =>
                    {
                        e.ConfigureConsumer<PatientConsumer>(context);
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "/swagger/{documentName}/swagger.json";
                });
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patient Service v1"));
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

    }
}
