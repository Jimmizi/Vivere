using System;
using System.IO;
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
** Last updated: 04/03/14
** Last updated by: Jake
******************************************/

namespace PlatformerGame
{
    public class Map
    {
        #region variables

        //List containers for the items and platforms featured in each level
        public List<Platform> platformList = new List<Platform>();
        List<Powerups> powerupList = new List<Powerups>();

        //StreamReader to read in the map file
        StreamReader mapReader;

        //Holds all of the raw map details:
        //Collision coordinates - Texture files - Powerup/enemy positions
        string rawMap;

        //Store extracted information from rawMap in respective strings
        string[] rawMapTextures;
        string[] rawCollisionPositions;
        string[] rawEnemyPositions;
        string[] rawPowerupPositions;
        string[] rawStartGoalPositions;

        string[] rawInvisiblePositions;
        string[] rawBreakablePositions;

        //Actual information used for positioning
        public Vector2[] collisionPositions;
        public string[] collisionTextures;

        public Vector2[] enemyPositions;
        public string[] enemyTextures;

        public Vector2[] powerupPositions;
        public string[] powerupTextures;

        public string[] mapTextures;
        public Vector2[] mapPositions;

        public Vector2 startPosition, goalPosition;

        public Vector2[] invisiblePositions;
        public string[] invisibleTextures;

        public Vector2[] breakablePositions;
        public string[] breakableTextures;

        public Vector2 mapDimensions;

        //The amount that needs to be read in
        public int collisionAmount;
        int mapTextureAmount;
        int enemyAmount;
        int powerupAmount;

        int invisibleAmount;
        int breakableAmount;
    
        string filePath;

        #endregion

        //MAP FUNCTIONS BEGIN.................................................................
        public Map(int level, ContentManager Content, GraphicsDevice gd)
        {
            load(level);
            convertCoords();

            for (int i = 0; i < collisionAmount; i++)
            {
                Platform platform = new Platform();
                platform.Load(Content, gd, collisionTextures[i]);
                platform.setPosition(collisionPositions[i].X, collisionPositions[i].Y);

                platformList.Add(platform);
            }

            for (int i = 0; i < invisibleAmount; i++)
            {
                Platform platform = new Platform();
                platform.invisible = true;
                platform.Load(Content, gd, invisibleTextures[i]);
                platform.setPosition(invisiblePositions[i].X, invisiblePositions[i].Y);

                platformList.Add(platform);
            }

            for (int i = 0; i < breakableAmount; i++)
            {
                Platform platform = new Platform();
                platform.breakable = true;
                platform.Load(Content, gd, breakableTextures[i]);
                platform.setPosition(breakablePositions[i].X, breakablePositions[i].Y);

                platformList.Add(platform);
            }

            Console.WriteLine("");
        }

        void Initilize()
        {
            rawMapTextures = new String[10];
            rawCollisionPositions = new String[collisionAmount];
            rawEnemyPositions = new String[enemyAmount];
            rawPowerupPositions = new String[powerupAmount];

            rawInvisiblePositions = new String[invisibleAmount];
            rawBreakablePositions = new String[breakableAmount];

            collisionPositions = new Vector2[collisionAmount];
            collisionTextures = new string[collisionAmount];

            enemyPositions = new Vector2[enemyAmount];
            enemyTextures = new string[enemyAmount];

            powerupPositions = new Vector2[powerupAmount];
            powerupTextures = new string[powerupAmount];

            mapTextures = new string[5];
            mapPositions = new Vector2[5];

            startPosition = new Vector2();
            goalPosition = new Vector2();

            rawStartGoalPositions = new string[2];

            invisiblePositions = new Vector2[invisibleAmount];
            invisibleTextures = new string[invisibleAmount];

            breakablePositions = new Vector2[breakableAmount];
            breakableTextures = new string[breakableAmount];

        }

