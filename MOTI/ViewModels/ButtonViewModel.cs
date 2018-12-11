using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MOTI.Models;
using MOTI.Models.Enums;
using MOTI.Providers;
using System.Collections.Generic;

namespace MOTI.ViewModels
{
    public static class ButtonViewModel
    {
        public static ButtonPurpose GetAction(MouseState mouseState, List<Button> buttons)
        {

            var buttonResult = ButtonPurpose.None;

            foreach(var button in buttons)
            {
                buttonResult = Update(mouseState, button);
                if(buttonResult == ButtonPurpose.Play)
                {
                    break;
                }
            }
            UpdateButtonAppearence(buttons);
            return buttonResult;
        }

        private static ButtonPurpose Update(MouseState mouseState, Button button)
        {
            var rectangle = new Rectangle(button.Position, button.Size);
            var buttonResult = ButtonPurpose.None;

            if (rectangle.Contains(mouseState.X, mouseState.Y))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    button.ButtonState = State.Pressed;
                    buttonResult = button.ButtonPurpose;
                }
                else if(button.ButtonState == State.Pressed)
                {
                    button.ButtonState = State.Normal;
                }
                else
                {
                    button.ButtonState = State.PointerOver;
                }
            }
            else
            {
                button.ButtonState = State.Normal;
            }

            return buttonResult;
        }

        private static void UpdateButtonAppearence(List<Button> buttons)
        {
            PlacingProvider.SetButtonSizes(buttons);

            foreach(var button in buttons)
            {
                switch(button.ButtonState)
                {
                    case State.PointerOver:
                        button.Size = new Point(button.Size.X - 6, button.Size.Y - 6);
                        break;
                    case State.Pressed:
                        button.Size = new Point(button.Size.X + 6, button.Size.Y + 6);
                        break;
                }
            }
        }
    }
}
