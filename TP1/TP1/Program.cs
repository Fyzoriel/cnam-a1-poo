using System;
using System.Linq;
using System.Threading;


namespace TP1
{
    class Program
    {
        static string AskString(bool allowNumber = true)
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
        
        static float AskFloat()
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

        static float AskFloatPositive()
        {
            float nb;
            bool isValid;
            do
            {
                nb = AskFloat();
                isValid = nb > 0;
                if (!isValid)
                { 
                    Console.WriteLine("Le nombre n'est pas supérieur à 0");
                }
            } while (!isValid);

            return nb;
        }
        static int AskInt()
        {
            return (int) AskFloat();
        }

        static int AskIntPositive()
        {
            return (int) AskFloatPositive();
        }
        
        static float GetIMC(int height, float weight)
        {
            var heightInMeter = (float) height / 100;
            return weight / (heightInMeter * heightInMeter);
        }

        static void CommentImc(float imc)
        {
            // D'après la documentation et les conventions de nommage associées (https://github.com/dotnet/runtime/tree/main/docs/coding-guidelines)
            // Les constantes locales utilisent le PascalCase 
            // "12 - We use PascalCasing to name all our constant local variables and fields.
            // The only exception is for interop code where the constant value should exactly match the name and value of
            // the code you are calling via interop."
            const string Inf16 = "Attention à l’anorexie !";
            const string Between16And18 = "Vous êtes un peu maigrichon !";
            const string Between18And25 = "Vous êtes de corpulence normale !";
            const string Between25And30 = "Vous êtes en surpoids !";
            const string Between30And35 = "Obésité modérée !";
            const string Between35And40 = "Obésité sévère !";
            const string Sup40 = "Obésité morbide !";

            if (imc < 16.5)
            {
                Console.WriteLine(Inf16);
            }
            else if (imc < 18.5)
            {
                Console.WriteLine(Between16And18);
            }
            else if (imc < 25.5)
            {
                Console.WriteLine(Between18And25);
            }
            else if (imc < 30.5)
            {
                Console.WriteLine(Between25And30);
            }
            else if (imc < 35.5)
            {
                Console.WriteLine(Between30And35);
            }
            else if (imc < 40.5)
            {
                Console.WriteLine(Between35And40);
            }
            else
            {
                Console.WriteLine(Sup40);
            }
        }
        
        static string GetFullName(string name, string lastName)
        {
            return $"{name} {lastName.ToUpper()}";
        }

        static void AskHair()
        {
            bool retry;
            Console.WriteLine("Combien as-tu de cheveux (Entre 100000 et 150000)");

            do
            {
                int nb = AskInt();
                retry = nb < 100000 || nb > 150000;
                if (retry)
                {
                    Console.WriteLine("Vous n'avez pas entré un nombre compris entre 100000 et 150000)");
                }
            } while (retry);
        }

        static void Count()
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
        }

        static void CallTatalJaqueline()
        {
            Console.WriteLine("Bonjour, vous êtes la boite vocale de votre Tata Jacqueline adorée, " +
                              "je suis actuellement absente donc vous pouvez laisser un message avec le truc sonore, " +
                              "le bip sonore, mince comment on modifie le messa. *BIP*");
        }
        
        
        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.WriteLine("Oh bonjour et bienvenue dans le HardCorner, jeune substitut de relation humaine");
                Console.WriteLine("Donne moi ton nom, vil chenapan: ");

                string lastName = AskString(false);
                Console.WriteLine("Et quel est ton prénom, petit galopin:");

                string name = AskString(false);
                Console.WriteLine($"Bonjour {GetFullName(name, lastName)}");

                Console.WriteLine("Quelle est ta taille en cm?");
                var height = AskIntPositive();

                Console.WriteLine("Quel est ton poids en kg?");
                var weight = AskFloatPositive();

                Console.WriteLine("Quel est ton age... en années?");
                int age = AskIntPositive();

                if (age < 18)
                {
                    Console.WriteLine("Tu as moins de 18 ans donc je vais t'apprendre un truc: tu as déjà raté ta vie");
                }

                float imc = GetIMC(height, weight);
                Console.WriteLine($"Voici votre IMC: {imc: 0.0}");
                CommentImc(imc);

                AskHair();
                
                do
                {
                    Console.WriteLine("1 - Quitter le programme");
                    Console.WriteLine("2 - Recommencer le programme");
                    Console.WriteLine("3 - Compter jusqu’à 10");
                    Console.WriteLine("4 - Téléphoner à Tata Jacqueline");
                    choice = AskInt();
                } while (choice < 0 || choice > 4);

                switch (choice)
                {
                    case 3:
                        Count();
                        break;
                    case 4:
                        CallTatalJaqueline();
                        break;
                }

            } while (choice == 2);
            
            Console.WriteLine("Au revoir !!");
            Thread.Sleep(3000);
        }
    }
}