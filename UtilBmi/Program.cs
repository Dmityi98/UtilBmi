using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UtilBmi;
using UtilBmi.DataBase;

namespace TestTask
{
    public class Program
    {
        static void Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(builder =>builder.AddConsole());
            var logger = loggerFactory.CreateLogger<Program>();

            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<DbContextBmi>();
            optionsBuilder.UseSqlite(connectionString);

            using (var context = new DbContextBmi(optionsBuilder.Options))
            {
                try
                {
                    context.Database.EnsureCreatedAsync();
                    logger.LogInformation("Database created");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error when create db");
                    return;
                }
            }
            Console.WriteLine("UtilBmi утилита для вычесления ИМТ");

            var databaseBmi = new DatabaseBmi(new DbContextBmi(optionsBuilder.Options));

            while(true)
            {
                Console.Write("> ");
                string? input = Console.ReadLine();
                Commands.InputCommand(input);
            }
        }
    }
}
