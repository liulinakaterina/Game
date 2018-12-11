using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MOTI.Models;
using MOTI.Models.Enums;
using MOTI.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MOTI.Providers
{
    public class UpdateProvider
    {
        public UpdateProvider()
        {

        }

        private GameField gameField
        {
            get
            {
                return Game1.GameField;
            }
        }

        private Rectangle MainFrame
        {
            get
            {
                return Game1.MainFrame;
            }
        }

        public void UpdateGame(MouseState mouseState)
        {
            switch (gameField.GameState)
            {
                case GameState.Start:
                    SetButtonsDefaultConfigs(gameField.Buttons);
                    this.UpdateButtons(mouseState);
                    break;
                case GameState.FirstPlayerTurn:
                    SetTowersDefaultConfigs(gameField.Towers.Cast<GameObject>().ToList());
                    SetWarriorsDefaultConfigs(Game1.GameField.Players[0].Enemy.Warriors.Cast<GameObject>().ToList());
                    break;
                case GameState.FirstPlayerMoving:
                    SetTowersDefaultConfigs(gameField.Towers.Cast<GameObject>().ToList());
                    this.UpdateFirstEnemy(mouseState);
                    break;
                case GameState.SecondPlayerTurn:
                    SetWarriorsDefaultConfigs(gameField.Players[1].Enemy.Warriors.Cast<GameObject>().ToList());
                    break;
            }
        }

        private void UpdateButtons(MouseState mouseState)
        {
            var chosenAction = ButtonViewModel.GetAction(mouseState, this.gameField.Buttons);
            switch (chosenAction)
            {
                case ButtonPurpose.Play:
                    Game1.GameField.GameState = GameState.FirstPlayerTurn;
                    break;
                case ButtonPurpose.Exit:
                    Game1.GameField.GameState = GameState.End;
                    break;
            }
        }

        private void UpdateFirstEnemy(MouseState mouseState)
        {
            var isAllWarriorsDistributed = WarriorViewModel.IsWarriorsDistributionFinished(mouseState, 
                gameField.Players[0].Enemy.Warriors, 
                gameField.Towers);
            if(isAllWarriorsDistributed)
            {
                Game1.GameField.GameState = GameState.SecondPlayerTurn;
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
            PlacingProvider.SetButtonSizes(buttons);
            PlacingProvider.PlaceButtonsOnInitPositions(buttons, MainFrame.Width, MainFrame.Height);
        }


    }
}
