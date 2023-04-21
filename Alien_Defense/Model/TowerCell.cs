using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    internal class TowerCell
    {
        public bool IsOccupied;
        public readonly Rectangle Position;
        public readonly Tower Tower;
        public TowerCell(bool isOccupied, Rectangle pos, Tower tower = null)
        {
            IsOccupied = isOccupied;
            Position = pos;
            Tower = tower;
        }
    }
}
