using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    /// <summary>
    /// класс ракеты
    /// </summary>
    public class Rocket : PositionBase
    {
        public double Health { get; set; }
        public double MaxHealth { get; private set; }
        public Vector2 Position { get; set; }
        public override Vector2 PositionBas { get => Position; internal set { } }

        public Rocket(double health, Vector2 pos)
        {
            Health = health;
            MaxHealth = health;
            Position = pos;
        }
    }
}
