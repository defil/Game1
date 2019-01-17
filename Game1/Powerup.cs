using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    abstract class Powerup : GameObject
    {
        protected Powerup(Vector2 startPosition, Texture2D texture) : base(texture, startPosition, Vector2.Zero, Color.White)
        {

        }

        public override void Update(GameTime gameTime)
        {



            base.Update(gameTime);
        }

        //public void Remove(Powerup powerups, Ball ball)
        //{
        //    //remove the powerup that's hit
        //    foreach (Powerup p in powerups)
        //    {
        //        if (p.Rectangle.Intersects(ball.Rectangle))
        //        {
        //            if (ball.LastTouch != null)
        //            {
        //                p.ApplyEffect(ball.LastTouch);
        //                this.powerups.Remove(p);
        //                break;
        //            }


        //        }
        //    }
        //}

        public abstract void ApplyEffect(Player player);

    }


    class ScoreSnack : Powerup
    {
        public ScoreSnack(Vector2 startPosition, ContentManager content) : base(startPosition, content.Load<Texture2D>("powerupx2"))
        {
            
        }


        public override void ApplyEffect(Player player)
        {
            player.Score += 1 * (int)Math.Sqrt(player.Score);
        }
    }

    class ResetToZero : Powerup
    {
        public ResetToZero(Vector2 startPosition, ContentManager content) : base(startPosition, content.Load<Texture2D>("powerupZero"))
        {

        }

        public override void ApplyEffect(Player player)
        {
            player.Score = 0;
        }

    }


}
