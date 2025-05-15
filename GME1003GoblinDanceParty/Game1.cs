using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;

namespace GME1003GoblinDanceParty
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Star variables
        private int _numStars;
        private List<int> _starsX;
        private List<int> _starsY;
        private List<Color> _colors;
        private List<float> _scales;
        private List<float> _transparencies;
        private List<float> _rotations;

        private Texture2D _starSprite;
        private Random _rng;

        // Goblin & music
        Goblin goblin;
        Song music;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _rng = new Random();
            _numStars = 150;

            _starsX = new List<int>();
            _starsY = new List<int>();
            _colors = new List<Color>();
            _scales = new List<float>();
            _transparencies = new List<float>();
            _rotations = new List<float>();

            for (int i = 0; i < _numStars; i++)
            {
                _starsX.Add(_rng.Next(0, 801));
                _starsY.Add(_rng.Next(0, 481));
                _colors.Add(new Color(
                    128 + _rng.Next(0, 129),
                    128 + _rng.Next(0, 129),
                    128 + _rng.Next(0, 129)
                ));
                _scales.Add(_rng.Next(25, 101) / 100f); // 0.25 - 1.0
                _transparencies.Add(_rng.Next(25, 101) / 100f); // 0.25 - 1.0
                _rotations.Add(_rng.Next(0, 101) / 100f); // 0.0 - 1.0
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _starSprite = Content.Load<Texture2D>("starSprite");

            // Goblin setup
            goblin = new Goblin(Content.Load<Texture2D>("goblinIdleSpriteSheet"), 400, 400);
            music = Content.Load<Song>("chiptune");
            MediaPlayer.Play(music);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            goblin.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black); // Black background 💀✨

            _spriteBatch.Begin();

            for (int i = 0; i < _numStars; i++)
            {
                _spriteBatch.Draw(
                    _starSprite,
                    new Vector2(_starsX[i], _starsY[i]),
                    null,
                    _colors[i] * _transparencies[i],
                    _rotations[i],
                    new Vector2(_starSprite.Width / 2, _starSprite.Height / 2),
                    new Vector2(_scales[i], _scales[i]),
                    SpriteEffects.None,
                    0f
                );
            }

            _spriteBatch.End();

            goblin.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
