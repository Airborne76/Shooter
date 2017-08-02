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
    class Enemy
    {
        public Texture2D EnemyTexture;
        public Vector2 Position;
        public int Height()
        {
            return EnemyTexture.Height;
        }
        public int Width()
        {
            return EnemyTexture.Width;
        }
        public Rectangle Enemyrectangle()
        {
            return new Rectangle((int)(Position.X - Width() / 2), (int)(Position.Y - Height() / 2), Width(), Height());
        }
        public void Initialize(Texture2D texture, Vector2 position)
        {
            EnemyTexture = texture;
            Position = position;
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EnemyTexture, Enemyrectangle(), Color.White);
        }
    }
}
