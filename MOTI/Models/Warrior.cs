using Microsoft.Xna.Framework;
using MOTI.Models.Enums;

namespace MOTI.Models
{
    public class Warrior : GameObject
    {
        public int Power { get; set; }
        public Tower Tower { get; set; }
        public State State { get; set; }
    }
}
