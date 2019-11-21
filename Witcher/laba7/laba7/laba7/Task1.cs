using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba7
{
    class Order
    {
        public int WitcherId{ get; set; }
        public int MonsterId { get; set; }
        public int CounOfMoney { get; set; }
    }

    class Witcher
    {
        public int WitcherId { get; set; }
        public string WitcherName { get; set; }
        public string SkillLevel { get; set; }
        public int NumberOfKills { get; set; }
    }

    class Monster
    {
        public int MonsterId { get; set; }
        public string MonsterName { get; set; }
        public string ThreatLevel { get; set; }
    }

    class Task1
    {
        public Task1()
        {
            IList<Order> OrderList = new List<Order>
            {
                new Order() { WitcherId=1, MonsterId=1, CounOfMoney = 21},
                new Order() { WitcherId=2, MonsterId=2, CounOfMoney = 21321},
                new Order() { WitcherId=3, MonsterId=3, CounOfMoney = 2321},
                new Order() { WitcherId=4, MonsterId=4, CounOfMoney = 241},
                new Order() { WitcherId=5, MonsterId=5, CounOfMoney = 2}
            };

            IList<Witcher> WitcherList = new List<Witcher>
            {
                new Witcher() { WitcherId = 1, WitcherName = "Boba", SkillLevel = "low", NumberOfKills = 21 },
                new Witcher() { WitcherId = 2, WitcherName = "Bib", SkillLevel = "low", NumberOfKills = 21 },
                new Witcher() { WitcherId = 3, WitcherName = "Ivan", SkillLevel = "frendly", NumberOfKills = 0 },
                new Witcher() { WitcherId = 4, WitcherName = "Karina", SkillLevel = "immortal", NumberOfKills = 600 },
                new Witcher() { WitcherId = 5, WitcherName = "Karyun", SkillLevel = "medium", NumberOfKills = 2 }
            };

            IList<Monster> MonsterList = new List<Monster>
            {
                new Monster() { MonsterId = 1, MonsterName = "Laba", ThreatLevel = "imposible"},
                new Monster() { MonsterId = 2, MonsterName = "Rezanova", ThreatLevel = "RunStupidBoy"},
                new Monster() { MonsterId = 3, MonsterName = "Kurov", ThreatLevel = "GiveMeYourKursach"},
                new Monster() { MonsterId = 4, MonsterName = "Feministka", ThreatLevel = "YouCantDoThis"},
                new Monster() { MonsterId = 5, MonsterName = "Koryun", ThreatLevel = "high"}
            };

            // Вывод всех значений
            for (int i = 0; i < OrderList.Count; i++)
            {
                Console.WriteLine(WitcherList[i].WitcherName + " | " + MonsterList[i].MonsterName + " | " + OrderList[i].CounOfMoney);
            }
                


            Console.WriteLine();
            // 1. Запрос
            Console.WriteLine("1. Запрос: заказы больше 100");
            var _1result = from c in OrderList join d in WitcherList on c.WitcherId equals d.WitcherId
                           let maxMoney = 100
                           where c.CounOfMoney > maxMoney
                           select d.WitcherName;

            foreach (var i in _1result)
                Console.WriteLine(i);

            Console.WriteLine();
            // 2. Запрос
            Console.WriteLine("2. Запрос: Имена ведьмаков у которых уровень low, отсортированные по деньгам");
            var _2result = from c in OrderList
                           join d in WitcherList on c.WitcherId equals d.WitcherId
                           where d.SkillLevel == "low"
                           orderby c.CounOfMoney descending
                           select $"WitchName {d.WitcherName}";

            foreach (var i in _2result)
                Console.WriteLine(i);
            
            Console.WriteLine();
            // 3. Запрос
            Console.WriteLine("3. Запрос: oftype WitcherName");
            var _3result = from c in WitcherList.OfType<Witcher>()
                           select c.WitcherName;
            
            foreach (var i in _3result)
                Console.WriteLine(i);

            Console.WriteLine();
            // 4. Запрос 
            Console.WriteLine("4. Запрос: oderby WitcherId then by CountOfMoney");
            var _4result = from c in OrderList
                           orderby c.WitcherId, c.CounOfMoney
                           select c;

            foreach (var i in _4result)
                Console.WriteLine(i.WitcherId + " - " + i.CounOfMoney);


            Console.WriteLine();
            // 5. Запрос
            Console.WriteLine("5. Запрос: количество Ведьмаков в каждой группе скила");
            var _5result = from c in OrderList
                           join d in WitcherList on c.WitcherId equals d.WitcherId
                           group c by d.SkillLevel into hpGroup
                           select new { first = hpGroup.Key, words = hpGroup.Count() };

            foreach (var item in _5result)
                Console.WriteLine("{0} имеет {1} элементов", item.first, item.words);

            Console.WriteLine();
            // 6. Запрос
            Console.WriteLine("6. Запрос: ");
            var _6result = from c in OrderList
                           join d in MonsterList on c.MonsterId equals d.MonsterId
                           select new { monstername = d.MonsterName, money = c.CounOfMoney };


            foreach (var item in _6result)
                Console.WriteLine("За {0} дают {1} денег", item.monstername, item.money);
        }
    }
}
