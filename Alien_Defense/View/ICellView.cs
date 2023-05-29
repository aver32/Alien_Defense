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
    /// Интерфейс для отображения клетки
    /// </summary>
    public interface ICellView
    {
        void Draw(SpriteBatch spriteBatch, ICell cell);
        void LoadContent(ContentManager content);
    }
}
