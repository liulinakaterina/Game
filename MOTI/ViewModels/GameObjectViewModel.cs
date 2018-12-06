using Microsoft.Xna.Framework;
using MOTI.Models;
using MOTI.Providers;
using System.Collections.Generic;

namespace MOTI.ViewModels
{
    public abstract class GameObjectViewModel
    {
        public virtual void Reset(List<GameObject> gameObjects, Rectangle frame)
        {
            this.Scale(gameObjects, frame);
        }

        public void Scale(List<GameObject> gameObjects, Rectangle frame)
        {
            var prefferedScaledSize = PlacingProvider.GetPrefferedObjectHeigth(gameObjects.Count, frame.Height);

            PlacingProvider.Scale(gameObjects, prefferedScaledSize);
        }
    }
}
