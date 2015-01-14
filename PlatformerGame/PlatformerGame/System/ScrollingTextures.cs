using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace PlatformerGame
{
    //class ScrollingTextures
    //{
    //    private SpriteBatch spriteBatch;

    //    //background textures for the three levels

    //    //for level 1
    //    private Texture2D level1Background;

    //    Texture2D level1Layer1;
    //    private float level1ScrollX1;
    //    private float level2ScrollY1;

    //    Texture2D level1Layer2;
    //    private float level1ScrollX2;
    //    private float level1ScrollY2;


    //    //for level 2
    //    // private Texture2D level2Background;

    //    Texture2D level2Layer1;
    //    private float level2ScrollX1;
    //    private float level2ScrollY1;

    //    Texture2D level2Layer2;
    //    private float level2ScrollX2;
    //    private float level2ScrollY2;


    //    //for level 3
    //    // private Texture2D level3Background;

    //     Texture2D level3Layer1;
    //    private float level3ScrollX1;
    //    private float level3ScrollY1;

    //    Texture2D level3Layer2;
    //    private float level3ScrollX2;
    //    private float level3ScrollY2;


    //    public ScrollingTextures()
    //    {
    //    }

    //    protected override void LoadContent(GraphicsDevice graphics, ContentManager Content)
    //    {
    //        spriteBatch = new SpriteBatch(graphics);
    //        level1Background = Content.Load<Texture2D>("");
    //        level1Layer1 = Content.Load<Texture2D>("");
    //        layer2 = Content.Load<Texture2D>("");
    //    }


    //    protected override void Update(GameTime gameTime)
    //    {
    //        if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.D))
    //        {
    //            //for level 1
    //            //scroll layer 1
    //            scrollX1 += 0.5f;
    //            scrollY1 += 0.5f;

    //            //scroll layer 2
    //            scrollX2 += 1.0f;
    //            scrollY2 += 0.8f;



    //            //for level 2
    //            //scroll layer 1


    //            //scroll layer 2 




    //            //for level 3
    //            //scroll layer 1



    //            //scroll layer 2



    //        }
    //    }

    //    protected override void Draw(SpriteBatch spriteBatch)
    //    {

    //        //TO BE PUT IN GAME1 CLASS!!!!!!!!!!!!!!
    //        //---------------------------------------------
    //       // spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
    //       // spriteBatch.Draw(level1Background, Vector2.Zero, Color.White);
    //       // spriteBatch.Draw(layer1, Vector2.Zero, new Rectangle((int)-scrollX1, (int)(-scrollY1), layer1.Width, layer1.Height), Color.White);
    //       // spriteBatch.Draw(layer2, Vector2.Zero, new Rectangle((int)-scrollX2, (int)(-scrollY2), layer2.Width, layer2.Height), Color.White);
    //        spriteBatch.End();
    //    }
    //}
}
