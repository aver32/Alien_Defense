using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    /// <summary>
    /// Пришелец
    /// </summary>
    public class GreenAlien : PositionBase, IAlien
    {
        public float DelaySpawnBulletTime { get; } = 2f;
        public double Damage { get; } = 35;
        public float Speed { get; } = 120f;
        public double Health { get; set; } = 100f;
        public double MaxHealth { get => 100f; }
        public List<Rectangle> Path { get; private set; }
        public int currentPathIndex;
        public float SpendTimeBullet { get; set; }
        public Vector2 Position { get; set; }
        public override Vector2 PositionBas { get => Position; internal set { } }
        public Rectangle Rectangle { get; private set; }
        

        public GreenAlien(Vector2 initPos, List<Rectangle> alienPath)
        {
            Path = new List<Rectangle>();
            Position = initPos;
            Path = alienPath;
        }
    }
}
