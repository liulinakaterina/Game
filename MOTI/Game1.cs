using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MOTI.Core;
using MOTI.Models;
using MOTI.Models.Enums;
using MOTI.Providers;

namespace MOTI
{
    public class Game1 : Game
    {
        public static GameField GameField;
        public static Rectangle MainFrame;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private DrawProvider drawProvider;
        private UpdateProvider updateProvider;

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
            GameField = gameInitializer.GameField;
            
            MainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            GameField.Backgrounds.Add(GameState.Start, Content.Load<Texture2D>("startMenuField"));
            GameField.Backgrounds.Add(GameState.FirstPlayerTurn, Content.Load<Texture2D>("field"));
            GameField.Backgrounds.Add(GameState.FirstPlayerMoving, Content.Load<Texture2D>("field"));
            GameField.Backgrounds.Add(GameState.SecondPlayerMoving, Content.Load<Texture2D>("field"));
            GameField.Backgrounds.Add(GameState.SecondPlayerTurn, Content.Load<Texture2D>("field"));
            GameField.Backgrounds.Add(GameState.Fight, Content.Load<Texture2D>("field"));
            GameField.Backgrounds.Add(GameState.Result, Content.Load<Texture2D>("gameOverField"));

            this.drawProvider = new DrawProvider(spriteBatch);
            this.updateProvider = new UpdateProvider();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var mouseState = Mouse.GetState();
            this.updateProvider.UpdateGame(mouseState);
            //switch (GameField.GameState)
            //{
            //    case GameState.Start:
            //        break;
            //    case GameState.FirstPlayerTurn:
            //        break;
            //    case GameState.SecondPlayerTurn:
            //        break;
            //    case GameState.Fight:
            //        break;
            //    case GameState.Result:
            //        break;
            //}


            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.drawProvider.Draw();
            //my draw method
            //this.drawProvider.Draw(GameField.GameState);
            base.Draw(gameTime);
        }

        private void DrawTowers()
        {
            var towers = GameField.Towers;
            for(int i = 0; i < towers.Count; i++)
            {
                var sizeSettings = new Rectangle(towers[i].Position.X, towers[i].Position.Y, 150, 200);
                spriteBatch.Draw(towers[i].Image, sizeSettings, Color.White);
            }
        }

        //private void DrawWarriors(Player player)
        //{
        //    var warriors = player.Enemy.Warriors;

        //    for (int i = 0; i < warriors.Count; i++)
        //    {
        //        if(i == 2)
        //        {
        //            var sizeSettings = new Rectangle(warriors[i].CurrentPosition.X, warriors[i].CurrentPosition.Y, 120, 140);
        //            spriteBatch.Draw(warriors[i].Image, sizeSettings, Color.White);
        //        }
        //        else
        //        {
        //            var sizeSettings = new Rectangle(warriors[i].CurrentPosition.X, warriors[i].CurrentPosition.Y, 230, 200);
        //            spriteBatch.Draw(warriors[i].Image, sizeSettings, Color.White);
        //        }
        //    }
        //}
    }
}
