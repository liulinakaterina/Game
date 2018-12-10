using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MOTI.Models.Enums;
using System.Collections.Generic;

namespace MOTI.Models
{
    public class GameField
    {
        public List<Player> Players { get; set; }
        public List<Tower> Towers { get; set; }
        public int InitWindowWidth { get; set; }
        public int InitWindowHeight { get; set; }
        public GameState GameState { get; set; }
        public Dictionary<GameState, Texture2D> Backgrounds;

        public GameField()
        {
            this.Backgrounds = new Dictionary<GameState, Texture2D>();
        }
    }
}
