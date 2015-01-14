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

//TO DO
//Spawn (or reposition) particles left or right to where the rain gets destroyed to
//create a splash effect

namespace PlatformerGame
{
    public class ParticleGenerator
    {
        public Texture2D texture;

        public float spawnWidth;
        public float density;

        //Weather Variables
        public int horizontalEmission = 0;
        public int verticalEmission = 0;

        //from 0.01 to 1. Spawn time of particles
        public float spawnTime = 0;


        public float timer;
        public Random rand1, rand2;

        public ParticleGenerator(Texture2D newTexture, float newSpawnWidth, float newDensity)
        {
            texture = newTexture;
            spawnWidth = newSpawnWidth;
            density = newDensity;

            rand1 = new Random();
            rand2 = new Random();
        }

        public virtual void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
