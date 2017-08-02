using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shooter
{    
    class Bullet
    {
        public Texture2D BulletTexture;
        public Vector2 Position;
        public int Height()
        {
            return (int)(BulletTexture.Height * 0.5);
        }
        public int Width()
        {
            return (int)(BulletTexture.Width * 0.5);
        }
        public Rectangle Bulletrectangle()
        {
            return new Rectangle((int)(Position.X - Width() / 2), (int)(Position.Y - Height() / 2), Width(), Height());
        }
        public void Initialize(Texture2D texture, Vector2 position)
        {
            BulletTexture = texture;
            Position = position;
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BulletTexture,Bulletrectangle(), Color.White);
        }
    }
}
