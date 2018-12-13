using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MOTI.Core;
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

        public void UpdateGame(MouseState mouseState, Keys[] pressedKeys)
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
                    WaitComputerThinking();
                    DefineWinner();
                    break;
                case GameState.Result:
                    break;
            }

            if(pressedKeys.Contains(Keys.Escape))
            {
                Game1.InitGameField();
                Game1.GameField.GameState = GameState.Start;
            }
            else if(pressedKeys.Contains(Keys.Enter))
            {
                Game1.GameField.GameState = GameState.Result;
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

        private void WaitComputerThinking()
        {
            var computerPlayer = new ComputerPlayer(gameField.Players[1], gameField.Towers, gameField.Players[0].Enemy.Warriors);
            var strategy = computerPlayer.GetOptimalStrategy();

            foreach(var tower in strategy.WarriorDistribution)
            {
                foreach(var warrior in tower.Value)
                {
                    int warriorIndex = gameField.Players[1].Enemy.Warriors.IndexOf(warrior);
                    gameField.Players[1].Enemy.Warriors[warriorIndex].Tower = tower.Key;
                }
            }
            var distance = 0;
            foreach(var warrior in gameField.Players[1].Enemy.Warriors)
            {
                PlacingProvider.SetWarriorNearTower(warrior, warrior.Tower, distance);
                distance += warrior.Size.X / 2;
            }

            gameField.Players[1].Enemy.IsAllWarriorsInTowers = true;
            if (gameField.GameState == GameState.SecondPlayerTurn)
            {
              Game1.GameField.GameState = GameState.SecondPlayerMoving;
            }
        }

        private void DefineWinner()
        {
            var firstPlayer = CalculateArmyReward(Game1.GameField.Players[0].Enemy.Warriors, Game1.GameField.Players[1].Enemy.Warriors);
            var secondPlayer = CalculateArmyReward(Game1.GameField.Players[1].Enemy.Warriors, Game1.GameField.Players[0].Enemy.Warriors);
            Game1.GameField.Players[0].CurrentScore = firstPlayer;
            Game1.GameField.Players[1].CurrentScore = secondPlayer;

            if (firstPlayer > secondPlayer)
            {
                Game1.GameField.Players[0].PlayerProgress = PlayerProgress.Winner;
                Game1.GameField.Players[1].PlayerProgress = PlayerProgress.Looser;
            }
            else if(firstPlayer > secondPlayer)
            {
                Game1.GameField.Players[0].PlayerProgress = PlayerProgress.Looser;
                Game1.GameField.Players[0].PlayerProgress = PlayerProgress.Winner;
            }
            else
            {
                Game1.GameField.Players[0].PlayerProgress = PlayerProgress.None;
                Game1.GameField.Players[0].PlayerProgress = PlayerProgress.None;
            }
        }

        private int CalculateArmyReward(List<Warrior> warriors, List<Warrior> enemies)
        {
            int reward = 0;

            var warriosVariant = new Variant(warriors.Count);
            var enemyVariant = new Variant(enemies.Count);

            foreach(var warrior in warriors)
            {
                warriosVariant.Add(warrior.Tower, warrior);
            }

            foreach (var warrior in enemies)
            {
                enemyVariant.Add(warrior.Tower, warrior);
            }

            reward = warriosVariant.GetEfficiency(enemyVariant);

            return reward;
        }


    }
}
