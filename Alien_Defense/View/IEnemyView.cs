using Alien_Defense.Model;
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
    /// Интерфейс для отображения сущности
    /// </summary>
    public interface IEnemyView
    {
        void LoadContent(ContentManager content);
        void Draw(SpriteBatch spriteBatch, PositionBase position);
    }
}
