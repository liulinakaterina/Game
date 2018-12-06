using Microsoft.Xna.Framework;
using MOTI.Models;
using MOTI.Providers;
using System.Collections.Generic;

namespace MOTI.ViewModels
{
    public class TowerViewModel : GameObjectViewModel
    {
        public override void Reset(List<GameObject> gameObjects, Rectangle frame)
        {
            base.Reset(gameObjects, frame);

            PlacingProvider.PlaceTowersOnInitPositions(gameObjects, frame.Width, frame.Height);
        }
    }
}
