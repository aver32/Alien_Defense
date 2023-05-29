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
    /// <summary>
    /// Класс отображения заднего фона
    /// </summary>
    public class BackgroundView
    {
        public Texture2D BackgroundTexture { get;private set; }
        private string _assetName = "background";
        /// <summary>
        /// Метод загрузки контента
        /// </summary>
        /// <param name="content"> контент менеджер</param>
        public void LoadContent(ContentManager content)
        {
            BackgroundTexture = content.Load<Texture2D>(_assetName);
        }
        /// <summary>
        /// Метод отрисовки заднего фона
        /// </summary>
        /// <param name="spriteBatch"> Кисть </param>
        /// <param name="graphicsDevice"> Графический девайс</param>
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Draw(BackgroundTexture, graphicsDevice.Viewport.Bounds, Color.White);
        }
    }
}
