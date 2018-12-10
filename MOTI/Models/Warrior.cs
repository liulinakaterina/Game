using Microsoft.Xna.Framework;

namespace MOTI.Models
{
    public class Warrior : GameObject
    {
        public Point CurrentPosition { get; set; }
        public int Power { get; set; }
        public Tower Tower { get; set; }
    }
}
