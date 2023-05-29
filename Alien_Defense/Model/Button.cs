using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    /// <summary>
    /// Класс для кнопки
    /// </summary>
    public class Button
    {

        private MouseState _currentMouse;
        public bool IsChosen { get; set; }

        public bool _isHovering { get; private set; }

        private MouseState _previousMouse;

        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Vector2 Position { get; set; }
        private int _textureWidth;
        private int _textureHeight;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _textureWidth, _textureHeight);
            }
        }

        public string Text { get; set; }

        public Button(Vector2 position, string text, int textureWidth, int textureHeight)
        {
            Position = position;
            Text = text;
            _textureWidth = textureWidth;
            _textureHeight = textureHeight;
        }

        /// <summary>
        /// обновление кнопки
        /// </summary>
        /// <param name="gameTime"> Игровое время </param>
        public void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
