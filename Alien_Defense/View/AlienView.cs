﻿using Alien_Defense.Model;
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
    /// Класс отображения пришельца
    /// </summary>
    internal class AlienView : IEnemyView
    {
        public Texture2D AlienTexture { get; private set; }
        private string _assetAlienName = "alien";
        public Texture2D HealthbarTexture { get; private set; }
        private string _assetHealthbarName = "healthbar";
        /// <summary>
        /// Метод загрузки контента
        /// </summary>
        /// <param name="content"> контент менеджер</param>
        public void LoadContent(ContentManager content)
        {
            AlienTexture = content.Load<Texture2D>(_assetAlienName);
            HealthbarTexture = content.Load<Texture2D>(_assetHealthbarName);
        }
        /// <summary>
        /// Метод отрисовки пришельца
        /// </summary>
        /// <param name="spriteBatch"> Кисть </param>
        /// <param name="alienPos"> Позиция пришельца </param>
        public void Draw(SpriteBatch spriteBatch, PositionBase alienPos)
        {
            spriteBatch.Draw(AlienTexture, alienPos.PositionBas, Color.White);

            var alien = alienPos as GreenAlien;
            if (alien == null) return;
            var alienHealth = alien.Health;
            var healtbarWidth = (alienHealth / alien.MaxHealth) * HealthbarTexture.Width;
            var healthbarRect = new Rectangle((int)alienPos.PositionBas.X,
                (int)alienPos.PositionBas.Y - AlienTexture.Height / 2,
                (int)healtbarWidth,
                HealthbarTexture.Height);
            if (healtbarWidth <= HealthbarTexture.Width / 2)
                spriteBatch.Draw(HealthbarTexture, healthbarRect, Color.Red);
            else
                spriteBatch.Draw(HealthbarTexture, healthbarRect, Color.LightGreen);
        }
    }
}
