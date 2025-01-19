using Microsoft.Extensions.Logging;
using UtilBmi.DataBase;

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
            else if(commandInput.Equals("stat "))
            {
                return;
            }
        }
        public static void CommandAdd(string input, ILogger logger, DatabaseBmi databaseBmi)
        {
            var command = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (command.Length < 3 || command.Length > 4)
            {
                Console.WriteLine("Error: add <height_in_cm>  <wight_in_kg> [<client_name>]");
            }
            if (!double.TryParse(command[1], out double height))
            {
                Console.WriteLine("Неправильно введённый параметр роста");
            }
            if (!double.TryParse(command[2], out double weight))
            {
                Console.WriteLine("Неправильно введённый параметр массы тела");
            }
            string? name = null;
            if (command.Length == 4)
            {
                name = command[3];
                Console.WriteLine(name);
            }

        }

        public static void CommandStat(string input, ILogger logger, DatabaseBmi databaseBmi)
        {
        }
    }
}
