using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


/*****************************************
** Main Author: Jake Thorne
** Secondary Author: Liam Ord
** Last updated: 12/03/14
** Last updated by: Jake
******************************************/

// TO-DO

namespace PlatformerGame
{
    public class Player : MovingEntity
    {
        public float speed = 5;
        float originalSpeed = 5;

        int maxTemp = 500;
        int minTemp = -500;

        public bool canMove = true;
        bool damaged = false;

        float pushBackTimer = 0;
        float colorChangeTimer = 0;
        bool colorOnOffSwitch = true;

        //local temperature variable for the draw function to use
        float temperatureForDraw;

        public bool stopWatch = false;


        float tempChangeFactor;
        int topOffset = 18;

        //PLAYER FUNCTIONS BEGIN.................................................................
        public Player()
        {
            dimensions = new Vector2(50, 60);
        }

        public override void Load(ContentManager content, GraphicsDevice newGraphics, string playerSelect)
        {
            leftCollision.Load(content, newGraphics, "Player/leftrightCollision");
            rightCollision.Load(content, newGraphics, "Player/leftrightCollision");
            bottomCollision.Load(content, newGraphics, "Player/bottomCollision");
            topCollision.Load(content, newGraphics, "Player/topCollision");

            if (playerSelect == "zelda")
            {
                animationStrings[0] = "Player/princess/rightIdle";
                animationStrings[1] = "Player/princess/leftIdle";
                animationStrings[2] = "Player/princess/rightRun";
                animationStrings[3] = "Player/princess/leftRun";
                animationStrings[4] = "Player/princess/rightJump";
                animationStrings[5] = "Player/princess/leftJump";

                tempChangeFactor = 0.1f;

                speed = 5;
            }
            else if (playerSelect == "zombie")
            {
                animationStrings[0] = "Player/zombie/zombieRightIdle";
                animationStrings[1] = "Player/zombie/zombieLeftIdle";
                animationStrings[2] = "Player/zombie/zombieRightRun";
                animationStrings[3] = "Player/zombie/zombieLeftRun";
                animationStrings[4] = "Player/zombie/zombieRightJump";
                animationStrings[5] = "Player/zombie/zombieLeftJump";

                tempChangeFactor = 0.05f;

                speed = 4;
                originalSpeed = 4;
            }
            else if (playerSelect == "link")
            {
                animationStrings[0] = "Player/link/linkRightIdle";
                animationStrings[1] = "Player/link/linkLeftIdle";
                animationStrings[2] = "Player/link/linkRightRun";
                animationStrings[3] = "Player/link/linkLeftRun";
                animationStrings[4] = "Player/link/linkRightJump";
                animationStrings[5] = "Player/link/linkLeftJump";

                tempChangeFactor = 0.15f;

                speed = 6;
                originalSpeed = 6;
            }

            base.Load(content, newGraphics);
        }

