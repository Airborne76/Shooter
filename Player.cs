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
    class Player
    {
        public Texture2D PlayerTexture;
        public Vector2 Position;
        public bool Active;
        public int Health;
        public int Height()
        {
            return (int)(PlayerTexture.Height*0.5);
        }
        public int Width()
        {
            return (int)(PlayerTexture.Width*0.5);
        }
        //只能在初始化后调用
        public Rectangle rectangle()
        {
            return new Rectangle((int)(Position.X-Width()/2), (int)(Position.Y-Height()/2), Width(),Height());
        }
        public void Initialize(Texture2D texture,Vector2 position)
        {
            PlayerTexture = texture;
            Position = position;
            Active = true;
            Health = 100;
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture, rectangle(), Color.White);
        }
    }
}
