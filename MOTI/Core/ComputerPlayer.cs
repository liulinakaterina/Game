using MOTI.Models;
using System.Collections.Generic;
using System.Linq;

namespace MOTI.Core
{
    public class ComputerPlayer
    {
        private Player player;
        private List<Tower> towers;
        private List<Warrior> enemyArmy;

        public ComputerPlayer(Player player, List<Tower> towers, List<Warrior> enemyArmy)
        {
            this.player = player;
            this.towers = towers;
            this.enemyArmy = enemyArmy;
        }

        private List<Variant> GetVariants(List<Warrior> warriors)
        {
            var allVariants = new List<Variant>();
            allVariants.Add(new Variant(warriors.Count));
            
            for(int i = 0; i < warriors.Count; i++)
            {
                var currentVariants = new List<List<Variant>>();
                foreach(var variant in allVariants)
                {
                    currentVariants.Add(GetBasicVariant(warriors[i], variant));
                }

                allVariants = currentVariants.SelectMany(x => x).ToList();
            }
            return allVariants;
        }

        private List<Variant> GetBasicVariant(Warrior warrior, Variant basicVariant)
        {
            var basicVariants = new List<Variant>();

            foreach(var tower in towers)
            {
                var currentVariant = basicVariant;
                currentVariant.Add(tower, warrior);
            }

            return basicVariants;
        }
    }

    class Variant
    {
        internal Dictionary<Tower, List<Warrior>> WarriorDistribution;
        int warriorCount;
        private int currentNumberOfWarriors;

        public Variant(int warriorCount)
        {
            this.WarriorDistribution = new Dictionary<Tower, List<Warrior>>();
            this.warriorCount = warriorCount;
            this.currentNumberOfWarriors = 0;
        }

        public void Add(Tower tower, Warrior warrior)
        {
            if(!this.IsFull())
            {
                if (WarriorDistribution.ContainsKey(tower))
                {
                    WarriorDistribution[tower].Add(warrior);
                }
                else
                {
                    WarriorDistribution.Add(tower, new List<Warrior>());
                    WarriorDistribution[tower].Add(warrior);
                }
                currentNumberOfWarriors++;
            }
        }

        public bool IsFull()
        {
            return warriorCount == currentNumberOfWarriors;
        }


    }
}
