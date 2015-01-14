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
** Last updated: 04/03/14
** Last updated by: Jake
******************************************/



namespace PlatformerGame
{
    public class Powerups : GameObject
    {
        public bool collected = false;
        string type;


        //POWERUPS FUNCTIONS BEGIN.................................................................
        public Powerups(string newType)
        {
            type = newType;
        }

        public void Load(ContentManager content, GraphicsDevice newGraphics)
        {
            base.Load(content, newGraphics, "Powerups/" + type);
        }

        public bool Obtained(Rectangle rect2, Color[] data2)
        {
            if (b_PixelCollision(rectangle, textureData, rect2, data2))
            {
                return true;
            }
            else return false;
        }

        public string GetPowerupType()
        {
            return type;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
