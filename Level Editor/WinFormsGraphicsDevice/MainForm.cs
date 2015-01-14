#region File Description
//-----------------------------------------------------------------------------
// MainForm.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion


namespace WinFormsGraphicsDevice
{
    // System.Drawing and the XNA Framework both define Color types.
    // To avoid conflicts, we define shortcut names for them both.
    using GdiColor = System.Drawing.Color;
    using XnaColor = Microsoft.Xna.Framework.Color;

    /// <summary>
    /// Custom form provides the main user interface for the program.
    /// In this sample we used the designer to add a splitter pane to the form,
    /// which contains a SpriteFontControl and a SpinningTriangleControl.
    /// </summary>
    public partial class MainForm : Form
    {
        System.Drawing.Size newPanelSize = new System.Drawing.Size(0,0);
        System.Drawing.Size newListSize = new System.Drawing.Size(0,0);

        Bitmap[] Textures = new Bitmap[100];
        Bitmap[] enemyTextures = new Bitmap[100];
        Bitmap[] powerupTextures = new Bitmap[100];
        Bitmap[] startgoalTextures = new Bitmap[2];

        string[] platformTextureNames = new string[100];
        string[] enemyTextureNames = new string[100];
        string[] powerupTextureNames = new string[100];
        string[] startgoalTextureNames = new string[2];

        //public MapArea mapArea1 = new WinFormsGraphicsDevice.MapArea();
        public MapArea mapArea1 = new WinFormsGraphicsDevice.MapArea();

        //store the opened file's name
        private string m_sfileName = ""; 

        public MainForm()
        {
            InitializeComponent();
            MainForm_Resize(null, null);
            IntialiseTileList();

            mapArea1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mapArea1_MouseDoubleClick);

            mapArea1.Name = "";
        }        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //clear the form text upon first open in the form caprion
        private void clearFormText()
        {
            this.Text = "Level Editor - [unnamed]";
        }

        //takes the filename and changes the forms caption to reflect
        //the new current file
        private void setFormText(string sFile)
        {
            // create an instance of a FileInfo class and use 
            // it to get the file's name without its path 
            System.IO.FileInfo fInfo;
            fInfo = new System.IO.FileInfo(sFile);
            this.Text = "Level Editor - [" + fInfo.Name + "]";
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            //Resize event to maintain a layout for the whole program
            int fromBottom = 35;

            newPanelSize.Height = this.ClientSize.Height - fromBottom;
            newPanelSize.Width = (this.ClientSize.Width / 5) * 4;

            newListSize.Height = newPanelSize.Height - fromBottom;
            newListSize.Width = (this.ClientSize.Width / 5 - 15);

            panel1.Size = newPanelSize;
            tabControl.Size = newListSize;

            tabControl.Location =
                new System.Drawing.Point(panel1.Location.X + panel1.Size.Width + 3,
                                         panel1.Location.Y);

            insertAssetButton.Location =
                new System.Drawing.Point(tabControl.Location.X,
                                         tabControl.Location.Y + tabControl.Size.Height);
        }

