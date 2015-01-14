#region Using Statements
using System.Diagnostics;
using System.Windows.Forms;
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
#endregion

namespace WinFormsGraphicsDevice
{
    // Example control inherits from GraphicsDeviceControl, which allows it to
    // render using a GraphicsDevice. This control shows how to use ContentManager
    // inside a WinForms application. It loads a SpriteFont object through the
    // ContentManager, then uses a SpriteBatch to draw text. The control is not
    // animated, so it only redraws itself in response to WinForms paint messages.
    public class MapArea : GraphicsDeviceControl
    {
        static public ContentManager content;
        SpriteBatch spriteBatch;
        SpriteFont font;

        List<Texture2D> textureList = new List<Texture2D>();
        List<Rectangle> rectangleList = new List<Rectangle>();

        List<Texture2D> enemyTextureList = new List<Texture2D>();
        List<Rectangle> enemyRectangleList = new List<Rectangle>();

        List<Texture2D> powerupTextureList = new List<Texture2D>();
        List<Rectangle> powerupRectangleList = new List<Rectangle>();

        List<Texture2D> startgoalTextureList = new List<Texture2D>();
        List<Rectangle> startgoalRectangleList = new List<Rectangle>();

        List<Texture2D> invisibleTextureList = new List<Texture2D>();
        List<Rectangle> invisibleRectangleList = new List<Rectangle>();

        List<Texture2D> breakableTextureList = new List<Texture2D>();
        List<Rectangle> breakableRectangleList = new List<Rectangle>();

        enum DragState { PLATFORM, ENEMY, POWERUP, STARTGOAL, INVISIBLE, BREAKABLE, NONE };
        DragState currentDragState = DragState.NONE;

        //We use mState to process clicks but we need a Point to calculate
        //the mouseposition locally to the xna form
        System.Drawing.Point relativeToForm;
        MouseState mState;
        MouseState mStateOld;

        //used to store the current item being moved. If it's -1 then there isn't a 
        //object being moved.
        int draggingObject = -1;

        Texture2D tempTexture;
        Rectangle tempRectangle;

        int textureLimit = 10000;
        

        /// <summary>
        /// Initializes the control, creating the ContentManager
        /// and using it to load a SpriteFont.
        /// </summary>
        protected override void Initialize()
        {
            content = new ContentManager(Services, "Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = content.Load<SpriteFont>("Font\\Times New Roman");

            //this is used to keep the MapArea updated otherwise
            //it would update once then it wouldn't continue to do so.
            Application.Idle += delegate { Invalidate(); };
        }

        /// <summary>
        /// Disposes the control, unloading the ContentManager.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                content.Unload();
            }

            base.Dispose(disposing);
        }

        public void Clear(bool clearPlatforms, 
                          bool clearEnemies, 
                          bool clearPowerups, 
                          bool clearStartgoal,
                          bool clearInvisible,
                          bool clearBreakable)
        {
            

            if (clearPlatforms)
            {
                textureList.Clear();
                rectangleList.Clear();
            }

            if (clearEnemies)
            {
                enemyTextureList.Clear();
                enemyRectangleList.Clear();
            }

            if (clearPowerups)
            {
                powerupTextureList.Clear();
                powerupRectangleList.Clear();
            }

            if (clearStartgoal)
            {
                startgoalTextureList.Clear();
                startgoalRectangleList.Clear();
            }

            if (clearInvisible)
            {
                invisibleTextureList.Clear();
                invisibleRectangleList.Clear();
            }

            if (clearBreakable)
            {
                breakableTextureList.Clear();
                breakableRectangleList.Clear();
            }
        }

