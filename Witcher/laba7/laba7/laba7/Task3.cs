using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;


namespace Laba7
{
    class Task3
    {

        public Task3()
        {
            string connectionString = @"Data Source=MSI;Initial Catalog=WitcherDataBase; Integrated Security=True";
            DataContext db = new DataContext(connectionString);
            
            // 1.
            Console.WriteLine("\nОднотабличный запрос на выборку.");
            var orders = from o in db.GetTable<OrderTable>()
                         join w in db.GetTable<WitcherTable>() on o.WitcherId equals w.Id
                         where o.CountOfMoney > 1000
                         select w.Name;

            Console.WriteLine("Имена ведьмаков выполневших заказы свыше 1000: ");
            foreach (var ord in orders)
                Console.WriteLine(ord);


            // 2.
            
            Console.WriteLine();
            Console.WriteLine("\nМноготабличный запрос на выборку.");
            var Monsters = from m in db.GetTable<MonsterTable>()
                       join s in db.GetTable<SusceptibilityTable>() on m.SusceptibilityId equals s.Id
                       select new { monstername = m.Name, susceptibility = s.Name };

            Console.WriteLine("Монстры и их уязвимости: ");
            foreach (var mon in Monsters)
                Console.WriteLine(mon);


            // 3.
            Console.WriteLine();
            Console.WriteLine("\nТри запроса на добавление, изменение и удаление данных в базе данных.");
            // Добавление
            Console.WriteLine("Добавление новой записи");
            Console.Write("Введите имя ведьмака:");
            var WitchName = Console.ReadLine();
            Console.Write("Введите уровень умения:");
            var Skill = Console.ReadLine();
            Console.Write("Количество убийств:");
            var CountOfKills = Convert.ToInt32(Console.ReadLine());

            WitcherTable newWitch = new WitcherTable()
            {
                Name = WitchName,
                SkillLevel =Skill,
                NumberOfKills = CountOfKills
            };
            db.GetTable<WitcherTable>().InsertOnSubmit(newWitch);
            Console.WriteLine("Сохранение...");
            db.SubmitChanges();
            Console.WriteLine("Добавление выполенено успешно.");
            Console.ReadKey();
           
            // Изменение
            Console.WriteLine("\n\nИзменение записи в таблице ");
            Console.WriteLine("Введите новое значение имени ведьмака: ");
            var newValue = Console.ReadLine();
            Console.WriteLine("\n\nВведите id: ");
            var newId = Convert.ToInt32(Console.ReadLine());

            var changeDB = db.GetTable<WitcherTable>().Where(p => p.Id == newId).FirstOrDefault();
            changeDB.Name= newValue;
            Console.WriteLine("Сохранение...");
            db.SubmitChanges();
            Console.WriteLine("Изменение выполенено успешно.");
            Console.ReadKey();
            
            // Удаление
            Console.WriteLine("\n\nУдаление записи в таблице ");
            Console.WriteLine("\n\nВведите id заказа: ");
            newId = Convert.ToInt32(Console.ReadLine());
            var delDB = db.GetTable<OrderTable>().Where(p => p.Id == newId).FirstOrDefault();
            db.GetTable<OrderTable>().DeleteOnSubmit(delDB);
            Console.WriteLine("Сохранение...");
            db.SubmitChanges();
            Console.WriteLine("Удаление выполенено успешно.");

            Console.ReadKey();
            
            // Получение доступа к данным, выполняя только хранимую процедуру.
            UserDataContext.UserDataContext1 db1 = new UserDataContext.UserDataContext1(connectionString);
            int _num;

            Console.WriteLine("Хранимая процедура: ");
            Console.WriteLine("\nВведите число:");
            _num = Convert.ToInt32(Console.ReadLine());

            db1.Getfactor(ref _num);
            Console.WriteLine($"Факториал равен {_num} ");
        }
    }
}
