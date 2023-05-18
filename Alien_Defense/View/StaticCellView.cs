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
    internal class StaticCellView
    {
        private Texture2D _wall;
        private string _assetWallName = "ground";
        private Texture2D _route;
        private string _assetRouteName = "route";
        private Texture2D _rocket;
        private string _assetRocketName = "rocket";
        public readonly int cellSpriteSize;

        public StaticCellView(ContentManager content)
        {
            LoadContent(content);
            cellSpriteSize = _wall.Height;
        }

        private void LoadContent(ContentManager content)
        {
            _wall = content.Load<Texture2D>(_assetWallName);
            _route = content.Load<Texture2D>(_assetRouteName);
            _rocket = content.Load<Texture2D>(_assetRocketName);
        }

        public void Draw(SpriteBatch spriteBatch, ICell cell)
        {
            switch (cell.State)
            {
                case (CellState.Wall):
                    spriteBatch.Draw(_wall, cell.Position, Color.White);
                    break;
                case (CellState.Rocket):
                    spriteBatch.Draw(_rocket, cell.Position, Color.White);
                    break;
                case (CellState.Route):
                    spriteBatch.Draw(_route, cell.Position, Color.White);
                    break;
            }
        }
    }
}