        public void Load(int level)
        {
            MapLoader loadMap = new MapLoader(level);
            this.Size = new System.Drawing.Size((int)loadMap.mapDimensions.X, (int)loadMap.mapDimensions.Y);
            this.Clear(true, true, true, true, true, true);

            //platforms
            for (int i = 0; i < loadMap.collisionPositions.Count(); i++)
            {
                tempTexture = content.Load<Texture2D>(loadMap.collisionTextures[i]);

                int temp = loadMap.collisionTextures[i].LastIndexOf("\\");

                tempTexture.Name = loadMap.collisionTextures[i];

                tempRectangle = new Rectangle((int)loadMap.collisionPositions[i].X, 
                                              (int)loadMap.collisionPositions[i].Y, 
                                              tempTexture.Width, 
                                              tempTexture.Height);

                textureList.Add(tempTexture);
                rectangleList.Add(tempRectangle);
            }

            //enemies
            for (int i = 0; i < loadMap.enemyPositions.Count(); i++)
            {
                tempTexture = content.Load<Texture2D>(loadMap.enemyTextures[i]);

                int temp = loadMap.enemyTextures[i].LastIndexOf("\\");

                tempTexture.Name = loadMap.enemyTextures[i];

                tempRectangle = new Rectangle((int)loadMap.enemyPositions[i].X,
                                              (int)loadMap.enemyPositions[i].Y,
                                              tempTexture.Width,
                                                tempTexture.Height);

                enemyTextureList.Add(tempTexture);
                enemyRectangleList.Add(tempRectangle);
            }

            //powerups
            for (int i = 0; i < loadMap.powerupPositions.Count(); i++)
            {
                tempTexture = content.Load<Texture2D>(loadMap.powerupTextures[i]);

                int temp = loadMap.powerupTextures[i].LastIndexOf("\\");

                tempTexture.Name = loadMap.powerupTextures[i];

                tempRectangle = new Rectangle((int)loadMap.powerupPositions[i].X,
                                              (int)loadMap.powerupPositions[i].Y,
                                              tempTexture.Width,
                                              tempTexture.Height);

                powerupTextureList.Add(tempTexture);
                powerupRectangleList.Add(tempRectangle);
            }

            //invisible platforms
            for (int i = 0; i < loadMap.invisiblePositions.Count(); i++)
            {
                tempTexture = content.Load<Texture2D>(loadMap.invisibleTextures[i]);

                int temp = loadMap.invisibleTextures[i].LastIndexOf("\\");

                tempTexture.Name = loadMap.invisibleTextures[i];

                tempRectangle = new Rectangle((int)loadMap.invisiblePositions[i].X,
                                              (int)loadMap.invisiblePositions[i].Y,
                                              tempTexture.Width,
                                              tempTexture.Height);

                invisibleTextureList.Add(tempTexture);
                invisibleRectangleList.Add(tempRectangle);
            }

            //breakable platforms
            for (int i = 0; i < loadMap.breakablePositions.Count(); i++)
            {
                tempTexture = content.Load<Texture2D>(loadMap.breakableTextures[i]);

                int temp = loadMap.breakableTextures[i].LastIndexOf("\\");

                tempTexture.Name = loadMap.breakableTextures[i];

                tempRectangle = new Rectangle((int)loadMap.breakablePositions[i].X,
                                              (int)loadMap.breakablePositions[i].Y,
                                              tempTexture.Width,
                                              tempTexture.Height);

                breakableTextureList.Add(tempTexture);
                breakableRectangleList.Add(tempRectangle);
            }

            #region spawn and goal nodes

            tempTexture = content.Load<Texture2D>("Misc\\spawn");

            tempRectangle = new Rectangle((int)loadMap.startPosition.X,
                                          (int)loadMap.startPosition.Y,
                                          tempTexture.Width,
                                          tempTexture.Height);

            startgoalTextureList.Add(tempTexture);
            startgoalRectangleList.Add(tempRectangle);

            tempTexture = content.Load<Texture2D>("Misc\\goal");

            tempRectangle = new Rectangle((int)loadMap.goalPosition.X,
                                          (int)loadMap.goalPosition.Y,
                                          tempTexture.Width,
                                          tempTexture.Height);

            startgoalTextureList.Add(tempTexture);
            startgoalRectangleList.Add(tempRectangle);

            #endregion

        }

