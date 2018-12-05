namespace MOTI.Models
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Tower
    {
        public Texture2D TowerImage { get; set; }
        public Point Position { get; set; }
        public int Power { get; set; }
        public int Reward { get; set; }
    }
}
