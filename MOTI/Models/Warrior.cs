using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MOTI.Models
{

    public class Warrior
    {
        public Texture2D WarriorImage { get; set; }
        public Point Position { get; set; }
        public Point CurrentPosition { get; set; }
        public int Power { get; set; }
        public Tower Tower { get; set; }
    }
}
