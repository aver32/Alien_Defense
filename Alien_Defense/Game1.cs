using Alien_Defense.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Alien_Defense;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _wall;
    private Texture2D _route;
    private Texture2D _tower;
    private Field field;
    private string text = File.ReadAllText("C:\\Users\\flyli\\source\\repos\\Alien_Defense\\Alien_Defense\\map1.txt");

    public Game1()
    {
        //CheckGit
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferHeight = 720,
            PreferredBackBufferWidth = 1280
        };
        Content.RootDirectory = "Content";
        field = Field.FromText(text);
        IsMouseVisible = true;
    }


    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _route = Content.Load<Texture2D>("route");
        _wall = Content.Load<Texture2D>("Chest");
        _tower = Content.Load<Texture2D>("ball");
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        // TODO: Add your update logic here
        var mouseState = Mouse.GetState();
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            foreach (var cell in field.Cells3)
                if (cell.Position.Contains(mousePosition) && cell.State == CellState.TowerCell)
                    field.AddTowerEvent(() =>
                    {
                        var pos = cell.Position;
                        var tower = new Tower(1, 1);
                        cell.State = CellState.CellWithTower;
                        return new TowerCell(true, pos, tower);
                    }
                    );
        }

        // Отображаем координаты мыши на экране
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        var cellWidth = _route.Width;
        var cellHeight = _route.Height;
        var width = field.Cells.GetLength(0);
        var height = field.Cells.GetLength(1);
        _spriteBatch.Begin();
        for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                Vector2 position = new Vector2(x * cellHeight, y * cellWidth);

                if (field.Cells[x, y] == FieldCell.Route)
                {
                    _spriteBatch.Draw(_route, position, Color.White);
                }
                else if (field.Cells[x,y] == FieldCell.Back)
                    _spriteBatch.Draw(_wall, position, Color.White);
                else if (field.Cells3[x,y].State == CellState.CellWithTower)
                    _spriteBatch.Draw(_tower, position, Color.White);

            }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
