using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    /// <summary>
    /// Класс уровня игры
    /// </summary>
    public class Level
    {
        public readonly int AlienCount;
        public readonly string Name;
        public readonly float DelaySpawnAlienTime;
        public Level(string name, int alienCount, float delaySpawnAlienTime)
        { 
            AlienCount = alienCount;
            DelaySpawnAlienTime = delaySpawnAlienTime;
            Name = name;
        }
    }
}
