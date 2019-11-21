using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace ConsoleApp1
{
    class Program
    {
        private static bool flag;
        static void Main(string[] args)
        {
            flag = true;
            XmlSchemaCollection sc = new XmlSchemaCollection();
            sc.Add("", "E:/Witcher/WitcherDTD.xsd");

            XmlTextReader tr = new XmlTextReader("E:/Witcher/WitcherDTD.xml");

            XmlValidatingReader vr = new XmlValidatingReader(tr);

            vr.ValidationType = ValidationType.Schema;

            vr.Schemas.Add(sc);

            vr.ValidationEventHandler += new ValidationEventHandler(MyHandler);

            try
            {
                while (vr.Read())
                {
                    if (vr.NodeType == XmlNodeType.Element && vr.LocalName == "CountOfMoney")
                    {
                        string name = vr.ReadElementString();
                        for (var i = 0; i < name.Length; i++)
                        {
                            int n;
                            if (int.TryParse(name.Substring(i), out n)) {  }
                            else
                                throw new XmlException("CountOfMoney cannot consider latters ");
                        }
                        int number = Convert.ToInt32(name);
                        if (number > 10000)
                        {
                            throw new XmlException("CountOfMoney cannot be more then 10000 ");
                        }
                        else if (number < 10)
                        {
                            throw new XmlException("CountOfMoney cannot be less then 10 ");
                        }
                        //Console.WriteLine("CountOfMoney: " + number);
                    }
                    if (vr.NodeType == XmlNodeType.Element && vr.LocalName == "WitcherName")
                    {
                        string name = vr.ReadElementString();
                        if (name == "")
                        {
                            throw new XmlException("WitcherName should be ");
                        }

                        for (var i = 0; i < name.Length; i++)
                        {
                            int n;
                            if (int.TryParse(name.Substring(i), out n))
                            {
                                throw new XmlException("WitcherName cant consider numbers " );
                            }
                             
                        }
                    }
                    if (vr.NodeType == XmlNodeType.Element && vr.LocalName == "MonsterName")
                    {
                        string name = vr.ReadElementString();
                        if (name == "")
                        {
                            throw new XmlException("MonsterName should be" + vr.XmlLang );
                        }
                        for (var i = 0; i < name.Length; i++)
                        {
                            int n;
                            if (int.TryParse(name.Substring(i), out n))
                            {
                                throw new XmlException("WitcherName cant consider numbers ");
                            }
                        }
                    }
                }
            }
            catch (XmlException ex)
            {
                flag = false;
                Console.WriteLine("XmlException: " + ex.Message);
            }
            finally
            {
                vr.Close();
            }
            if (flag == true)
            {
                Console.WriteLine("Success!");
            }
            Console.ReadKey();
        }

        public static void MyHandler(object sender, ValidationEventArgs e)
        {
            flag = false;
            Console.WriteLine("Validation failed! Error message: " + e.Message);
        }
    }
}
