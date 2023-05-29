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
    /// Класс отображения башни
    /// </summary>
    internal class TowerView : IEnemyView
    {
        private Texture2D _tower;
        private string _assetTowerName = "tower";
        /// <summary>
        /// Метод загрузки контента
        /// </summary>
        /// <param name="content"> контент менеджер</param>
        public void LoadContent(ContentManager content)
        {
            _tower = content.Load<Texture2D>(_assetTowerName);
        }
        /// <summary>
        /// Метод отрисовки
        /// </summary>
        /// <param name="spriteBatch"> Кисть </param>
        /// <param name="towerPosBase"> башня </param>
        public void Draw(SpriteBatch spriteBatch, PositionBase towerPosBase)
        {
            spriteBatch.Draw(_tower, towerPosBase.PositionBas, Color.White);
        }
    }
}
