using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Laba7
{
    class Program
    {
        static void Main(string[] args)
        {
            //FirstTask first = new FirstTask();
            //Console.Clear();

            try
            {
                bool flag = true;

                while (flag)
                {
                    Console.WriteLine("\nМеню:");
                    Console.WriteLine("0. Выход");
                    Console.WriteLine("1. Запросы");
                    Console.WriteLine("2. LINQ to XML");
                    Console.WriteLine("3. LINQ to SQL");
                    Console.Write("Ввод: ");

                    var input = Console.ReadLine();
                    switch (input)
                    {
                        case "0":
                            flag = false;
                            break;
                        case "1":
                            Task1 first = new Task1();
                            break;
                        case "2":
                            Task2 second = new Task2();
                            break;
                        case "3":
                            Task3 third = new Task3();
                            break;
                        default:
                            Console.WriteLine("Попробуйте еще раз.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Ошибка. " + ex.Message);
            }
            Console.ReadKey();
        }

    }
}
