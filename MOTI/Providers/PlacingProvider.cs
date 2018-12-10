using Microsoft.Xna.Framework;
using MOTI.Models;
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

        public static void Scale(List<GameObject> gameObjects, int prefferedSize)
        {
            foreach(var gameObject in gameObjects)
            {
                ScaleImage(gameObject, prefferedSize);
            }
        }

        public static int GetPrefferedObjectHeigth(int objectCount, int windowHeigth)
        {
            int prefferedHeigth = windowHeigth / (objectCount + 2);
            return prefferedHeigth;
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
    }
}
