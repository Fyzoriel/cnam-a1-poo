using System;
using System.Linq;


namespace TP3.Utils
{
    public static class Ask
    {
        public static string AskString(bool allowNumber = true)
        {
            string str;
            bool isValid;
            do
            {
                str = Console.ReadLine();
                isValid = !(string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str));
                if (!isValid)
                {
                    Console.WriteLine("Vous n'avez pas entré une valeur valide !");
                }
                else if (!allowNumber && str.Any(char.IsDigit))
                {
                    Console.WriteLine("Vous ne pouvez pas mettre de nombre");
                    isValid = false;
                }

            } while (!isValid);
            return str;
        }
        public static float AskFloat()
        {
            float nb;
            bool isValid;
            do
            {
                isValid = float.TryParse(AskString(), out nb);
                if (!isValid)
                {
                    Console.WriteLine("Vous n'avez pas entré un nombre !");
                }
            } while (!isValid);
            
            return nb;
        }
        public static int AskInt()
        {
            return (int) AskFloat();
        }
    }
}