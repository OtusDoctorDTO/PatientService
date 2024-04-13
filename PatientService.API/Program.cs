using MassTransit;
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
                .AddJsonFile("appsettings.Development.json", true)
                .Build();

            if (configuration.Get<ApplicationSettings>() is not ApplicationSettings receptionConfig)
                throw new ConfigurationException("������ ��� ����������� � ApplicationSettings");

            string connection = configuration!.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connection))
                throw new ConfigurationException("������ �����������");

            builder.Services.AddDbContext<PatientDbContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddTransient<IPatientRepository, PatientRepository>();
            //builder.Services.AddTransient<IPatientService, PatientService>();

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<PatientConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(receptionConfig.RabbitMqSetting.Host, receptionConfig.RabbitMqSetting.Port, receptionConfig.RabbitMqSetting.Path, h =>
                    {
                        h.Username(receptionConfig.RabbitMqSetting.Username);
                        h.Password(receptionConfig.RabbitMqSetting.Password);
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

    }
}
