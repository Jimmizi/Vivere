using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*****************************************
** Main Author: Julian Stopher
** Secondary Author: N/A
** Last updated: 30/03/14
** Last updated by: Julian
******************************************/



namespace PlatformerGame
{
    public class ExitDoor : GameObject
    {
        public bool entered = false;
        string type;

        public ExitDoor(string newType)
        {
            type = newType;
        }
        public void Load(ContentManager content, GraphicsDevice newGraphics)
        {
            base.Load(content, newGraphics, "Misc/" + type);
        }
        public bool Entered(Rectangle rect2, Color[] data2)
        {
            if (b_PixelCollision(rectangle, textureData, rect2, data2))
            {
                return true;
               
            }
            else return false;
        }
        public string GetDoorType()
        {
            return type;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