        private void IntialiseTileList()
        {
            #region platform loading in

            resourceList.Images.Clear();
            resourceListView.Clear();

            //set all of the platform texture strings
            platformTextureNames[0] = "..\\..\\Content\\Platforms\\platform.PNG";
            platformTextureNames[1] = "..\\..\\Content\\Platforms\\artist's desk level\\1Floorblock.PNG";
            platformTextureNames[2] = "..\\..\\Content\\Platforms\\artist's desk level\\5floorblock.PNG";
            platformTextureNames[3] = "..\\..\\Content\\Platforms\\artist's desk level\\chalkboard.PNG";
            platformTextureNames[4] = "..\\..\\Content\\Platforms\\artist's desk level\\inkspot1.PNG";
            platformTextureNames[5] = "..\\..\\Content\\Platforms\\artist's desk level\\paperstack.PNG";
            platformTextureNames[6] = "..\\..\\Content\\Platforms\\artist's desk level\\printer.PNG";
            platformTextureNames[7] = "..\\..\\Content\\Platforms\\artist's desk level\\sharppencil.PNG";
            platformTextureNames[8] = "..\\..\\Content\\Platforms\\comic book level\\1Floorblock.PNG";
            platformTextureNames[9] = "..\\..\\Content\\Platforms\\comic book level\\5Floorblock.PNG";
            platformTextureNames[10] = "..\\..\\Content\\Platforms\\comic book level\\bookstack1.PNG";
            platformTextureNames[11] = "..\\..\\Content\\Platforms\\comic book level\\comicstack2.PNG";
            platformTextureNames[12] = "..\\..\\Content\\Platforms\\comic book level\\openbook.PNG";
            platformTextureNames[13] = "..\\..\\Content\\Platforms\\comic book level\\comicbookrack.PNG";
            platformTextureNames[14] = "..\\..\\Content\\Platforms\\streets level\\1Floorblock.PNG";
            platformTextureNames[15] = "..\\..\\Content\\Platforms\\streets level\\5floorblock.PNG";
            platformTextureNames[16] = "..\\..\\Content\\Platforms\\streets level\\redradiator.PNG";
            platformTextureNames[17] = "..\\..\\Content\\Platforms\\streets level\\sewergrate.PNG";
            platformTextureNames[18] = "..\\..\\Content\\Platforms\\streets level\\tap drop.PNG";
            platformTextureNames[19] = "..\\..\\Content\\Platforms\\streets level\\tap.PNG";
            platformTextureNames[20] = "..\\..\\Content\\Platforms\\streets level\\watercooler.PNG";
            




            //platforms (normal, invisible and breakable)
            for (int i = 0; i < 21; i++)
            {
                Textures[i] = new Bitmap(platformTextureNames[i]);
                resourceList.Images.Add(Textures[i]);
                resourceListView.Items.Add(new ListViewItem("" + (i + 1), i));

                //getting just the name in order for MapArea Add function to properly add textures
                string tempName = platformTextureNames[i].Substring(platformTextureNames[i].IndexOf("Content") + 8,
                    platformTextureNames[i].IndexOf(".PNG") - platformTextureNames[i].IndexOf("Content") - 8);

                resourceListView.Items[i].Name = tempName;

                //assign the platform variables to the invisible and breakable lists
                invisibleResourceList.Images.Add(Textures[i]);
                invisibleResourceListView.Items.Add(new ListViewItem("" + (i + 1), i));

                string tempName1 = platformTextureNames[i].Substring(platformTextureNames[i].IndexOf("Content") + 8,
                    platformTextureNames[i].IndexOf(".PNG") - platformTextureNames[i].IndexOf("Content") - 8);

                invisibleResourceListView.Items[i].Name = tempName;

                //same for breakable object list
                breakableResourceList.Images.Add(Textures[i]);
                breakableResourceListView.Items.Add(new ListViewItem("" + (i + 1), i));

                string tempName2 = platformTextureNames[i].Substring(platformTextureNames[i].IndexOf("Content") + 8,
                    platformTextureNames[i].IndexOf(".PNG") - platformTextureNames[i].IndexOf("Content") - 8);

                breakableResourceListView.Items[i].Name = tempName;
            }

            #endregion
            #region enemy loading in

            enemyResourceList.Images.Clear();
            enemyResourceListView.Clear();

            //enemy textures
            enemyTextureNames[0] = "..\\..\\Content\\Enemy\\zombie.PNG";
            enemyTextureNames[1] = "..\\..\\Content\\Enemy\\scissors.PNG";

            for (int i = 0; i < 2; i++)
            {
                enemyTextures[i] = new Bitmap(enemyTextureNames[i]);
                enemyResourceList.Images.Add(enemyTextures[i]);
                enemyResourceListView.Items.Add(new ListViewItem("" + (i + 1), i));

                //getting just the name in order for MapArea Add function to properly add textures
                string tempName = enemyTextureNames[i].Substring(enemyTextureNames[i].IndexOf("Content") + 8,
                    enemyTextureNames[i].IndexOf(".PNG") - enemyTextureNames[i].IndexOf("Content") - 8);

                enemyResourceListView.Items[i].Name = tempName;
            }

            #endregion
            #region powerup loading in

            powerupsResourceList.Images.Clear();
            powerupsResourceListView.Clear();

            //powerup textures
            powerupTextureNames[0] = "..\\..\\Content\\Powerups\\bigfreeze.PNG";
            powerupTextureNames[1] = "..\\..\\Content\\Powerups\\blueball.PNG";
            powerupTextureNames[2] = "..\\..\\Content\\Powerups\\death.PNG";
            powerupTextureNames[3] = "..\\..\\Content\\Powerups\\heatwave.PNG";
            powerupTextureNames[4] = "..\\..\\Content\\Powerups\\invincibility.PNG";
            powerupTextureNames[5] = "..\\..\\Content\\Powerups\\redball.PNG";
            powerupTextureNames[6] = "..\\..\\Content\\Powerups\\stopwatch.PNG";
            powerupTextureNames[7] = "..\\..\\Content\\Powerups\\coin.PNG";


            for (int i = 0; i < 8; i++)
            {
                powerupTextures[i] = new Bitmap(powerupTextureNames[i]);
                powerupsResourceList.Images.Add(powerupTextures[i]);
                powerupsResourceListView.Items.Add(new ListViewItem("" + (i + 1), i));

                //getting just the name in order for MapArea Add function to properly add textures
                string tempName = powerupTextureNames[i].Substring(powerupTextureNames[i].IndexOf("Content") + 8,
                    powerupTextureNames[i].IndexOf(".PNG") - powerupTextureNames[i].IndexOf("Content") - 8);

                powerupsResourceListView.Items[i].Name = tempName;
            }

            #endregion
            #region start/goal loading in

            startgoalResourceList.Images.Clear();
            startgoalResourceListView.Clear();

            startgoalTextureNames[0] = "..\\..\\Content\\Misc\\spawn.PNG";
            startgoalTextureNames[1] = "..\\..\\Content\\Misc\\goal.PNG";

            for (int i = 0; i < 2; i++)
            {
                startgoalTextures[i] = new Bitmap(startgoalTextureNames[i]);
                startgoalResourceList.Images.Add(startgoalTextures[i]);
                startgoalResourceListView.Items.Add(new ListViewItem("" + (i + 1), i));

                //getting just the name in order for MapArea Add function to properly add textures
                string tempName = startgoalTextureNames[i].Substring(startgoalTextureNames[i].IndexOf("Content") + 8,
                    startgoalTextureNames[i].IndexOf(".PNG") - startgoalTextureNames[i].IndexOf("Content") - 8);

                startgoalResourceListView.Items[i].Name = tempName;
            }

            #endregion
        }

