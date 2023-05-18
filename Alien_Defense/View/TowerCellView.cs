using Alien_Defense.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.View
{
    internal class TowerCellView
    {
        private Texture2D _tower;
        private string _assetTowerName = "tower";
        private Texture2D _towerCell;
        private string _assetTowerCellName = "TowerCell";

        public TowerCellView(ContentManager content)
        {
            LoadContent(content);
        }

        private void LoadContent(ContentManager content)
        {
            _tower = content.Load<Texture2D>(_assetTowerName);
            _towerCell = content.Load<Texture2D>(_assetTowerCellName);
        }

        public void Draw(SpriteBatch spriteBatch, ICell cell)
        {
            switch (cell.State)
            {
                case (CellState.TowerCellFree):
                    spriteBatch.Draw(_towerCell, cell.Position, Color.White);
                    break;
                case (CellState.TowerCellOcup):
                    spriteBatch.Draw(_tower, cell.Position, Color.White);
                    break;
            }
        }
    }
}
