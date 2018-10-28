using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class Player : GameObject
    {
        private KeyboardState keyboardState;
        private Keys 
            left,
            right;
        private int score;
        private string score_str;

        public Player(Texture2D texture, Vector2 startPosition, Keys left, Keys right) : base(texture, startPosition, new Vector2(10, 0), Color.White)
        {
            this.left = left;
            this.right = right;
            this.score_str = "0";
        }

        public string Score_Str => score_str;
        public int Score
        {
            get => score;
            set
            {
                score = value;

                score_str = score.ToString();
            }
        }

        public override void Update(GameTime gameTime)
        {
            this.keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(left))
            {
                if(velocity.X < speed.X)
                    velocity.X += (float)gameTime.ElapsedGameTime.TotalSeconds * 32;

                direction.X = -1;
            }
            else if (keyboardState.IsKeyDown(right))
            {
                if (velocity.X < speed.X)
                    velocity.X += (float)gameTime.ElapsedGameTime.TotalSeconds * 32;

                direction.X = 1;
            }
            else
            {
                direction = Vector2.Zero;
                velocity = Vector2.Zero;
            }

            base.Update(gameTime);
        }
    }
}
