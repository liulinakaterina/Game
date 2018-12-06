using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MOTI.Models;
using MOTI.Providers;

namespace MOTI.ViewModels
{
    public class WarriorViewModel : GameObjectViewModel
    {
        public override void Reset(List<GameObject> gameObjects, Rectangle frame)
        {
            base.Reset(gameObjects, frame);

            PlacingProvider.PlaceWarriorsOnInitPositions(gameObjects, frame.Width, frame.Height);
        }
    }
}
