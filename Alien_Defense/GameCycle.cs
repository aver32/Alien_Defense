using Alien_Defense.Controller;
using Alien_Defense.Extensions;
using Alien_Defense.Model;
using Alien_Defense.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using static Alien_Defense.Model.Field;

namespace Alien_Defense;

public class GameCycle : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public int Money = 100;
    private GameController _engine;
    public List<Level> Levels {  get; private set; }
    private State _currentState;
    private State _nextState;
    private bool _firstChange = true;

    
    /// <summary>
    /// Конструктор основного игрового цикла
    /// </summary>
    public GameCycle()
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferHeight = 1080,
            PreferredBackBufferWidth = 1920,
            IsFullScreen = false,
        };
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Levels = new List<Level>()
        {
            new Level("map1", 15, 1.5f),
            new Level("map2", 11, 1f)
        };
        
    }
    /// <summary>
    /// Смена состояния
    /// </summary>
    /// <param name="state"> Состояние </param>
    public void ChangeState(State state)
    {
        _nextState = state;
    }
    /// <summary>
    /// Обработчик события когда мышка на клетке
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Engine_MouseOnCell(object sender, Point e)
    {
        _engine.SetMouseOnTowerCell(e);
    }
    /// <summary>
    /// Обработчик события когда мышка нажимает на клетку и обновляет башню
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="mouseState"></param>
    private void Engine_CellTowerUpdate(object sender, MouseState mouseState)
    {
        _engine.FieldTowerAddOrDelete(mouseState);
    }
    /// <summary>
    /// Обработчик события когда цикл обновился
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="gameTime"></param>
    private void Engine_CycleUpdate(object sender, GameTime gameTime)
    {
        _engine.Update(gameTime);
    }
    /// <summary>
    /// Метод инициализации
    /// </summary>
    protected override void Initialize()
    {
        base.Initialize();
    }
    /// <summary>
    /// загрузка контента
    /// </summary>
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);
    }
    /// <summary>
    /// Выгрузка контента
    /// </summary>
    protected override void UnloadContent()
    {
        base.UnloadContent();
    }
    /// <summary>
    /// Обновление состояний моделей
    /// </summary>
    /// <param name="gameTime"></param>
    protected override void Update(GameTime gameTime)
    {
        UpdateGameState();
        if (_currentState.StateName == StateGame.GameState && _firstChange)
        {
            InitializeEngineState();
        }
        else
            _currentState.Update(gameTime);
        if (_engine != null && _currentState.StateName == StateGame.GameState)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var mouseState = Mouse.GetState();
            var mousePosition = mouseState.Position;
            _engine.OnMouseOnCell(mousePosition);

            if (mouseState.LeftButton == ButtonState.Pressed || mouseState.RightButton == ButtonState.Pressed)
                _engine.OnMousePressed(mouseState);
            _engine.OnCycleUpdate(gameTime);
        }
        base.Update(gameTime);
    }
    /// <summary>
    /// Метод инициализации контроллера
    /// </summary>
    private void InitializeEngineState()
    {
        _firstChange = false;
        var state = (GameState)_currentState;
        _engine = state.Engine;
        _engine.CellTowerUpdate += Engine_CellTowerUpdate;
        _engine.CycleUpdate += Engine_CycleUpdate;
        _engine.MouseAtTowerCell += Engine_MouseOnCell;
        _engine.GameFinish += Engine_GameFinish;
    }
    /// <summary>
    /// обновление состояния игры
    /// </summary>
    private void UpdateGameState()
    {
        if (_nextState != null)
        {
            _currentState = _nextState;

            _nextState = null;
        }
    }
    /// <summary>
    /// Обработчик события когда игра закончится
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="isWin"></param>
    private void Engine_GameFinish(object sender, bool isWin)
    {
        _engine = null;
        _firstChange= true;
        if (isWin)
            ChangeState(new WinGameState(this, _graphics.GraphicsDevice, Content));
        else
            ChangeState(new LossGameState(this, _graphics.GraphicsDevice, Content));
    }
    /// <summary>
    /// рисование моделей
    /// </summary>
    /// <param name="gameTime"> Игровое время </param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();
        if (_engine != null && _currentState.StateName == StateGame.GameState)
        {
            _engine.BackgroundView.Draw(_spriteBatch, GraphicsDevice);
            DrawCell(_engine.CellView, _engine.field.AllCells.ToEnumerable<ICell>());
            DrawEnemys(_engine.EnemyViews[typeof(ITower)], _engine.field.Towers.ToEnumerable<PositionBase>());
            DrawEnemys(_engine.EnemyViews[typeof(Bullet)], _engine.Bullets);
            DrawEnemys(_engine.EnemyViews[typeof(GreenAlien)], _engine.Aliens);
            _engine.EnemyViews[typeof(Rocket)].Draw(_spriteBatch, _engine.field.Rocket);
            _engine.FontView.DrawString(_spriteBatch,
                $"Money: {_engine.Player.Money}",
                new Vector2(_graphics.PreferredBackBufferWidth - 500, 1),
                Color.White);
            _engine.FontView.DrawString(_spriteBatch,
                $"Tower Cost: {_engine.TowerCost}",
                new Vector2(_graphics.PreferredBackBufferWidth - 500, 30),
                Color.White);
            _engine.FontView.DrawString(_spriteBatch,
                $"Aliens Count in last cell : {_engine.AliensCountInLastCellInPath}",
                new Vector2(_graphics.PreferredBackBufferWidth - 500, 60),
                Color.White);
            foreach (var button in _engine.TowerButtons)
                _engine.ButtonView.Draw(_spriteBatch, button);
        }
        _currentState.Draw(gameTime, _spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
    /// <summary>
    /// Метод рисования сущностей
    /// </summary>
    /// <param name="view"> Вью модели </param>
    /// <param name="positions"> Позиция </param>
    public void DrawEnemys(IEnemyView view, IEnumerable<PositionBase> positions)
    {
        foreach (var item in positions)
        {
            view.Draw(_spriteBatch, item);
        }
    }
    /// <summary>
    /// Метод рисования клеток
    /// </summary>
    /// <param name="view"> Вью модели </param>
    /// <param name="cells"> Клетка </param>
    public void DrawCell(ICellView view, IEnumerable<ICell> cells)
    {
        foreach (var item in cells)
        {
            view.Draw(_spriteBatch, item);
        }
    }
}
