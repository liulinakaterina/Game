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

                    //SetButtonsDefaultConfigs(GameField.Buttons);
                    Draw(gameField.Buttons.Cast<GameObject>().ToList());
                    break;
                case GameState.FirstPlayerTurn:
                    //SetTowersDefaultConfigs(GameField.Towers.Cast<GameObject>().ToList());
                    //SetWarriorsDefaultConfigs(GameField.Players[0].Enemy.Warriors.Cast<GameObject>().ToList());
                    Draw(gameField.Towers.Cast<GameObject>().ToList());
                    Draw(gameField.Players[0].Enemy.Warriors.Cast<GameObject>().ToList());
                 
                    break;
                case GameState.FirstPlayerMoving:
                    Draw(gameField.Towers.Cast<GameObject>().ToList());
                    Draw(gameField.Players[0].Enemy.Warriors.Cast<GameObject>().ToList());
                    break;
                case GameState.SecondPlayerTurn:
                    Draw(gameField.Towers.Cast<GameObject>().ToList());
                    Draw(gameField.Players[0].Enemy.Warriors.Cast<GameObject>().ToList());
                    Draw(gameField.Players[1].Enemy.Warriors.Cast<GameObject>().ToList());
                    break;
                case GameState.SecondPlayerMoving:
                    Draw(gameField.Towers.Cast<GameObject>().ToList());
                    Draw(gameField.Players[0].Enemy.Warriors.Cast<GameObject>().ToList());
                    Draw(gameField.Players[1].Enemy.Warriors.Cast<GameObject>().ToList());
                    break;
                case GameState.Result:
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
