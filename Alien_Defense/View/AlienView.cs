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
    internal class AlienView
    {
        private Texture2D _alien;
        private string _assetAlienName = "alien";

        public AlienView(ContentManager content)
        {
            LoadContent(content);
        }

        private void LoadContent(ContentManager content)
        {
            _alien = content.Load<Texture2D>(_assetAlienName);
        }

        public void Draw(SpriteBatch spriteBatch, Alien alien)
        {
            spriteBatch.Draw(_alien, alien.currentPosition, Color.White);
        }
    }
}
