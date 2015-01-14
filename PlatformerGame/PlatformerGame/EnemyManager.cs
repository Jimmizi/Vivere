using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace PlatformerGame
{
    public class EnemyManager
    {
       Enemy enemy;
       List<Enemy> enemylist = new List<Enemy>();

        
       //CONSTRUCTOR
       public EnemyManager()
       {


       }

       //SPAWN ENEMY FUNCTION
       public void Spawn(ContentManager Content)
       {
         // enemy = Content.Load<Texture2D>("");
          enemy.alive = true;
          enemylist.Add(enemy);
       }

    
        
        //UPDATE FUNCTION
       public void update()
       {

       }

        
        //DRAW FUNCTION
       public void Draw()
       {

       }

    }
}
