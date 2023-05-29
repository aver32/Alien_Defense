using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model
{
    /// <summary>
    /// Enum состояния клетки
    /// </summary>
    public enum CellState
    {
        Wall,
        TowerCellFree,
        TowerCellOcup,
        Rocket,
        Route
    }
}
