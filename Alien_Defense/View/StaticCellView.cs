using Alien_Defense.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.View
{
    /// <summary>
    /// Класс отображения клеток
    /// </summary>
    public class StaticCellView : ICellView
    {
        private Texture2D _wall;
        private string _assetWallName = "ground";
        private Texture2D _route;
        private string _assetRouteName = "route";
        private Texture2D _towerCell;
        private string _assetTowerCellName = "TowerCell";
        private bool onCell = false;
        public int cellSpriteSize {  get; private set; }

        /// <summary>
        /// Метод загрузки контента
        /// </summary>
        /// <param name="content"> контент менеджер</param>
        public void LoadContent(ContentManager content)
        {
            _wall = content.Load<Texture2D>(_assetWallName);
            _route = content.Load<Texture2D>(_assetRouteName);
            _towerCell = content.Load<Texture2D>(_assetTowerCellName);
            if (_wall != null)
                cellSpriteSize = _wall.Width;
        }
        /// <summary>
        /// Метод отрисовки
        /// </summary>
        /// <param name="spriteBatch"> Кисть рисования </param>
        /// <param name="cell"> Клетка </param>
        public void Draw(SpriteBatch spriteBatch, ICell cell)
        {
            switch (cell.State)
            {
                case (CellState.Wall):
                    spriteBatch.Draw(_wall, cell.Rectangle, Color.White);
                    break;
                case (CellState.Route):
                    spriteBatch.Draw(_route, cell.Rectangle, Color.White);
                    break;
                case (CellState.TowerCellFree):
                    var towerCell = cell as TowerCell;
                    if (towerCell != null)
                    {
                        if (towerCell.MouseOnCell)
                        {
                            spriteBatch.Draw(_towerCell, cell.Rectangle, Color.Gray);
                            towerCell.MouseOnCell = false;
                        }
                        else
                            spriteBatch.Draw(_towerCell, cell.Rectangle, Color.White);
                    }
                    break;
            }
        }
        /// <summary>
        /// Метод установки значения поля, что мышка на клетке
        /// </summary>
        /// <param name="onCell"> Утверждение что мышка на клетке </param>
        public void MouseOnCell(bool onCell)
        {
            this.onCell = onCell;
        }
    }
}
