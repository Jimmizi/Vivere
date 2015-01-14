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
    public class RainGenerator : ParticleGenerator
    {
        int wind = 1;
        int fallingSpeed = 8;

        public List<Rain> rain = new List<Rain>();

        public RainGenerator(Texture2D newTexture, float newSpawnWidth, float newDensity)
            : base(newTexture, newSpawnWidth,newDensity)
            
        {
        }

        public void CreateParticle()
        {
            rain.Add(new Rain(texture,
                     new Vector2(horizontalEmission + (float)rand1.NextDouble() * spawnWidth, verticalEmission * 5),
                     new Vector2(wind + rand1.Next(-1,1), rand2.Next(fallingSpeed, fallingSpeed))));
        }

        public void SetEmissionPoint(int x, int y)
        {
            horizontalEmission = x;
            verticalEmission = y;
        }

        public void SetWind(int newWind = 1)
        {
            wind = newWind;
        }

        public void Update(GameTime gameTime, GraphicsDevice graphics, List<Platform> platformList)
        {
            while (timer > spawnTime)
            {
                timer -= 1f / density;
                CreateParticle();
            }

            //check for collision against the platform list
            for (int i = 0; i < rain.Count; i++)
            {
                rain[i].Update(gameTime);

                if (rain[i].b_RainCollide(platformList))
                {
                    rain.RemoveAt(i);
                    i--;
                }
            }

            //if the particle hasn't been deleted by hitting a platform
            //check if it goes off of the screen and delete it
            for (int i = 0; i < rain.Count; i++)
            {
                if (rain[i].life < 0)
                {
                    rain.RemoveAt(i);
                    i--;
                }
            }

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Rain r in rain)
                r.Draw(spriteBatch);
        }
    }
}
