using Alien_Defense.Model;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alien_Defense.View;
using System.Runtime.CompilerServices;

namespace Alien_Defense
{
    /// <summary>
    /// Главное меню
    /// </summary>
    public class MenuState : State
    {
        private List<Button> _buttons;
        private ButtonView _buttonView;
        private BackgroundView _backgroundView;
        public override StateGame StateName { get => StateGame.MainMenu; set { } }
        public MenuState(GameCycle game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _buttonView = new ButtonView();
            _backgroundView = new BackgroundView();
            _buttonView.LoadContent(content, ButtonGameState.MenuButton);
            _backgroundView.LoadContent(content);
            var buttonTexture = _buttonView.ButtonTexture;

            var newGameButton = new Button(new Vector2((graphicsDevice.Viewport.Width - buttonTexture.Width) / 2,
                (graphicsDevice.Viewport.Height - buttonTexture.Height) / 2 - 10),
                "New Game",
                buttonTexture.Width,
                buttonTexture.Height);

            newGameButton.Click += NewGameButton_Click;

            var educationButton = new Button(new Vector2((graphicsDevice.Viewport.Width - buttonTexture.Width) / 2,
                (graphicsDevice.Viewport.Height - buttonTexture.Height) / 2 + buttonTexture.Height + 10),
                "Education",
                buttonTexture.Width,
                buttonTexture.Height);

            educationButton.Click += EducationButton_Click;

            var quitGameButton = new Button(new Vector2((graphicsDevice.Viewport.Width - buttonTexture.Width) / 2,
                (graphicsDevice.Viewport.Height - buttonTexture.Height) / 2 + buttonTexture.Height * 2 + 15 * 2),
                "Quit Game",
                buttonTexture.Width,
                buttonTexture.Height);

            quitGameButton.Click += QuitGameButton_Click;

            _buttons = new List<Button>()
            {
                newGameButton,
                educationButton,
                quitGameButton,
            };
        }

        private void EducationButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new EducationState(_game, _graphicsDevice, _content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _backgroundView.Draw(spriteBatch, _graphicsDevice);
            foreach (var button in _buttons)
                _buttonView.Draw(spriteBatch, button);
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new ChoiceMapState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var button in _buttons)
                button.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
