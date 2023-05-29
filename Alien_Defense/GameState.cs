using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alien_Defense.Controller;
using Alien_Defense.Model;

namespace Alien_Defense
{
    /// <summary>
    /// Состояния игры
    /// </summary>
    public class GameState : State
    {
        public GameController Engine { get; private set; }
        public override StateGame StateName { get => StateGame.GameState; set { } }
        public GameState(GameCycle game, GraphicsDevice graphicsDevice, ContentManager content, string lvlName)
          : base(game, graphicsDevice, content)
        {
            var lvl = game.Levels.Where(x => x.Name.Contains(lvlName)).First();
            Engine = new GameController(lvl, graphicsDevice);
            Engine.Initialize();
            Engine.LoadContent(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

    }
}
