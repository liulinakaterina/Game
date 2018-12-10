﻿using Microsoft.Xna.Framework;
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
        public GameField GameField;
        public Rectangle MainFrame { get; set; }

        public DrawProvider(SpriteBatch spriteBatch, Rectangle mainFrame, GameField gameField)
        {
            this.spriteBatch = spriteBatch;
            this.MainFrame = mainFrame;
            this.GameField = gameField;
        }

        public void Draw(GameState gameState)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this.GameField.Backgrounds[GameState.Result], this.MainFrame, Color.White);

            switch(gameState)
            {
                case GameState.Start:

                    SetButtonsDefaultConfigs(GameField.Buttons);
                    Draw(GameField.Buttons.Cast<GameObject>().ToList());
                    break;
                case GameState.FirstPlayerTurn:
                    SetTowersDefaultConfigs(GameField.Towers.Cast<GameObject>().ToList());
                    SetWarriorsDefaultConfigs(GameField.Players[0].Enemy.Warriors.Cast<GameObject>().ToList());
                    Draw(GameField.Towers.Cast<GameObject>().ToList());
                    Draw(GameField.Players[0].Enemy.Warriors.Cast<GameObject>().ToList());
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
            PlacingProvider.PlaceTowersOnInitPositions(gameObjects, MainFrame.Width, MainFrame.Height);
            var prefferedImageHigth = PlacingProvider.GetPrefferedObjectHeigth(gameObjects.Count, MainFrame.Height);
            PlacingProvider.Scale(gameObjects.Cast<GameObject>().ToList(), prefferedImageHigth);
        }

        private void SetWarriorsDefaultConfigs(List<GameObject> gameObjects)
        {
            PlacingProvider.PlaceWarriorsOnInitPositions(gameObjects, MainFrame.Width, MainFrame.Height);
            var prefferedImageHigth = PlacingProvider.GetPrefferedObjectHeigth(gameObjects.Count, MainFrame.Height);
            PlacingProvider.Scale(gameObjects.Cast<GameObject>().ToList(), prefferedImageHigth);
        }

        private void SetButtonsDefaultConfigs(List<Button> buttons)
        {
            PlacingProvider.SetButtonSizes(buttons, MainFrame.Width);
            PlacingProvider.PlaceButtonsOnInitPositions(buttons, MainFrame.Width, MainFrame.Height);
            
        }


    }
}