        //function to insert a selected object from the list to the MapArea
        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabControl.TabPages["Platforms"])
            {
                if (resourceListView.SelectedItems.Count != 0 &&
                    mapArea1.Name != "")
                    mapArea1.Add(resourceListView.SelectedItems[0].Name,"Platform");
            }
            else if (tabControl.SelectedTab == tabControl.TabPages["Enemies"])
            {
                if (enemyResourceListView.SelectedItems.Count != 0 &&
                    mapArea1.Name != "")
                    mapArea1.Add(enemyResourceListView.SelectedItems[0].Name,"Enemy");
            }
            else if (tabControl.SelectedTab == tabControl.TabPages["Powerups"])
            {
                if (powerupsResourceListView.SelectedItems.Count != 0 &&
                    mapArea1.Name != "")
                    mapArea1.Add(powerupsResourceListView.SelectedItems[0].Name,"Powerup");
            }
            else if (tabControl.SelectedTab == tabControl.TabPages["StartGoal"])
            {
                if (startgoalResourceListView.SelectedItems.Count != 0 &&
                    mapArea1.Name != "")
                    mapArea1.Add(startgoalResourceListView.SelectedItems[0].Name, "StartGoal");
            }
            else if (tabControl.SelectedTab == tabControl.TabPages["Invisible"])
            {
                if (invisibleResourceListView.SelectedItems.Count != 0 &&
                    mapArea1.Name != "")
                    mapArea1.AddAtMouse(invisibleResourceListView.SelectedItems[0].Name, "Invisible");
            }
            else if (tabControl.SelectedTab == tabControl.TabPages["Breakable"])
            {
                if (breakableResourceListView.SelectedItems.Count != 0 &&
                    mapArea1.Name != "")
                    mapArea1.AddAtMouse(breakableResourceListView.SelectedItems[0].Name, "Breakable");
            }
        }

        private void mapArea1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (tabControl.SelectedTab == tabControl.TabPages["Platforms"])
            {
                if (resourceListView.SelectedItems.Count != 0 &&
                    mapArea1.Name != "")
                    mapArea1.AddAtMouse(resourceListView.SelectedItems[0].Name, "Platform");
            }
            else if (tabControl.SelectedTab == tabControl.TabPages["Enemies"])
            {
                if (enemyResourceListView.SelectedItems.Count != 0 &&
                    mapArea1.Name != "")
                    mapArea1.AddAtMouse(enemyResourceListView.SelectedItems[0].Name, "Enemy");
            }
            else if (tabControl.SelectedTab == tabControl.TabPages["Powerups"])
            {
                if (powerupsResourceListView.SelectedItems.Count != 0 &&
                    mapArea1.Name != "")
                    mapArea1.AddAtMouse(powerupsResourceListView.SelectedItems[0].Name, "Powerup");
            }
            else if (tabControl.SelectedTab == tabControl.TabPages["StartGoal"])
            {
                if (startgoalResourceListView.SelectedItems.Count != 0 &&
                    mapArea1.Name != "")
                    mapArea1.AddAtMouse(startgoalResourceListView.SelectedItems[0].Name, "StartGoal");
            }
            else if (tabControl.SelectedTab == tabControl.TabPages["Invisible"])
            {
                if (invisibleResourceListView.SelectedItems.Count != 0 &&
                    mapArea1.Name != "")
                    mapArea1.AddAtMouse(invisibleResourceListView.SelectedItems[0].Name, "Invisible");
            }
            else if (tabControl.SelectedTab == tabControl.TabPages["Breakable"])
            {
                if (breakableResourceListView.SelectedItems.Count != 0 &&
                    mapArea1.Name != "")
                    mapArea1.AddAtMouse(breakableResourceListView.SelectedItems[0].Name, "Breakable");
            }
        }

        #region menu controls
        private void menu_EditClear_Click(object sender, EventArgs e)
        {
            if (mapArea1.Name != "")
                mapArea1.Clear(true,true,true,true,true,true);
        }
        private void menu_EditUndo_Click(object sender, EventArgs e)
        {

        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        //shows the "about" menu if the user clicks it
        private void helpAboutMenu_Click(object sender, EventArgs e)
        {
            //creating an instance of the form
            frmAbout fAb;

            //calling the form method
            // show the About box 
            fAb = new frmAbout();
            fAb.ShowDialog(); 

        }

        //initiating a new MapArea
        private void menu_FileNew_Click(object sender, EventArgs e)
        {
            //creating an instance of the form
            frmNew fNew = new frmNew();

            if (mapArea1.GetObjectCount() > 0)
            {
                if (MessageBox.Show("Really clear?", "Create New Map", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    fNew.ShowDialog();
                }
            }
            else fNew.ShowDialog();

            if (fNew.newDimensionsPicked)
            {
                //make the new map area
                mapArea1.Clear(true, true, true, true, true, true);
                mapArea1.Location = new System.Drawing.Point(10, 10);
                mapArea1.Name = "mapArea1";
                mapArea1.Size = new System.Drawing.Size(fNew.x, fNew.y);
                mapArea1.TabIndex = 0;
                mapArea1.Text = "mapArea1";

                panel1.Controls.Add(mapArea1);
            }
        }
        //save program
        private void fileSaveMenu_Click(object sender, EventArgs e)
        {
            frmSave fSave = new frmSave();

            if (mapArea1.GetObjectCount() > 0)
                fSave.ShowDialog();

            if(!fSave.canceled)
                mapArea1.Save(Convert.ToString(fSave.toSave));
        }
        
        //exit program if clicked
        private void fileExitMenu_Click(object sender, EventArgs e)
        {
            // exit the application but query user if they exit after new mapArea is created
            if (mapArea1.Name != "")
            {
                if (MessageBox.Show("Really exit?", "Confirm exit", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    Application.Exit();
                }
            }

            //general exit of application
            if (menuStrip1.Name != "")
            {
                Application.Exit();
            }
        }

        #region specific clear buttons
        private void clearPlatformsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapArea1.Name != "")
                mapArea1.Clear(true, false, false, false, false, false);
        }
        private void clearEnemiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapArea1.Name != "")
                mapArea1.Clear(false, true, false, false, false, false);
        }
        private void clearPoweToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapArea1.Name != "")
                mapArea1.Clear(false, false, true, false, false, false);
        }
        private void clearStartGoalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapArea1.Name != "")
                mapArea1.Clear(false, false, false, true, false, false);
        }
        private void clearInvisibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapArea1.Name != "")
                mapArea1.Clear(false, false, false, false, true, false);
        }
        private void clearBreakableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapArea1.Name != "")
                mapArea1.Clear(false, false, false, false, false, true);
        }
        #endregion

        private void fileLoadMenu_Click(object sender, EventArgs e)
        {
            frmLoad fLoad = new frmLoad();
            fLoad.ShowDialog();

            if (!fLoad.canceled)
            {
                if (mapArea1.Name == "")
                {
                    //make the new map area if there isn't one
                    mapArea1.Clear(true, true, true, true, true, true);
                    mapArea1.Location = new System.Drawing.Point(10, 10);
                    mapArea1.Name = "mapArea1";
                    mapArea1.Size = new System.Drawing.Size(10, 10);
                    mapArea1.TabIndex = 0;
                    mapArea1.Text = "mapArea1";

                    panel1.Controls.Add(mapArea1);
                }

                mapArea1.Load(fLoad.toLoad);
            }
        }

        #endregion
    }
}
