using System.Collections.Generic;

namespace MOTI.Models
{
    public class Enemy
    {
        public List<Warrior> Warriors { get; set; }

        public bool IsAllWarriorsInTowers { get; set; }
    }
}
