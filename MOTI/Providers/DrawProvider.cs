using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MOTI.Models;
using MOTI.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace MOTI.Providers
{
    public class DrawProvider
    {
        private SpriteBatch spriteBatch;

        public DrawProvider(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        private GameField gameField
        {
            get
            {
                return Game1.GameField;
            }
        }

        private Rectangle mainFrame
        {
            get
            {
                return Game1.MainFrame;
            }
        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this.gameField.Backgrounds[gameField.GameState], this.mainFrame, Color.White);

            switch(gameField.GameState)
            {
                case GameState.Start:
                    Draw(gameField.Buttons.Cast<GameObject>().ToList());
                    break;
                case GameState.FirstPlayerTurn:
                    Draw(gameField.Towers.Cast<GameObject>().ToList());
                    DrawText(gameField.Towers);
                    Draw(gameField.Players[0].Enemy.Warriors.Cast<GameObject>().ToList());
                    DrawText(gameField.Players[0].Enemy.Warriors);

                    break;
                case GameState.FirstPlayerMoving:
                    Draw(gameField.Towers.Cast<GameObject>().ToList());
                    DrawText(gameField.Towers);
                    Draw(gameField.Players[0].Enemy.Warriors.Cast<GameObject>().ToList());
                    DrawText(gameField.Players[0].Enemy.Warriors);
                    break;
                case GameState.SecondPlayerTurn:
                    Draw(gameField.Towers.Cast<GameObject>().ToList());
                    DrawText(gameField.Towers);
                    Draw(gameField.Players[0].Enemy.Warriors.Cast<GameObject>().ToList());
                    DrawText(gameField.Players[0].Enemy.Warriors);
                    Draw(gameField.Players[1].Enemy.Warriors.Cast<GameObject>().ToList());
                    DrawText(gameField.Players[1].Enemy.Warriors, false);
                    break;
                case GameState.SecondPlayerMoving:
                    Draw(gameField.Towers.Cast<GameObject>().ToList());
                    DrawText(gameField.Towers);
                    Draw(gameField.Players[0].Enemy.Warriors.Cast<GameObject>().ToList());
                    DrawText(gameField.Players[0].Enemy.Warriors);
                    Draw(gameField.Players[1].Enemy.Warriors.Cast<GameObject>().ToList());
                    DrawText(gameField.Players[1].Enemy.Warriors, false);
                    break;
                case GameState.Result:
                    PrintResult();
                    break; 
            }

            if(gameField.GameState == GameState.FirstPlayerTurn)
            {
                Game1.GameField.GameState = GameState.FirstPlayerMoving;
            }

            
            spriteBatch.End();
        }

        private void Draw(List<GameObject> gameObjects)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                var sizeSettings = new Rectangle(
                    gameObjects[i].Position.X, 
                    gameObjects[i].Position.Y, 
                    gameObjects[i].Size.X, 
                    gameObjects[i].Size.Y
                    );

                spriteBatch.Draw(gameObjects[i].Image, sizeSettings, Color.White);
            }
        }

        private void DrawText(List<Tower> towers)
        {
            foreach(var tower in towers)
            {
                spriteBatch.DrawString(Game1.SpriteFont, "+ " + tower.Reward.ToString(), new Vector2(tower.Position.X - 10, tower.Position.Y), Color.White);
                spriteBatch.DrawString(Game1.SpriteFont, "- " + tower.Power.ToString(), new Vector2(tower.Position.X - 10, tower.Position.Y + tower.Size.Y), Color.Red);
            }
        }

        private void DrawText(List<Warrior> warriors, bool isFriend = true)
        {
            foreach (var warrior in warriors)
            {
                if(!isFriend)
                {
                    spriteBatch.DrawString(Game1.SpriteFont, "- " + warrior.Power.ToString(), new Vector2(warrior.Position.X + warrior.Size.X / 2, warrior.Position.Y - 18), Color.DarkRed);
                }
                else
                {
                    spriteBatch.DrawString(Game1.SpriteFont, "+ " + warrior.Power.ToString(), new Vector2(warrior.Position.X + warrior.Size.X / 2, warrior.Position.Y - 18), Color.Blue);
                }
            }
        }

        private void PrintResult()
        {
            var firstPlayer = Game1.GameField.Players[0];
            var secondPlayer = Game1.GameField.Players[1];
            var firstColor = Color.White;
            var secondColor = Color.White;
            var message = "Friendship won the quest!";

            if (firstPlayer.PlayerProgress == PlayerProgress.Looser)
            {
                firstColor = Color.Red;
                secondColor = Color.Gold;
                message = "You LOST!";
            }
            else if(firstPlayer.PlayerProgress == PlayerProgress.Winner)
            {
                firstColor = Color.Gold;
                secondColor = Color.Red;
                message = "YOU WON!";
            }
            spriteBatch.DrawString(Game1.SpriteFont,
                "YOU",
                new Vector2(100, 50),
                firstColor);
            spriteBatch.DrawString(Game1.SpriteFont, 
                Game1.GameField.Players[0].CurrentScore.ToString(), 
                new Vector2(100, mainFrame.Height / 2),
                firstColor);

            spriteBatch.DrawString(Game1.SpriteFont, 
                message, 
                new Vector2(mainFrame.Width / 2 - 50, mainFrame.Height / 2), 
                firstColor);

            spriteBatch.DrawString(Game1.SpriteFont,
                "COMPUTER",
                new Vector2(mainFrame.Width - 200, 50),
                secondColor);

            spriteBatch.DrawString(Game1.SpriteFont,
                Game1.GameField.Players[1].CurrentScore.ToString(),
                new Vector2(mainFrame.Width - 200, mainFrame.Height / 2),
                secondColor);
        }

        private void SetTowersDefaultConfigs(List<GameObject> gameObjects)
        {
            PlacingProvider.PlaceTowersOnInitPositions(gameObjects, mainFrame.Width, mainFrame.Height);
            var prefferedImageHigth = PlacingProvider.GetPrefferedObjectHeigth(gameObjects.Count, mainFrame.Height);
            PlacingProvider.Scale(gameObjects.Cast<GameObject>().ToList(), prefferedImageHigth);
        }

        private void SetWarriorsDefaultConfigs(List<GameObject> gameObjects)
        {
            PlacingProvider.PlaceWarriorsOnInitPositions(gameObjects, mainFrame.Width, mainFrame.Height);
            var prefferedImageHigth = PlacingProvider.GetPrefferedObjectHeigth(gameObjects.Count, mainFrame.Height);
            PlacingProvider.Scale(gameObjects.Cast<GameObject>().ToList(), prefferedImageHigth);
        }

        private void SetButtonsDefaultConfigs(List<Button> buttons)
        {
            PlacingProvider.SetButtonSizes(buttons);
            PlacingProvider.PlaceButtonsOnInitPositions(buttons, mainFrame.Width, mainFrame.Height);
        }


    }
}
