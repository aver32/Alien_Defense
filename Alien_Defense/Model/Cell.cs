

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Alien_Defense.Model
{
    public class Cell : ICell
    {
        public Rectangle Position { get; set; }
        public Texture2D Texture { get; set; }
        public CellState State { get; set; }
        public Cell(Rectangle pos, CellState state) 
        {
            Position = pos;
            State = state;
        }
    }
}
