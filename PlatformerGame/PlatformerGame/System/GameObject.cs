using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

/*****************************************
** Main Author: Jake Thorne
** Secondary Author: N/A
** Last updated: 10/03/14
** Last updated by: Julian
******************************************/

namespace PlatformerGame
{
    public class GameObject
    {
        public Texture2D texture;
        public Color[] textureData;
        public Rectangle rectangle;
        public Vector2 position;
        public Vector2 origin;

        public GraphicsDevice graphics;


        //GAMEOBJECT FUNCTIONS BEGIN.................................................................
        public GameObject()
        {

        }

        public virtual void Load(ContentManager content, GraphicsDevice newGraphics, string toLoad)
        {
            graphics = newGraphics;

            if(toLoad != null)
                texture = content.Load<Texture2D>(toLoad);

            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);

        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public Rectangle getRect()
        {
            return rectangle;
        }

        public Color[] getColor()
        {
            return textureData;
        }

        public virtual void setPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public virtual void setPosition(Vector2 newPos)
        {
            position = newPos;
        }

        public static bool b_PixelCollision(Rectangle rect1, Color[] data1,
                                    Rectangle rect2, Color[] data2)
        {
            //Checks colour values of the objects that are passed through it.
            int top = Math.Max(rect1.Top, rect2.Top);
            int bottom = Math.Min(rect1.Bottom, rect2.Bottom);
            int left = Math.Max(rect1.Left, rect2.Left);
            int right = Math.Min(rect1.Right, rect2.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color color1 = data1[(x - rect1.Left) +
                                         (y - rect1.Top) * rect1.Width];

                    Color color2 = data2[(x - rect2.Left) +
                                         (y - rect2.Top) * rect2.Width];

                    //if both color regions are not transparent
                    if (color1.A != 0 && color2.A != 0)
                        return true;
                }
            }

            return false;
        }

        public static float yAxis_PixelCollision(Rectangle rect1, Color[] data1,
                                    Rectangle rect2, Color[] data2)
        {
            int top = Math.Max(rect1.Top, rect2.Top);
            int bottom = Math.Min(rect1.Bottom, rect2.Bottom);
            int left = Math.Max(rect1.Left, rect2.Left);
            int right = Math.Min(rect1.Right, rect2.Right);

            int width = rect1.Width;

            //for (int y = bottom - 1; y > top; y--)
            //{
            //    for (int x = right - 1; x > left; x--)
            //    {
            //        Color color1 = data1[(x - rect1.Left) + (y - rect1.Top) * rect1.Width];

            //        Color color2 = data2[(x - rect2.Left) + (y - rect2.Top) * rect2.Width];

            //        //if both color regions are not transparent
            //        if (color1.A != 0 && color2.A != 0)
            //            return y;
            //    }
            //}

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color color1 = data1[(x - rect1.Left) + (y - rect1.Top) * rect1.Width];

                    Color color2 = data2[(x - rect2.Left) + (y - rect2.Top) * rect2.Width];

                    //if both color regions are not transparent
                    if (color1.A != 0 && color2.A != 0)
                        return y;
                }
            }

            return 0;
        }

        

        public virtual void Update()
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            origin = new Vector2(rectangle.Height / 2, rectangle.Height / 2);
        }

        public virtual void Update(List<Platform> platformList) { }
        public virtual void Update(List<Platform> platformList, GameTime time) { }
        public virtual void Update(Game1.Data data) { }

        
        public virtual void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

    }
}
