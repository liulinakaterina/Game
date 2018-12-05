using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MOTI.Models;
using System.Collections.Generic;

namespace MOTI.Providers
{
    public class WarriorConfig
    {
        private ContentManager contentManager { get; set; }
        List<string> enemy1Names = new List<string>() { "cat", "fox", "pet" };
        List<string> enemy2Names = new List<string>() { "cat2", "fox2", "pet2" };

        public WarriorConfig(ContentManager manager)
        {
            this.contentManager = manager;
        }

        public Warrior Cat
        {
            get
            {
                return new Warrior()
                {
                    WarriorImage = contentManager.Load<Texture2D>(enemy1Names[0]),
                    Power = 3,
                    Position = new Point(350, 400),
                    CurrentPosition = new Point(350, 400),
                    ActualHeigth = 900,
                    ActualWidth = 740
                };
            }
            set { }
        }

        public Warrior Fox
        {
            get
            {
                return new Warrior()
                {
                    WarriorImage = contentManager.Load<Texture2D>(enemy1Names[1]),
                    Power = 3,
                    Position = new Point(700, 400),
                    CurrentPosition = new Point(700, 400),
                    ActualHeigth = 1000,
                    ActualWidth = 900
                };
            }
            set { }
        }

        public Warrior Pet
        {
            get
            {
                return new Warrior()
                {
                    WarriorImage = contentManager.Load<Texture2D>(enemy1Names[2]),
                    Power = 3,
                    Position = new Point(750, 200),
                    CurrentPosition = new Point(750, 200),
                    ActualHeigth = 1100,
                    ActualWidth = 720
                };
            }
            set { }
        }

        public Warrior Cat2
        {
            get
            {
                return new Warrior()
                {
                    WarriorImage = contentManager.Load<Texture2D>(enemy2Names[0]),
                    Power = 3,
                    Position = new Point(350, 400),
                    CurrentPosition = new Point(350, 400),
                    ActualHeigth = 700,
                    ActualWidth = 900
                };
            }
            set { }
        }

        public Warrior Fox2
        {
            get
            {
                return new Warrior()
                {
                    WarriorImage = contentManager.Load<Texture2D>(enemy2Names[1]),
                    Power = 3,
                    Position = new Point(700, 400),
                    CurrentPosition = new Point(700, 400),
                    ActualHeigth = 192,
                    ActualWidth = 256
                };
            }
            set { }
        }

        public Warrior Pet2
        {
            get
            {
                return new Warrior()
                {
                    WarriorImage = contentManager.Load<Texture2D>(enemy2Names[2]),
                    Power = 3,
                    Position = new Point(750, 200),
                    CurrentPosition = new Point(750, 200),
                    ActualHeigth = 960,
                    ActualWidth = 900
                };
            }
            set { }
        }

    }
}
