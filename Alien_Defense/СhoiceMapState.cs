using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alien_Defense.View;
using Alien_Defense.Model;

namespace Alien_Defense
{
    /// <summary>
    /// Меню выбора карты
    /// </summary>
    public class ChoiceMapState : State
    {
        private List<Button> _buttons = new List<Button>();
        private ButtonView _buttonView;
        private BackgroundView _backgroundView;
        public override StateGame StateName { get => StateGame.ChoiceLvlMenu; set { } }
        public ChoiceMapState(GameCycle game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _buttonView = new ButtonView();
            _backgroundView = new BackgroundView();
            _buttonView.LoadContent(content, ButtonGameState.ChoiseMapButton);
            _backgroundView.LoadContent(content);
            var buttonTexture = _buttonView.ButtonChoiceLvlTexture;
            var pos = new Vector2(10, 10);
            for (var i = 1; i < game.Levels.Count + 1; i++)
            {
                var button = new Button(pos, $"{i}", buttonTexture.Width, buttonTexture.Height);
                _buttons.Add(button);
                button.Click += Button_Click;
                pos += new Vector2(buttonTexture.Width + 10, 0);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, ((Button)sender).Text));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _backgroundView.Draw(spriteBatch, _graphicsDevice);
            foreach (var button in _buttons)
                _buttonView.Draw(spriteBatch, button);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (var button in _buttons)
                button.Update(gameTime);
        }
    }
}
