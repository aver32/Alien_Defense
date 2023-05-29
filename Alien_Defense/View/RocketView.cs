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
    /// Класс отображения ракеты
    /// </summary>
    public class RocketView : IEnemyView
    {
        private Texture2D _rocket;
        private string _assetRocketName = "rocket";
        public Texture2D HealthbarTexture { get; private set; }
        private string _assetHealthbarName = "healthbar";

        /// <summary>
        /// Метод загрузки контента
        /// </summary>
        /// <param name="content"> контент менеджер</param>
        public void LoadContent(ContentManager content)
        {
            _rocket = content.Load<Texture2D>(_assetRocketName);
            HealthbarTexture = content.Load<Texture2D>(_assetHealthbarName);
        }
        /// <summary>
        /// Метод отрисовки
        /// </summary>
        /// <param name="spriteBatch"> Кисть </param>
        /// <param name="rocketPosBase"> ракета </param>
        public void Draw(SpriteBatch spriteBatch, PositionBase rocketPosBase)
        {
            spriteBatch.Draw(_rocket, rocketPosBase.PositionBas, Color.White);
            var rock = rocketPosBase as Rocket;
            if (rock == null) return;
            
            var rockHealth = rock.Health;
            var healtbarWidth = (rockHealth / rock.MaxHealth) * HealthbarTexture.Width;
            var healthbarRect = new Rectangle((int)rocketPosBase.PositionBas.X,
                (int)rocketPosBase.PositionBas.Y - _rocket.Height / 4,
                (int)healtbarWidth,
                HealthbarTexture.Height);
            if (healtbarWidth <= HealthbarTexture.Width / 2)
                spriteBatch.Draw(HealthbarTexture, healthbarRect, Color.Red);
            else
                spriteBatch.Draw(HealthbarTexture, healthbarRect, Color.LightGreen);
        }
    }
}
