

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Alien_Defense.Model
{
    /// <summary>
    /// Основной класс клетки поля
    /// </summary>
    public class Cell : PositionBase, ICell
    {
        public Rectangle Rectangle { get; set; }
        public override Vector2 PositionBas { get => new Vector2(Rectangle.X, Rectangle.Y); internal set { } } 
        public Texture2D Texture { get; set; }
        public CellState State { get; set; }
        public Cell(Rectangle pos, CellState state) 
        {
            Rectangle = pos;
            State = state;
        }
    }
}
