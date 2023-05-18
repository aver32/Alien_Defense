using Alien_Defense.Model;
using Alien_Defense.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Controller
{
    //Все изменения моделей(обработчик событий)
    class GameController
    {
        public Field field { get; }
        public event EventHandler<MouseState> CellTowerUpdate;
        public event EventHandler<GameTime> CycleUpdate;
        private Spawner spawner;
        public StaticCellView staticCellView { get; private set; }
        public BackgroundView backgroundView { get; private set; }
        public TowerCellView towerCellView { get; private set; }
        public AlienView alienView { get; private set; }
        public List<Alien> _aliens { get; private set; }
        private Vector2 alienInitPos;
        private const int cellSpriteSize = 64;

        public GameController()
        {
            Field.cellSpriteSize = cellSpriteSize;
            field = Field.FromText("map1");
        }
        public void OnMousePressed(MouseState mouseState)
        {
            CellTowerUpdate?.Invoke(this, mouseState);
        }
        public void OnCycleUpdate(GameTime gameTime)
        {
            CycleUpdate?.Invoke(this, gameTime);
        }
        public void Initialize()
        {
            _aliens = new List<Alien>();
            spawner = new Spawner(3, 3f);
            alienInitPos = new Vector2(Field.initPos.X, Field.initPos.Y);
            //_aliens.Add(new Alien(alienInitPos, GetAlienPath(), 40, 100f));
        }
        public void LoadContent(ContentManager content)
        {
            staticCellView = new StaticCellView(content);
            backgroundView = new BackgroundView(content);
            towerCellView = new TowerCellView(content);
            alienView = new AlienView(content);
        }

        public void Update(GameTime gameTime)
        {
            if (spawner.curCount < spawner.aliensCount && spawner.CanSpawnAlien(gameTime))
            {
                _aliens.Add(new Alien(alienInitPos, GetAlienPath(), 40, 100f));
                spawner.curCount++;
            }
            foreach (var alien in _aliens)
                AlienUpdate(alien, gameTime);
        }

        private void AlienUpdate(Alien alien, GameTime gameTime)
        {
            if (alien.currentPathIndex < alien.path.Count)
            {
                var targetPosition = new Vector2(alien.path[alien.currentPathIndex].X,
                    alien.path[alien.currentPathIndex].Y);
                var direction = targetPosition - alien.currentPosition;
                var distance = direction.Length();

                if (distance > 0)
                {
                    var step = alien.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (step < distance)
                    {
                        direction.Normalize();
                        alien.currentPosition += direction * step;
                    }
                    else
                    {
                        alien.currentPosition = targetPosition;
                        alien.currentPathIndex++;
                    }
                }

            }
        }

        private void FieldTowerAdd(MouseState mouseState)
        {
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            foreach (var cell in field.TowerCells)
                if (cell.Position.Contains(mousePosition) && cell.State == CellState.TowerCellFree)
                {
                    var tower = new Tower(1, 30);
                    cell.State = CellState.TowerCellOcup;
                    cell.Tower = tower;
                    field.Towers.Add(tower);
                }
        }

        private void FieldTowerDelete(MouseState mouseState)
        {
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            foreach (var cell in field.TowerCells)
                if (cell.Position.Contains(mousePosition) &&
                    cell.State == CellState.TowerCellOcup)
                {
                    cell.State = CellState.TowerCellFree;
                    field.Towers.Remove(cell.Tower);
                    cell.Tower = null;
                }
        }

        public void FieldTowerAddOrDelete(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
                FieldTowerAdd(mouseState);
            else if (mouseState.RightButton == ButtonState.Pressed)
                FieldTowerDelete(mouseState);
        }

        public List<Rectangle> GetAlienPath()
        {
            var path = new List<Rectangle>(); // Путь будет представлен списком точек (Vector2)
            var curPos = Field.initPos;
            var lastPos = Field.RoutesCells.Last().Position;
            while (!(curPos == lastPos))
            {
                foreach (var cell in Field.RoutesCells)
                {
                    curPos = GetCurPositionAndAddToPath(path, curPos, cell);
                }
            }
            return path;
        }

        private Rectangle GetCurPositionAndAddToPath(List<Rectangle> path, Rectangle curPos, ICell cell)
        {
            var rightCell = new Rectangle(curPos.X + cellSpriteSize,
                curPos.Y,
                cellSpriteSize,
                cellSpriteSize);
            var leftCell = new Rectangle(curPos.X - cellSpriteSize,
                curPos.Y,
                cellSpriteSize,
                cellSpriteSize);
            var bottomCell = new Rectangle(curPos.X,
                curPos.Y + cellSpriteSize,
                cellSpriteSize,
                cellSpriteSize);
            var upCell = new Rectangle(curPos.X,
                curPos.Y - cellSpriteSize,
                cellSpriteSize,
                cellSpriteSize);
            if (cell.Position.Contains(upCell))
            {
                curPos = upCell;
                path.Add(upCell);
            }
            else if (cell.Position.Contains(bottomCell))
            {
                curPos = bottomCell;
                path.Add(bottomCell);
            }
            else if (cell.Position.Contains(leftCell))
            {
                curPos = leftCell;
                path.Add(leftCell);
            }
            else if (cell.Position.Contains(rightCell))
            {
                curPos = rightCell;
                path.Add(rightCell);
            }

            return curPos;
        }
    }
}
