

using Microsoft.Xna.Framework;

namespace Alien_Defense.Model
{
    public class Cell
    {
        public Rectangle Position { get; set; }
        
        public CellState State { get; set; }
        public Cell(Rectangle pos, CellState state) 
        {
            Position = pos;
            State = state;
        }

    }
}