        public void Update(Game1.objects lists,
                           List<Platform> platformList,
                           GameTime time,
                           Temperature temp)
        {
            #region collsion positioning

            //This section keeps the invisible collision for left, right and bottom close to the player
            positionRight = new Vector2(position.X + dimensions.X + (int)velocity.X * 2,
                                        position.Y - 5);

            positionLeft = new Vector2(position.X - 5 + (int)velocity.X * 2,
                                       position.Y - 5);

            positionBottom = new Vector2(position.X + dimensions.X / 3,
                                         position.Y + dimensions.Y - 7);

            positionTop = new Vector2(position.X + dimensions.X / 3 - 5,
                                      rectangle.Top - topOffset + (int)velocity.Y);

            #endregion
            #region key input

            if (canMove)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A) && leftClear)
                {
                    velocity.X = -speed;
                    goingRight = false;
                    goingLeft = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D) && rightClear)
                {
                    velocity.X = speed;
                    goingRight = true;
                    goingLeft = false;
                }
                else
                {
                    //if the velocity isn't 0, we want to decrement it until it
                    //reaches 0
                    if (velocity.X > speed / 8)
                        velocity.X -= speed / 8;
                    else if (velocity.X < -speed / 8)
                        velocity.X += speed / 8;
                    else velocity.X = 0;
                }
            }

            #endregion

            #region updates

            float elapsed = (float)time.ElapsedGameTime.TotalSeconds;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 50, 60);

            //proper jumping animation fix
            for (int i = 0; i < Frames; i++)
            {
                if (i == 4 && animations[4].getFrame() == 3)
                    i = 4;
                else if (i == 5 && animations[5].getFrame() == 3)
                    i = 5;
                else
                    animations[i].UpdateFrame(elapsed);
            }

            base.Update(platformList, time);

            #endregion

            if (canMove)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W) && !hasJumped)
                {
                    position.Y -= speed * 1.5f;
                    velocity.Y = -speed * 2;

                    animations[4].resetFrame();
                    animations[5].resetFrame();

                    hasJumped = true;
                }
            }

            #region player/enemy collision

            pushBackTimer += (float)time.ElapsedGameTime.TotalSeconds;
            colorChangeTimer += (float)time.ElapsedGameTime.TotalSeconds;

            foreach (Enemy e in lists.enemyManager.enemyList)
            {
                if (b_PixelCollision(getRect(), getColor(),
                                     e.getRect(), e.getColor()) && canMove)
                {
                    if (getPosition().X < e.getPosition().X)
                        SetVelocity(new Vector2(-5, 0));
                    else
                        SetVelocity(new Vector2(5, 0));

                    if (temp.GetTemperature() <= 0)
                        temp.DropTemperature(tempChangeFactor * 500);
                    else
                        temp.RaiseTemperature(tempChangeFactor * 500);

                    canMove = false;
                    damaged = true;

                    pushBackTimer = 0;
                    colorChangeTimer = 0;

                    break;
                }
            }

            if (!canMove && pushBackTimer > 0.7f)
            {
                canMove = true;
                damaged = false;
            }
            foreach (Platform plat in platformList)
            {
                if (b_PixelCollision(rightCollision.getRect(), rightCollision.getColor(), plat.rectangle, plat.textureData))
                {
                    if (!canMove)
                        velocity.X = 0;
                }
            }

            #endregion
            #region player temperature logic

            //if rain comes into contact with the player, decrease temperature
            foreach (RainGenerator rainGen in lists.rainList)
                foreach (Rain r in rainGen.rain)
                {
                    if (rectangle.Contains(new Point((int)r.Position.X, (int)r.Position.Y)) &&
                        r.Position.X > rectangle.X + rectangle.Width * 0.33f &&
                        r.Position.X < rectangle.X + rectangle.Width * 0.66f)
                    {
                        if (temp.GetTemperature() > -Temperature.tempLimit && !stopWatch)
                            temp.DropTemperature(tempChangeFactor);
                    }
                }

            if (temp.GetTemperature() >= 400)
            {
                if (speed < 6)
                    speed += tempChangeFactor / 10;
            }
            else if (temp.GetTemperature() <= -400)
            {
                //the colder the player is, the slower they will move after dropping below
                //-400 temperature
                speed = originalSpeed * Math.Abs(-minTemp - -temp.GetTemperature()) / 100;

                //lower the idle limit so that when the character is really slow then the
                //running animations will still go off
                idleLimit = new Vector2(0.1f, 0.1f);
            }
            else speed = originalSpeed;

            //fire logic
            foreach (FireGenerator fireGen in lists.fireList)
            {
                foreach (Fire f in fireGen.fire)
                {
                    if (rectangle.Contains(new Point((int)f.Position.X, (int)f.Position.Y)))
                    {
                        if (temp.GetTemperature() > -Temperature.tempLimit)
                            temp.RaiseTemperature(tempChangeFactor / 10);
                    }
                }

                Vector2 distanceFrom
                    = new Vector2(Math.Abs(position.X + rectangle.Width / 2 - fireGen.spawnPos.X),
                                  Math.Abs(position.Y + rectangle.Height / 2 - fireGen.spawnPos.Y));

                if (distanceFrom.X < 60 && distanceFrom.Y < 150)
                {
                    float increase = 2 / distanceFrom.X +
                                     2 / distanceFrom.Y;

                    if (increase > tempChangeFactor * 5)
                        increase = tempChangeFactor * 5;

                    temp.RaiseTemperature(increase);
                }
            }

            #endregion

            //move the main rain system along with us
            lists.rainList[0].horizontalEmission = (int)position.X - 700;
            //to see what the current temperature is
            temperatureForDraw = temp.GetTemperature();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (damaged)
            {
                Color tempColor;

                if (temperatureForDraw <= 0)
                    tempColor = Color.Aqua;
                else
                    tempColor = Color.Red;

                if (colorChangeTimer > 0.1f)
                {
                    colorOnOffSwitch = !colorOnOffSwitch;
                    colorChangeTimer = 0;
                }

                if (colorOnOffSwitch)
                    base.Draw(spriteBatch, tempColor);
                else
                    base.Draw(spriteBatch, Color.White);
            }
            else base.Draw(spriteBatch, Color.White);
        }

        public void SetVelocity(Vector2 newVel)
        {
            velocity = newVel;
        }

        public Vector2 GetVelocity()
        {
            return velocity;
        }
    }
}
