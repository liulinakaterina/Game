using MOTI.Models.Enums;

namespace MOTI.Models
{
    public class Button : GameObject
    {
        public ButtonPurpose ButtonPurpose { get; set; }
        public State ButtonState { get; set; }
    }
}
