﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    /// <summary>
    /// интерфейс для клетки
    /// </summary>
    public interface ICell
    {
        public Rectangle Rectangle { get; set; }

        public CellState State { get; set; }
    }
}
