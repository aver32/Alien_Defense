using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    /// <summary>
    /// интерфейс для пули
    /// </summary>
    internal interface IBullet
    {
        public Vector2 _position { get;set; }
        public int Damage { get; set; }
    }
}
