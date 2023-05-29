using Alien_Defense.Model;
using Alien_Defense.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense
{
    /// <summary>
    /// Меню выигрыша
    /// </summary>
    public class WinGameState : State
    {
        private List<Button> _buttons;
        private ButtonView _buttonView;
        private BackgroundView _backgroundView;
        private FontView _fontView;
        public override StateGame StateName { get => StateGame.Win; set { } }
        public WinGameState(GameCycle game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _buttonView = new ButtonView();
            _backgroundView = new BackgroundView();
            _fontView = new FontView();
            _buttonView.LoadContent(content, ButtonGameState.MenuButton);
            _backgroundView.LoadContent(content);
            _fontView.LoadContent(content);
            var buttonTexture = _buttonView.ButtonTexture;

            var choiceMapButton = new Button(new Vector2((graphicsDevice.Viewport.Width - buttonTexture.Width) / 2,
                (graphicsDevice.Viewport.Height - buttonTexture.Height) / 2 - 10),
                "Choice map",
                buttonTexture.Width,
                buttonTexture.Height);

            choiceMapButton.Click += NewGameButton_Click;

            var quitGameButton = new Button(new Vector2((graphicsDevice.Viewport.Width - buttonTexture.Width) / 2,
                (graphicsDevice.Viewport.Height - buttonTexture.Height) / 2 + buttonTexture.Height + 10),
                "Quit Game",
                buttonTexture.Width,
                buttonTexture.Height);

            quitGameButton.Click += QuitGameButton_Click;

            _buttons = new List<Button>()
            {
                choiceMapButton,
                quitGameButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _backgroundView.Draw(spriteBatch, _graphicsDevice);
            foreach (var button in _buttons)
                _buttonView.Draw(spriteBatch, button);
            _fontView.DrawString(spriteBatch,
                "You win!!!",
                new Vector2(_buttons.First().Position.X, _buttons.First().Position.Y - 100),
                Color.White);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var button in _buttons)
                button.Update(gameTime);
        }
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new ChoiceMapState(_game, _graphicsDevice, _content));
        }
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
