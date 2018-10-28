using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Wall : GameObject
    {
        public Wall(Texture2D texture, Vector2 startPosition) : base(texture, startPosition, new Vector2(0,0 ), Color.White)
        {
        }

        public void Rotate()
        {
            rotation = 0.9f;
        }
           
           
    }
}
