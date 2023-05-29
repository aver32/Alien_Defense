using Alien_Defense.Model;
using Alien_Defense.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alien_Defense.Controller
{
    //Все изменения моделей(обработчик событий)
    public class GameController
    {
        private int _alienCount;
        private GraphicsDevice _graphicsDevice;
        private float _delaySpawnAlienTime;
        public int TowerCost { get; private set; }
        public Field field { get; }
        public TowerType ClickedTowerType;
        public event EventHandler<MouseState> CellTowerUpdate;
        public event EventHandler<Point> MouseAtTowerCell;
        public event EventHandler<GameTime> CycleUpdate;
        public event EventHandler<bool> GameFinish;
        private Spawner _spawnerAliens;
        private Spawner _spawnerBullets;
        private Spawner _spawnerAlienAttack;
        public BackgroundView BackgroundView { get; private set; }
        public StaticCellView CellView { get; private set; }
        public FontView FontView { get; private set; }
        public ButtonView ButtonView { get; private set; }
        public Dictionary<Type, IEnemyView> EnemyViews { get; private set; }
        public int AliensCountInLastCellInPath { get; private set; }
        public List<GreenAlien> Aliens { get; private set; }
        public List<Bullet> Bullets = new List<Bullet>();
        public List<Button> TowerButtons;
        private Vector2 _alienInitPos;
        private List<Rectangle> _alienPath;
        public Player Player { get; private set; }

        /// <summary>
        /// Конструктор контроллера
        /// </summary>
        /// <param name="lvl"> Уровень </param>
        /// <param name="graphicsDevice"> Графический девайс</param>
        public GameController(Level lvl, GraphicsDevice graphicsDevice)
        {
            field = new Field(lvl.Name);
            _graphicsDevice = graphicsDevice;
            _alienCount = lvl.AlienCount;
            _delaySpawnAlienTime = lvl.DelaySpawnAlienTime;
            BackgroundView = new BackgroundView();
            EnemyViews = new Dictionary<Type, IEnemyView>()
            {
                [typeof(GreenAlien)] = new AlienView(),
                [typeof(Bullet)] = new BulletView(),
                [typeof(ITower)] = new TowerView(),
                [typeof(Rocket)] = new RocketView()
            };
            Player = new Player(110);
            CellView = new StaticCellView();
            FontView = new FontView();
            ButtonView = new ButtonView();

        }
        /// <summary>
        /// Обработчик события нажатия мыши
        /// </summary>
        /// <param name="mouseState"></param>
        public void OnMousePressed(MouseState mouseState)
        {
            CellTowerUpdate?.Invoke(this, mouseState);
        }
        /// <summary>
        /// Обработчик события обновления цикла
        /// </summary>
        /// <param name="mouseState"></param>
        public void OnCycleUpdate(GameTime gameTime)
        {
            CycleUpdate?.Invoke(this, gameTime);
        }
        /// <summary>
        /// Обработчик события когда мышка на клетке
        /// </summary>
        /// <param name="mouseState"></param>
        public void OnMouseOnCell(Point mousePos)
        {
            MouseAtTowerCell?.Invoke(this, mousePos);
        }
        /// <summary>
        /// Метод инициализации элементов
        /// </summary>
        public void Initialize()
        {
            Aliens = new List<GreenAlien>();
            _spawnerAliens = new Spawner(_alienCount, _delaySpawnAlienTime);
            _spawnerBullets = new Spawner();
            _spawnerAlienAttack = new Spawner();
            var initCellToAlien = field.AllCells[field.initAlienPos.X, field.initAlienPos.Y];
            _alienInitPos = new Vector2(initCellToAlien.Rectangle.X, initCellToAlien.Rectangle.Y);
            _alienPath = GetAlienPath();
        }
        /// <summary>
        /// Метод загрузки контента
        /// </summary>
        /// <param name="content"> Контент менеджер </param>
        public void LoadContent(ContentManager content)
        {
            foreach (var view in EnemyViews.Values)
                view.LoadContent(content);
            CellView.LoadContent(content);
            BackgroundView.LoadContent(content);
            FontView.LoadContent(content);
            ButtonView.LoadContent(content, ButtonGameState.TowerButton);
            var rigthAngleScreen = new Vector2(_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height);
            TowerButtons = new List<Button>()
            {
                new Button(rigthAngleScreen -= new Vector2(100,100),
                "Fire",
                ButtonView.ButtonTowerTexture.Width,
                ButtonView.ButtonTowerTexture.Height),
                new Button(rigthAngleScreen -= new Vector2(0,100),
                "Inferno",
                ButtonView.ButtonTowerTexture.Width,
                ButtonView.ButtonTowerTexture.Height)
            };
            foreach (var button in TowerButtons)
                button.Click += (sender, e) =>
                {
                    ClickedTowerType = (TowerType)Enum.Parse(typeof(TowerType), button.Text);
                    button.IsChosen = true;
                    switch (ClickedTowerType)
                    {
                        case TowerType.Inferno:
                            TowerCost = 70;
                            break;
                        case TowerType.Fire:
                            TowerCost = 30;
                            break;
                    }
                    foreach (var previousTowerButton in TowerButtons)
                    {
                        previousTowerButton.IsChosen = previousTowerButton == button;
                    }
                };
        }
        /// <summary>
        /// Обновление моделей
        /// </summary>
        /// <param name="gameTime"> Игровое время </param>
        public void Update(GameTime gameTime)
        {
            CheckWinOrLoss();
            AlienSpawn(gameTime);
            if (Aliens.Any())
            {
                foreach (var tower in field.Towers)
                {
                    var bullet = _spawnerBullets.BulletsSpawn(gameTime, tower);
                    if (bullet != null)
                        Bullets.Add(bullet);
                }
            }

            foreach (var bullet in Bullets.ToList())
            {
                BulletUpdate(bullet, gameTime);
            }
            foreach (var button in TowerButtons)
                button.Update(gameTime);

            AliensCountInLastCellInPath = 0;
            foreach (var alien in Aliens.ToList())
            {
                AlienDead(alien);
                AlienUpdate(alien, gameTime);
                if (alien.currentPathIndex == alien.Path.Count)
                {
                    if (_spawnerAlienAttack.CanAlienAttack(gameTime, alien))
                        field.Rocket.Health -= alien.Damage;
                    AliensCountInLastCellInPath++;
                }
            }

        }
        /// <summary>
        /// Метод проверки проигрыша или выигрыша игры
        /// </summary>
        private void CheckWinOrLoss()
        {
            if (_spawnerAliens.aliensCount == _spawnerAliens.alienCurCount
                            && Aliens.Count == 0)
                GameFinish?.Invoke(this, true);
            if (field.Rocket.Health <= 0)
                GameFinish?.Invoke(this, false);
        }
        /// <summary>
        /// Метод изменения поля, которое показывает находится ли мышка на клетке
        /// </summary>
        /// <param name="mousePosition"> Позиция мышки </param>
        public void SetMouseOnTowerCell(Point mousePosition)
        {
            foreach (var towerCell in field.TowerCells)
            {
                if (towerCell.Rectangle.Contains(mousePosition))
                {
                    ((TowerCell)(field.AllCells[towerCell.CellCoordinatesArray.X,
                        towerCell.CellCoordinatesArray.Y])).MouseOnCell = true;
                }
            }
        }
        /// <summary>
        /// Спавн пришельца
        /// </summary>
        /// <param name="gameTime"> Игровое время </param>
        private void AlienSpawn(GameTime gameTime)
        {
            if (!(_spawnerAliens.alienCurCount < _spawnerAliens.aliensCount
                && _spawnerAliens.CanSpawnAlien(gameTime)))
                return;

            var alien = new GreenAlien(_alienInitPos, _alienPath);
            Aliens.Add(alien);
            _spawnerAliens.alienCurCount++;
        }
        /// <summary>
        /// Обновление пули
        /// </summary>
        /// <param name="bullet"> Пуля </param>
        /// <param name="gameTime"> Игровое время </param>
        private void BulletUpdate(Bullet bullet, GameTime gameTime)
        {
            if (Aliens.Count > 0)
            {
                var targetPosition = Aliens[0].Position;
                var alien = Aliens[0];
                var recAlien = new Rectangle((int)alien.Position.X,
                    (int)alien.Position.Y,
                    ((AlienView)EnemyViews[typeof(GreenAlien)]).AlienTexture.Width / 2,
                    ((AlienView)EnemyViews[typeof(GreenAlien)]).AlienTexture.Height / 2);
                var recBullet = new Rectangle((int)bullet.Position.X,
                    (int)bullet.Position.Y,
                    ((BulletView)EnemyViews[typeof(Bullet)])._bulletTexture.Width / 2,
                    ((BulletView)EnemyViews[typeof(Bullet)])._bulletTexture.Width / 2);
                var direction = targetPosition - bullet.Position;
                AlienAndBulletCollision(bullet, gameTime, alien, recAlien, recBullet, direction);
            }
            else Bullets.Clear();
        }
        /// <summary>
        /// проверка коллизии пришельца и пули 
        /// </summary>
        /// <param name="bullet"> Пуля </param>
        /// <param name="gameTime"> Игровое время </param>
        /// <param name="alien"> Пришелец </param>
        /// <param name="recAlien"> Прямоугольник пришельца </param>
        /// <param name="recBullet"> Прямоугольник пули </param>
        /// <param name="direction"> Направление </param>
        private void AlienAndBulletCollision(Bullet bullet,
            GameTime gameTime,
            GreenAlien alien,
            Rectangle recAlien,
            Rectangle recBullet,
            Vector2 direction)
        {
            if (!recAlien.Intersects(recBullet))
            {
                var step = bullet.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                direction.Normalize();
                bullet.Position += direction * step;
            }
            else
            {
                alien.Health -= bullet.Damage;
                Bullets.Remove(bullet);
            }
        }
        /// <summary>
        /// Убийство пришельца
        /// </summary>
        /// <param name="alien"> Пришелец </param>
        private void AlienDead(GreenAlien alien)
        {
            if (alien.Health <= 0)
                Aliens.RemoveAt(0);
        }
        /// <summary>
        /// обнровление состояния пришельца
        /// </summary>
        /// <param name="alien"> пришелец </param>
        /// <param name="gameTime"> игровое время </param>
        private void AlienUpdate(GreenAlien alien, GameTime gameTime)
        {
            if (alien.currentPathIndex < alien.Path.Count)
            {
                var targetPosition = new Vector2(alien.Path[alien.currentPathIndex].X,
                    alien.Path[alien.currentPathIndex].Y);
                var direction = targetPosition - alien.Position;
                var distance = direction.Length();

                if (distance > 0)
                {
                    var step = alien.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (step < distance)
                    {
                        direction.Normalize();
                        alien.Position += direction * step;
                    }
                    else
                    {
                        alien.Position = targetPosition;
                        alien.currentPathIndex++;
                    }
                }

            }
        }
        /// <summary>
        /// Метод добавления башни
        /// </summary>
        /// <param name="mouseState"> состояние мыши </param>
        private void FieldTowerAdd(MouseState mouseState)
        {
            var mousePosition = new Vector2(mouseState.X, mouseState.Y);
            foreach (var cell in field.TowerCells)
                if (cell.Rectangle.Contains(mousePosition)
                    && cell.State == CellState.TowerCellFree)
                {
                    var tower = CreateTower(cell);
                    if (tower == null) return;
                    if (Player.Money - tower.Cost <= 0) return;
                    cell.State = CellState.TowerCellOcup;
                    cell.Tower = tower;
                    field.Towers.Add(tower);
                    Player.Money -= tower.Cost;
                }
        }
        /// <summary>
        /// Метод вызова конструктора башни
        /// </summary>
        /// <typeparam name="T"> Тип башни </typeparam>
        /// <param name="cell"> клетка </param>
        /// <returns> Башню </returns>
        private ITower InvokeConstructor<T>(TowerCell cell)
        {
            var towerType = typeof(T);

            var constructor = towerType.GetConstructor(new Type[]
                {
                        typeof(TowerCell)
                }
            );
            var tower = constructor.Invoke(new object[]
            {
                         cell
            });

            return (ITower)tower;
        }
        /// <summary>
        /// Создание башни с помощью выбранной кнопки
        /// </summary>
        /// <param name="cell"> Клетка башни </param>
        /// <returns> Башню </returns>
        private ITower CreateTower(TowerCell cell)
        {
            switch (ClickedTowerType)
            {
                case TowerType.Fire:
                    return InvokeConstructor<FireTower>(cell);
                case TowerType.Inferno:
                    return InvokeConstructor<InfernoTower>(cell);
                default:
                    return null;
            }
        }
        /// <summary>
        /// Метод удаления башни
        /// </summary>
        /// <param name="mouseState"> Состояние мыши </param>
        private void FieldTowerDelete(MouseState mouseState)
        {
            var mousePosition = new Vector2(mouseState.X, mouseState.Y);
            foreach (var cell in field.TowerCells)
                if (cell.Rectangle.Contains(mousePosition) &&
                    cell.State == CellState.TowerCellOcup)
                {
                    Player.Money += cell.Tower.Cost;
                    cell.State = CellState.TowerCellFree;
                    field.Towers.Remove(cell.Tower);
                    cell.Tower = null;
                }
        }
        /// <summary>
        /// Удаление или добавление башни 
        /// </summary>
        /// <param name="mouseState"> Состояние </param>
        public void FieldTowerAddOrDelete(MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
                FieldTowerAdd(mouseState);
            else if (mouseState.RightButton == ButtonState.Pressed)
                FieldTowerDelete(mouseState);
        }
        /// <summary>
        /// Получение пути для пришельца
        /// </summary>
        /// <returns> Список клеток пути </returns>
        public List<Rectangle> GetAlienPath()
        {
            var curPos = field.initAlienPos;
            var path = new List<Rectangle>();
            var visitedCells = new List<CellCoordinatesArray>() { curPos };
            for (var i = 0; i < field.RoutesCells.Count; i++)
            {
                var neighboursCell = GetNeighboursCellsCoordinates(curPos);
                foreach (var coord in neighboursCell)
                {
                    var cell = field.AllCells[coord.X, coord.Y];
                    if (cell.State == CellState.Route && !visitedCells.Contains(coord))
                    {
                        path.Add(cell.Rectangle);
                        visitedCells.Add(coord);
                        curPos = coord;
                        break;
                    }
                }
            }

            return path;
        }

        /// <summary>
        /// Метод получения соседних клеток
        /// </summary>
        /// <param name="cellPos"> позиция клетки в массиве координат </param>
        /// <returns> Список соседних клеток </returns>
        private List<CellCoordinatesArray> GetNeighboursCellsCoordinates(CellCoordinatesArray cellPos)
        {
            var x = cellPos.X;
            var y = cellPos.Y;
            var rightCell = new CellCoordinatesArray(x + 1, y);
            var leftCell = new CellCoordinatesArray(x - 1, y);
            var bottomCell = new CellCoordinatesArray(x, y + 1);
            var topCell = new CellCoordinatesArray(x, y - 1);

            return new List<CellCoordinatesArray>()
            {
                rightCell,
                leftCell,
                bottomCell,
                topCell
            };
        }
    }
}
