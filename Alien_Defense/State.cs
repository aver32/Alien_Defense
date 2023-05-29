using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alien_Defense.Model;

namespace Alien_Defense
{
    /// <summary>
    /// Абстрактный класс для реализацию состояний игры
    /// </summary>
    public abstract class State
    {
        protected ContentManager _content;

        protected GraphicsDevice _graphicsDevice;

        protected GameCycle _game;
        public abstract StateGame StateName { get; set; }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        public State(GameCycle game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _game = game;

            _graphicsDevice = graphicsDevice;

            _content = content;
        }

        public abstract void Update(GameTime gameTime);
    }
}
