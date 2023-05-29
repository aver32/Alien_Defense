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
    /// Класс отображения шрифта(текста)
    /// </summary>
    public class FontView
    {
        public SpriteFont SpriteFont { get; private set; }
        private string _assetFontName = "myfont";
        /// <summary>
        /// Метод загрузки контента
        /// </summary>
        /// <param name="content"> контент менеджер</param>
        public void LoadContent(ContentManager content)
        {
            SpriteFont = content.Load<SpriteFont>(_assetFontName);
        }
        /// <summary>
        /// Метод отрисовки шрифта
        /// </summary>
        /// <param name="spriteBatch"> кисть </param>
        /// <param name="text"> текст </param>
        /// <param name="pos"> позиция </param>
        /// <param name="color"> цвет </param>
        public void DrawString(SpriteBatch spriteBatch, string text, Vector2 pos, Color color)
        { 
            spriteBatch.DrawString(SpriteFont, text, pos, color);
        }
    }
}
