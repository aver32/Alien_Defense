using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    internal interface ICell
    {
        public Rectangle Position { get; set; }

        public CellState State { get; set; }
    }
}
