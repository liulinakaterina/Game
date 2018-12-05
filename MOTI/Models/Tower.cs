namespace MOTI.Models
{
    using Microsoft.Xna.Framework.Graphics;

    public class Tower : GameObject
    {
        public Texture2D TowerImage { get; set; }
        public int Power { get; set; }
        public int Reward { get; set; }
    }
}
