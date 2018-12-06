using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MOTI.Models;
using MOTI.Models.Enums;
using MOTI.Providers;

namespace MOTI
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameField gameField;
        private Texture2D background;
        private Rectangle mainFrame;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/Images";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var gameInitializer = new GameInitializer(Content);
            this.gameField = gameInitializer.GameField;

            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            background = Content.Load<Texture2D>("field");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            switch (gameField.GameState)
            {
                case GameState.Start:
                    break;
                case GameState.FirstPlayerTurn:
                    break;
                case GameState.SecondPlayerTurn:
                    break;
                case GameState.Fight:
                    break;
                case GameState.Result:
                    break;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //my draw method

            base.Draw(gameTime);
        }

        private void DrawTowers()
        {
            var towers = gameField.Towers;
            for(int i = 0; i < towers.Count; i++)
            {
                var sizeSettings = new Rectangle(towers[i].Position.X, towers[i].Position.Y, 150, 200);
                spriteBatch.Draw(towers[i].TowerImage, sizeSettings, Color.White);
            }
        }

        private void DrawWarriors(Player player)
        {
            var warriors = player.Enemy.Warriors;

            for (int i = 0; i < warriors.Count; i++)
            {
                if(i == 2)
                {
                    var sizeSettings = new Rectangle(warriors[i].CurrentPosition.X, warriors[i].CurrentPosition.Y, 120, 140);
                    spriteBatch.Draw(warriors[i].WarriorImage, sizeSettings, Color.White);
                }
                else
                {
                    var sizeSettings = new Rectangle(warriors[i].CurrentPosition.X, warriors[i].CurrentPosition.Y, 230, 200);
                    spriteBatch.Draw(warriors[i].WarriorImage, sizeSettings, Color.White);
                }
            }
        }
    }
}
