﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Sprint0
{
    public class Game1 : Game
    {
        Texture2D texture;
        Texture2D enemyAttack;
        Vector2 position;
        Vector2 EnemyPosition;

        ISprite sprite;
        List<object> controllerList;
        IPlayer player;

        ISprite spriteI;
        Texture2D textureI;
        Vector2 positionI;

        Texture2D textureB;
        int currentBlockIndex;
        Rectangle currentBlockRect;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        public void ChangeSprite(ISprite newSprite)
        {
            sprite = newSprite;
        }
        public void ChangeItem(ISprite newSprite)
        {
            spriteI = newSprite;
        }
        public void changeBlock(int index)
        {
            currentBlockIndex = index;
        }
            public Rectangle GetScreenBounds()
        {
            return new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Initialize()
        {
            position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            EnemyPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 + 100, _graphics.PreferredBackBufferHeight / 2 + 100);
            positionI = new Vector2(400, 300);
            controllerList = new List<object>();
            currentBlockIndex = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Content.Load<Texture2D>("sheet");
            textureI = Content.Load<Texture2D>("items");
            textureB = Content.Load<Texture2D>("blocks");
            enemyAttack = Content.Load<Texture2D>("EnemyAttack");
            sprite = new Enemy1(texture, EnemyPosition);
            spriteI = new Item1(textureI, positionI);
            player = new PlayerSprite(texture, position, GetScreenBounds());
            controllerList.Add(new KeyboardController(this, texture, enemyAttack, position, EnemyPosition, textureI, positionI, textureB));

        }

        protected override void Update(GameTime gameTime){
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (IController controller in controllerList)
            {
                controller.Update(gameTime);
            }

            player.Update(gameTime);
            sprite.Update(gameTime);
            spriteI.Update(gameTime);
            currentBlockRect = new Rectangle(currentBlockIndex * 16, 0, 16, 16);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin();
            player.Draw(_spriteBatch);
            sprite.Draw(_spriteBatch);
            spriteI.Draw(_spriteBatch);
            _spriteBatch.Draw(textureB, new Vector2(300, 150), currentBlockRect, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void takeDamage()
        {
            player.damaged();
        }

        public void shotFireBall()
        {
            player.fireball();
        }

        public void shotMissile()
        {
            player.missile();
        }
    }
}
