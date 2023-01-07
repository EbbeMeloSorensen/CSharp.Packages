using ClassLibrary;
using ConsoleApp;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<App>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var serilogLogger = new LoggerConfiguration()
                                    .Enrich.FromLogContext()
                                    .WriteTo.File("logs/MediatRSample.txt", rollingInterval: RollingInterval.Day)
                                    .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.AddSerilog(logger: serilogLogger, dispose: true);
            });

            // Denne sikrer, at den constructor, som vi har i bl.a. List.Handler, får den DataContext, den skal bruge!
            //services.AddDbContext<DataContext>(opt =>
            //{
            //    //opt.UseSqlite("connectionString");
            //    //opt.UseNpgsql("connectionString");
            //    opt.UseSqlServer("connectionString");
            //});

            services.AddMediatR(Assembly.GetExecutingAssembly());

            // Denne er nødvendig for at kunne injecte en dependency til den request og requesthandler, der ligger i det eksterne class library
            services.AddMediatR(assemblies: typeof(List.Handler).Assembly);

            // Denne sikrer, at den constructor, som vi har i bl.a. List.Handler, får den mapper, den skal bruge
            //services.AddAutoMapper(assemblies: typeof(MappingProfiles).Assembly);

            services.AddTransient<IMediatorService, MediatorService>();
            services.AddTransient<App>();
            return services;
        }
    }
}

