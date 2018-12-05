using Microsoft.Xna.Framework.Content;
using MOTI.Models;
using System.Collections.Generic;

namespace MOTI.Providers
{
    public class EnemiesConfig
    {
        private ContentManager contentManager { get; set; }
        private WarriorConfig warriorConfig;
        private TowerConfig towerConfig;

        public EnemiesConfig(ContentManager manager)
        {
            this.contentManager = manager;
            this.warriorConfig = new WarriorConfig(manager);
            this.towerConfig = new TowerConfig(manager);
        }

        private List<Warrior> warriorsSet
        {
            get
            {
                return new List<Warrior>()
                {
                    warriorConfig.Cat,
                    warriorConfig.Fox,
                    warriorConfig.Pet
                };
            }
            set { }
        }

        private List<Warrior> warriorsSet2
        {
            get
            {
                return new List<Warrior>()
                {
                    warriorConfig.Cat2,
                    warriorConfig.Fox2,
                    warriorConfig.Pet2
                };
            }
            set { }
        }

        public Enemy Enemy1
        {
            get
            {
                return new Enemy()
                {
                    Warriors = this.warriorsSet,
                    IsAllWarriorsInTowers = false
                };
            }
            set { }
        }

        public Enemy Enemy2
        {
            get
            {
                return new Enemy()
                {
                    Warriors = this.warriorsSet2,
                    IsAllWarriorsInTowers = false
                };
            }
            set { }
        }
    }
}
