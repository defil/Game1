using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player[] players;
        Ball ball;
        Texture2D ball_shine, powerup_texture;

        Wall wall_left, wall_right;

        Powerup powerup;
        List<Powerup> powerups;

        List<Rectangle> intersections;
        Rectangle intersection;

        // Some misc. content
        Texture2D box;
        SpriteFont scoreFont;

        SoundEffect
            ballBounce;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            players = new Player[2];

            var texture = Content.Load<Texture2D>("player");

            var center = new Vector2((Window.ClientBounds.Width / 2) - texture.Width / 2, Window.ClientBounds.Height - texture.Height);
            var center2 = new Vector2((Window.ClientBounds.Width / 2) - texture.Width / 2, 0 + texture.Height);

            players[0] = new Player(texture, center, Keys.A, Keys.D);
            players[1] = new Player(texture, center2, Keys.Left, Keys.Right);

            box = Content.Load<Texture2D>("box");

            var ball_texture = Content.Load<Texture2D>("ball_0");
            ball_shine = Content.Load<Texture2D>("ball_1");

            ball = new Ball(ball_texture, new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2));

            intersections = new List<Rectangle>();

            var wall_tex = Content.Load<Texture2D>("wall");

            wall_left = new Wall(wall_tex, new Vector2(0 + wall_tex.Width / 2, 0 + Window.ClientBounds.Height / 2)); //(Window.ClientBounds.Height / 2) - wall_tex.Height / 2 )
            wall_right = new Wall(wall_tex, new Vector2(Window.ClientBounds.Width - wall_tex.Width / 2, 0 + Window.ClientBounds.Height / 2));

            powerup_texture = Content.Load<Texture2D>("powerup");
            powerups = new List<Powerup>();          



            base.Initialize();
        }

        private void ResetBall()
        {
            ball.ResetPosition();

            ball.LastTouch = null;

            System.Random random = new System.Random();
            var direction = new Vector2(
                0.1f, 1);
            ball.SetDirection(direction);

            ball.SetSpeed(Vector2.One * 2);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            scoreFont = Content.Load<SpriteFont>("scoreFont");
            ballBounce = Content.Load<SoundEffect>("ballBounce");
        }

        protected override void UnloadContent()
        {
        }



        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                ResetBall();
            }


            ball.Update(gameTime);

            //if(ball.Position.X > Window.ClientBounds.Width || ball.Position.X < 0)
            //{
            //    Vector2 new_direction = Vector2.Reflect(ball.Direction, Vector2.Normalize(ball.Direction));

            //    //new_direction.X -= Vector2.Normalize(
            //    //    new Vector2(Window.ClientBounds.Y + Window.ClientBounds.Height / 2 - intersection.Center.Y, 0)).X;



            //    ball.SetDirection(new_direction);
            //}

            //Scoring
            if (ball.Position.Y > Window.ClientBounds.Height)
            {
                //ball.LastTouch.Score += 1;

                players[1].Score++;
                this.ResetBall();

            }
            else if (ball.Position.Y < 0)
            {
                players[0].Score++;
                this.ResetBall();
            }

            //ball direction reflection
            for (int i = 0; i < players.Length; ++i)
            {
                players[i].Update(gameTime);

                intersection = Rectangle.Intersect(players[i].Rectangle, ball.Rectangle);

                if (intersection != Rectangle.Empty)
                {
                    intersections.Add(intersection);
                    ball.LastTouch = players[i];
                    if (ball.Rectangle.Bottom > players[i].Rectangle.Top)
                    {
                        Vector2 new_direction = Vector2.Reflect(ball.Direction, Vector2.Normalize(ball.Direction));


                        new_direction.X -= Vector2.Normalize(
                            new Vector2(players[i].Rectangle.Center.X - intersection.Center.X, 0)).X;

                        ball.SetSpeed(ball.Speed + (Vector2.One / 5));

                        ball.SetDirection(new_direction);

                        ballBounce.Play();

                    }
                }
            }

            //collision for walls
            //ball.Rectangle.Contains(wall_left.Rectangle);
            if (Rectangle.Intersect(ball.Rectangle, wall_left.Rectangle) != Rectangle.Empty)
            {
                ball.SetDirection(new Vector2(1, ball.Direction.Y));
                //Power up
                //ball.SetDirection(new Vector2(ball.Direction.Y, 1));
            }
            else if (Rectangle.Intersect(ball.Rectangle, wall_right.Rectangle) != Rectangle.Empty)
            {
                ball.SetDirection(new Vector2(-1, ball.Direction.Y));
            }

            wall_left.Update(gameTime);
            wall_right.Update(gameTime);

            //Spawn powerup
            System.Random rng = new System.Random();
            if (rng.Next(0, 60000) < 10000)
            {
                float rngPUx = rng.Next(16, Window.ClientBounds.Width - 16);
                float rngPUy = Window.ClientBounds.Height / 2;
                if (powerups.Count < 5)


                    powerup = null;
                switch (rng.Next(0, 10))
                {
                    case 0:                    
                        powerup = new ScoreSnack(new Vector2(rngPUx, rngPUy), this.Content);                        
                        powerups.Add(powerup);
                        break;

                    case 1:
                        powerup = new ResetToZero(new Vector2(rngPUx, rngPUy), this.Content);
                        powerups.Add(powerup);
                        break;
                    

                }

            }

            //remove the powerup that's hit
            foreach (Powerup p in powerups)
            {
                if (p.Rectangle.Intersects(ball.Rectangle))
                {
                    if (ball.LastTouch != null)
                    {
                        p.ApplyEffect(ball.LastTouch);
                        this.powerups.Remove(p);
                        break;
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            //spriteBatch.Draw(box, ball.Rectangle, Color.Red * 0.4f);

            ball.Draw(spriteBatch);
            spriteBatch.Draw(ball_shine, ball.Rectangle, Color.White);

            for (int i = 0; i < players.Length; ++i)
            {
                players[i].Draw(spriteBatch);

                //spriteBatch.Draw(box, players[i].Rectangle, Color.Red * 0.4f);


                spriteBatch.DrawString(scoreFont, players[i].Score_Str, players[i].Position - scoreFont.MeasureString(players[i].Score_Str) / 2, Color.Black);
            }

            foreach (var foo in intersections)
                spriteBatch.Draw(box, foo, Color.Black * 0.1f);

            wall_left.Draw(spriteBatch);
            wall_right.Draw(spriteBatch);



            foreach (var pu in powerups)
            {
                pu.Draw(spriteBatch);
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
