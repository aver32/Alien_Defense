using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    internal class Spawner
    {
        public int aliensCount;
        public int curCount;
        public float spawnTime;
        public float spendTime;

        public Spawner(int aliensCount, float spawnTime)
        {
            this.aliensCount = aliensCount;
            this.spawnTime = spawnTime;
            curCount = 0;
        }

        public bool CanSpawnAlien(GameTime gameTime)
        {
            if (spendTime < spawnTime)
            {
                spendTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                return false;
            }
            else
            {
                spendTime = 0f;
                return true;
            }

        }
    }
}
