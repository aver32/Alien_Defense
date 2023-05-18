using Alien_Defense.Controller;
using Alien_Defense.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using static Alien_Defense.Model.Field;

namespace Alien_Defense;

public class GameCycle : Game
{
    float moveDelay = 1f; // Задержка между перемещениями в секундах
    float elapsedMoveTime = 0f;
    
    bool isT = false;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Alien alien;
    private float _timeElapsed = 0f;
    private float _spawnInterval = 1f;
    public int Money = 100;
    private SpriteFont font;
    private GameController engine;
    private Field field;

    public GameCycle()
    {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferHeight = 720,
            PreferredBackBufferWidth = 1280
        };
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        engine = new GameController();
        field = engine.field;
        engine.CellTowerUpdate += Engine_CellTowerUpdate;
        engine.CycleUpdate += Engine_CycleUpdate;
    }

    private void Engine_CycleUpdate(object sender, GameTime e)
    {
        engine.Update(e);
    }

    private void Engine_CellTowerUpdate(object sender, MouseState mouseState)
    {
        //Обновление башни
        engine.FieldTowerAddOrDelete(mouseState);
    }

    protected override void Initialize()
    {
        engine.Initialize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        engine.LoadContent(Content);
        font = Content.Load<SpriteFont>("myfont");
    }
    protected override void UnloadContent()
    {
        engine.CellTowerUpdate -= Engine_CellTowerUpdate;
        base.UnloadContent();
    }
    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        
        var mouseState = Mouse.GetState();
        engine.OnMousePressed(mouseState);
        engine.OnCycleUpdate(gameTime);
        base.Update(gameTime);

    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();

        engine.backgroundView.Draw(_spriteBatch, GraphicsDevice);
        foreach (var cell in field.StaticCells)
        {
            engine.staticCellView.Draw(_spriteBatch, cell);
        }


        foreach (var cell in field.TowerCells)
        {
            engine.towerCellView.Draw(_spriteBatch, cell);
        }

        foreach (var alien in engine._aliens)
        {
            engine.alienView.Draw(_spriteBatch, alien);
        }

        _spriteBatch.DrawString(font, $"Money: {Money.ToString()}", new Vector2(1000, 1), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
        _spriteBatch.DrawString(font, $"Tower Cost: 30", new Vector2(1000, 30), Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
