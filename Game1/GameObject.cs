using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    abstract class GameObject
    {
        private readonly Texture2D texture;
        private Rectangle rectangle;

        private Vector2
            position,
            startPosition;

        protected Vector2
            direction,
            velocity,
            speed;

        private Color drawColor;

        public GameObject(Texture2D texture, Vector2 startPosition, Vector2 speed, Color drawColor)
        {
            this.texture = texture;

            this.position = startPosition;
            this.startPosition = startPosition;

            this.drawColor = drawColor;
            this.speed = speed;

            this.rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public Vector2 StartPosition => startPosition;
        public Rectangle Rectangle => rectangle;
        public Vector2 Position => position;
        public Vector2 Velocity => velocity;
        public Vector2 Speed => speed;
        public Vector2 Direction => direction;

        //public Vector2 CenterPosition => new Vector2(, position.Y - texture.Height / 2);

        public void ResetPosition()
        {
            this.position = this.startPosition;
        }

        public virtual void Update(GameTime gameTime)
        {
            this.position += (velocity * direction);

            rectangle.X = (int)position.X - texture.Width / 2;
            rectangle.Y = (int)position.Y - texture.Height / 2;
        }


        protected float rotation;
        Vector2 origin;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, drawColor, rotation, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
