using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MOTI.Models;
using System.Collections.Generic;

namespace MOTI.Providers
{
    public class TowerConfig
    {
        private ContentManager contentManager { get; set; }
        List<string> towerNames = new List<string>() { "cannon_tower", "teleport", "wizard" };

        public TowerConfig(ContentManager manager)
        {
            this.contentManager = manager;
        }

        public Tower CannonTower
        {
            get
            {
                return new Tower()
                {
                    Image = contentManager.Load<Texture2D>(towerNames[0]),
                    Power = 4,
                    Reward = 1,
                    Position = new Point(500, 0),
                    ActualWidth = 256,
                    ActualHeigth = 454
                };
            }
            set { }
        }

        public Tower WizardTower
        {
            get
            {
                return new Tower()
                {
                    Image = contentManager.Load<Texture2D>(towerNames[2]),
                    Power = 0,
                    Reward = 1,
                    Position = new Point(70, 0),
                    ActualHeigth = 502,
                    ActualWidth = 256
                };
            }
            set { }
        }

        public Tower TeleportTower
        {
            get
            {
                return new Tower()
                {
                    Image = contentManager.Load<Texture2D>(towerNames[1]),
                    Power = 3,
                    Reward = 1,
                    Position = new Point(50, 350),
                    ActualHeigth = 512,
                    ActualWidth = 256
                };
            }
            set { }
        }
    }
}
