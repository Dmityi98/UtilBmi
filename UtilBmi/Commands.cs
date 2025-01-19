using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilBmi
{
    public class Commands
    {
        public static void InputCommand(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) 
                return;


            string commandInput = input.Trim().ToLower();

            if (commandInput.StartsWith("add"))
            {
                Console.WriteLine("Команда для добавления");
                return;
            }
            else if(commandInput.Equals("stat "))
            {

            }
        }
        public static void CommandAdd(string input)
        {

        }

        public static void CommandStat(string input)
        {
        }
    }
}