        //Called when the Insert Asset button is called
        public void Add(string textureName, string type)
        {
            if (textureList.Count() != textureLimit)
            {
                try
                {
                    tempTexture = content.Load<Texture2D>(textureName);
                    tempTexture.Name = textureName;

                    tempRectangle = new Rectangle(0, 0, tempTexture.Width, tempTexture.Height);

                    switch (type)
                    {
                        case "Platform":
                            textureList.Add(tempTexture);
                            rectangleList.Add(tempRectangle);

                            break;
                        case "Enemy":
                            enemyTextureList.Add(tempTexture);
                            enemyRectangleList.Add(tempRectangle);

                            break;
                        case "Powerup":
                            powerupTextureList.Add(tempTexture);
                            powerupRectangleList.Add(tempRectangle);

                            break;
                        case "StartGoal":
                            startgoalTextureList.Add(tempTexture);
                            startgoalRectangleList.Add(tempRectangle);

                            break;
                        case "Invisible":
                            invisibleTextureList.Add(tempTexture);
                            invisibleRectangleList.Add(tempRectangle);

                            break;
                        case "Breakable":
                            breakableTextureList.Add(tempTexture);
                            breakableRectangleList.Add(tempRectangle);

                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return;
            }
        }
        public void AddAtMouse(string textureName, string type)
        {
            mState = Mouse.GetState();
            relativeToForm = Program.mainForm.mapArea1.PointToClient(Cursor.Position);

            if (textureList.Count() != textureLimit)
            {
                try
                {
                    tempTexture = content.Load<Texture2D>(textureName);
                    tempTexture.Name = textureName;

                    tempRectangle = new Rectangle(relativeToForm.X - tempTexture.Width / 2,
                                                  relativeToForm.Y - tempTexture.Height/ 2, 
                                                  tempTexture.Width, tempTexture.Height);

                    switch (type)
                    {
                        case "Platform":
                            textureList.Add(tempTexture);
                            rectangleList.Add(tempRectangle);

                            break;
                        case "Enemy":
                            enemyTextureList.Add(tempTexture);
                            enemyRectangleList.Add(tempRectangle);

                            break;
                        case "Powerup":
                            powerupTextureList.Add(tempTexture);
                            powerupRectangleList.Add(tempRectangle);

                            break;
                        case "StartGoal":
                            startgoalTextureList.Add(tempTexture);
                            startgoalRectangleList.Add(tempRectangle);

                            break;
                        case "Invisible":
                            invisibleTextureList.Add(tempTexture);
                            invisibleRectangleList.Add(tempRectangle);

                            break;
                        case "Breakable":
                            breakableTextureList.Add(tempTexture);
                            breakableRectangleList.Add(tempRectangle);

                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                return;
            }
        }

        //Saving to a format abiding text file
        public void Save(string saveTo)
        {
            #region saving variables 

            string collisionAmount;
            string mapTextureAmount;
            string enemyAmount;
            string powerupAmount;
            string invisibleAmount;
            string breakableAmount;

            string[] collisionCoords = new string[rectangleList.Count()];
            string[] startgoalCoords = new string[2];
            string[] collisionTextures = new string[textureList.Count()];
            string[] mapTextures;

            string[] enemyCoords = new string[enemyRectangleList.Count()];
            string[] enemyTextures = new string[enemyTextureList.Count()];

            string[] powerupCoords = new string[powerupRectangleList.Count()];
            string[] powerupTextures = new string[powerupTextureList.Count()];

            string[] invisibleCoords = new string[invisibleRectangleList.Count()];
            string[] invisibleTextures = new string[invisibleTextureList.Count()];

            string[] breakableCoords = new string[breakableRectangleList.Count()];
            string[] breakableTextures = new string[breakableTextureList.Count()];

            #endregion

            #region Appending 0s to write load-in amounts

            //append 0s to the front of the collision amount depending on how many
            //platforms are in the level
            if(rectangleList.Count() < 10)
                collisionAmount = "00" + Convert.ToString(rectangleList.Count());
            else if(rectangleList.Count() < 100 && rectangleList.Count() >= 10)
                collisionAmount = "0" + Convert.ToString(rectangleList.Count());
            else
                collisionAmount = Convert.ToString(rectangleList.Count());

            //for enemies
            if (enemyRectangleList.Count() < 10)
                enemyAmount = "00" + Convert.ToString(enemyRectangleList.Count());
            else if (enemyRectangleList.Count() < 100 && enemyRectangleList.Count() >= 10)
                enemyAmount = "0" + Convert.ToString(enemyRectangleList.Count());
            else
                enemyAmount = Convert.ToString(enemyRectangleList.Count());

            //for powerups
            if (powerupRectangleList.Count() < 10)
                powerupAmount = "00" + Convert.ToString(powerupRectangleList.Count());
            else if (powerupRectangleList.Count() < 100 && powerupRectangleList.Count() >= 10)
                powerupAmount = "0" + Convert.ToString(powerupRectangleList.Count());
            else
                powerupAmount = Convert.ToString(powerupRectangleList.Count());

            //for invisible platforms
            if (invisibleRectangleList.Count() < 10)
                invisibleAmount = "00" + Convert.ToString(invisibleRectangleList.Count());
            else if (invisibleRectangleList.Count() < 100 && invisibleRectangleList.Count() >= 10)
                invisibleAmount = "0" + Convert.ToString(invisibleRectangleList.Count());
            else
                invisibleAmount = Convert.ToString(invisibleRectangleList.Count());

            //for breakable platforms
            if (breakableRectangleList.Count() < 10)
                breakableAmount = "00" + Convert.ToString(breakableRectangleList.Count());
            else if (breakableRectangleList.Count() < 100 && breakableRectangleList.Count() >= 10)
                breakableAmount = "0" + Convert.ToString(breakableRectangleList.Count());
            else
                breakableAmount = Convert.ToString(breakableRectangleList.Count());


            #endregion
            #region begin conversion to save format

            //platforms
            for (int i = 0; i < rectangleList.Count(); i++)
            {
                string toAddX = Convert.ToString(rectangleList[i].X);
                string toAddY = Convert.ToString(rectangleList[i].Y);

                #region Appending 0s

                // We append 0s to the front of the X and Y so that when loading it in
                // it can load in any value between 0 and 99999
                switch (toAddX.Length)
                {
                    case 1:
                        toAddX = "0000" + toAddX;
                        break;
                    case 2:
                        toAddX = "000" + toAddX;
                        break;
                    case 3:
                        toAddX = "00" + toAddX;
                        break;
                    case 4:
                        toAddX = "0" + toAddX;
                        break;
                }

                switch (toAddY.Length)
                {
                    case 1:
                        toAddY = "0000" + toAddY;
                        break;
                    case 2:
                        toAddY = "000" + toAddY;
                        break;
                    case 3:
                        toAddY = "00" + toAddY;
                        break;
                    case 4:
                        toAddY = "0" + toAddY;
                        break;
                }
                #endregion

                //convert variables to a load-in friendly format
                collisionTextures[i] +=
                    "[" + textureList[i].Name + "]";

                collisionCoords[i] +=
                    "[" + toAddX + "," + toAddY + "]";
            }

            //enemies
            for (int i = 0; i < enemyRectangleList.Count(); i++)
            {
                string toAddX = Convert.ToString(enemyRectangleList[i].X);
                string toAddY = Convert.ToString(enemyRectangleList[i].Y);

                #region Appending 0s

                // We append 0s to the front of the X and Y so that when loading it in
                // it can load in any value between 0 and 99999
                switch (toAddX.Length)
                {
                    case 1:
                        toAddX = "0000" + toAddX;
                        break;
                    case 2:
                        toAddX = "000" + toAddX;
                        break;
                    case 3:
                        toAddX = "00" + toAddX;
                        break;
                    case 4:
                        toAddX = "0" + toAddX;
                        break;
                }

                switch (toAddY.Length)
                {
                    case 1:
                        toAddY = "0000" + toAddY;
                        break;
                    case 2:
                        toAddY = "000" + toAddY;
                        break;
                    case 3:
                        toAddY = "00" + toAddY;
                        break;
                    case 4:
                        toAddY = "0" + toAddY;
                        break;
                }
                #endregion

                //convert variables to a load-in friendly format
                enemyTextures[i] +=
                    "[" + enemyTextureList[i].Name + "]";

                enemyCoords[i] +=
                    "[" + toAddX + "," + toAddY + "]";
            }

            //powerups
            for (int i = 0; i < powerupRectangleList.Count(); i++)
            {
                string toAddX = Convert.ToString(powerupRectangleList[i].X);
                string toAddY = Convert.ToString(powerupRectangleList[i].Y);

                #region Appending 0s

                // We append 0s to the front of the X and Y so that when loading it in
                // it can load in any value between 0 and 99999
                switch (toAddX.Length)
                {
                    case 1:
                        toAddX = "0000" + toAddX;
                        break;
                    case 2:
                        toAddX = "000" + toAddX;
                        break;
                    case 3:
                        toAddX = "00" + toAddX;
                        break;
                    case 4:
                        toAddX = "0" + toAddX;
                        break;
                }

                switch (toAddY.Length)
                {
                    case 1:
                        toAddY = "0000" + toAddY;
                        break;
                    case 2:
                        toAddY = "000" + toAddY;
                        break;
                    case 3:
                        toAddY = "00" + toAddY;
                        break;
                    case 4:
                        toAddY = "0" + toAddY;
                        break;
                }
                #endregion

                //convert variables to a load-in friendly format
                powerupTextures[i] +=
                    "[" + powerupTextureList[i].Name + "]";

                powerupCoords[i] +=
                    "[" + toAddX + "," + toAddY + "]";
            }

            if(startgoalRectangleList.Count() > 0)
            {
                string toAddX = Convert.ToString(startgoalRectangleList[0].X);
                string toAddY = Convert.ToString(startgoalRectangleList[0].Y);

                #region Appending 0s

                // We append 0s to the front of the X and Y so that when loading it in
                // it can load in any value between 0 and 99999
                switch (toAddX.Length)
                {
                    case 1:
                        toAddX = "0000" + toAddX;
                        break;
                    case 2:
                        toAddX = "000" + toAddX;
                        break;
                    case 3:
                        toAddX = "00" + toAddX;
                        break;
                    case 4:
                        toAddX = "0" + toAddX;
                        break;
                }

                switch (toAddY.Length)
                {
                    case 1:
                        toAddY = "0000" + toAddY;
                        break;
                    case 2:
                        toAddY = "000" + toAddY;
                        break;
                    case 3:
                        toAddY = "00" + toAddY;
                        break;
                    case 4:
                        toAddY = "0" + toAddY;
                        break;
                }
                #endregion
                startgoalCoords[0] = "[" + toAddX + "," + toAddY + "]";

                toAddX = Convert.ToString(startgoalRectangleList[1].X);
                toAddY = Convert.ToString(startgoalRectangleList[1].Y);

                #region Appending 0s

                // We append 0s to the front of the X and Y so that when loading it in
                // it can load in any value between 0 and 99999
                switch (toAddX.Length)
                {
                    case 1:
                        toAddX = "0000" + toAddX;
                        break;
                    case 2:
                        toAddX = "000" + toAddX;
                        break;
                    case 3:
                        toAddX = "00" + toAddX;
                        break;
                    case 4:
                        toAddX = "0" + toAddX;
                        break;
                }

                switch (toAddY.Length)
                {
                    case 1:
                        toAddY = "0000" + toAddY;
                        break;
                    case 2:
                        toAddY = "000" + toAddY;
                        break;
                    case 3:
                        toAddY = "00" + toAddY;
                        break;
                    case 4:
                        toAddY = "0" + toAddY;
                        break;
                }
                #endregion
                startgoalCoords[1] = "[" + toAddX + "," + toAddY + "]";
            }

            //invisible platforms
            for (int i = 0; i < invisibleRectangleList.Count(); i++)
            {
                string toAddX = Convert.ToString(invisibleRectangleList[i].X);
                string toAddY = Convert.ToString(invisibleRectangleList[i].Y);

                #region Appending 0s

                // We append 0s to the front of the X and Y so that when loading it in
                // it can load in any value between 0 and 99999
                switch (toAddX.Length)
                {
                    case 1:
                        toAddX = "0000" + toAddX;
                        break;
                    case 2:
                        toAddX = "000" + toAddX;
                        break;
                    case 3:
                        toAddX = "00" + toAddX;
                        break;
                    case 4:
                        toAddX = "0" + toAddX;
                        break;
                }

                switch (toAddY.Length)
                {
                    case 1:
                        toAddY = "0000" + toAddY;
                        break;
                    case 2:
                        toAddY = "000" + toAddY;
                        break;
                    case 3:
                        toAddY = "00" + toAddY;
                        break;
                    case 4:
                        toAddY = "0" + toAddY;
                        break;
                }
                #endregion

                //convert variables to a load-in friendly format
                invisibleTextures[i] +=
                    "[" + invisibleTextureList[i].Name + "]";

                invisibleCoords[i] +=
                    "[" + toAddX + "," + toAddY + "]";
            }

            //breakable platforms
            for (int i = 0; i < breakableRectangleList.Count(); i++)
            {
                string toAddX = Convert.ToString(breakableRectangleList[i].X);
                string toAddY = Convert.ToString(breakableRectangleList[i].Y);

                #region Appending 0s

                // We append 0s to the front of the X and Y so that when loading it in
                // it can load in any value between 0 and 99999
                switch (toAddX.Length)
                {
                    case 1:
                        toAddX = "0000" + toAddX;
                        break;
                    case 2:
                        toAddX = "000" + toAddX;
                        break;
                    case 3:
                        toAddX = "00" + toAddX;
                        break;
                    case 4:
                        toAddX = "0" + toAddX;
                        break;
                }

                switch (toAddY.Length)
                {
                    case 1:
                        toAddY = "0000" + toAddY;
                        break;
                    case 2:
                        toAddY = "000" + toAddY;
                        break;
                    case 3:
                        toAddY = "00" + toAddY;
                        break;
                    case 4:
                        toAddY = "0" + toAddY;
                        break;
                }
                #endregion

                //convert variables to a load-in friendly format
                breakableTextures[i] +=
                    "[" + breakableTextureList[i].Name + "]";

                breakableCoords[i] +=
                    "[" + toAddX + "," + toAddY + "]";
            }

            #endregion

            //Erase the current contents of the file being written to
            string[] lines = {"[" + this.Size.Width + "," + this.Size.Height + "]"};
            System.IO.File.WriteAllLines(@"..\\..\\Content\\Maps\map" + saveTo + ".txt", lines);

            //Here is where all of the lines in the textfile are defined
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\\..\\Content\\Maps\map" + saveTo + ".txt", true))
            {
                file.WriteLine("#MAP_TEXTURES");
                file.WriteLine("000");
                file.WriteLine("");

                #region platform saving

                file.WriteLine("#COLLISION_COORDS");
                file.WriteLine(collisionAmount);

                for (int i = 0; i < rectangleList.Count(); i++)
                {
                    file.WriteLine(collisionCoords[i]);
                }
                file.WriteLine("");

                file.WriteLine("#COLLISION_TEXTURES");

                for (int i = 0; i < rectangleList.Count(); i++)
                {
                    file.WriteLine(collisionTextures[i]);
                }
                file.WriteLine("");

                #endregion
                #region enemy saving

                file.WriteLine("#ENEMY_COORDS");
                file.WriteLine(enemyAmount);

                for (int i = 0; i < enemyRectangleList.Count(); i++)
                {
                    file.WriteLine(enemyCoords[i]);
                }
                file.WriteLine("");

                file.WriteLine("#ENEMY_TEXTURES");

                for (int i = 0; i < enemyRectangleList.Count(); i++)
                {
                    file.WriteLine(enemyTextures[i]);
                }
                file.WriteLine("");

                #endregion
                #region powerup saving

                file.WriteLine("#POWER_UP_COORDS");
                file.WriteLine(powerupAmount);

                for (int i = 0; i < powerupRectangleList.Count(); i++)
                {
                    file.WriteLine(powerupCoords[i]);
                }
                file.WriteLine("");

                file.WriteLine("#POWER_UP_TEXTURES");
                file.WriteLine(powerupAmount);

                for (int i = 0; i < powerupRectangleList.Count(); i++)
                {
                    file.WriteLine(powerupTextures[i]);
                }

                #endregion
                #region startgoal saving

                file.WriteLine("");
                file.WriteLine("#STARTGOAL_COORDS");

                if (startgoalRectangleList.Count() > 0)
                {
                    file.WriteLine(startgoalCoords[0]);
                    file.WriteLine(startgoalCoords[1]);
                }
                file.WriteLine("");
                #endregion
                #region invisible platform saving

                file.WriteLine("#INVISIBLE_COORDS");
                file.WriteLine(invisibleAmount);

                for (int i = 0; i < invisibleRectangleList.Count(); i++)
                {
                    file.WriteLine(invisibleCoords[i]);
                }
                file.WriteLine("");

                file.WriteLine("#INVISIBLE_TEXTURES");

                for (int i = 0; i < invisibleRectangleList.Count(); i++)
                {
                    file.WriteLine(invisibleTextures[i]);
                }
                file.WriteLine("");

                #endregion
                #region breakable platform saving

                file.WriteLine("#BREAKABLE_COORDS");
                file.WriteLine(breakableAmount);

                for (int i = 0; i < breakableRectangleList.Count(); i++)
                {
                    file.WriteLine(breakableCoords[i]);
                }
                file.WriteLine("");

                file.WriteLine("#BREAKABLE_TEXTURES");

                for (int i = 0; i < breakableRectangleList.Count(); i++)
                {
                    file.WriteLine(breakableTextures[i]);
                }
                file.WriteLine("");

                #endregion
            }
        }

        public void Update()
        {
            mState = Mouse.GetState();

            //A hack to get the mouse position relative to the xna form in this class.
            //could have made a new instance of MainForm but this way seems like it would
            //be a lot cheaper
            relativeToForm = Program.mainForm.mapArea1.PointToClient(Cursor.Position);

            #region Using left mouse to drag objects around the field
            if (mState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                //Have to convert from C#'s Point to XNA's
                Microsoft.Xna.Framework.Point tempPoint;
                tempPoint.X = relativeToForm.X;
                tempPoint.Y = relativeToForm.Y;

                //-1 means that no object is currently being dragged
                if (draggingObject != -1)
                {
                    #region currentDragState switch

                    switch (currentDragState)
                    {
                        //see what item is being dragged and then start to move it
                        case DragState.PLATFORM:
                            rectangleList[draggingObject] =
                                new Rectangle(relativeToForm.X - rectangleList[draggingObject].Width / 2,
                                              relativeToForm.Y - rectangleList[draggingObject].Height / 2,
                                              textureList[draggingObject].Width,
                                              textureList[draggingObject].Height);

                            break;
                        case DragState.ENEMY:
                            enemyRectangleList[draggingObject] =
                                new Rectangle(relativeToForm.X - enemyRectangleList[draggingObject].Width / 2,
                                              relativeToForm.Y - enemyRectangleList[draggingObject].Height / 2,
                                              enemyTextureList[draggingObject].Width,
                                              enemyTextureList[draggingObject].Height);

                            break;
                        case DragState.POWERUP:
                            powerupRectangleList[draggingObject] =
                                new Rectangle(relativeToForm.X - powerupRectangleList[draggingObject].Width / 2,
                                              relativeToForm.Y - powerupRectangleList[draggingObject].Height / 2,
                                              powerupTextureList[draggingObject].Width,
                                              powerupTextureList[draggingObject].Height);

                            break;
                        case DragState.STARTGOAL:
                            startgoalRectangleList[draggingObject] =
                                new Rectangle(relativeToForm.X - startgoalRectangleList[draggingObject].Width / 2,
                                              relativeToForm.Y - startgoalRectangleList[draggingObject].Height / 2,
                                              startgoalTextureList[draggingObject].Width,
                                              startgoalTextureList[draggingObject].Height);
                            break;
                        case DragState.INVISIBLE:
                            invisibleRectangleList[draggingObject] =
                                new Rectangle(relativeToForm.X - invisibleRectangleList[draggingObject].Width / 2,
                                              relativeToForm.Y - invisibleRectangleList[draggingObject].Height / 2,
                                              invisibleTextureList[draggingObject].Width,
                                              invisibleTextureList[draggingObject].Height);
                            break;
                        case DragState.BREAKABLE:
                            breakableRectangleList[draggingObject] =
                                new Rectangle(relativeToForm.X - breakableRectangleList[draggingObject].Width / 2,
                                              relativeToForm.Y - breakableRectangleList[draggingObject].Height / 2,
                                              breakableTextureList[draggingObject].Width,
                                              breakableTextureList[draggingObject].Height);
                            break;
                    }

                    #endregion
                }

                #region finding an object to drag if one isn't currently being dragged

                for (int i = 0; i < textureList.Count(); i++)
                {
                    if (rectangleList[i].Contains(tempPoint) && draggingObject == -1)
                    {
                        draggingObject = i;
                        currentDragState = DragState.PLATFORM;

                        break;
                    }
                }

                for (int i = 0; i < enemyTextureList.Count(); i++)
                {
                    if (enemyRectangleList[i].Contains(tempPoint) && draggingObject == -1)
                    {
                        draggingObject = i;
                        currentDragState = DragState.ENEMY;

                        break;
                    }
                }

                for (int i = 0; i < powerupTextureList.Count(); i++)
                {
                    if (powerupRectangleList[i].Contains(tempPoint) && draggingObject == -1)
                    {
                        draggingObject = i;
                        currentDragState = DragState.POWERUP;

                        break;
                    }
                }

                for (int i = 0; i < startgoalTextureList.Count(); i++)
                {
                    if (startgoalRectangleList[i].Contains(tempPoint) && draggingObject == -1)
                    {
                        draggingObject = i;
                        currentDragState = DragState.STARTGOAL;

                        break;
                    }
                }

                for (int i = 0; i < invisibleTextureList.Count(); i++)
                {
                    if (invisibleRectangleList[i].Contains(tempPoint) && draggingObject == -1)
                    {
                        draggingObject = i;
                        currentDragState = DragState.INVISIBLE;

                        break;
                    }
                }

                for (int i = 0; i < breakableTextureList.Count(); i++)
                {
                    if (breakableRectangleList[i].Contains(tempPoint) && draggingObject == -1)
                    {
                        draggingObject = i;
                        currentDragState = DragState.BREAKABLE;

                        break;
                    }
                }

                #endregion
            }
            else draggingObject = -1;

            #endregion
            #region right mouse deletes the object the mouse is over

            if (mState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed &&
                mStateOld.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                //Have to convert from C#'s Point to XNA's
                Microsoft.Xna.Framework.Point tempPoint;
                tempPoint.X = relativeToForm.X;
                tempPoint.Y = relativeToForm.Y;

                //for platforms
                for (int i = 0; i < textureList.Count(); i++)
                {
                    if (rectangleList[i].Contains(tempPoint))
                    {
                        textureList.Remove(textureList[i]);
                        rectangleList.Remove(rectangleList[i]);
                    }
                }

                //for enemies
                for (int i = 0; i < enemyTextureList.Count(); i++)
                {
                    if (enemyRectangleList[i].Contains(tempPoint))
                    {
                        enemyTextureList.Remove(enemyTextureList[i]);
                        enemyRectangleList.Remove(enemyRectangleList[i]);
                        return;
                    }
                }

                //for powerups
                for (int i = 0; i < powerupTextureList.Count(); i++)
                {
                    if (powerupRectangleList[i].Contains(tempPoint))
                    {
                        powerupTextureList.Remove(powerupTextureList[i]);
                        powerupRectangleList.Remove(powerupRectangleList[i]);
                    }
                    return;
                }

                for (int i = 0; i < startgoalTextureList.Count(); i++)
                {
                    if (startgoalRectangleList[i].Contains(tempPoint))
                    {
                        startgoalTextureList.Remove(startgoalTextureList[i]);
                        startgoalRectangleList.Remove(startgoalRectangleList[i]);
                    }
                    return;
                }

                for (int i = 0; i < invisibleTextureList.Count(); i++)
                {
                    if (invisibleRectangleList[i].Contains(tempPoint))
                    {
                        invisibleTextureList.Remove(invisibleTextureList[i]);
                        invisibleRectangleList.Remove(invisibleRectangleList[i]);
                    }
                    return;
                }

                for (int i = 0; i < breakableTextureList.Count(); i++)
                {
                    if (breakableRectangleList[i].Contains(tempPoint))
                    {
                        breakableTextureList.Remove(breakableTextureList[i]);
                        breakableRectangleList.Remove(breakableRectangleList[i]);
                    }
                    return;
                }

            }

            mStateOld = mState;

            #endregion

            #region stopping the piece being taken out of the viewport

            //platforms
            for (int i = 0; i < rectangleList.Count(); i++)
            {
                if (rectangleList[i].X < 0)
                    rectangleList[i] = new Rectangle(0,
                                                     rectangleList[i].Y,
                                                     textureList[i].Width,
                                                     textureList[i].Height);

                if (rectangleList[i].X > this.Size.Width - rectangleList[i].Width)
                    rectangleList[i] = new Rectangle(this.Size.Width - rectangleList[i].Width,
                                                     rectangleList[i].Y,
                                                     textureList[i].Width,
                                                     textureList[i].Height);

                if (rectangleList[i].Y < 0)
                    rectangleList[i] = new Rectangle(rectangleList[i].X,
                                                     0,
                                                     textureList[i].Width,
                                                     textureList[i].Height);

                if (rectangleList[i].Y > this.Size.Height - rectangleList[i].Height)
                    rectangleList[i] = new Rectangle(rectangleList[i].X,
                                                     this.Size.Height - rectangleList[i].Height,
                                                     textureList[i].Width,
                                                     textureList[i].Height);
            }

            //enemies
            for (int i = 0; i < enemyRectangleList.Count(); i++)
            {
                if (enemyRectangleList[i].X < 0)
                    enemyRectangleList[i] = new Rectangle(0,
                                                          enemyRectangleList[i].Y,
                                                          enemyTextureList[i].Width,
                                                          enemyTextureList[i].Height);

                if (enemyRectangleList[i].X > this.Size.Width - enemyRectangleList[i].Width)
                    enemyRectangleList[i] = new Rectangle(this.Size.Width - enemyRectangleList[i].Width,
                                                          enemyRectangleList[i].Y,
                                                          enemyTextureList[i].Width,
                                                          enemyTextureList[i].Height);

                if (enemyRectangleList[i].Y < 0)
                    enemyRectangleList[i] = new Rectangle(enemyRectangleList[i].X,
                                                          0,
                                                          enemyTextureList[i].Width,
                                                          enemyTextureList[i].Height);

                if (enemyRectangleList[i].Y > this.Size.Height - enemyRectangleList[i].Height)
                    enemyRectangleList[i] = new Rectangle(enemyRectangleList[i].X,
                                                          this.Size.Height - enemyRectangleList[i].Height,
                                                          enemyTextureList[i].Width,
                                                          enemyTextureList[i].Height);
            }

            //powerps
            for (int i = 0; i < powerupRectangleList.Count(); i++)
            {
                if (powerupRectangleList[i].X < 0)
                    powerupRectangleList[i] = new Rectangle(0,
                                                            powerupRectangleList[i].Y,
                                                            powerupTextureList[i].Width,
                                                            powerupTextureList[i].Height);

                if (powerupRectangleList[i].X > this.Size.Width - powerupRectangleList[i].Width)
                    powerupRectangleList[i] = new Rectangle(this.Size.Width - powerupRectangleList[i].Width,
                                                            powerupRectangleList[i].Y,
                                                            powerupTextureList[i].Width,
                                                            powerupTextureList[i].Height);

                if (powerupRectangleList[i].Y < 0)
                    powerupRectangleList[i] = new Rectangle(powerupRectangleList[i].X,
                                                     0,
                                                     powerupTextureList[i].Width,
                                                     powerupTextureList[i].Height);

                if (powerupRectangleList[i].Y > this.Size.Height - powerupRectangleList[i].Height)
                    powerupRectangleList[i] = new Rectangle(powerupRectangleList[i].X,
                                                            this.Size.Height - powerupRectangleList[i].Height,
                                                            powerupTextureList[i].Width,
                                                             powerupTextureList[i].Height);
            }

            //start and goal
            for (int i = 0; i < startgoalRectangleList.Count(); i++)
            {
                if (startgoalRectangleList[i].X < 0)
                    startgoalRectangleList[i] = new Rectangle(0,
                                                              startgoalRectangleList[i].Y,
                                                              startgoalTextureList[i].Width,
                                                              startgoalTextureList[i].Height);

                if (startgoalRectangleList[i].X > this.Size.Width - startgoalRectangleList[i].Width)
                    startgoalRectangleList[i] = new Rectangle(this.Size.Width - startgoalRectangleList[i].Width,
                                                            startgoalRectangleList[i].Y,
                                                            startgoalTextureList[i].Width,
                                                            startgoalTextureList[i].Height);

                if (startgoalRectangleList[i].Y < 0)
                    startgoalRectangleList[i] = new Rectangle(startgoalRectangleList[i].X,
                                                     0,
                                                     startgoalTextureList[i].Width,
                                                     startgoalTextureList[i].Height);

                if (startgoalRectangleList[i].Y > this.Size.Height - startgoalRectangleList[i].Height)
                    startgoalRectangleList[i] = new Rectangle(startgoalRectangleList[i].X,
                                                              this.Size.Height - startgoalRectangleList[i].Height,
                                                              startgoalTextureList[i].Width,
                                                              startgoalTextureList[i].Height);
            }

            //invisible tiles
            for (int i = 0; i < invisibleRectangleList.Count(); i++)
            {
                if (invisibleRectangleList[i].X < 0)
                    invisibleRectangleList[i] = new Rectangle(0,
                                                              invisibleRectangleList[i].Y,
                                                              invisibleTextureList[i].Width,
                                                              invisibleTextureList[i].Height);

                if (invisibleRectangleList[i].X > this.Size.Width - invisibleRectangleList[i].Width)
                    invisibleRectangleList[i] = new Rectangle(this.Size.Width - invisibleRectangleList[i].Width,
                                                            invisibleRectangleList[i].Y,
                                                            invisibleTextureList[i].Width,
                                                            invisibleTextureList[i].Height);

                if (invisibleRectangleList[i].Y < 0)
                    invisibleRectangleList[i] = new Rectangle(invisibleRectangleList[i].X,
                                                     0,
                                                     invisibleTextureList[i].Width,
                                                     invisibleTextureList[i].Height);

                if (invisibleRectangleList[i].Y > this.Size.Height - invisibleRectangleList[i].Height)
                    invisibleRectangleList[i] = new Rectangle(invisibleRectangleList[i].X,
                                                              this.Size.Height - invisibleRectangleList[i].Height,
                                                              invisibleTextureList[i].Width,
                                                              invisibleTextureList[i].Height);
            }

            //breakable tiles
            for (int i = 0; i < breakableRectangleList.Count(); i++)
            {
                if (breakableRectangleList[i].X < 0)
                    breakableRectangleList[i] = new Rectangle(0,
                                                              breakableRectangleList[i].Y,
                                                              breakableTextureList[i].Width,
                                                              breakableTextureList[i].Height);

                if (breakableRectangleList[i].X > this.Size.Width - breakableRectangleList[i].Width)
                    breakableRectangleList[i] = new Rectangle(this.Size.Width - breakableRectangleList[i].Width,
                                                            breakableRectangleList[i].Y,
                                                            breakableTextureList[i].Width,
                                                            breakableTextureList[i].Height);

                if (breakableRectangleList[i].Y < 0)
                    breakableRectangleList[i] = new Rectangle(breakableRectangleList[i].X,
                                                     0,
                                                     breakableTextureList[i].Width,
                                                     breakableTextureList[i].Height);

                if (breakableRectangleList[i].Y > this.Size.Height - breakableRectangleList[i].Height)
                    breakableRectangleList[i] = new Rectangle(breakableRectangleList[i].X,
                                                              this.Size.Height - breakableRectangleList[i].Height,
                                                              breakableTextureList[i].Width,
                                                              breakableTextureList[i].Height);
            }

            #endregion
        }

        public int GetObjectCount()
        {
            return rectangleList.Count() +
                   enemyRectangleList.Count() +
                   powerupRectangleList.Count() +
                   startgoalRectangleList.Count() +
                   invisibleRectangleList.Count() +
                   breakableRectangleList.Count();
        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
           
            spriteBatch.Begin();

            for (int i = 0; i < textureList.Count(); i++)
                spriteBatch.Draw(textureList[i], rectangleList[i], Color.White);

            for (int i = 0; i < enemyTextureList.Count(); i++)
                spriteBatch.Draw(enemyTextureList[i], enemyRectangleList[i], Color.White);

            for (int i = 0; i < powerupTextureList.Count(); i++)
                spriteBatch.Draw(powerupTextureList[i], powerupRectangleList[i], Color.White);

            for (int i = 0; i < startgoalTextureList.Count(); i++)
                spriteBatch.Draw(startgoalTextureList[i], startgoalRectangleList[i], Color.White);

            for (int i = 0; i < invisibleTextureList.Count(); i++)
                spriteBatch.Draw(invisibleTextureList[i], invisibleRectangleList[i], new Color(255,255,255,150));

            for (int i = 0; i < breakableTextureList.Count(); i++)
                spriteBatch.Draw(breakableTextureList[i], breakableRectangleList[i], Color.Red);

            spriteBatch.End();

            Update();
        }
    }
}
