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
        public bool IsAbsoluteStrategy { get; private set; }

        public ComputerPlayer(Player player, List<Tower> towers, List<Warrior> enemyArmy)
        {
            this.player = player;
            this.towers = towers;
            this.enemyArmy = enemyArmy;
        }

        public Variant GetOptimalStrategy()
        {
            var firstPlayerStrategies = this.GetVariants(this.player.Enemy.Warriors);
            var secondPlayerStrategies = this.GetVariants(this.enemyArmy);

            var efficiencies = new int[firstPlayerStrategies.Count, firstPlayerStrategies.Count];

            for(int i = 0; i < firstPlayerStrategies.Count; i++)
            {
                for(int j = 0; j < secondPlayerStrategies.Count; j++)
                {
                    efficiencies[i, j] = firstPlayerStrategies[i].GetEfficiency(secondPlayerStrategies[j]);
                }
            }

            int minMax = 0, maxMin = 0;
            var computerOptimalStrategy = GetOptimalStrategyForComputer(efficiencies, out maxMin);
            var enemyOptimalStrategy = GetOptimalStrategyForEnemy(efficiencies, out minMax);

            this.IsAbsoluteStrategy = computerOptimalStrategy == enemyOptimalStrategy;

            return firstPlayerStrategies[computerOptimalStrategy];
        }

        private int GetOptimalStrategyForEnemy(int[,] efficiencies, out int minMax)
        {
            var maxValues = new List<int>();
            for(int i = 0; i < efficiencies.GetLength(1); i++)
            {
                var max = 0;
                for(int j = 0; j < efficiencies.GetLength(0); j++)
                {
                    if(efficiencies[i, j] > max)
                    {
                        max = efficiencies[i, j];
                    }
                }

                maxValues.Add(max);
            }

            minMax = maxValues.Min();
            return maxValues.IndexOf(minMax);
        }

        private int GetOptimalStrategyForComputer(int[,] efficiencies, out int maxMin)
        {
            var minValues = new List<int>();
            for (int i = 0; i < efficiencies.GetLength(0); i++)
            {
                var min = 1000000;
                for (int j = 0; j < efficiencies.GetLength(1); j++)
                {
                    if (efficiencies[i, j] < min)
                    {
                        min = efficiencies[i, j];
                    }
                }

                minValues.Add(min);
            }

            maxMin = minValues.Max();
            
            return minValues.IndexOf(maxMin);
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
                var currentVariant = basicVariant.Copy();
                currentVariant.Add(tower, warrior);
                basicVariants.Add(currentVariant);
            }

            return basicVariants;
        }
    }

    public class Variant
    {
        public Dictionary<Tower, List<Warrior>> WarriorDistribution;
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

        public int GetEfficiency(Variant variant)
        {
            int points = 0;

            foreach(var tower in this.WarriorDistribution)
            {
                var enemiesInTower = (variant.WarriorDistribution.ContainsKey(tower.Key)) ?
                    variant.WarriorDistribution[tower.Key] : new List<Warrior>();

                var enemyPowerInTower = 0;
                if (enemiesInTower.Count > 0)
                {
                    enemyPowerInTower = enemiesInTower.Select(x => x.Power).Sum();
                }

                var armyPower = tower.Value.Select(x => x.Power).Sum();

                var currentPoints = armyPower - enemyPowerInTower;

                if(currentPoints > 0)
                {
                    points += tower.Key.Reward - tower.Key.Power + enemyPowerInTower;
                }
                else
                {
                    points += -tower.Key.Reward + tower.Key.Power - armyPower;
                }
            }

            return points;
        }

        public Variant Copy()
        {
            var copy = new Variant(this.warriorCount);

            foreach(var tower in this.WarriorDistribution)
            {
                foreach(var warrior in tower.Value)
                {
                    copy.Add(tower.Key, warrior);
                }
            }

            return copy;
        }

        public bool IsFull()
        {
            return warriorCount == currentNumberOfWarriors;
        }


    }
}
