using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense
{
    /// <summary>
    /// Вспомогательный класс для хранения координата клетки в массиве
    /// </summary>
    public class CellCoordinatesArray
    {
        public readonly int X;
        public readonly int Y;

        public CellCoordinatesArray(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherCoord = (CellCoordinatesArray)obj;
            return X == otherCoord.X && Y == otherCoord.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }
    }
}
