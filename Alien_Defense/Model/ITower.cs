using Microsoft.Xna.Framework;

namespace Alien_Defense.Model
{
    /// <summary>
    /// интерфейс для башни
    /// </summary>
    public interface ITower
    {
        double Damage { get; }
        public int Cost { get; }
        TowerCell Cell { get; }
        Vector2 PositionBas { get; }
        float SpendTimeBullet { get; set; }
        float DelaySpawnBulletTime { get; }
        float BulletSpeed { get; }
    }
}