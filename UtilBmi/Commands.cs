using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using UtilBmi.DataBase;
using UtilBmi.Models;

namespace UtilBmi
{
    public class Commands
    {
        public static void InputCommand(string input, ILogger logger, DatabaseBmi databaseBmi)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;


            string commandInput = input.Trim().ToLower();

            if (commandInput.StartsWith("add"))
            {
                CommandAdd(input, logger, databaseBmi);
                return;
            }
            else if (commandInput.StartsWith("stat"))
            {
                CommandStat(logger, databaseBmi);
                return;
            }
        }
        public static void CommandAdd(string input, ILogger logger, DatabaseBmi databaseBmi)
        {
            var command = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (command.Length < 3 || command.Length > 4)
            {
                logger.LogError("Error: add <height_in_cm>  <wight_in_kg> [<client_name>]");
            }
            if (!double.TryParse(command[1], out double height))
            {
                logger.LogError("Неправильно введён параметр роста");
                Environment.Exit(0);
                
            }
            if (!double.TryParse(command[2], out double weight))
            {
                logger.LogError("Неправильно введён параметр веса");
                Environment.Exit(0);
                
            }
            string name = null;
            if (command.Length == 4)
            {
                name = command[3];
                Console.WriteLine(name);
            }

            double newHeight = height / 100 ;
            var bmi = CalculateBmi(newHeight, weight);
            var bmiModel = new BmiModel
            {
                UserName = name ?? "unknown",
                Height = newHeight,
                Weight = weight,
                Bmi = bmi,
            };

            logger.LogInformation($"Add new bmi result, user: {bmiModel.UserName}, bmi: {bmiModel.Bmi}, height: {bmiModel.Height}, weight: {bmiModel.Weight}");
            databaseBmi.SaveBmiResultAsync(bmiModel);

        }

        public async static void CommandStat(ILogger logger, DatabaseBmi databaseBmi)
        {
            int count = await databaseBmi.GetCountUsersAsunc();
            var normalBmi = await databaseBmi.GetNormalBmiAsync();
            var insufficientBmi = await databaseBmi.GetInsufficientBmiAsync();
            var redundantBmi = await databaseBmi.GetRedundantBmiAsync();
            var highestClient = await databaseBmi.GetHighestAsunc();
            var heaviestClient = await databaseBmi.GetHeaviestAsunc();
            if (count == 0)
            {
                logger.LogInformation("Сначало надо добавить запись");
            }
            else
            {
                Console.WriteLine($"Кол-во записей {count}");
                Console.WriteLine($"Число пользователей с нормальным ИМТ {normalBmi.Count}");
                Console.WriteLine("Пользователи с недостаточным ИМТ");
                foreach (var item in insufficientBmi)
                {
                    Console.WriteLine($"Имя: {item.UserName} Рост: {item.Height}");
                }
                Console.WriteLine("Пользователи с избыточным ИМТ");
                foreach (var item in redundantBmi)
                {
                    Console.WriteLine($"Имя: {item.UserName} Рост: {item.Height}");
                }
                Console.WriteLine($"Самый высокий клиент: Имя {highestClient.UserName}; Рост {highestClient.Height}");
                Console.WriteLine($"Самый тяжёлого клиент: Имя {heaviestClient.UserName}; Рост {heaviestClient.Height}");
            }
            
        }
        public static double CalculateBmi(double height, double weight)
        {
            if(height <= 0)
            {
                throw new ArgumentException((nameof(height)),"Рост обязательно должен быть больше 0");
            }
            return weight / (height * height);
        }
    }
}
