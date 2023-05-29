using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    /// <summary>
    /// Класс для работы со спавном сущностей
    /// </summary>
    internal class Spawner
    {
        public int aliensCount;
        public float spawnTimeBullet;
        public float spendTimeBullet;
        public int alienCurCount;
        public float spawnTimeAlien;
        public float spendTimeAlien;

        /// <summary>
        /// Спавнер для пришельцев
        /// </summary>
        /// <param name="aliensCount"> Количество пришельцев</param>
        /// <param name="spawnTimeAlien"> Время спавна между пришельцами </param>
        public Spawner(int aliensCount, float spawnTimeAlien)
        {
            this.aliensCount = aliensCount;
            this.spawnTimeAlien = spawnTimeAlien;
            alienCurCount = 0;
        }

        public Spawner(float spawnTimeBullet)
        {
            this.spawnTimeBullet = spawnTimeBullet;
        }
        public Spawner()
        {
        }
        /// <summary>
        /// Метод проверки можно ли заспавнить пришельца
        /// </summary>
        /// <param name="gameTime"> Игровое время </param>
        /// <returns></returns>
        public bool CanSpawnAlien(GameTime gameTime)
        {
            if (spendTimeAlien < spawnTimeAlien)
            {
                spendTimeAlien += (float)gameTime.ElapsedGameTime.TotalSeconds;
                return false;
            }
            else
            {
                spendTimeAlien = 0f;
                return true;
            }

        }
        /// <summary>
        /// Метод спавна пули
        /// </summary>
        /// <param name="gameTime"> Игровое время </param>
        /// <param name="tower"> Башня </param>
        /// <returns> Пулю </returns>
        public Bullet BulletsSpawn(GameTime gameTime, ITower tower)
        {
            if (tower.SpendTimeBullet < tower.DelaySpawnBulletTime)
            {
                tower.SpendTimeBullet += (float)gameTime.ElapsedGameTime.TotalSeconds;
                return null;
            }
            else
            {
                tower.SpendTimeBullet = 0f;
                var bul = new Bullet(tower);
                return bul;
            }
        }
        /// <summary>
        /// Может ли пришелец ударить
        /// </summary>
        /// <param name="gameTime"> Игровое время </param>
        /// <param name="alien"> Пришелец </param>
        /// <returns> True или false</returns>
        public bool CanAlienAttack(GameTime gameTime, IAlien alien)
        {
            if (alien.SpendTimeBullet < alien.DelaySpawnBulletTime)
            {
                alien.SpendTimeBullet += (float)gameTime.ElapsedGameTime.TotalSeconds;
                return false;
            }
            else
            {
                alien.SpendTimeBullet = 0f;
                return true;
            }
        }
    }
}

