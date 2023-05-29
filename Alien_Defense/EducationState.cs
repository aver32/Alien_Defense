using Alien_Defense.Model;
using Alien_Defense.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense
{
    /// <summary>
    /// Меню обучения
    /// </summary>
    public class EducationState : State
    {
        private List<Button> _buttons;
        private ButtonView _buttonView;
        private BackgroundView _backgroundView;
        private FontView _fontView;
        public override StateGame StateName { get => StateGame.Education; set => throw new NotImplementedException(); }

        public EducationState(GameCycle game, GraphicsDevice graphicsDevice, ContentManager content)
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

            var quitEducationButton = new Button(new Vector2((graphicsDevice.Viewport.Width - buttonTexture.Width) / 2,
                (graphicsDevice.Viewport.Height - buttonTexture.Height) / 2 + buttonTexture.Height + 10),
                "Quit education",
                buttonTexture.Width,
                buttonTexture.Height);

            quitEducationButton.Click += QuitGameButton_Click;

            _buttons = new List<Button>()
            {
                choiceMapButton,
                quitEducationButton,
            };
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _backgroundView.Draw(spriteBatch, _graphicsDevice);
            foreach (var button in _buttons)
                _buttonView.Draw(spriteBatch, button);

            var message1 = "Для добавления башни, наведите мышку на ячейку где изображена цель, затем нажмите левую кнопку мыши";
            var message2 = "Для удаления башни, наведите мышку на ячейку где изображена башня, затем нажмите правую кнопку мыши";
            var message3 = "Справа от игрового поля находится информация о:";
            var message4 = "Сколько у вас осталось денег";
            var message5 = "Стоимости башни";
            var message6 = "Количестве пришельцев в последней клетке перед ракетой";
            var message7 = "Вы победите, если убьете всех пришельцев, до того как они сломают ракету";
            var message8 = "Удачи!!";

            var messages = new List<string>()
            {
                message1, message2, message3, message4, message5, message6,
                message7, message8
            };

            var position = new Vector2(5, 0);
            var font = _fontView.SpriteFont;

            // Определите высоту строки шрифта
            float lineHeight = font.MeasureString(message1).Y;
            for (var i = 0; i < messages.Count; i++)
            {
                if (i == 3) position.X += 30f;
                if (i == 6) position.X -= 30f;
                spriteBatch.DrawString(font, messages[i], position, Color.White);
                position.Y += lineHeight;
            }
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
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }
    }
}
