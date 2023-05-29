using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Alien_Defense.Model
{
    /// <summary>
    /// Интерфейс для пришельца
    /// </summary>
    public interface IAlien
    {
        float DelaySpawnBulletTime { get; }
        double Health { get; set; }
        double MaxHealth { get; }
        List<Rectangle> Path { get; }
        Vector2 Position { get; set; }
        Vector2 PositionBas { get; }
        Rectangle Rectangle { get; }
        float SpendTimeBullet { get; set; }
    }
}