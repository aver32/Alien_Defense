using Alien_Defense.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.View
{
    internal class BackgroundView
    {
        private Texture2D _background;
        private string _assetName = "back";

        public BackgroundView(ContentManager content)
        {
            LoadContent(content);
        }

        private void LoadContent(ContentManager content)
        {
            _background = content.Load<Texture2D>(_assetName);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Draw(_background, graphicsDevice.Viewport.Bounds, Color.White);
        }
    }
}
