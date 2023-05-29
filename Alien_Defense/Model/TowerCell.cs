using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    /// <summary>
    /// Класс для клетки которая может хранить башню
    /// </summary>
    public class TowerCell : Cell
    {
        public ITower Tower;
        public bool MouseOnCell = false;
        public readonly CellCoordinatesArray CellCoordinatesArray;
        public TowerCell(Rectangle pos, CellState state, CellCoordinatesArray cellCoordinatesArray, ITower tower = null)
            : base(pos, state)
        {
            CellCoordinatesArray = cellCoordinatesArray;
            Tower = tower;
        }
    }
}
