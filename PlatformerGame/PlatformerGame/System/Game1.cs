using System;
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
** Last updated: 6/04/14
** Last updated by: Julian
******************************************/

namespace PlatformerGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        #region variables

        #region sound variables
        AudioEngine audioEngine;
        WaveBank waveBank;
        SoundBank soundBank;
        Cue currentSong;
        bool firstCollision = true;

        ClockTimer menuTimer = new ClockTimer();
        ClockTimer videoTimer = new ClockTimer();
        ClockTimer optionsTimer = new ClockTimer();
        ClockTimer gameOverTimer = new ClockTimer();
        ClockTimer doorTimer = new ClockTimer();
       
        SoundEffect jumpEffect;
        SoundEffectInstance jumpEffectInstance;

        #endregion

        int score = 0;
        SpriteFont font;
        
       
        Temperature temperature;

        public struct objects
        {
            public EnemyManager enemyManager;
            public List<ExitDoor> exitDoorList;
            public List<Powerups> powerupList;
            public List<FireGenerator> fireList;
            public List<RainGenerator> rainList;
        }
        public struct Data
        {
            public KeyboardState kState;
            public KeyboardState oldKeyState;
            public MouseState mState;
            public float dt;

            public GraphicsDeviceManager graphics;

            public SpriteBatch spriteBatch;
        }

        Player player;
        Camera camera;
        Map map;
        Video video;
        VideoPlayer videoPlayer;

        enum GameState { MENU, VIDEO, GAME_OVER, OPTIONS, GAME, LEVEL_SELECT, CHAR_SELECT, PAUSE, CONFIRM_EXIT };
        GameState currentGameState = GameState.MENU;

        Data data = new Data();
        objects lists = new objects();

        Texture2D texture1;
        Rectangle rectangle1;
        Rectangle rectangle2;

        Texture2D texture2;
        Texture2D texture3;

		

        #region MENU VARIABLES

        GameObject mainMenuBG, optionsBG, characterSelectBG, menuTitle;
        Vector2 menuCamPos = new Vector2(800, -60);
        bool scrolling = false;
        int currentSelection = 1;
        int menuScrollSpeed = 75;

        #region main menu variables start

        string play = "Play Game";
        string options = "Options";
        string exit = "Exit";

        Vector2 position_MM = new Vector2(820, -60);

        Texture2D menuBackground;

        Vector2 menuPlayPos = new Vector2(10.0f, 20.0f);
        Vector2 menuOptionsPos = new Vector2(10.0f, 40.0f);
        Vector2 menuExitPos = new Vector2(10.0f, 60.0f);

        Color menuPlayColor = Color.Black;
        Color menuOptionsColor = Color.Black;
        Color menuExitColor = Color.Black;
        
        #endregion

        #region options menu variables start
        
        string fullscreen = "Toggle Fullscreen";
        string mainMenu = "Back to Main Menu";

        Vector2 position_O = new Vector2(-1660, 1140);

        GameObject optionsBackground;

        Vector2 optionsFullScreenPos = new Vector2(10.0f, 20.0f);
        Vector2 optionsMainMenuPos = new Vector2(10.0f, 40.0f);

        Color optionsFullScreenColor = Color.Black;
        Color optionsMainMenuColor = Color.Black;
        
        #endregion

        #region pause menu variables start

        string resume = "Paused";

        Vector2 resumePos = new Vector2(10.0f, 20.0f);

        Color resumeColor = Color.White;

        Vector2 resumeOrigin;
        Rectangle overlay;

        Texture2D overlayTexture;

        #endregion

        #region exit confirm variables start
        //EXIT CONFIRM VARIABLES START -----------------------
        string exitYes = "Yes";
        string exitNo = "No, back to menu.";

        GameObject exitBackground;

        Vector2 exitYesPos = new Vector2(20.0f, 40.0f);
        Vector2 exitNoPos = new Vector2(20.0f, 50.0f);

        Color exitYesColor = Color.White;
        Color exitNoColor = Color.White;
        //EXIT CONFIRM VARIABLES END -------------------------
        #endregion

        #region game over variables start
        
        string gameOverYes = "Return to Menu";
        string gameOverNo = "Restart Level";

        GameObject gameOverbackground;

        Vector2 gameOverYesPos = new Vector2(20.0f, 40.0f);
        Vector2 gameOverNoPos = new Vector2(20.0f, 60.0f);

        Color gameOverYesColor = Color.White;
        Color gameOverNoColor = Color.White;
        
        #endregion

        #region character select menu start
        
        string characterOne = "Select Zelda";
        string characterTwo = "Select Zombie";
        string characterThree = "Select Link";
        string charBack = "Back";

        Vector2 position_CS = new Vector2(2980,660);
        string playerSelected = "zelda";

        Vector2 charOnePos;
        Vector2 charTwoPos;
        Vector2 charThreePos;
        Vector2 charBackPos;

        Color charOneColor = Color.Black;
        Color charTwoColor = Color.Black;
        Color charThreeColor = Color.Black;
        Color charBackColor = Color.Black;
        
        #endregion

        #region level selection variables start

        string levelSelect1 = "Level 1";
        string levelSelect2 = "Level 2";
        string levelSelect3 = "Level 3";
        string levelSelect4 = "Level 1 (Hardcore)";
        string levelSelect5 = "Level 2 (Hardcore)";
        string levelSelect6 = "Level 3 (Hardcore)";
        string levelSelect7 = "Level 'Troll'";
        string levelBack = "Back";

        GameObject selectLevelBackground;

        Vector2 levelSelect1Pos = new Vector2(10.0f, 40.0f);
        Vector2 levelSelect2Pos = new Vector2(10.0f, 60.0f);
        Vector2 levelSelect3Pos = new Vector2(10.0f, 40.0f);
        Vector2 levelSelect4Pos = new Vector2(10.0f, 60.0f);
        Vector2 levelSelect5Pos = new Vector2(10.0f, 80.0f);
        Vector2 levelSelect6Pos = new Vector2(10.0f, 100.0f);
        Vector2 levelSelect7Pos = new Vector2(10.0f, 120.0f);
        Vector2 levelBackPos;

        Color levelSelect1Color = Color.Black;
        Color levelSelect2Color = Color.Black;
        Color levelSelect3Color = Color.Black;
        Color levelSelect4Color = Color.Black;
        Color levelSelect5Color = Color.Black;
        Color levelSelect6Color = Color.Black;
        Color levelSelect7Color = Color.Black;
        Color levelBackColor = Color.Black;

        int currentMapSelection = 1;

        #endregion

        #endregion

        #endregion

        public Game1()
        {
            data.graphics = new GraphicsDeviceManager(this);

            //Set the screen width and height  
            data.graphics.PreferredBackBufferWidth = 1280;
            data.graphics.PreferredBackBufferHeight = 720;

            //Apply the changes made to the device
            data.graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            //SoundManager.Content = Content;
        }

        protected override void Initialize()
        {
            #region menu background loading

            mainMenuBG = new GameObject();
            mainMenuBG.Load(Content, GraphicsDevice, "Menu/book2");
            mainMenuBG.setPosition(530, 20);
            mainMenuBG.Update();

            menuTitle = new GameObject();
            menuTitle.Load(Content, GraphicsDevice, "Menu/vivere");
            menuTitle.setPosition(20, 50);
            menuTitle.Update();

            optionsBG = new GameObject();
            optionsBG.Load(Content, GraphicsDevice, "Menu/book");
            optionsBG.setPosition(-830, 600);
            optionsBG.Update();

            characterSelectBG = new GameObject();
            characterSelectBG.Load(Content, GraphicsDevice, "Menu/book3");
            characterSelectBG.setPosition(1500, 400);
            characterSelectBG.Update();

            //gameOverbackground = new GameObject();
            //gameOverbackground.Load(Content, GraphicsDevice, "Background/bggameover");
            //gameOverbackground.Update();

            //exitBackground = new GameObject();
            //exitBackground.Load(Content, GraphicsDevice, "Background/bgexit");
            //exitBackground.Update();

            //selectPlayerBackground = new GameObject();
            //selectPlayerBackground.Load(Content, GraphicsDevice, "Background/SelectPlayer");
            //selectPlayerBackground.Update();

            //selectLevelBackground = new GameObject();
            //selectLevelBackground.Load(Content, GraphicsDevice, "Background/SelectLevel");
            //selectLevelBackground.Update();

            //optionsBackground = new GameObject();
            //optionsBackground.Load(Content, GraphicsDevice, "Background/optionsBackground");
            //optionsBackground.Update();


            #endregion

            #region menu text positioning

            Vector2 offset_MM = new Vector2(400, 0);

            menuPlayPos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2 + offset_MM.X,
               data.graphics.GraphicsDevice.Viewport.Height / 2 + offset_MM.Y);

            menuOptionsPos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2 + offset_MM.X,
                data.graphics.GraphicsDevice.Viewport.Height / 2 + 30 + offset_MM.Y);

            menuExitPos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2 + offset_MM.X,
                data.graphics.GraphicsDevice.Viewport.Height / 2 + 60 + offset_MM.Y);

            exitYesPos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2,
                data.graphics.GraphicsDevice.Viewport.Height / 2);

            exitNoPos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2,
                data.graphics.GraphicsDevice.Viewport.Height / 2 + 60);

            #endregion

            #region options text positioning

            Vector2 offset_O = new Vector2(-1250,500);

            optionsFullScreenPos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2 + offset_O.X,
               data.graphics.GraphicsDevice.Viewport.Height / 2 + offset_O.Y);

            optionsMainMenuPos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2 + offset_O.X,
             data.graphics.GraphicsDevice.Viewport.Height / 2 + 30 + offset_O.Y);

            #endregion

            #region pause text positioning and variables

            resumePos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2,
               data.graphics.GraphicsDevice.Viewport.Height / 2);

            overlayTexture = Content.Load<Texture2D>("UI/hot");

            #endregion

            #region character select text positioning

            Vector2 offset_CS = new Vector2(1100, 300);

            charOnePos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2 + offset_CS.X,
               data.graphics.GraphicsDevice.Viewport.Height / 2 + offset_CS.Y);

            charTwoPos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2 + offset_CS.X,
               data.graphics.GraphicsDevice.Viewport.Height / 2 + 30 + offset_CS.Y);

            charThreePos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2+ offset_CS.X,
               data.graphics.GraphicsDevice.Viewport.Height / 2 + 60 + offset_CS.Y);

            charBackPos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2 + offset_CS.X,
               data.graphics.GraphicsDevice.Viewport.Height / 2 + 90 + offset_CS.Y);

            #endregion 

            #region level select text positioning

            Vector2 offset_LS = new Vector2(1450, 300);

            levelSelect1Pos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2 + offset_LS.X,
               data.graphics.GraphicsDevice.Viewport.Height / 2 + offset_LS.Y);

            levelSelect2Pos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2 + offset_LS.X - 2,
               data.graphics.GraphicsDevice.Viewport.Height / 2 + 30 + offset_LS.Y);

            levelSelect3Pos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2 + offset_LS.X - 4,
               data.graphics.GraphicsDevice.Viewport.Height / 2 + 60 + offset_LS.Y);

            levelSelect4Pos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2 + offset_LS.X - 6,
               data.graphics.GraphicsDevice.Viewport.Height / 2 + 90 + offset_LS.Y);

            levelSelect5Pos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2 + offset_LS.X - 7,
               data.graphics.GraphicsDevice.Viewport.Height / 2 +  120 + offset_LS.Y);

            levelSelect6Pos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2 + offset_LS.X - 10,
               data.graphics.GraphicsDevice.Viewport.Height / 2 + 150 + offset_LS.Y);

            levelSelect7Pos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2 + offset_LS.X - 12,
               data.graphics.GraphicsDevice.Viewport.Height / 2 + 180 + offset_LS.Y);

            levelBackPos = new Vector2(data.graphics.GraphicsDevice.Viewport.Width / 2 + offset_LS.X - 14,
               data.graphics.GraphicsDevice.Viewport.Height / 2 + 210 + offset_LS.Y);


            #endregion 


            lists.powerupList = new List<Powerups>();
            lists.fireList = new List<FireGenerator>();
            lists.rainList = new List<RainGenerator>();
            lists.exitDoorList = new List<ExitDoor>();

            camera = new Camera(GraphicsDevice.Viewport);

            RainGenerator rain = new RainGenerator(Content.Load<Texture2D>("Misc/rain"),
                                         GraphicsDevice.Viewport.Width + 300,
                                         175);

            lists.rainList.Add(rain);

            
            //ResetGame();

            base.Initialize();
        }

        //function that is called to reset all game elements to the default values
        //when the game is reset
        public void ResetGame()
        {
            currentGameState = GameState.MENU;

            rectangle1 = new Rectangle(-1920,-600, 1920, 2000);
            rectangle2 = new Rectangle(0, -600, 1920, 2000);

            score = 0;


            //clear the lists of any objects added to them
            lists.powerupList.Clear();
            lists.exitDoorList.Clear();

            map = new Map(currentMapSelection, Content, GraphicsDevice);

            temperature = new Temperature(Content);
            temperature.SetTemperature(0);
             
            player = new Player();

            player.Load(Content, GraphicsDevice, playerSelected);
            player.setPosition(map.startPosition);

            lists.enemyManager = new EnemyManager();
            lists.enemyManager.Load(Content, GraphicsDevice, map);

            //powerup load in
            for (int i = 0; i < map.powerupPositions.Count(); i++)
            {
                string toLoad = map.powerupTextures[i].
                    Substring(9, map.powerupTextures[i].Length - 9);

                Powerups powerup = new Powerups(toLoad);
                powerup.Load(Content, GraphicsDevice);
                powerup.setPosition(map.powerupPositions[i]);
                lists.powerupList.Add(powerup);
            }

            ExitDoor exitdoor1 = new ExitDoor("door");
            exitdoor1.Load(Content, GraphicsDevice);
            exitdoor1.setPosition(map.goalPosition);
            lists.exitDoorList.Add(exitdoor1);

            //soundManager.Initialize();
            // soundManager.Play();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            data.spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("Font/Times New Roman");

            //sound stuff
            audioEngine = new AudioEngine(Content.RootDirectory + "//XACT for PlatformGameV2.xgs");
            waveBank = new WaveBank(audioEngine, Content.RootDirectory + "//Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, Content.RootDirectory + "//Sound Bank.xsb");
            currentSong = soundBank.GetCue("Menu");
            currentSong.Play();
            SoundManager.musicVolume = 1.0f;

            jumpEffect = Content.Load<SoundEffect>("Audio/EventMusic/jumping_teon");
            jumpEffectInstance = jumpEffect.CreateInstance();
            jumpEffectInstance.Volume = 0.1f;


            //loading in the video file used for the attract screen and creating
            //new videoPlayer item
            video = Content.Load<Video>("Video//video");
            videoPlayer = new VideoPlayer();

            texture1 = this.Content.Load<Texture2D>("Background/level1Background");
            texture2 = this.Content.Load<Texture2D>("Background/level2Background");
            texture3 = this.Content.Load<Texture2D>("Background/level3Background");


            //ResetGame();
        }
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            #region updates

            data.kState = Keyboard.GetState();
            data.dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            data.mState = Mouse.GetState();

            #endregion

            #region volume/music controls

            if (data.kState.IsKeyDown(Keys.Up))
            {
                SoundManager.musicVolume += 0.01f;

                if (SoundManager.musicVolume > 1.0f)
                    SoundManager.musicVolume = 1.0f;

                audioEngine.GetCategory("Music").SetVolume(SoundManager.musicVolume);
            }

            if (data.kState.IsKeyDown(Keys.Down))
            {
                SoundManager.musicVolume -= 0.01f;

                if (SoundManager.musicVolume < 0)
                    SoundManager.musicVolume = 0;

                audioEngine.GetCategory("Music").SetVolume(SoundManager.musicVolume);
            }

            //pause song (basically a mute toggle)
            if (data.kState.IsKeyDown(Keys.M) && data.oldKeyState.IsKeyUp(Keys.M))
            {
                if (currentSong.IsPaused)
                    currentSong.Resume();
                else if (currentSong.IsPlaying)
                    currentSong.Pause();
                else
                {
                    currentSong = soundBank.GetCue(currentSong.Name);
                    currentSong.Play();
                }
            }

            //next song
            if (data.kState.IsKeyDown(Keys.N) && data.oldKeyState.IsKeyUp(Keys.N))
            {
                currentSong.Stop(AudioStopOptions.Immediate);

                if (currentSong.Name == "Menu")
                    currentSong = soundBank.GetCue("Level1");
                else
                    currentSong = soundBank.GetCue("Menu");

                currentSong.Play();
            }

            #endregion

            if (currentGameState == GameState.MENU || currentGameState == GameState.OPTIONS ||
               currentGameState == GameState.CHAR_SELECT || currentGameState == GameState.LEVEL_SELECT)
            {
                camera.Update(gameTime, menuCamPos);
            }

            switch (currentGameState)
            {
                case GameState.MENU:

                    #region attract mode controls and video playing implementation


                    //setting the timer for the attract state
                    //if no keys have been pressed then start the timer for 30 seconds
                    //during this time checking the time in the timer class
                    //if the timer is finished then go the VIDEO state
                    if (currentGameState == GameState.MENU)
                    {
                        if (Keyboard.GetState().GetPressedKeys().Length == 0)
                        {
                            if (menuTimer.isRunning == false)
                            {
                                menuTimer.start(30);
                            }
                            else
                            {
                                menuTimer.checkTime(gameTime);
                            }

                            if (menuTimer.isFinished == true)
                            {
                                currentGameState = GameState.VIDEO;
                            }
                        }
                    }
                 
                    #endregion 

                    #region menu controls

                    if (!scrolling)
                    {
                        if (data.kState.IsKeyDown(Keys.W) && data.oldKeyState.IsKeyUp(Keys.W))
                        {
                            if (currentSelection > 1)
                                currentSelection--;
                        }
                        else if (data.kState.IsKeyDown(Keys.S) && data.oldKeyState.IsKeyUp(Keys.S))
                        {
                            if (currentSelection < 3)
                                currentSelection++;
                        }
                    }

                    if (data.kState.IsKeyDown(Keys.Escape) && data.oldKeyState.IsKeyUp(Keys.Escape))
                    {
                        this.Exit();
                    }

                    if (data.kState.IsKeyDown(Keys.Space) && data.oldKeyState.IsKeyUp(Keys.Space) || scrolling)
                    {
                        switch (currentSelection)
                        {
                            case 1:

                                float f_angle = (float)Math.Atan2((position_CS.Y - menuCamPos.Y),
                                                                   position_CS.X - menuCamPos.X);

                                Vector2 velocity = new Vector2((float)Math.Cos(f_angle) * 1,
                                                   (float)Math.Sin(f_angle) * 1);

                                if (menuCamPos.X <= position_CS.X)
                                    menuCamPos.X += velocity.X * menuScrollSpeed;

                                if (menuCamPos.Y <= position_CS.Y)
                                    menuCamPos.Y += velocity.Y * menuScrollSpeed;

                                if (menuCamPos.X > position_CS.X && menuCamPos.Y > position_CS.Y)
                                {
                                    scrolling = false;
                                    currentGameState = GameState.CHAR_SELECT;
                                }
                                else
                                    scrolling = true;

                                break;

                            case 2:

                                float f_angle1 = (float)Math.Atan2((position_O.Y - menuCamPos.Y),
                                                                   position_O.X - menuCamPos.X);

                                Vector2 velocity1 = new Vector2((float)Math.Cos(f_angle1) * 1,
                                                   (float)Math.Sin(f_angle1) * 1);

                                if (menuCamPos.X >= position_O.X)
                                    menuCamPos.X += velocity1.X * menuScrollSpeed;

                                if (menuCamPos.Y <= position_O.Y)
                                    menuCamPos.Y += velocity1.Y * menuScrollSpeed;

                                if (menuCamPos.X < position_O.X && menuCamPos.Y > position_O.Y)
                                {
                                    currentSelection = 1;
                                    scrolling = false;
                                    currentGameState = GameState.OPTIONS;
                                }
                                else
                                    scrolling = true;

                                break;
                            case 3:
                                this.Exit();
                                break;
                        }
                    }

                    #endregion
                    #region menu text colour editing

                    switch (currentSelection)
                    {
                        case 1:
                            menuPlayColor = Color.Red;
                            menuOptionsColor = Color.Black;
                            menuExitColor = Color.Black;

                            break;
                        case 2:
                            menuPlayColor = Color.Black;
                            menuOptionsColor = Color.Red;
                            menuExitColor = Color.Black;

                            break;
                        case 3:
                            menuPlayColor = Color.Black;
                            menuOptionsColor = Color.Black;
                            menuExitColor = Color.Red;

                            break;
                    }

                    #endregion
                    break;

                case GameState.VIDEO:

                    #region video playing logic start

                    //when going to the VIDEO state, play the video
                    //if the video is playing, start the videoTimer for duration of video
                    //if the videoTimer is finished, stop the video and reset the videoTimer
                    //then change the current game state to MENU
  
                    videoPlayer.Play(video);

                    if (videoPlayer.State == MediaState.Playing)
                    {
                        if(!videoTimer.isRunning)
                            videoTimer.start(16);
                    }

                    if (videoTimer.checkTime(gameTime))
                    {
                        videoPlayer.Stop();
                        videoTimer.reset();
                       // menuTimer.reset();
                        currentGameState = GameState.MENU;
                        menuTimer.reset();
                    }

                    //ALSO IF
                    //if any buttons on the keyboard are pressed then stop the video
                    //reset the videoTimer and change the current gamestate to MENU

                if (Keyboard.GetState().GetPressedKeys().Length > 0)
                    {
                        videoPlayer.Stop();
                        videoTimer.reset();
                        //menuTimer.reset();
                        currentGameState = GameState.MENU;
                        menuTimer.reset();
                    }

                    #endregion 
                    break;

                case GameState.GAME:
                    
                    if (data.kState.IsKeyDown(Keys.Escape) && data.oldKeyState.IsKeyUp(Keys.Escape))
                    {
                        currentSelection = 1;
                        currentGameState = GameState.CONFIRM_EXIT;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.W) && !player.hasJumped)
                        jumpEffectInstance.Play();

                    #region game updates (player,enemy,camera,powerups,exitdoor)

                    lists.enemyManager.Update(map.platformList, gameTime, player);

                    map.Update();
                    camera.Update(gameTime, player);
                    player.Update(lists, map.platformList, gameTime, temperature);

                    temperature.Update(camera.GetCenter(), gameTime);

                    if(currentMapSelection != 2)
                        foreach (RainGenerator r in lists.rainList)
                            r.Update(gameTime, GraphicsDevice, map.platformList);

                    foreach (FireGenerator f in lists.fireList)
                        f.Update(gameTime, Content, map.platformList);

                    if (data.kState.IsKeyDown(Keys.P) && data.oldKeyState.IsKeyUp(Keys.P))
                    {
                        currentSelection = 1;
                        currentGameState = GameState.PAUSE;
                    }

                    foreach (ExitDoor ed in lists.exitDoorList)
                    {
                        ed.Update();
                        if (ed.Entered(player.getRect(), player.getColor()))
                        {
                            switch (ed.GetDoorType())
                            {
                                case "door":
                                    // firstCollision = true;
                                    player.canMove = false;
                                    player.velocity = Vector2.Zero;
                                    if (firstCollision)
                                    {

                                        //currentSong.Pause();
                                        currentSong.Stop(AudioStopOptions.Immediate);
                                        currentSong = soundBank.GetCue("Victory");
                                        currentSong.Play();
                                        firstCollision = false;
                                    }
                                    if (doorTimer.isRunning == false)
                                    {
                                        doorTimer.start(4);
                                    }
                                    else
                                    {
                                        doorTimer.checkTime(gameTime);
                                    }
                                    if (doorTimer.isFinished == true)
                                    {
                                        currentGameState = GameState.LEVEL_SELECT;
                                        //currentSong.Pause();
                                        currentSong = soundBank.GetCue("Menu");
                                        currentSong.Play();
                                        doorTimer.reset();
                                    }
                                    break;
                            }
                            break;
                        }
                    }
                    foreach (Powerups pu in lists.powerupList)
                    {
                        pu.Update();

                        if (pu.Obtained(player.getRect(), player.getColor()))
                        {
                            switch (pu.GetPowerupType())
                            {
                                case "death":
                                    ResetGame();
                                    currentSelection = 1;
                                    currentGameState = GameState.GAME_OVER;

                                    break;
                                case "blueball":
                                    temperature.DropTemperature(120);
                                    lists.powerupList.Remove(pu);
                                    break;
                                case "bigfreeze":
                                    temperature.DropTemperature(20);
                                    lists.powerupList.Remove(pu);
                                    break;
                                case "redball":
                                    temperature.RaiseTemperature(20);
                                    lists.powerupList.Remove(pu);
                                    break;
                                case "heatwave":
                                    temperature.RaiseTemperature(120);
                                    lists.powerupList.Remove(pu);
                                    break;
                                case "coin":
                                    score++;
                                    lists.powerupList.Remove(pu);
                                    break;
                            }
                            break;
                        }
                    }


                    if (currentMapSelection == 1)
                    {
                        if (data.kState.IsKeyDown(Keys.D))
                        {
                            if (player.GetVelocity().X != 0)
                            {
                                rectangle1.X -= 1;
                                rectangle2.X -= 1;
                            }
                        }



                        if (data.kState.IsKeyDown(Keys.A))
                        {
                            if (player.GetVelocity().X != 0)
                            {
                                rectangle1.X += 1;
                                rectangle2.X += 1;
                            }

                        }

                        // If player is going right
                        if (player.GetVelocity().X > 0)
                        {
                            if ((player.position.X) > rectangle2.X + texture1.Width / 2)
                                rectangle1.X = rectangle2.X + texture1.Width;

                            if ((player.position.X) > rectangle1.X + texture1.Width / 2)
                                rectangle2.X = rectangle1.X + texture1.Width;
                        }

                        // If player is going left
                        if (player.GetVelocity().X < 0)
                        {
                            if ((player.position.X) < rectangle2.X + (texture1.Width / 2))
                                rectangle1.X = rectangle2.X - texture1.Width;
                            // Then repeat this check for rectangle2.

                            if ((player.position.X) < rectangle1.X + texture1.Width / 2)
                                rectangle2.X = rectangle1.X - texture1.Width;
                        }
                    }



                    if (currentMapSelection == 2)
                    {

                        if (data.kState.IsKeyDown(Keys.D))
                        {
                            if (player.GetVelocity().X != 0)
                            {
                                rectangle1.X -= 1;
                                rectangle2.X -= 1;
                            }
                        }



                        if (data.kState.IsKeyDown(Keys.A))
                        {
                            if (player.GetVelocity().X != 0)
                            {
                                rectangle1.X += 1;
                                rectangle2.X += 1;
                            }

                        }

                        // If player is going right
                        if (player.GetVelocity().X > 0)
                        {
                            if ((player.position.X) > rectangle2.X + texture1.Width / 2)
                                rectangle1.X = rectangle2.X + texture1.Width;

                            if ((player.position.X) > rectangle1.X + texture1.Width / 2)
                                rectangle2.X = rectangle1.X + texture1.Width;
                        }

                        // If player is going left
                        if (player.GetVelocity().X < 0)
                        {
                            if ((player.position.X) < rectangle2.X + (texture1.Width / 2))
                                rectangle1.X = rectangle2.X - texture1.Width;
                            // Then repeat this check for rectangle2.

                            if ((player.position.X) < rectangle1.X + texture1.Width / 2)
                                rectangle2.X = rectangle1.X - texture1.Width;
                        }

                    }



                    if (currentMapSelection == 3)
                    {

                        if (data.kState.IsKeyDown(Keys.D))
                        {
                            if (player.GetVelocity().X != 0)
                            {
                                rectangle1.X -= 1;
                                rectangle2.X -= 1;
                            }
                        }



                        if (data.kState.IsKeyDown(Keys.A))
                        {
                            if (player.GetVelocity().X != 0)
                            {
                                rectangle1.X += 1;
                                rectangle2.X += 1;
                            }
                        }

                        // If player is going right
                        if (player.GetVelocity().X > 0)
                        {
                            if ((player.position.X) > rectangle2.X + texture1.Width / 2)
                                rectangle1.X = rectangle2.X + texture1.Width;

                            if ((player.position.X) > rectangle1.X + texture1.Width / 2)
                                rectangle2.X = rectangle1.X + texture1.Width;
                        }

                        // If player is going left
                        if (player.GetVelocity().X < 0)
                        {
                            if ((player.position.X) < rectangle2.X + (texture1.Width / 2))
                                rectangle1.X = rectangle2.X - texture1.Width;
                            // Then repeat this check for rectangle2.

                            if ((player.position.X) < rectangle1.X + texture1.Width / 2)
                                rectangle2.X = rectangle1.X - texture1.Width;
                        }

                    }
                    

                    #endregion
                    #region temperature updates
                    //============================================================
                    //logic here for slowing down/speeding up the player when they
                    //are over 400 or under -400. Also the dying logic for when the
                    //player hits max coldness/hotness
                    //============================================================

                    if (temperature.GetTemperature() >= 500 ||
                        temperature.GetTemperature() <= -500)
                    {
                        ResetGame();

                        currentSelection = 1;
                        currentGameState = GameState.GAME_OVER;
                    }

                    #endregion
                    #region player out of bounds

                    if (player.getPosition().Y > map.mapDimensions.Y ||
                        player.getPosition().X > map.mapDimensions.X + 200 ||
                        player.getPosition().X < -200)
                    {
                        ResetGame();
                        currentSelection = 1;
                        currentGameState = GameState.GAME_OVER;
                    }

                    #endregion
                    break;

                case GameState.LEVEL_SELECT:
                    #region level select controls/implementation

                    if (data.kState.IsKeyDown(Keys.W) && data.oldKeyState.IsKeyUp(Keys.W))
                        {
                            if (currentSelection > 1)
                                currentSelection--;
                        }
                        else if (data.kState.IsKeyDown(Keys.S) && data.oldKeyState.IsKeyUp(Keys.S))
                        {
                            if (currentSelection < 8)
                                currentSelection++;
                        }
                    
                        if (data.kState.IsKeyDown(Keys.Space) && data.oldKeyState.IsKeyUp(Keys.Space))
                        {
                            switch (currentSelection)
                            {
                                case 1:
                                    currentMapSelection = 1;
                                    ResetGame();
                                    currentSong.Pause();
                                    currentSong = soundBank.GetCue("Level1");
                                    currentSong.Play();
                                    currentGameState = GameState.GAME;
                                    break;
                                case 2:
                                    currentMapSelection = 2;
                                    ResetGame();
                                    currentSong.Pause();
                                    currentSong = soundBank.GetCue("Level2");
                                    currentSong.Play();
                                    currentGameState = GameState.GAME;
                                    break;
                                case 3:
                                    currentMapSelection = 3;
                                    ResetGame();
                                    currentSong.Pause();
                                    currentSong = soundBank.GetCue("Level3");
                                    currentSong.Play();
                                    currentGameState = GameState.GAME;
                                    break;

                                case 4:
                                    currentMapSelection = 4;
                                    ResetGame();
                                    currentSong.Pause();
                                    currentSong = soundBank.GetCue("Level1");
                                    currentSong.Play();
                                    currentGameState = GameState.GAME;
                                    break;

                                case 5:
                                    currentMapSelection = 5;
                                    ResetGame();
                                    currentSong.Pause();
                                    currentSong = soundBank.GetCue("Level2");
                                    currentSong.Play();
                                    currentGameState = GameState.GAME;
                                    break;

                                case 6:
                                    currentMapSelection = 6;
                                    ResetGame();
                                    currentSong.Pause();
                                    currentSong = soundBank.GetCue("Level3");
                                    currentSong.Play();
                                    currentGameState = GameState.GAME;
                                    break;

                                case 7:
                                    currentMapSelection = 7;
                                    ResetGame();
                                    currentSong.Pause();
                                    currentSong = soundBank.GetCue("Level1");
                                    currentSong.Play();
                                    currentGameState = GameState.GAME;
                                    break;

                                case 8:
                                    currentSelection = 1;
                                    currentGameState = GameState.CHAR_SELECT;
                                    break;
                            }
                        }

                        if (data.kState.IsKeyDown(Keys.Escape) && data.oldKeyState.IsKeyUp(Keys.Escape))
                        {
                            currentSelection = 1;
                            currentGameState = GameState.CHAR_SELECT;
                        }

                #endregion
                    #region colour changing controls

                    switch (currentSelection)
                    {
                        case 1:
                            levelSelect1Color = Color.Red;
                            levelSelect2Color = Color.Black;
                            levelSelect3Color = Color.Black;
                            levelSelect4Color = Color.Black;
                            levelSelect5Color = Color.Black;
                            levelSelect6Color = Color.Black;
                            levelSelect7Color = Color.Black;
                            levelBackColor = Color.Black;
                            break;
                        case 2:
                            levelSelect1Color = Color.Black;
                            levelSelect2Color = Color.Red;
                            levelSelect4Color = Color.Black;
                            levelSelect5Color = Color.Black;
                            levelSelect6Color = Color.Black;
                            levelSelect7Color = Color.Black;
                            levelSelect3Color = Color.Black;
                            levelBackColor = Color.Black;
                            break;
                        case 3:
                            levelSelect1Color = Color.Black;
                            levelSelect2Color = Color.Black;
                            levelSelect3Color = Color.Red;
                            levelSelect4Color = Color.Black;
                            levelSelect5Color = Color.Black;
                            levelSelect6Color = Color.Black;
                            levelSelect7Color = Color.Black;
                            levelBackColor = Color.Black;
                            break;
                        case 4:
                            levelSelect1Color = Color.Black;
                            levelSelect2Color = Color.Black;
                            levelSelect3Color = Color.Black;
                            levelSelect4Color = Color.Red;
                            levelSelect5Color = Color.Black;
                            levelSelect6Color = Color.Black;
                            levelSelect7Color = Color.Black;
                            levelBackColor = Color.Black;
                            break;
                        case 5:
                            levelSelect1Color = Color.Black;
                            levelSelect2Color = Color.Black;
                            levelSelect3Color = Color.Black;
                            levelSelect4Color = Color.Black;
                            levelSelect5Color = Color.Red;
                            levelSelect6Color = Color.Black;
                            levelSelect7Color = Color.Black;
                            levelBackColor = Color.Black;
                            break;
                        case 6:
                            levelSelect1Color = Color.Black;
                            levelSelect2Color = Color.Black;
                            levelSelect3Color = Color.Black;
                            levelSelect4Color = Color.Black;
                            levelSelect5Color = Color.Black;
                            levelSelect6Color = Color.Red;
                            levelSelect7Color = Color.Black;
                            levelBackColor = Color.Black;
                            break;
                        case 7:
                            levelSelect1Color = Color.Black;
                            levelSelect2Color = Color.Black;
                            levelSelect3Color = Color.Black;
                            levelSelect4Color = Color.Black;
                            levelSelect5Color = Color.Black;
                            levelSelect6Color = Color.Black;
                            levelSelect7Color = Color.Red;
                            levelBackColor = Color.Black;
                            break;
                        case 8:
                            levelSelect1Color = Color.Black;
                            levelSelect2Color = Color.Black;
                            levelSelect3Color = Color.Black;
                            levelSelect4Color = Color.Black;
                            levelSelect5Color = Color.Black;
                            levelSelect6Color = Color.Black;
                            levelSelect7Color = Color.Black;
                            levelBackColor = Color.Red;
                            break;
                    }

                #endregion 
                    break;
                    
                case GameState.CHAR_SELECT:
                    #region char select controls/implementation

                    if (!scrolling)
                    {
                        if (data.kState.IsKeyDown(Keys.W) && data.oldKeyState.IsKeyUp(Keys.W))
                        {
                            if (currentSelection > 1)
                                currentSelection--;
                        }
                        else if (data.kState.IsKeyDown(Keys.S) && data.oldKeyState.IsKeyUp(Keys.S))
                        {
                            if (currentSelection < 4)
                                currentSelection++;
                        }
                    }

                    
                    if (data.kState.IsKeyDown(Keys.Space) && data.oldKeyState.IsKeyUp(Keys.Space) || scrolling)
                    {
                        switch (currentSelection)
                        {
                            case 1:
                                currentGameState = GameState.LEVEL_SELECT;
                                playerSelected = "zelda";
                                break;
                            case 2:
                                currentGameState = GameState.LEVEL_SELECT;
                                playerSelected = "zombie";
                                currentSelection = 1;
                                break;
                            case 3:
                                currentGameState = GameState.LEVEL_SELECT;
                                playerSelected = "link";
                                currentSelection = 1;
                                break;
                            case 4:

                                float f_angle = (float)Math.Atan2((position_MM.Y - menuCamPos.Y),
                                                                   position_MM.X - menuCamPos.X);

                                Vector2 velocity = new Vector2((float)Math.Cos(f_angle) * 1,
                                                   (float)Math.Sin(f_angle) * 1);

                                if (menuCamPos.X >= position_MM.X)
                                    menuCamPos.X += velocity.X * menuScrollSpeed;

                                if (menuCamPos.Y >= position_MM.Y)
                                    menuCamPos.Y += velocity.Y * menuScrollSpeed;

                                if (menuCamPos.X < position_MM.X && menuCamPos.Y < position_MM.Y)
                                {
                                    scrolling = false;
                                    currentSelection = 1;
                                    currentGameState = GameState.MENU;
                                }
                                else
                                    scrolling = true;
                                
                                break;
                        }
                    }


                    if (data.kState.IsKeyDown(Keys.Escape) && data.oldKeyState.IsKeyUp(Keys.Escape))
                    {
                        currentSelection = 4;
                        scrolling = true;
                    }

                    #endregion
                    #region colour changing controls
                    switch (currentSelection)
                    {
                        case 1:
                            charOneColor = Color.Red;
                            charTwoColor = Color.Black;
                            charThreeColor = Color.Black;
                            charBackColor = Color.Black;
                            break;

                        case 2:
                            charOneColor = Color.Black;
                            charTwoColor = Color.Red;
                            charThreeColor = Color.Black;
                            charBackColor = Color.Black;
                            break;

                        case 3:
                            charOneColor = Color.Black;
                            charTwoColor = Color.Black;
                            charThreeColor = Color.Red;
                            charBackColor = Color.Black;
                            break;
                        case 4:
                            charOneColor = Color.Black;
                            charTwoColor = Color.Black;
                            charThreeColor = Color.Black;
                            charBackColor = Color.Red;
                            break;

                    }
                    #endregion
                    break;

                case GameState.OPTIONS:

                    #region return to menu state after 30 seconds


                    if(Keyboard.GetState().GetPressedKeys().Length == 0)
                    {
                        if (optionsTimer.isRunning == false)
                        {
                            optionsTimer.start(30);
                        }
                        else
                        {
                            optionsTimer.checkTime(gameTime);
                        }

                        if (optionsTimer.isFinished == true)
                        {
                            optionsTimer.reset();
                            menuTimer.reset();
                            currentSelection = 1;
                            menuCamPos = position_MM;
                            currentGameState = GameState.MENU; 
                        }

                    }

                    #endregion

                    #region options menu controls

                    if (!scrolling)
                    {
                        if (data.kState.IsKeyDown(Keys.W) && data.oldKeyState.IsKeyUp(Keys.W))
                        {
                            if (currentSelection > 1)
                                currentSelection--;
                        }
                        else if (data.kState.IsKeyDown(Keys.S) && data.oldKeyState.IsKeyUp(Keys.S))
                        {
                            if (currentSelection < 2)
                                currentSelection++;
                        }
                    }

                    if (data.kState.IsKeyDown(Keys.Space) && data.oldKeyState.IsKeyUp(Keys.Space) || scrolling)
                    {
                        switch (currentSelection)
                        {
                            case 1:
                                //when clicked, enter fullscreen
                                //when clicked for the second time, exit fullscreen
                                data.graphics.ToggleFullScreen();
                                break;
                            case 2:
                                float f_angle = (float)Math.Atan2((position_MM.Y - menuCamPos.Y),
                                                                   position_MM.X - menuCamPos.X);

                                Vector2 velocity = new Vector2((float)Math.Cos(f_angle) * 1,
                                                   (float)Math.Sin(f_angle) * 1);

                                if (menuCamPos.X <= position_MM.X)
                                    menuCamPos.X += velocity.X * menuScrollSpeed;

                                if (menuCamPos.Y >= position_MM.Y)
                                    menuCamPos.Y += velocity.Y * menuScrollSpeed;

                                if (menuCamPos.X > position_MM.X && menuCamPos.Y < position_MM.Y)
                                {
                                    currentSelection = 2;
                                    scrolling = false;
                                    currentGameState = GameState.MENU;
                                }
                                else
                                    scrolling = true;

                                break;
                        }
                    }

                    if (data.kState.IsKeyDown(Keys.Escape) && data.oldKeyState.IsKeyUp(Keys.Escape))
                    {
                        currentSelection = 2;
                        scrolling = true;
                    }

                    #endregion
                    #region colour changing controls

                    switch (currentSelection)
                    {
                        case 1:
                            optionsFullScreenColor = Color.Red;
                            optionsMainMenuColor = Color.Black;
                            break;
                        case 2:
                            optionsFullScreenColor = Color.Black;
                            optionsMainMenuColor = Color.Red;
                            break;
                    }
                    #endregion
                    break;
                case GameState.GAME_OVER:

                    #region return to menu state after 30 seconds


                    if (Keyboard.GetState().GetPressedKeys().Length == 0)
                    {
                        if (gameOverTimer.isRunning == false)
                        {
                            gameOverTimer.start(30);
                        }
                        else
                        {
                            gameOverTimer.checkTime(gameTime);
                        }

                        if (gameOverTimer.isFinished == true)
                        {

                            gameOverTimer.reset();
                            menuTimer.reset();
                            currentSelection = 1;
                            menuCamPos = position_MM;
                            currentGameState = GameState.MENU;
                        }

                    }

                    #endregion

                    #region game over controls

                    if (data.kState.IsKeyDown(Keys.W) && data.oldKeyState.IsKeyUp(Keys.W))
                    {
                        if (currentSelection > 1)
                            currentSelection--;
                    }
                    else if (data.kState.IsKeyDown(Keys.S) && data.oldKeyState.IsKeyUp(Keys.S))
                    {
                        if (currentSelection < 2)
                            currentSelection++;
                    }

                    if (data.kState.IsKeyDown(Keys.Space) && data.oldKeyState.IsKeyUp(Keys.Space))
                    {
                        switch (currentSelection)
                        {
                            case 1:
                                currentSelection = 1;
                                currentSelection = 1;
                                menuCamPos = position_MM;
                                currentGameState = GameState.MENU;
                                break;
                            case 2:
                                currentGameState = GameState.GAME;
                                break;
                        }
                    }

                    #endregion
                    #region colour changing

                    switch (currentSelection)
                    {
                        case 1:
                            gameOverYesColor = Color.Red;
                            gameOverNoColor = Color.White;
                            break;
                        case 2:
                            gameOverYesColor = Color.White;
                            gameOverNoColor = Color.Red;
                            break;
                    }

                    #endregion
                    break;
                case GameState.PAUSE:

                    if (data.kState.IsKeyDown(Keys.Space) && data.oldKeyState.IsKeyUp(Keys.Space) ||
                        data.kState.IsKeyDown(Keys.Escape) && data.oldKeyState.IsKeyUp(Keys.Escape) ||
                        data.kState.IsKeyDown(Keys.P) && data.oldKeyState.IsKeyUp(Keys.P))
                        currentGameState = GameState.GAME;    
                    
                    break;
                case GameState.CONFIRM_EXIT:

                    #region confirm exit controls

                    if (data.kState.IsKeyDown(Keys.W) && data.oldKeyState.IsKeyUp(Keys.W))
                    {
                        if (currentSelection > 1)
                            currentSelection--;
                    }
                    else if (data.kState.IsKeyDown(Keys.S) && data.oldKeyState.IsKeyUp(Keys.S))
                    {
                        if (currentSelection < 2)
                            currentSelection++;
                    }

                    if (data.kState.IsKeyDown(Keys.Space) && data.oldKeyState.IsKeyUp(Keys.Space))
                    {
                        switch (currentSelection)
                        {
                            case 1:
                                this.Exit();
                                break;
                            case 2:
                                currentSelection = 1;
                                menuCamPos = position_MM;
                                currentGameState = GameState.MENU;
                                break;
                        }
                    }

                    #endregion
                    #region colour changing

                    switch (currentSelection)
                    {
                        case 1:
                            exitYesColor = Color.Red;
                            exitNoColor = Color.White;
                            break;
                        case 2:
                            exitYesColor = Color.White;
                            exitNoColor = Color.Red;
                            break;

                    }
                    #endregion
                    break;
            }

            data.oldKeyState = data.kState;

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            if(currentGameState == GameState.GAME)
                GraphicsDevice.Clear(Color.Gray);

            #region menu drawing 

            if (currentGameState == GameState.MENU || currentGameState == GameState.OPTIONS ||
               currentGameState == GameState.CHAR_SELECT || currentGameState == GameState.LEVEL_SELECT)
            {
                GraphicsDevice.Clear(Color.White);

                data.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

                mainMenuBG.Draw(data.spriteBatch);
                optionsBG.Draw(data.spriteBatch);
                characterSelectBG.Draw(data.spriteBatch);
                menuTitle.Draw(data.spriteBatch);

                data.spriteBatch.End();
            }

            #endregion

            switch (currentGameState)
            {
                case GameState.MENU:

                    #region set origins for menu text

                    Vector2 playOrigin = font.MeasureString(play) / 2;
                    Vector2 optionsOrigin = font.MeasureString(options) / 2;
                    Vector2 exitOrigin = font.MeasureString(exit) / 2;

                    #endregion
                    #region drawing menu objects

                    data.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

                    data.spriteBatch.DrawString(font, play, menuPlayPos, menuPlayColor,
                        0.13f, playOrigin, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.DrawString(font, options, menuOptionsPos, menuOptionsColor,
                        0.13f, optionsOrigin, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.DrawString(font, exit, menuExitPos, menuExitColor,
                        0.13f, exitOrigin, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.End();

                    #endregion
                    break;

                case GameState.VIDEO:

                    if (videoPlayer.State != MediaState.Stopped)
                    {
                        Texture2D videoTexture = videoPlayer.GetTexture();
                        if (videoTexture != null)
                        {
                            data.spriteBatch.Begin();
                            data.spriteBatch.Draw(videoTexture, new Rectangle(0, 0, 1280, 720),
                                Color.White);
                            data.spriteBatch.End();
                        }
                    }

                    break;
    

                case GameState.GAME:

                    #region drawing game objects

                    data.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
                   

                    
                    if (currentMapSelection == 1)
                    {
                        data.spriteBatch.Draw(texture1, rectangle1, Color.White);
                        data.spriteBatch.Draw(texture1, rectangle2, Color.White);

                    }
                    else if (currentMapSelection == 2)
                    {
                        data.spriteBatch.Draw(texture2, rectangle1, Color.White);
                        data.spriteBatch.Draw(texture2, rectangle2, Color.White);

                    }
                    else
                    {
                        data.spriteBatch.Draw(texture3, rectangle1, Color.White);
                        data.spriteBatch.Draw(texture3, rectangle2, Color.White);

                    }
                    
                    map.Draw(data.spriteBatch);

                    foreach (Powerups pu in lists.powerupList)
                    {
                        pu.Draw(data.spriteBatch);
                    }
                    foreach (ExitDoor ed in lists.exitDoorList)
                    {
                        ed.Draw(data.spriteBatch);
                    }

                    lists.enemyManager.Draw(data.spriteBatch);
                    player.Draw(data.spriteBatch);

                    temperature.Draw(data.spriteBatch);

                    if (currentMapSelection != 2)
                        foreach (RainGenerator r in lists.rainList)
                            r.Draw(data.spriteBatch);

                    foreach (FireGenerator f in lists.fireList)
                        f.Draw(data.spriteBatch);
                    

                    data.spriteBatch.End();
                    data.spriteBatch.Begin();
                    data.spriteBatch.DrawString(font, "Score: " + score, new Vector2(10, 15), Color.White);
                    data.spriteBatch.End();


                    #endregion
                    break;

                case GameState.LEVEL_SELECT:

                     #region set origins for level select text

                     Vector2 levelSelect1Origin = font.MeasureString(levelSelect1) / 2;
                     Vector2 levelSelect2Origin = font.MeasureString(levelSelect2) / 2;
                     Vector2 levelSelect3Origin = font.MeasureString(levelSelect3) / 2;
                     Vector2 levelSelect4Origin = font.MeasureString(levelSelect4) / 2;
                     Vector2 levelSelect5Origin = font.MeasureString(levelSelect5) / 2;
                     Vector2 levelSelect6Origin = font.MeasureString(levelSelect6) / 2;
                     Vector2 levelSelect7Origin = font.MeasureString(levelSelect7) / 2;
                     Vector2 levelBackOrigin = font.MeasureString(levelBack) / 2;

                     Vector2 charOneOrigin_Dupe = font.MeasureString(characterOne) / 2;
                     Vector2 charTwoOrigin_Dupe = font.MeasureString(characterTwo) / 2;
                     Vector2 charThreeOrigin_Dupe = font.MeasureString(characterThree) / 2;
                     Vector2 charBackOrigin_Dupe = font.MeasureString(levelBack) / 2;

                    #endregion                   
                    #region drawing level select objects

                     data.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

                    data.spriteBatch.DrawString(font, levelSelect1, levelSelect1Pos, levelSelect1Color,
                        0.05f, levelSelect1Origin, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.DrawString(font, levelSelect2, levelSelect2Pos, levelSelect2Color,
                        0.05f, levelSelect2Origin, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.DrawString(font, levelSelect3, levelSelect3Pos, levelSelect3Color,
                        0.05f, levelSelect3Origin, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.DrawString(font, levelSelect4, levelSelect4Pos, levelSelect4Color,
                        0.05f, levelSelect4Origin, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.DrawString(font, levelSelect5, levelSelect5Pos, levelSelect5Color,
                        0.05f, levelSelect5Origin, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.DrawString(font, levelSelect6, levelSelect6Pos, levelSelect6Color,
                        0.05f, levelSelect6Origin, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.DrawString(font, levelSelect7, levelSelect7Pos, levelSelect7Color,
                        0.05f, levelSelect7Origin, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.DrawString(font, levelBack, levelBackPos, levelBackColor,
                        0.05f, levelBackOrigin, 1.0f, SpriteEffects.None, 0.5f);

                    //also draw char select text
                    data.spriteBatch.DrawString(font, characterOne, charOnePos, Color.Black,
                        0.05f, charOneOrigin_Dupe, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.DrawString(font, characterTwo, charTwoPos, Color.Black,
                        0.05f, charTwoOrigin_Dupe, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.DrawString(font, characterThree, charThreePos, Color.Black,
                        0.05f, charThreeOrigin_Dupe, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.DrawString(font, charBack, charBackPos, Color.Black,
                        0.05f, charBackOrigin_Dupe, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.End();
                    
                    #endregion
                    break;

                case GameState.CHAR_SELECT:

                    #region set origins for char select text

                 Vector2 charOneOrigin = font.MeasureString(characterOne) / 2;
                 Vector2 charTwoOrigin = font.MeasureString(characterTwo) / 2;
                 Vector2 charThreeOrigin = font.MeasureString(characterThree) / 2;
                 Vector2 charBackOrigin = font.MeasureString(levelBack) / 2;

                #endregion                   
                    #region drawing character select objects

                 data.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

                data.spriteBatch.DrawString(font, characterOne, charOnePos, charOneColor,
                   0.05f, charOneOrigin, 1.0f, SpriteEffects.None, 0.5f);

                data.spriteBatch.DrawString(font, characterTwo, charTwoPos, charTwoColor,
                    0.05f, charTwoOrigin, 1.0f, SpriteEffects.None, 0.5f);

                data.spriteBatch.DrawString(font, characterThree, charThreePos, charThreeColor,
                    0.05f, charThreeOrigin, 1.0f, SpriteEffects.None, 0.5f);

                data.spriteBatch.DrawString(font, charBack, charBackPos, charBackColor,
                    0.05f, charBackOrigin, 1.0f, SpriteEffects.None, 0.5f);

                data.spriteBatch.End();
                    
                #endregion
                    break;
                case GameState.OPTIONS:

                    #region set origins for options text

                    Vector2 fullscreenOrigin = font.MeasureString(fullscreen) / 2;
                    Vector2 mainMenuOrigin = font.MeasureString(mainMenu) / 2;

                    #endregion
                    #region drawing options objects

                    data.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

                    string controls = "Current Volume: " + SoundManager.musicVolume * 100 + "%" +
                                        "\nPress the up arrow key to raise the volume" +
                                        "\nPress the down arrow key to lower the volume" +
                                        "\nPress P to pause" +
                                        "\nPress N to switch game audio" +
                                        "\nThe current song playing is " + currentSong.Name +
                                        "\nPress M to pause the song" +
                                        "\nPress W to go up/jump" +
                                        "\nPress D to go right" +
                                        "\nPress A to go left" +
                                        "\nPress S to go down" +
                                        "\nPress ESC to exit";

                    data.spriteBatch.DrawString(font, 
                                                controls, 
                                                new Vector2(optionsFullScreenPos.X + 280, optionsFullScreenPos.Y), 
                                                Color.Black,
                                                -0.14f, 
                                                mainMenuOrigin, 
                                                1.0f, 
                                                SpriteEffects.None, 
                                                0.5f);

                    data.spriteBatch.DrawString(font, fullscreen, optionsFullScreenPos, optionsFullScreenColor,
                        -0.14f, fullscreenOrigin, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.DrawString(font, mainMenu, optionsMainMenuPos, optionsMainMenuColor,
                      -0.14f, mainMenuOrigin, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.End();

                    #endregion
                    break;
                case GameState.PAUSE:

                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    resumeOrigin = font.MeasureString(resume) / 2;
                    overlay = new Rectangle((int)player.getPosition().X - 700,
                                            (int)player.getPosition().Y - data.graphics.GraphicsDevice.Viewport.Height / 2,
                                            2000,
                                            1000);

                    #region drawing game objects

                    data.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

                    map.Draw(data.spriteBatch);

                    foreach (Powerups pu in lists.powerupList)
                    {
                        pu.Draw(data.spriteBatch);
                    }
                    foreach (ExitDoor ed in lists.exitDoorList)
                    {
                        ed.Draw(data.spriteBatch);
                    }

                    lists.enemyManager.Draw(data.spriteBatch);
                    player.Draw(data.spriteBatch);

                    temperature.Draw(data.spriteBatch);

                    foreach (RainGenerator r in lists.rainList)
                        r.Draw(data.spriteBatch);

                    foreach (FireGenerator f in lists.fireList)
                        f.Draw(data.spriteBatch);

                    data.spriteBatch.Draw(overlayTexture, overlay, new Color(0, 0, 0, 180));

                    data.spriteBatch.End();

                    #endregion
                    #region drawing pause objects

                    data.spriteBatch.Begin();

                    data.spriteBatch.DrawString(font, resume, resumePos, resumeColor,
                        0, resumeOrigin, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.End();


                    #endregion

                    break;
                case GameState.GAME_OVER:

                    #region setting text origins

                    Vector2 gameoverYesOrigin = font.MeasureString(gameOverYes) / 2;
                    Vector2 gameoverNoOrigin = font.MeasureString(gameOverNo) / 2;

                    #endregion
                    #region game over object drawing

                    gameOverbackground = new GameObject();
                    gameOverbackground.Load(Content,GraphicsDevice,"Background/bggameover");
                    gameOverbackground.Update();

                    data.spriteBatch.Begin();
                    gameOverbackground.Draw(data.spriteBatch);

                    data.spriteBatch.DrawString(font, gameOverYes, exitYesPos, gameOverYesColor,
                        0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
                    data.spriteBatch.DrawString(font, gameOverNo, exitNoPos, gameOverNoColor,
                        0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.End();

                    #endregion
                    break;
                case GameState.CONFIRM_EXIT:

                    #region setting text origins

                    Vector2 exitYesOrigin = font.MeasureString(exitYes) / 2;
                    Vector2 exitNoOrigin = font.MeasureString(exitNo) / 2;

                    #endregion
                    #region drawing confirm exit objects

                    data.spriteBatch.Begin();

                    //exitBackground.Draw(data.spriteBatch);

                    data.spriteBatch.DrawString(font, exitYes, exitYesPos, exitYesColor,
                        0, exitYesOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    data.spriteBatch.DrawString(font, exitNo, exitNoPos, exitNoColor,
                        0, exitNoOrigin, 1.0f, SpriteEffects.None, 0.5f);

                    data.spriteBatch.End();

                    #endregion
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
