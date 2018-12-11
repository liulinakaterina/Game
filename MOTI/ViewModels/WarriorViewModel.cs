using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MOTI.Models;
using MOTI.Models.Enums;
using MOTI.Providers;
using System.Collections.Generic;
using System.Linq;

namespace MOTI.ViewModels
{
    public class WarriorViewModel
    {
        public static bool IsWarriorsDistributionFinished(MouseState mouseState, List<Warrior> warriors, List<Tower> towers)
        {

           // var isAllWarriorsPotentionallyDistributed = true;
            var isAllWarriorsPotentionallyDistributed = false;

            foreach (var warrior in warriors)
            {
                UpdateStates(mouseState, warrior);
            }

            UpdateWarriorAppearence(warriors);

            foreach (var warrior in warriors)
            {
                var isNearTower = IsDistributed(warrior, towers);
                if(!isNearTower && isAllWarriorsPotentionallyDistributed)
                {
                    isAllWarriorsPotentionallyDistributed = false;
                }
            }

            return isAllWarriorsPotentionallyDistributed;
        }

        private static void UpdateStates(MouseState mouseState, Warrior warrior)
        {
            var rectangle = new Rectangle(warrior.Position, warrior.Size);

            if (rectangle.Contains(mouseState.X, mouseState.Y))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    warrior.State = State.Pressed;
                    Move(mouseState, warrior);
                }
                else if (warrior.State == State.Pressed)
                {
                    warrior.State = State.Released;
                }
                else
                {
                    warrior.State = State.PointerOver;
                }
            }
            else
            {
                warrior.State = State.Normal;
            }
        }

        private static void UpdateWarriorAppearence(List<Warrior> warriors)
        {
            PlacingProvider.Scale(warriors.Cast<GameObject>().ToList(), 130);

            foreach (var button in warriors)
            {
                switch (button.State)
                {
                    case State.PointerOver:
                        button.Size = new Point(button.Size.X - 6, button.Size.Y - 6);
                        break;
                    case State.Pressed:
                        button.Size = new Point(button.Size.X + 6, button.Size.Y + 6);
                        break;
                }
            }
        }

        private static void Move(MouseState mouseState, Warrior warrior)
        {
            warrior.Position = new Point(mouseState.Position.X, mouseState.Position.Y);
        }

        private static bool IsDistributed(Warrior warrior, List<Tower> towers)
        {
            var isIntersects = false;
            foreach(var tower in towers)
            {
                var rectangle = new Rectangle(tower.Position, tower.Size);
                isIntersects = rectangle.Contains(warrior.Position);
                if (isIntersects)
                {
                    warrior.Tower = tower;
                }
            }

            return isIntersects;
        }
    }
}
