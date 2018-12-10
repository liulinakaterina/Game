using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MOTI.Models;
using MOTI.Models.Enums;
using System.Collections.Generic;

namespace MOTI.Providers
{
    public class GameInitializer
    {
        private ContentManager contentManager;
        private EnemiesConfig enemiesConfig;
        private TowerConfig towerConfig;
        private ButtonConfig buttonConfig;
        private const int WindowWidth = 850;
        private const int WindowHeight = 700;
        private Player player1;
        private Player player2;
        private List<Tower> towers;

        public GameInitializer(ContentManager manager)
        {
            this.contentManager = manager;
            this.enemiesConfig = new EnemiesConfig(manager);
            this.towerConfig = new TowerConfig(manager);
            this.buttonConfig = new ButtonConfig(manager);
            this.InitPlayers();
            this.InitTowers();
        }

        private void InitPlayers()
        {
            this.player1 = new Player()
            {
                Enemy = this.enemiesConfig.Enemy1,
                CurrentScore = 0,
                PlayerProgress = PlayerProgress.None
            };

            this.player2 = new Player()
            {
                Enemy = this.enemiesConfig.Enemy2,
                CurrentScore = 0,
                PlayerProgress = PlayerProgress.None
            };
        }

        private void InitTowers()
        {
            this.towers = new List<Tower>()
            {
                this.towerConfig.CannonTower,
                this.towerConfig.TeleportTower,
                this.towerConfig.WizardTower
            };
        }

        public GameField GameField
        {
            get
            {
                return new GameField()
                {
                    Players = new List<Player>()
                    {
                        this.player1,
                        this.player2
                    },
                    GameState = GameState.Start,
                    InitWindowHeight = WindowWidth,
                    InitWindowWidth = WindowHeight,
                    Towers = this.towers,
                    Buttons = new List<Button>()
                    {
                        this.buttonConfig.PlayButton,
                        this.buttonConfig.ExitButton
                    }
                };
            }
        }
    }
}