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
    /// Класс отображения пули
    /// </summary>
    internal class BulletView : IEnemyView
    {
        public Texture2D _bulletTexture { get; private set; }
        private string _assetBulletName = "fireball";

        /// <summary>
        /// Метод загрузки контента
        /// </summary>
        /// <param name="content"> контент менеджер</param>
        public void LoadContent(ContentManager content)
        {
            _bulletTexture = content.Load<Texture2D>(_assetBulletName);
        }
        /// <summary>
        /// Метод отрисовки пули
        /// </summary>
        /// <param name="spriteBatch"> Кисть </param>
        /// <param name="bulletPosBase"> пуля </param>
        public void Draw(SpriteBatch spriteBatch, PositionBase bulletPosBase)
        {
            spriteBatch.Draw(_bulletTexture, bulletPosBase.PositionBas, Color.White);
        }
    }
}
