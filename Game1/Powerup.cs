using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Powerup : GameObject
    {
        public Powerup(Texture2D texture, Vector2 startPosition) : base(texture, startPosition, new Vector2(0, 0), Color.White)
        {

        }      
    }
}
