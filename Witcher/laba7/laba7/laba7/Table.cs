using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace Laba7
{
    [Table(Name = "Order")]
    public class OrderTable
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public int WitcherId { get; set; }

        [Column]
        public int MonsterId { get; set; }

        [Column]
        public int CountOfMoney { get; set; }

    }

    [Table(Name = "Witcher")]
    public class WitcherTable
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }
        
        [Column]
        public string SkillLevel { get; set; }

        [Column]
        public int NumberOfKills { get; set; }

    }

    [Table(Name = "Monster")]
    public class MonsterTable
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string ThreatLevel { get; set; }

        [Column]
        public string Class { get; set; }

        [Column]
        public int SusceptibilityId { get; set; }

    }

    [Table(Name = "Susceptibility")]
    public class SusceptibilityTable
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Type { get; set; }

    }


}
