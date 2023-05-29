using Alien_Defense.View;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    /// <summary>
    /// Класс пули башни
    /// </summary>
    public class Bullet : PositionBase
    {
        public Vector2 Position { get; set; }
        public override Vector2 PositionBas { get => Position; internal set { } }
        public readonly double Damage;
        public readonly float Speed;
        public Bullet(ITower tower)
        {
            Damage = tower.Damage;
            Position = tower.PositionBas;
            Speed = tower.BulletSpeed;
        }
    }
}
