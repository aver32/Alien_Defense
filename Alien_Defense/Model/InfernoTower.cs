using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    /// <summary>
    /// Башня "инферно"
    /// </summary>
    public class InfernoTower : PositionBase, ITower
    {
        public double Damage { get; } = 0.5;
        public int Cost { get; } = 70;
        public float DelaySpawnBulletTime { get; } = 0f;
        public float BulletSpeed { get; } = 250f;
        public float SpendTimeBullet { get; set; }
        public override Vector2 PositionBas { get => Cell.PositionBas; internal set { } }
        public TowerCell Cell { get; }
        public InfernoTower(TowerCell cell)
        {
            Cell = cell;
        }
    }
}
