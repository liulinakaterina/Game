using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MOTI.Models
{
    public class GameObject
    {
        public Texture2D Image { get; set; }
        public Point Position { get; set; }
        public Point Size { get; set; }
        public int ActualWidth { get; set; }
        public int ActualHeigth { get; set; }
    }
}
