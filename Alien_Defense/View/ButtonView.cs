using Alien_Defense.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Alien_Defense.View
{
    /// <summary>
    /// Класс отображения кнопки
    /// </summary>
    public class ButtonView
    {
        public Texture2D ButtonTexture { get; private set; }
        private string _assetButtonName = "button";
        public Texture2D ButtonChoiceLvlTexture { get; private set; }
        private string _assetButtonChoiceLvlName = "buttonLevel";

        public Texture2D ButtonTowerTexture { get; private set; }
        private string _assetTowerButtonName = "buttonTower";
        private FontView _fontView;
        public Color PenColour { get; private set; } = Color.White;
        public ButtonGameState ButtonState { get; private set;}
        /// <summary>
        /// Метод загрузки контента
        /// </summary>
        /// <param name="content"> контент менеджер</param>
        public void LoadContent(ContentManager content, ButtonGameState buttonGameState)
        {
            ButtonState = buttonGameState;
            switch (ButtonState)
            {
                case (ButtonGameState.ChoiseMapButton):
                    ButtonChoiceLvlTexture = content.Load<Texture2D>(_assetButtonChoiceLvlName);
                    break;
                case (ButtonGameState.MenuButton):
                    ButtonTexture = content.Load<Texture2D>(_assetButtonName);
                    break;
                case (ButtonGameState.TowerButton):
                    ButtonTowerTexture = content.Load<Texture2D>(_assetTowerButtonName);
                    break;

            }
            _fontView = new FontView();
            _fontView.LoadContent(content);
        }

        /// <summary>
        /// Метод отрисовки кнопки
        /// </summary>
        /// <param name="spriteBatch"> Кисть </param>
        /// <param name="button"> Кнопка </param>
        public void Draw(SpriteBatch spriteBatch, Button button)
        {
            var colour = Color.White;

            if (button._isHovering)
            {
                colour = Color.Gray;
            }
            var buttonRect = button.Rectangle;
            switch (ButtonState)
            {
                case (ButtonGameState.ChoiseMapButton):
                    spriteBatch.Draw(ButtonChoiceLvlTexture, buttonRect, colour);
                    break;
                case (ButtonGameState.MenuButton):
                    spriteBatch.Draw(ButtonTexture, buttonRect, colour);
                    break;
                case (ButtonGameState.TowerButton):
                    if (button._isHovering)
                    {
                        colour = Color.Gray;
                    }
                    else if (button.IsChosen)
                    {
                        colour = Color.DarkGray;
                    }
                    spriteBatch.Draw(ButtonTowerTexture, buttonRect, colour);
                    break;
            }



            if (!string.IsNullOrEmpty(button.Text))
            {
                var x = (buttonRect.X + (buttonRect.Width / 2)) - (_fontView.SpriteFont.MeasureString(button.Text).X / 2);
                var y = (buttonRect.Y + (buttonRect.Height / 2)) - (_fontView.SpriteFont.MeasureString(button.Text).Y / 2);

                _fontView.DrawString(spriteBatch, button.Text, new Vector2(x, y), PenColour);
            }
        }
    }
}
