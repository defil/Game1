using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Ball : GameObject
    {
        public Ball(Texture2D texture, Vector2 startPosition) : base(texture, startPosition, new Vector2(2, 2), Color.White)
        {
            System.Random random = new System.Random();
            this.direction = new Vector2(
                0.1f, 1 );
        }

        public Player LastTouch { get; set; }

        public void SetDirection(Vector2 value)
        {
            this.direction = value;
        }

        public void SetSpeed(Vector2 value) => speed = value;

        public override void Update(GameTime gameTime)
        {
            rotation += (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

            this.velocity = speed;

            base.Update(gameTime);
        }

    }
}
