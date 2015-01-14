using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PlatformerGame
{
    public class Rain
    {
        Texture2D texture;
        Color[] textureData;
        Vector2 position;
        Vector2 velocity;

        public float life = 10;

        public Vector2 Position
        {
            get { return position; }
        }

        public Rain(Texture2D newTexture, Vector2 newPosition, Vector2 newVelocity)
        {
            texture = newTexture;
            position = newPosition;
            velocity = newVelocity;

            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
        }

        public void Update(GameTime gameTime)
        {
            life -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public bool b_RainCollide(List<Platform> platformList)
        {
            foreach (Platform p in platformList)
            {
                if (p.getPosition().Y < position.Y)
                {
                    Rectangle rect1 = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                    Rectangle rect2 = p.getRect();

                    //Checks colour values of the objects that are passed through it.
                    int top = Math.Max(rect1.Top, rect2.Top);
                    int bottom = Math.Min(rect1.Bottom, rect2.Bottom);
                    int left = Math.Max(rect1.Left, rect2.Left);
                    int right = Math.Min(rect1.Right, rect2.Right);

                    for (int y = top; y < bottom; y++)
                    {
                        for (int x = left; x < right; x++)
                        {
                            Color color1 = textureData[(x - rect1.Left) +
                                                       (y - rect1.Top) * rect1.Width];

                            Color color2 = p.getColor()[(x - rect2.Left) +
                                                 (y - rect2.Top) * rect2.Width];

                            //if both color regions are not transparent
                            if (color1.A != 0 && color2.A != 0)
                                return true;
                        }
                    }
                }
                else return false;
            }

            return false;
        }
    }
}

