using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

/*****************************************
** Main Author: Jack Summers
** Secondary Author: N/A
** Last updated: 23/03/14
** Last updated by: Jake
******************************************/

namespace PlatformerGame
{
    public class EnemyManager
    {
        public List<Enemy> enemyList = new List<Enemy>();

        public EnemyManager()
        {

        }

        public void Load(ContentManager Content, GraphicsDevice gd, Map map)
        {
            //enemy load in
            for (int i = 0; i < map.enemyPositions.Count(); i++)
            {
                string toLoad = map.enemyTextures[i].
                    Substring(6, map.enemyTextures[i].Length - 6);

                Enemy newEnemy = new Enemy();
                newEnemy.Load(Content, gd, toLoad);
                newEnemy.setPosition(map.enemyPositions[i]);
                enemyList.Add(newEnemy);
            }
        }

        public void Update(List<Platform> platformList, GameTime gameTime, Player p)
        {
            foreach (Enemy e in enemyList)
            {
                e.Update(platformList, gameTime);

                if (!e.stationary)
                    e.Move(platformList, gameTime,p);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy e in enemyList)
            {
                e.Draw(spriteBatch);
            }
        }
    }
}
