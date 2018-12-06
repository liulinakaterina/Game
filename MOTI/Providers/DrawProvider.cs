using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MOTI.Models.Enums;

namespace MOTI.Providers
{
    public class DrawProvider
    {
        private SpriteBatch spriteBatch;
        private Texture2D background;
        public Rectangle MainFrame { get; set; }

        public DrawProvider(SpriteBatch spriteBatch, Texture2D background, Rectangle mainFrame)
        {
            this.spriteBatch = spriteBatch;
            this.background = background;
            this.MainFrame = mainFrame;
        }

        public void Draw(GameState gameState)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this.background, this.MainFrame, Color.White);

            switch(gameState)
            {
                case GameState.Start:
                    break;
                case GameState.FirstPlayerTurn:
                    break;
                case GameState.FirstPlayerMoving:
                    break;
                case GameState.SecondPlayerTurn:
                    break;
                case GameState.SecondPlayerMoving:
                    break;
                case GameState.Result:
                    break; 
            }
            //this.DrawTowers();
            //this.DrawWarriors(gameField.Players[0]);
            spriteBatch.End();
        }

        private void DrawTowers()
        {

        }
        
    }
}
