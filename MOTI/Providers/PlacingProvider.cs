using Microsoft.Xna.Framework;
using MOTI.Models;
using System;
using System.Collections.Generic;

namespace MOTI.Providers
{
    public static class PlacingProvider
    {
        public static void PlaceWarriorsOnInitPositions(List<GameObject> warriors, int windowWidth, int windowHeigth)
        {
            int deltaX = windowWidth - 2 * windowWidth / 12;
            SetCoordinates(warriors, windowHeigth, deltaX);
        }

        public static void PlaceTowersOnInitPositions(List<GameObject> towers, int windowWidth, int windowHeigth)
        {
            int deltaX = windowWidth / 12;
            SetCoordinates(towers, windowHeigth, deltaX);
        }

        public static void PlaceButtonsOnInitPositions(List<Button> buttons, int windowWidth, int windowHeigth)
        {
            SetCoordinates(buttons, windowWidth, windowHeigth);
        }

        public static void Scale(List<GameObject> gameObjects, int prefferedSize)
        {
            foreach(var gameObject in gameObjects)
            {
                ScaleImage(gameObject, prefferedSize);
            }
        }

        public static void SetButtonSizes(List<Button> gameObjects)
        {
            var width = 310;
            var height = 55;

            foreach (var button in gameObjects)
            {
                button.Size = new Point(width, height);
            }
        }

        public static int GetPrefferedObjectHeigth(int objectCount, int windowHeigth)
        {
            int prefferedHeigth = windowHeigth / (objectCount + 2);
            return prefferedHeigth;
        }

        public static void SetWarriorNearTower(Warrior warrior, Tower tower, int distanceX)
        {
            warrior.Position = new Point(tower.Position.X + 2 * tower.Size.X + distanceX,
                tower.Position.Y);
        }

        private static void ScaleImage(GameObject gameObject, int prefferedSide)
        {
            var width = gameObject.ActualWidth;
            var height = gameObject.ActualHeigth;

            var scaledWidth = width * prefferedSide / height;
            gameObject.Size = new Point(scaledWidth, prefferedSide);
        }

        private static void SetCoordinates(List<GameObject> gameObjects, int windowHeigth, int xCoordinate)
        {
            int warriorPrefferedHeigth = GetPrefferedObjectHeigth(gameObjects.Count, windowHeigth);

            int deltaY = (windowHeigth - gameObjects.Count * warriorPrefferedHeigth) / (gameObjects.Count + 1);

            int currentY = deltaY;

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Position = new Point(xCoordinate, currentY);
                currentY += warriorPrefferedHeigth + deltaY;
            }
        }

        internal static void Scale<TResult>(Func<IEnumerable<TResult>> cast)
        {
            throw new NotImplementedException();
        }

        private static void SetCoordinates(List<Button> buttons, int windowWidth, int windowHeigth)
        {
            var centerX = windowWidth / 2;
            var centerY = windowHeigth / 2;

            var X = centerX - buttons[0].Size.X / 2;
            var deltaY = (int)(1.5 * buttons[0].Size.Y);
            var buttonPanelSize = buttons.Count * buttons[0].Size.Y + (buttons.Count - 1) * deltaY;

            var Y = (windowHeigth - buttonPanelSize) / 2;

            foreach(var button in buttons)
            {
                button.Position = new Point(X, Y);
                Y += deltaY;
            }
        }

        
    }
}
