using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

/*****************************************
** Main Author: Jack Summers
** Secondary Author: N/A
** Last updated: 4/04/14
** Last updated by: Jack
******************************************/

namespace PlatformerGame
{
    public class Camera
    {
        public Matrix transform;
        Viewport view;
        Vector2 center;
     
        public Camera(Viewport newView)
        {
            view = newView;
        }

        //update
        public void Update(GameTime gameTime, Player player)
        {
            //setting the centre of the camera both position values of the player gameobject
            //in the game1 class this will then be set to the specific player manager
            //the camera tracks the player in the x position (left to right)
            //the camera then also tracks the y position (up to down)
            center = new Vector2(player.position.X - 200, player.position.Y - 250);

            //setting the scale of the camera, on the x axis and the y axis
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                Matrix.CreateTranslation(new Vector3(-center.X + 400, -center.Y, 0));
        }

        public void Update(GameTime gameTime, Vector2 pos)
        {
            //setting the centre of the camera both position values of the player gameobject
            //in the game1 class this will then be set to the specific player manager
            //the camera tracks the player in the x position (left to right)
            //the camera then also tracks the y position (up to down)
            center = new Vector2(pos.X / 2, pos.Y / 2);

            //setting the scale of the camera, on the x axis and the y axis
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                Matrix.CreateTranslation(new Vector3(-center.X + 400, -center.Y, 0));
        }

        //function that allows textures to be set to the same center that the player is
        public Vector2 GetCenter()
        {
            return center;
        }

    }
}
