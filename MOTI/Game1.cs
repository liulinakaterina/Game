using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        public static SpriteFont SpriteFont;
        public static ContentManager ContentManager;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private DrawProvider drawProvider;
        private UpdateProvider updateProvider;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/Images";
            ContentManager = Content;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            InitGameField();
            MainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            SpriteFont = Content.Load<SpriteFont>("FontArial");

            this.drawProvider = new DrawProvider(spriteBatch);
            this.updateProvider = new UpdateProvider();
        }

        public static void InitGameField()
        {
            var gameInitializer = new GameInitializer(ContentManager);

            GameField = gameInitializer.GameField;
            GameField.Backgrounds.Add(GameState.Start, ContentManager.Load<Texture2D>("startMenuField"));
            GameField.Backgrounds.Add(GameState.FirstPlayerTurn, ContentManager.Load<Texture2D>("field"));
            GameField.Backgrounds.Add(GameState.FirstPlayerMoving, ContentManager.Load<Texture2D>("field"));
            GameField.Backgrounds.Add(GameState.SecondPlayerMoving, ContentManager.Load<Texture2D>("field"));
            GameField.Backgrounds.Add(GameState.SecondPlayerTurn, ContentManager.Load<Texture2D>("field"));
            GameField.Backgrounds.Add(GameState.Fight, ContentManager.Load<Texture2D>("field"));
            GameField.Backgrounds.Add(GameState.Result, ContentManager.Load<Texture2D>("gameOverField"));
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var mouseState = Mouse.GetState();
            var keyPress = Keyboard.GetState().GetPressedKeys();
            this.updateProvider.UpdateGame(mouseState, keyPress);            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.drawProvider.Draw();
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
