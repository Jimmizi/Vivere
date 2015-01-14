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
    public class Fire
    {
        Vector2 position;
        Vector2 velocity;

        Rectangle rectangle;
        Texture2D texture;

        int sizeX = 4;
        int sizeY = 4;

        float localTimer;
        float center;

        //particle break down lifespan
        float lifeSpan;

        float radius = 15;
        double msElapsed;
        float offset;

        public bool queueDeletion = false;
        Random rand = new Random();

        public Vector2 Position
        {
            get { return position; }
        }

        public Fire(string type, ContentManager content, Vector2 newPosition, Vector2 newVelocity)
        {
            position = newPosition;
            velocity = newVelocity;

            if (type == "red")
            {
                texture = content.Load<Texture2D>("Misc/fire");
                lifeSpan = (float)rand.Next(1, 5) / 10;
            }
            else if (type == "yellow")
            {
                texture = content.Load<Texture2D>("Misc/fireYellow");
                lifeSpan = (float)rand.Next(1, 4) / 10;
                sizeX = 2;
                sizeY = 2;
            }

            rectangle = new Rectangle((int)position.X, (int)position.Y, sizeX, sizeY);
            center = position.X;
        }

        public void Update(GameTime gameTime)
        {
            localTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            


            msElapsed = rand.Next(-10, 10);

            offset = (float)Math.Sin(msElapsed);
            position.X += offset + velocity.X;

            position.Y += velocity.Y;

            

            if (localTimer > lifeSpan)
            {
                if (sizeX != 1 && sizeY != 1)
                    rectangle = new Rectangle((int)position.X, (int)position.Y, --sizeX, --sizeY);
                else
                    //else queue for deletion
                    queueDeletion = true;

                localTimer = 0;
            }

            rectangle = new Rectangle((int)position.X, (int)position.Y, rectangle.Width, rectangle.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public bool b_RainCollide(List<Platform> platformList)
        {
            foreach (Platform p in platformList)
            {
                if (p.getPosition().Y < position.Y)
                {
                    Rectangle rect2 = p.getRect();

                    
                }
                else return false;
            }

            return false;
        }
    }
}
