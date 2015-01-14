using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PlatformerGame
{
    public class MovingEntity : GameObject
    {
        public Vector2 positionRight;
        public Vector2 positionBottom;
        public Vector2 positionLeft;
        public Vector2 positionTop;

        public GameObject rightCollision = new GameObject();
        public GameObject leftCollision = new GameObject();
        public GameObject bottomCollision = new GameObject();
        public GameObject topCollision = new GameObject();

        #region animation variables

        //in order: rightIdle, leftIdle
        //          rightRun, leftRun
        //          rightJump, leftJump
        public AnimatedTexture[] animations = new AnimatedTexture[6];
        public string[] animationStrings = new string[6];
        public Vector2 dimensions;

        public bool goingLeft = false;
        public bool goingRight = true;

        const float Rotation = 0;
        const float Scale = 1;
        const float Depth = 1;
        public const int Frames = 6;
        const int FPS = 8;
        const int idleFPS = 6;

        //stops the game from displaying the running animation when
        //on a steep ledge
        public Vector2 idleLimit = new Vector2(0.4f, 0.4f);

        #endregion

        //to tell if the player can move left and right
        public bool leftClear = true;
        public bool rightClear = true;

        public Vector2 velocity;
        public bool gravityEnabled = true;
        public bool hasJumped = true;

        public float terrainAlignment = 3.7f;


        //MOVING ENTITY FUNCTIONS BEGIN.................................................................
        public MovingEntity()
        {

        }

        public virtual void Load(ContentManager content, GraphicsDevice newGraphics)
        {
            for (int i = 0; i < Frames; i++)
            {
                animations[i] = new AnimatedTexture(Vector2.Zero,
                    Rotation, Scale, Depth);

                if (i == 0 || i == 1)
                    animations[i].Load(content, animationStrings[i], Frames, idleFPS);
                else
                    animations[i].Load(content, animationStrings[i], Frames, FPS);
            }

            texture = content.Load<Texture2D>("Player/collision");

            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
        }

        public override void Update(List<Platform> platformList, GameTime time)
        {
            position += velocity;

            #region collsion positioning

            leftCollision.setPosition(positionLeft);
            rightCollision.setPosition(positionRight);
            bottomCollision.setPosition(positionBottom);
            topCollision.setPosition(positionTop);

            leftCollision.Update();
            rightCollision.Update();
            bottomCollision.Update();
            topCollision.Update();

            #endregion

            #region platform collision checking/gravity/jumping

            foreach (Platform plat in platformList)
            {
                if (!plat.broken)
                {
                    //check where the collision is happening on the y axis and position the player there
                    //also only do this if the bottom collider is on the floor - this means that the entity won't be pushed up
                    //if they jump up against another platform
                    if (yAxis_PixelCollision(bottomCollision.getRect(),
                                             bottomCollision.getColor(),
                                             plat.rectangle,
                                             plat.textureData) != 0)
                    {
                        float tempY = yAxis_PixelCollision(bottomCollision.getRect(),
                                                           bottomCollision.getColor(),
                                                           plat.rectangle,
                                                           plat.textureData);

                        position.Y = tempY - dimensions.Y + terrainAlignment;

                        //make the terrain visible if it was previously invisible
                        if (plat.invisible)
                            plat.invisible = false;
                        //break the terrain if it's breakable
                        if (plat.breakable)
                            plat.broken = true;

                        velocity.Y = 0;
                        hasJumped = false;
                    }

                    //Activate gravity if the entity isn't on the ground
                    if (!b_PixelCollision(bottomCollision.getRect(), bottomCollision.getColor(), plat.rectangle, plat.textureData))
                        gravityEnabled = true;
                    else
                        gravityEnabled = false;

                    //top collision - only push down if the top collider has triggered and the entity is jumping
                    // the latter part as before the entity would be pushed down continuously
                    if (b_PixelCollision(topCollision.getRect(), topCollision.getColor(), plat.rectangle, plat.textureData) && hasJumped)
                    {
                        velocity.Y = 1;

                        //make the terrain visible if it was previously invisible
                        if (plat.invisible)
                            plat.invisible = false;
                        //break the terrain if it's breakable
                        if (plat.breakable)
                            plat.broken = true;
                    }

                    //right and left collision logic to stop the entity from continuing to move if they
                    //have hit an obstacle
                    if (b_PixelCollision(rightCollision.getRect(), rightCollision.getColor(), plat.rectangle, plat.textureData))
                    {
                        rightClear = false;

                        //make the terrain visible if it was previously invisible
                        if (plat.invisible)
                            plat.invisible = false;
                        //break the terrain if it's breakable
                        if (plat.breakable)
                            plat.broken = true;

                        if (hasJumped)
                            velocity.X = 0;
                    }
                    else
                    {
                        //check right collision against all platforms (was having a problem where
                        //rightClear would default to true because one platform isn't colliding with
                        //the collider
                        bool allClear = true;

                        for (int i = 0; i < platformList.Count(); i++)
                        {
                            if (b_PixelCollision(rightCollision.getRect(), rightCollision.getColor(),
                                                 platformList[i].rectangle, platformList[i].textureData))
                            {
                                allClear = false;
                                break;
                            }
                        }

                        if (allClear)
                            rightClear = true;
                    }

                    if (b_PixelCollision(leftCollision.getRect(), leftCollision.getColor(), plat.rectangle, plat.textureData))
                    {
                        leftClear = false;

                        //make the terrain visible if it was previously invisible
                        if (plat.invisible)
                            plat.invisible = false;
                        //break the terrain if it's breakable
                        if (plat.breakable)
                            plat.broken = true;

                        if (hasJumped)
                            velocity.X = 0;
                    }
                    else
                    {
                        //check right collision against all platforms (was having a problem where
                        //rightClear would default to true because one platform isn't colliding with
                        //the collider
                        bool allClear = true;

                        for (int i = 0; i < platformList.Count(); i++)
                        {
                            if (b_PixelCollision(leftCollision.getRect(), leftCollision.getColor(),
                                                 platformList[i].rectangle, platformList[i].textureData))
                            {
                                allClear = false;
                                break;
                            }
                        }

                        if (allClear)
                            leftClear = true;
                    }
                }
            }

            if (gravityEnabled)
                velocity.Y += 0.15f * 2f;

            #endregion
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            //idle animations
            if (velocity.X < idleLimit.X && velocity.Y < idleLimit.Y &&
                velocity.X > -idleLimit.X && velocity.Y > -idleLimit.Y &&
                !hasJumped)
            {
                if (goingRight && !goingLeft)
                    animations[0].DrawFrame(spriteBatch, position, color);
                if (!goingRight && goingLeft)
                    animations[1].DrawFrame(spriteBatch, position, color);
            }
            //running animations
            else if (!hasJumped)
            {
                if (goingRight && !goingLeft)
                    animations[2].DrawFrame(spriteBatch, position, color);
                else if (!goingRight && goingLeft)
                    animations[3].DrawFrame(spriteBatch, position, color);
            }
            //jumping animations
            else if (hasJumped)
            {
                if (goingRight && !goingLeft)
                    animations[4].DrawFrame(spriteBatch, position, color);
                else if (!goingRight && goingLeft)
                    animations[5].DrawFrame(spriteBatch, position, color);
            }
        }
    }
}
