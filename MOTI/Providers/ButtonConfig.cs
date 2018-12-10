using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MOTI.Models;
using MOTI.Models.Enums;
using System.Collections.Generic;

namespace MOTI.Providers
{
    public class ButtonConfig
    {
        private ContentManager contentManager { get; set; }
        List<string> buttonNames = new List<string>() { "PlayButton", "ExitButton" };

        public ButtonConfig(ContentManager manager)
        {
            this.contentManager = manager;
        }

        public Button PlayButton
        {
            get
            {
                return new Button()
                {
                    Image = contentManager.Load<Texture2D>(buttonNames[0]),
                    ButtonPurpose = ButtonPurpose.Play,
                    Position = new Point(500, 0),
                    ActualWidth = 311,
                    ActualHeigth = 54
                };
            }
            set { }
        }

        public Button ExitButton
        {
            get
            {
                return new Button()
                {
                    Image = contentManager.Load<Texture2D>(buttonNames[1]),
                    ButtonPurpose = ButtonPurpose.Exit,
                    Position = new Point(70, 0),
                    ActualHeigth = 310,
                    ActualWidth = 55
                };
            }
            set { }
        }
    }
}