        void load(int level)
        {
            //Alter the filePath dependant on what is passed to the Map constructor
            filePath = @"..\\..\\x86\\Debug\\Content\\Maps\map" + level + ".txt";

            try
            {
                mapReader = new StreamReader(filePath);
                rawMap = mapReader.ReadToEnd();
                mapReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            //getting map dimensions
            int comma = rawMap.IndexOf(',');
            int sqrBracket = rawMap.IndexOf(']');

            mapDimensions.X = Convert.ToInt32(rawMap.Substring(1, comma - 1));
            mapDimensions.Y = Convert.ToInt32(rawMap.Substring(comma + 1, sqrBracket - comma - 1));

            #region Find the amount of data that needs to be read in

            int currCollision = rawMap.IndexOf("#COLLISION_COORDS") + 19;
            int currCollisionTexture = rawMap.IndexOf("#COLLISION_TEXTURES") + 22;

            int currMapTexture = rawMap.IndexOf("#MAP_TEXTURES") + 17;

            int currEnemy = rawMap.IndexOf("#ENEMY_COORDS") + 17;
            int currEnemyTexture = rawMap.IndexOf("#ENEMY_TEXTURES") + 18;

            int currPowerUP = rawMap.IndexOf("#POWER_UP_COORDS") + 18;
            int currPowerupTexture = rawMap.IndexOf("#POWER_UP_TEXTURES") + 22;

            int currStartGoal = rawMap.IndexOf("#STARTGOAL_COORDS") + 19;

            int currInvisible = rawMap.IndexOf("#INVISIBLE_COORDS") + 19;
            int currInvisibleTexture = rawMap.IndexOf("#INVISIBLE_TEXTURES") + 22;

            int currBreakable = rawMap.IndexOf("#BREAKABLE_COORDS") + 19;
            int currBreakableTexture = rawMap.IndexOf("#BREAKABLE_TEXTURES") + 22;

            //convert the amount to be read from string to int
            collisionAmount = Convert.ToInt32(rawMap.Substring(currCollision, 3));
            mapTextureAmount = Convert.ToInt32(rawMap.Substring(currMapTexture, 3));
            enemyAmount = Convert.ToInt32(rawMap.Substring(currEnemy, 3));
            powerupAmount = Convert.ToInt32(rawMap.Substring(currPowerUP, 3));

            invisibleAmount = Convert.ToInt32(rawMap.Substring(currInvisible, 3));
            breakableAmount = Convert.ToInt32(rawMap.Substring(currBreakable, 3));

            #endregion

            //move the current string read to the beginning of the first coord
            currCollision = rawMap.IndexOf("#COLLISION_COORDS") + 25;

            //same for others
            currMapTexture += 4;
            currEnemy += 4;
            currPowerUP += 6;
            currPowerupTexture += 4;
            currStartGoal++;

            currInvisible += 6;
            currBreakable += 6;

            Initilize();

            #region getting all of the raw data from rawMap

            //getting raw map textures
            for (int i = 0; i < mapTextureAmount; i++)
            {
                do
                {
                    rawMapTextures[i] += rawMap[currMapTexture];
                    currMapTexture++;

                } while (rawMap[currMapTexture] != ']');

                currMapTexture += 4;
            }

            //platform texture and position load in
            for (int i = 0; i < collisionAmount; i++)
            {
                do
                {
                    rawCollisionPositions[i] += rawMap[currCollision];
                    currCollision++;

                } while (rawMap[currCollision] != ']');

                do
                {
                    collisionTextures[i] += rawMap[currCollisionTexture];
                    currCollisionTexture++;

                } while (rawMap[currCollisionTexture] != ']');

                currCollision += 4;
                currCollisionTexture += 4;
            }

            //enemy coords/texture load in
            for (int i = 0; i < enemyAmount; i++)
            {
                do
                {
                    rawEnemyPositions[i] += rawMap[currEnemy];
                    currEnemy++;

                } while (rawMap[currEnemy] != ']');

                do
                {
                    enemyTextures[i] += rawMap[currEnemyTexture];
                    currEnemyTexture++;

                } while (rawMap[currEnemyTexture] != ']');

                currEnemy += 4;
                currEnemyTexture += 4;
            }

            //power up load in
            for (int i = 0; i < powerupAmount; i++)
            {
                do
                {
                    rawPowerupPositions[i] += rawMap[currPowerUP];
                    currPowerUP++;

                } while (rawMap[currPowerUP] != ']');

                do
                {
                    powerupTextures[i] += rawMap[currPowerupTexture];
                    currPowerupTexture++;

                } while (rawMap[currPowerupTexture] != ']');

                currPowerUP += 4;
                currPowerupTexture += 4;
            }

            //START AND GOAL load in
            for (int i = 0; i < 2; i++)
            {
                do
                {
                    rawStartGoalPositions[i] += rawMap[currStartGoal];
                    currStartGoal++;

                } while (rawMap[currStartGoal] != ']');

                currStartGoal += 4;
            }

            //invisible platform texture and position load in
            for (int i = 0; i < invisibleAmount; i++)
            {
                do
                {
                    rawInvisiblePositions[i] += rawMap[currInvisible];
                    currInvisible++;

                } while (rawMap[currInvisible] != ']');

                do
                {
                    invisibleTextures[i] += rawMap[currInvisibleTexture];
                    currInvisibleTexture++;

                } while (rawMap[currInvisibleTexture] != ']');

                currInvisible += 4;
                currInvisibleTexture += 4;
            }

            //breakable platform texture and position load in
            for (int i = 0; i < breakableAmount; i++)
            {
                do
                {
                    rawBreakablePositions[i] += rawMap[currBreakable];
                    currBreakable++;

                } while (rawMap[currBreakable] != ']');

                do
                {
                    breakableTextures[i] += rawMap[currBreakableTexture];
                    currBreakableTexture++;

                } while (rawMap[currBreakableTexture] != ']');

                currBreakable += 4;
                currBreakableTexture += 4;
            }

            #endregion
        }

        void convertCoords()
        {
            //here we convert all of the raw coordinates and texture paths to something
            //that is usable by the program
            for (int i = 0; i < collisionAmount; i++)
            {
                collisionPositions[i] = new Vector2(Convert.ToInt32(rawCollisionPositions[i].Substring(0, 5)),
                                                    Convert.ToInt32(rawCollisionPositions[i].Substring(6, 5)));
            }

            for (int i = 0; i < enemyAmount; i++)
            {
                enemyPositions[i] = new Vector2(Convert.ToInt32(rawEnemyPositions[i].Substring(0, 5)),
                                                Convert.ToInt32(rawEnemyPositions[i].Substring(6, 5)));
            }

            for (int i = 0; i < mapTextureAmount; i++)
            {
                mapTextures[i] = rawMapTextures[i];
                mapPositions[i] = new Vector2(0, 0);
            }

            for (int i = 0; i < powerupAmount; i++)
            {
                powerupPositions[i] = new Vector2(Convert.ToInt32(rawPowerupPositions[i].Substring(0, 5)),
                                                  Convert.ToInt32(rawPowerupPositions[i].Substring(6, 5)));
            }

            startPosition = new Vector2(Convert.ToInt32(rawStartGoalPositions[0].Substring(0, 5)),
                                        Convert.ToInt32(rawStartGoalPositions[0].Substring(6, 5)));

            goalPosition = new Vector2(Convert.ToInt32(rawStartGoalPositions[1].Substring(0, 5)),
                                       Convert.ToInt32(rawStartGoalPositions[1].Substring(6, 5)));

            for (int i = 0; i < invisibleAmount; i++)
            {
                invisiblePositions[i] = new Vector2(Convert.ToInt32(rawInvisiblePositions[i].Substring(0, 5)),
                                                    Convert.ToInt32(rawInvisiblePositions[i].Substring(6, 5)));
            }

            for (int i = 0; i < breakableAmount; i++)
            {
                breakablePositions[i] = new Vector2(Convert.ToInt32(rawBreakablePositions[i].Substring(0, 5)),
                                                    Convert.ToInt32(rawBreakablePositions[i].Substring(6, 5)));
            }
        }

        public void Update()
        {
            foreach (Platform p in platformList)
                p.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Platform p in platformList)
                p.Draw(spriteBatch);
        }

        public Vector2[] getPlatformPath(List<Platform> platformList)
        {
            Vector2[] toReturn = new Vector2[2];
            return toReturn;
        }
    }
}


    


