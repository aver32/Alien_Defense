using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    /// <summary>
    /// Огненная башня
    /// </summary>
    public class FireTower : PositionBase, ITower
    {
        public double Damage { get; } = 20;
        public int Cost { get; } = 30;
        public float DelaySpawnBulletTime { get; } = 2f;
        public float BulletSpeed { get; } = 300f;
        public float SpendTimeBullet { get; set; }
        public override Vector2 PositionBas { get => Cell.PositionBas; internal set { } }
        public TowerCell Cell { get; }


        public FireTower(TowerCell cell)
        {
            Cell = cell;
        }
    }
}
