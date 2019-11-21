using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Linq;


namespace Laba7
{
    class Task2
    {
        private XDocument xdoc = XDocument.Load("Witcher.xml");

        public void readFromXML()
        {
            foreach (XElement element in xdoc.Elements("Orders").Elements("Order"))
            {
                XElement WitcherName = element.Element("WitcherName");
                XElement MonsterName = element.Element("MonsterName");
                XElement CountOfMoney = element.Element("CountOfMoney");

                Console.WriteLine("Имя ведьмака: " + WitcherName.Value);
                Console.WriteLine("Название монстра: " + MonsterName.Value);
                Console.WriteLine("Количество денег: " + CountOfMoney.Value);
                Console.WriteLine();
            }
        }

        public void updateXML()
        {
            var root = xdoc.Elements("Orders");

            Console.WriteLine("\nВвидите имя ведьмака: ");
            var Witchname = Console.ReadLine();

            Console.WriteLine("\nВведите название элемента для изменения: ");
            var elementTag = Console.ReadLine();

            Console.WriteLine("\nВведите новое значение для элемента: ");
            var newElementValue = Console.ReadLine();
            //xe.Remove();
            foreach (XElement xe in root.Elements("Order").ToList())
            {
                if (xe.Element("WitcherName").Value == Witchname)
                {
                    xe.Element(elementTag).Value = newElementValue;
                }
            }
            xdoc.Save("WitcherUpdate.xml");
        }

        public void addToXML()
        {
            Console.Write("Введите имя ведьмака: ");
            var Witchername = Console.ReadLine();

            Console.WriteLine();
            Console.Write("Введите название монстра: ");
            var Monstername = Console.ReadLine();

            Console.WriteLine();
            Console.Write("Введите количество денег за заказ: ");
            var CountOfMoney = Console.ReadLine();

            xdoc.Element("Orders").Add(new XElement("Order",
                                  new XElement("WitcherName", Witchername),
                                  new XElement("MonsterName", Monstername),
                                  new XElement("CountOfMoney", CountOfMoney)
                                  ));
            xdoc.Save("WitcherAdd.xml");
        }

        public Task2()
        {
            bool flag = true;

            while (flag)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("0. Назад.");
                Console.WriteLine("1. Чтение из XML документа.");
                Console.WriteLine("2. Обновление XML документа.");
                Console.WriteLine("3. Запись (Добавление) в XML документ.");
                Console.Write("Ввод: ");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        flag = false;
                        break;
                    case "1":
                        readFromXML();
                        break;
                    case "2":
                        updateXML();
                        break;
                    case "3":
                        addToXML();
                        break;
                    default:
                        Console.WriteLine("Попробуйте еще раз.");
                        break;
                }
            }
        }
    }
}