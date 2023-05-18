﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    internal class TowerCell : Cell
    {
        public Tower Tower;
        public TowerCell(Rectangle pos, CellState state, Tower tower = null) 
            : base(pos, state)
        {
            Tower = tower;
        }
    }
}
