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
    public class FireGenerator : ParticleGenerator
    {
        public List<Fire> fire = new List<Fire>();
        List<Fire> fireYellow = new List<Fire>();

        int minY = -2;
        int maxY = 0;
        public Vector2 spawnPos = new Vector2(300,300);

        public FireGenerator(Texture2D newTexture, float newSpawnWidth, float newDensity)
            : base(newTexture, newSpawnWidth,newDensity)
            
        {
            spawnTime = 0;
        }

        public void SetY(int newMinY, int newMaxY)
        {
            minY = newMinY;
            maxY = newMaxY;
        }

        public void SetSpawnPos(Vector2 newSpawnPos)
        {
            spawnPos = newSpawnPos;
        }

        public void CreateParticle(ContentManager content)
        {
            float velX = (float)rand1.Next(-1,2) / 5;

            fire.Add(new Fire("red", content,
                              new Vector2(spawnPos.X + rand1.Next(-10, 10), spawnPos.Y),
                              new Vector2(velX, rand1.Next(minY, maxY))));

        }

        public void CreateYellowParticle(ContentManager content)
        {
            float velX = (float)rand1.Next(-1, 2) / 5;

            fireYellow.Add(new Fire("yellow", content,
                              new Vector2(spawnPos.X + rand1.Next(-7, 7), spawnPos.Y),
                              new Vector2(velX, rand1.Next(minY, maxY))));

        }

        public void Update(GameTime gameTime, ContentManager content, List<Platform> platformList)
        {
            while (timer > spawnTime)
            {
                timer -= 1f / density;
                CreateParticle(content);
                CreateYellowParticle(content);

            }

            //check for collision against the platform list
            for (int i = 0; i < fire.Count; i++)
            {
                if (!fire[i].queueDeletion)
                    fire[i].Update(gameTime);
                else
                {
                    fire.RemoveAt(i);
                    i--;
                    continue;
                }
            }

            for (int i = 0; i < fireYellow.Count; i++)
            {
                if (!fireYellow[i].queueDeletion)
                    fireYellow[i].Update(gameTime);
                else
                {
                    fireYellow.RemoveAt(i);
                    i--;
                    continue;
                }
            }

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Fire f in fire)
                f.Draw(spriteBatch);

            foreach (Fire fy in fireYellow)
                fy.Draw(spriteBatch);
        }
    }
}
