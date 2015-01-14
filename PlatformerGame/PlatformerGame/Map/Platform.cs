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
** Last updated: 16/02/2014
** Last updated by: Jake 
******************************************/

namespace PlatformerGame
{
    public class Platform : GameObject
    {
        bool moving = false;
        public bool invisible = false;
        public bool breakable = false;
        public bool broken = false;

        //If moving, then these will govern the points it moves to
        Vector2 point1, point2;

        //PLATFORM FUNCTIONS BEGIN.................................................................
        public Platform()
        {

        }

        public override void Load(ContentManager content, GraphicsDevice newGraphics, string toLoad)
        {
            base.Load(content, newGraphics, toLoad);
        }

        public void setMoving(Vector2 firstPoint, Vector2 secondPoint)
        {
            point1 = firstPoint;
            point2 = secondPoint;

            position = point1;

            moving = true;
        }

        public override void setPosition(float x, float y)
        {
            position = new Vector2(x, y);

            //Will only be using setPosition if it's static
            //so we need to use base.Update() once to set the rectangle
            base.Update();
        }

        public override void Update()
        {
            if (moving)
                base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(!invisible && !broken)
                base.Draw(spriteBatch);
        }

        //to get the starting position of the platform
        public float GetLeftX()
        {
            return position.X;
        }

        //return the rightmost point of the platform
        public float GetRightX()
        {
            return position.X + rectangle.Width;
        }

    }
}
