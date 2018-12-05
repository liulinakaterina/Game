using MOTI.Models.Enums;

namespace MOTI.Models
{
    public class Player
    {
        public Enemy Enemy { get; set; }
        public int CurrentScore { get; set; }
        public PlayerProgress PlayerProgress { get; set; }
    }
}
