using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PlatformerGame
{
    public class Temperature
    {
        float temperature = -400;

        float shrinkage = 0;

        Rectangle rectangle;
        Texture2D textureHot;
        Texture2D textureCold;

        public static int tempLimit = 500;

        float timer = 0;

        public Temperature(ContentManager Content)
        {
            textureHot = Content.Load<Texture2D>("UI/hot");
            textureCold = Content.Load<Texture2D>("UI/cold");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(temperature > 0)
                spriteBatch.Draw(textureHot, rectangle, Color.White);
            else
                spriteBatch.Draw(textureCold, rectangle, Color.White);
        }

        public void DropTemperature(float drop = 1)
        {
            shrinkage += drop;
        }

        public void RaiseTemperature(float drop = 1)
        {
            shrinkage -= drop;
        }

        public float GetTemperature()
        {
            return temperature;
        }

        public void SetTemperature(float newTemp)
        {
            temperature = newTemp;
        }

        public void Update(Vector2 newPos, GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (shrinkage > 0)
            {
                if (timer > 0.01f)
                {
                    if (temperature <= tempLimit && temperature >= -tempLimit)
                    {
                        temperature -= 1f;
                        shrinkage -= 1f;
                        timer = 0;
                    }
                }
            }
            else if (shrinkage < 0)
            {
                if (timer > 0.01f)
                {
                    if (temperature <= tempLimit && temperature >= -tempLimit)
                    {
                        temperature += 1f;
                        shrinkage += 1f;
                        timer = 0;
                    }
                }
            }

            int offset = 220;

            if(temperature > 0)
                rectangle = new Rectangle((int)newPos.X + offset, (int)newPos.Y + 10,
                                          (int)temperature, 10);
            else
                rectangle = new Rectangle((int)newPos.X + offset - (int)-temperature, (int)newPos.Y + 10,
                                          (int)-temperature, 10);

            if (temperature < -500)
                temperature = -500;
            else if (temperature > 500)
                temperature = 500;
        }
    }
}
