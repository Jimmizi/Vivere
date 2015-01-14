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
    public class Enemy : MovingEntity
    {
        float minMovement = 50;
        float maxMovement = 500;

        int minWait = 1;
        int maxWait = 4;

        //patrol variables
        float moveLeft, moveRight;
        bool movingLeft, movingRight;

        float speed = 2;
        float wait;

        float hitPlayerStopTimer = 0;

        public bool stationary = false;

        //PLAYER FUNCTIONS BEGIN.................................................................
        public Enemy()
        {
            dimensions = new Vector2(50, 60);

            //randomise if the enemy starts off going left or right
            Random rand = new Random();
            switch (rand.Next(1, 2))
            {
                case 1:
                    movingLeft = true;
                    movingRight = false;

                    break;
                case 2:
                    movingLeft = false;
                    movingRight = true;

                    break;
            }
        }

        public override void Load(ContentManager content, GraphicsDevice newGraphics, string enemyType)
        {
            leftCollision.Load(content, newGraphics, "Player/leftrightCollision");
            rightCollision.Load(content, newGraphics, "Player/leftrightCollision");
            bottomCollision.Load(content, newGraphics, "Player/bottomCollision");

            if (enemyType == "zombie")
            {
                animationStrings[0] = "Enemy/zombie/zombieRightIdle";
                animationStrings[1] = "Enemy/zombie/zombieLeftIdle";
                animationStrings[2] = "Enemy/zombie/zombieRightRun";
                animationStrings[3] = "Enemy/zombie/zombieLeftRun";
                animationStrings[4] = "Enemy/zombie/zombieRightJump";
                animationStrings[5] = "Enemy/zombie/zombieLeftJump";
            }
            else if (enemyType == "scissors")
            {
                animationStrings[0] = "Enemy/scissors/scissors";
                animationStrings[1] = "Enemy/scissors/scissors";
                animationStrings[2] = "Enemy/scissors/scissors";
                animationStrings[3] = "Enemy/scissors/scissors";
                animationStrings[4] = "Enemy/scissors/scissors";
                animationStrings[5] = "Enemy/scissors/scissors";
                stationary = true;
            }

            base.Load(content, newGraphics);
        }
        public override void Update(List<Platform> platformList, GameTime time)
        {
            
            #region collision positioning

            positionBottom = new Vector2(position.X + dimensions.X / 3,
                                            position.Y + dimensions.Y - 7);

            positionRight = new Vector2(position.X + dimensions.X,
                                        position.Y - 5);

            positionLeft = new Vector2(position.X - 5,
                                        position.Y - 5);

            bottomCollision.setPosition(positionBottom);
            bottomCollision.Update();

            leftCollision.setPosition(positionLeft);
            rightCollision.setPosition(positionRight);

            leftCollision.Update();
            rightCollision.Update();

            #endregion

            //for our enemy, we only want to perform a few checks as they don't need as 
            //many as the player
            if (!stationary)
            {
                foreach (Platform plat in platformList)
                {
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

                        velocity.Y = 0;
                        hasJumped = false;
                    }

                    if (!b_PixelCollision(bottomCollision.getRect(), bottomCollision.getColor(), plat.rectangle, plat.textureData))
                        gravityEnabled = true;
                    else
                        gravityEnabled = false;
                }

                if (gravityEnabled)
                    velocity.Y += 0.15f * 2f;
            }

            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 50, 60);
            

            float elapsed = (float)time.ElapsedGameTime.TotalSeconds;

            //update the animations
            for (int i = 0; i < 6; i++)
                animations[i].UpdateFrame(elapsed);
        }

        public void Move(List<Platform> platformList, GameTime time, Player p)
        {
            //if the enemy isn't waiting between movement
            if (wait < 0)
            {
                #region collide with platform stop

                foreach (Platform plat in platformList)
                {
                    if (b_PixelCollision(leftCollision.getRect(), leftCollision.getColor(), plat.rectangle, plat.textureData) && movingLeft)
                    {
                        movingLeft = false;
                        movingRight = true;

                        randomRight();

                        Random rand = new Random();
                        wait = rand.Next(minWait, maxWait);

                        return;
                    }

                    if (b_PixelCollision(rightCollision.getRect(), rightCollision.getColor(), plat.rectangle, plat.textureData) && movingRight)
                    {
                        movingLeft = true;
                        movingRight = false;

                        randomLeft();

                        Random rand = new Random();
                        wait = rand.Next(minWait, maxWait);

                        return;
                    }
                }

                #endregion
                #region collide with player stop

                if (b_PixelCollision(leftCollision.getRect(), leftCollision.getColor(), p.rectangle, p.textureData) && movingLeft)
                {
                    hitPlayerStopTimer += (float)time.ElapsedGameTime.TotalSeconds;

                    if (hitPlayerStopTimer > 0.2f)
                    {
                        movingLeft = false;
                        movingRight = true;

                        randomRight();

                        Random rand = new Random();
                        wait = rand.Next(minWait, maxWait);

                        hitPlayerStopTimer = 0;

                        return;
                    }
                }

                if (b_PixelCollision(rightCollision.getRect(), rightCollision.getColor(), p.rectangle, p.textureData) && movingRight)
                {
                    hitPlayerStopTimer += (float)time.ElapsedGameTime.TotalSeconds;

                    if (hitPlayerStopTimer > 0.2f)
                    {
                        movingLeft = true;
                        movingRight = false;

                        randomLeft();

                        Random rand = new Random();
                        wait = rand.Next(minWait, maxWait);

                        hitPlayerStopTimer = 0;

                        return;
                    }
                }

                #endregion
                #region patrol movement stop
                //if the enemy gets to the end of their left or right patrol
                //amount then we want to stop them

                //left movement
                if (movingLeft && moveLeft > 0)
                {
                    velocity.X = -speed;
                    moveLeft -= speed;

                    //running animation
                    goingLeft = true;
                    goingRight = false;
                }
                else if (movingLeft && moveLeft <= 0)
                {
                    movingLeft = false;
                    movingRight = true;

                    randomRight();

                    Random rand = new Random();
                    wait = rand.Next(minWait, maxWait);

                    return;
                }

                //right movement
                if (movingRight && moveRight > 0)
                {
                    velocity.X = speed;
                    moveRight -= speed;

                    //running animation
                    goingRight = true;
                    goingLeft = false;
                }
                else if (movingRight && moveRight <= 0)
                {
                    movingLeft = true;
                    movingRight = false;

                    randomLeft();

                    Random rand = new Random();
                    wait = rand.Next(minWait, maxWait);
                    return;
                }

                #endregion
                #region end of platform stop

                //we want to stop the enemy before the end or beginning of a platform so
                //we don't have enemies walking off the edges
                foreach (Platform plat in platformList)
                {
                    if (b_PixelCollision(bottomCollision.getRect(), bottomCollision.getColor(), plat.rectangle, plat.textureData) && movingLeft)
                    {
                        if (position.X < plat.GetLeftX())
                        {
                            movingLeft = false;
                            movingRight = true;

                            randomRight();

                            Random rand = new Random();
                            wait = rand.Next(minWait, maxWait);

                            return;
                        }
                    }

                    if (b_PixelCollision(bottomCollision.getRect(), bottomCollision.getColor(), plat.rectangle, plat.textureData) && movingRight)
                    {
                        if (position.X + dimensions.X > plat.GetRightX())
                        {
                            movingLeft = true;
                            movingRight = false;

                            randomLeft();

                            Random rand = new Random();
                            wait = rand.Next(minWait, maxWait);

                            return;
                        }
                    }
                }

                #endregion
            }
            else
            {
                wait -= (float)time.ElapsedGameTime.TotalSeconds;
                velocity.X = 0;
            }
        }

        public void randomLeft()
        {
            Random rand = new Random();
            moveLeft = rand.Next((int)minMovement, (int)maxMovement);
        }
        public void randomRight()
        {
            Random rand = new Random();
            moveRight = rand.Next((int)minMovement, (int)maxMovement);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch, Color.White);
        }
    }
}
