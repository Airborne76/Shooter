using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Shooter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        private int _screenWidth;
        private int _screenHeight;
        private Texture2D bullettexture;        
        private List<Bullet> bulletlist;
        private string HP;
        private SpriteFont HPtexture;
        private Texture2D enemytexture;
        private List<Enemy> enemylist;
        private double timeforbullet;
        private double timeforenemy;
        private Random ran;
        private Menu menu;
        public Game1()
        {
            //IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 480;
            //menu = new Menu(this);
            //this.Components.Add(menu);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            HP = "";
            timeforbullet = 0;
            timeforenemy = 0;
            ran = new Random();        
            player = new Player();
            bulletlist = new List<Bullet>();
            enemylist = new List<Enemy>();
            _screenHeight = Window.ClientBounds.Height;
            _screenWidth = Window.ClientBounds.Width;
            var PlayerPosition = new Vector2(_screenWidth*0.5f,_screenHeight*0.8f);
            player.Initialize(Content.Load<Texture2D>("player"), PlayerPosition);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bullettexture = Content.Load<Texture2D>("shit");
            enemytexture = Content.Load<Texture2D>("enemy");
            HPtexture = Content.Load<SpriteFont>("ShooterFont");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
                
            player.Position.X = Mouse.GetState().X;
            player.Position.Y = Mouse.GetState().Y;
            player.Position.X = MathHelper.Clamp(player.Position.X, player.Width()/2,_screenWidth- player.Width() / 2);
            player.Position.Y = MathHelper.Clamp(player.Position.Y, player.Height()/2, _screenHeight - player.Height()/2);
            if (gameTime.TotalGameTime.TotalMilliseconds>=timeforenemy+150)
            {
                timeforenemy = gameTime.TotalGameTime.TotalMilliseconds;
                Enemy enemy = new Enemy();
                //Random ran = new Random();
                enemy.Initialize(enemytexture, new Vector2()
                {
                    X = ran.Next(0, _screenWidth - 20),
                    Y = 0
                });
                enemylist.Add(enemy);
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds>=timeforbullet+150)
                {
                    timeforbullet = gameTime.TotalGameTime.TotalMilliseconds;
                    Bullet bullet = new Bullet();                
                    bullet.Initialize(bullettexture, player.Position);
                    bulletlist.Add(bullet);
                }                                
            }
            if (bulletlist.Count>0)
            {
                for (int i = bulletlist.Count-1; i >=0; i--)
                {
                    bulletlist[i].Position.Y -= 10;
                    if (bulletlist[i].Position.Y<=bulletlist[i].Height()/2)
                    {
                        bulletlist.Remove(bulletlist[i]);
                    }                    
                }
            }
            if (enemylist.Count>0)
            {
                for (int i = enemylist.Count-1; i >=0; i--)
                {
                    enemylist[i].Position.Y += 5;                    
                    if (enemylist[i].Position.Y>=_screenHeight- enemylist[i].Height()/2)
                    {
                        enemylist.Remove(enemylist[i]);

                    }
                }
            }
            for (int i = enemylist.Count - 1; i >= 0; i--)
            {
                for (int n = bulletlist.Count - 1; n >= 0; n--)
                {
                    if (i>=enemylist.Count)
                    {
                        //enemylist.Remove(enemylist[enemylist.Count - 1]);
                    }
                    else if (bulletlist[n].Bulletrectangle().Intersects(enemylist[i].Enemyrectangle()))
                    {
                        enemylist.Remove(enemylist[i]);
                        bulletlist.Remove(bulletlist[n]);                        
                    }
                }
            }
            for (int i = enemylist.Count - 1; i >= 0; i--)
            {
                if (enemylist[i].Enemyrectangle().Intersects(player.rectangle()))
                {
                    enemylist.Remove(enemylist[i]);
                    player.Health -= 5;
                }
            }            
            // TODO: Add your update logic here            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            //menu.Draw(gameTime);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.DrawString(HPtexture,$"HP:{player.Health}",new Vector2() { X=5,Y=5},Color.Green);
            player.Draw(spriteBatch);
            if (bulletlist.Count>0)
            {
                foreach (var bullet in bulletlist)
                {
                    bullet.Draw(spriteBatch);
                }
            }
            if (enemylist.Count>0)
            {
                foreach (var enemy in enemylist)
                {
                    enemy.Draw(spriteBatch);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
