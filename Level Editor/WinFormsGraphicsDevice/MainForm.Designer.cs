namespace WinFormsGraphicsDevice
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        //public MapArea mapArea1 = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.resourceList = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_FileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.fileLoadMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.fileExitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_EditClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearPlatformsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearEnemiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearPoweToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearStartGoalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpAboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.insertAssetButton = new System.Windows.Forms.Button();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.OpenFileDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.Platforms = new System.Windows.Forms.TabPage();
            this.resourceListView = new System.Windows.Forms.ListView();
            this.Enemies = new System.Windows.Forms.TabPage();
            this.enemyResourceListView = new System.Windows.Forms.ListView();
            this.enemyResourceList = new System.Windows.Forms.ImageList(this.components);
            this.Powerups = new System.Windows.Forms.TabPage();
            this.powerupsResourceListView = new System.Windows.Forms.ListView();
            this.powerupsResourceList = new System.Windows.Forms.ImageList(this.components);
            this.StartGoal = new System.Windows.Forms.TabPage();
            this.startgoalResourceListView = new System.Windows.Forms.ListView();
            this.startgoalResourceList = new System.Windows.Forms.ImageList(this.components);
            this.invisibleResourceList = new System.Windows.Forms.ImageList(this.components);
            this.breakableResourceList = new System.Windows.Forms.ImageList(this.components);
            this.Invisible = new System.Windows.Forms.TabPage();
            this.Breakable = new System.Windows.Forms.TabPage();
            this.invisibleResourceListView = new System.Windows.Forms.ListView();
            this.breakableResourceListView = new System.Windows.Forms.ListView();
            this.clearInvisibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearBreakableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.Platforms.SuspendLayout();
            this.Enemies.SuspendLayout();
            this.Powerups.SuspendLayout();
            this.StartGoal.SuspendLayout();
            this.Invisible.SuspendLayout();
            this.Breakable.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Location = new System.Drawing.Point(8, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(556, 558);
            this.panel1.TabIndex = 0;
            // 
            // resourceList
            // 
            this.resourceList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.resourceList.ImageSize = new System.Drawing.Size(16, 16);
            this.resourceList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(796, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_FileNew,
            this.fileLoadMenu,
            this.fileSaveMenu,
            this.fileExitMenu});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 22);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // menu_FileNew
            // 
            this.menu_FileNew.Name = "menu_FileNew";
            this.menu_FileNew.Size = new System.Drawing.Size(107, 22);
            this.menu_FileNew.Text = "&New...";
            this.menu_FileNew.Click += new System.EventHandler(this.menu_FileNew_Click);
            // 
            // fileLoadMenu
            // 
            this.fileLoadMenu.Name = "fileLoadMenu";
            this.fileLoadMenu.Size = new System.Drawing.Size(107, 22);
            this.fileLoadMenu.Text = "&Load";
            this.fileLoadMenu.Click += new System.EventHandler(this.fileLoadMenu_Click);
            // 
            // fileSaveMenu
            // 
            this.fileSaveMenu.Name = "fileSaveMenu";
            this.fileSaveMenu.Size = new System.Drawing.Size(107, 22);
            this.fileSaveMenu.Text = "&Save";
            this.fileSaveMenu.Click += new System.EventHandler(this.fileSaveMenu_Click);
            // 
            // fileExitMenu
            // 
            this.fileExitMenu.Name = "fileExitMenu";
            this.fileExitMenu.Size = new System.Drawing.Size(107, 22);
            this.fileExitMenu.Text = "&Exit";
            this.fileExitMenu.Click += new System.EventHandler(this.fileExitMenu_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_EditClear,
            this.toolStripSeparator2,
            this.clearPlatformsToolStripMenuItem,
            this.clearEnemiesToolStripMenuItem,
            this.clearPoweToolStripMenuItem,
            this.clearStartGoalToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearInvisibleToolStripMenuItem,
            this.clearBreakableToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 22);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // menu_EditClear
            // 
            this.menu_EditClear.Name = "menu_EditClear";
            this.menu_EditClear.Size = new System.Drawing.Size(157, 22);
            this.menu_EditClear.Text = "Clear &All";
            this.menu_EditClear.Click += new System.EventHandler(this.menu_EditClear_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(154, 6);
            // 
            // clearPlatformsToolStripMenuItem
            // 
            this.clearPlatformsToolStripMenuItem.Name = "clearPlatformsToolStripMenuItem";
            this.clearPlatformsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.clearPlatformsToolStripMenuItem.Text = "Clear &Platforms";
            this.clearPlatformsToolStripMenuItem.Click += new System.EventHandler(this.clearPlatformsToolStripMenuItem_Click);
            // 
            // clearEnemiesToolStripMenuItem
            // 
            this.clearEnemiesToolStripMenuItem.Name = "clearEnemiesToolStripMenuItem";
            this.clearEnemiesToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.clearEnemiesToolStripMenuItem.Text = "Clear &Enemies";
            this.clearEnemiesToolStripMenuItem.Click += new System.EventHandler(this.clearEnemiesToolStripMenuItem_Click);
            // 
            // clearPoweToolStripMenuItem
            // 
            this.clearPoweToolStripMenuItem.Name = "clearPoweToolStripMenuItem";
            this.clearPoweToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.clearPoweToolStripMenuItem.Text = "Clear Po&werups";
            this.clearPoweToolStripMenuItem.Click += new System.EventHandler(this.clearPoweToolStripMenuItem_Click);
            // 
            // clearStartGoalToolStripMenuItem
            // 
            this.clearStartGoalToolStripMenuItem.Name = "clearStartGoalToolStripMenuItem";
            this.clearStartGoalToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.clearStartGoalToolStripMenuItem.Text = "Clear &Start/Goal";
            this.clearStartGoalToolStripMenuItem.Click += new System.EventHandler(this.clearStartGoalToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpAboutMenu});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 22);
            this.helpMenu.Text = "&Help";
            // 
            // helpAboutMenu
            // 
            this.helpAboutMenu.Name = "helpAboutMenu";
            this.helpAboutMenu.Size = new System.Drawing.Size(107, 22);
            this.helpAboutMenu.Text = "&About";
            this.helpAboutMenu.Click += new System.EventHandler(this.helpAboutMenu_Click);
            // 
            // insertAssetButton
            // 
            this.insertAssetButton.Location = new System.Drawing.Point(568, 560);
            this.insertAssetButton.Margin = new System.Windows.Forms.Padding(2);
            this.insertAssetButton.Name = "insertAssetButton";
            this.insertAssetButton.Size = new System.Drawing.Size(75, 21);
            this.insertAssetButton.TabIndex = 3;
            this.insertAssetButton.Text = "Insert Asset";
            this.insertAssetButton.UseVisualStyleBackColor = true;
            this.insertAssetButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // dlgOpen
            // 
            this.dlgOpen.FileName = "openFileDialog1";
            // 
            // dlgSave
            // 
            this.dlgSave.FileName = "openFileDialog1";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.Platforms);
            this.tabControl.Controls.Add(this.Enemies);
            this.tabControl.Controls.Add(this.Powerups);
            this.tabControl.Controls.Add(this.StartGoal);
            this.tabControl.Controls.Add(this.Invisible);
            this.tabControl.Controls.Add(this.Breakable);
            this.tabControl.Location = new System.Drawing.Point(568, 32);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(216, 525);
            this.tabControl.TabIndex = 0;
            // 
            // Platforms
            // 
            this.Platforms.Controls.Add(this.resourceListView);
            this.Platforms.Location = new System.Drawing.Point(4, 22);
            this.Platforms.Margin = new System.Windows.Forms.Padding(2);
            this.Platforms.Name = "Platforms";
            this.Platforms.Padding = new System.Windows.Forms.Padding(2);
            this.Platforms.Size = new System.Drawing.Size(208, 499);
            this.Platforms.TabIndex = 0;
            this.Platforms.Text = "Platforms";
            this.Platforms.UseVisualStyleBackColor = true;
            // 
            // resourceListView
            // 
            this.resourceListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resourceListView.HideSelection = false;
            this.resourceListView.LargeImageList = this.resourceList;
            this.resourceListView.Location = new System.Drawing.Point(2, 2);
            this.resourceListView.Margin = new System.Windows.Forms.Padding(2);
            this.resourceListView.MaximumSize = new System.Drawing.Size(300, 499);
            this.resourceListView.MultiSelect = false;
            this.resourceListView.Name = "resourceListView";
            this.resourceListView.Size = new System.Drawing.Size(207, 499);
            this.resourceListView.TabIndex = 0;
            this.resourceListView.UseCompatibleStateImageBehavior = false;
            // 
            // Enemies
            // 
            this.Enemies.Controls.Add(this.enemyResourceListView);
            this.Enemies.Location = new System.Drawing.Point(4, 22);
            this.Enemies.Margin = new System.Windows.Forms.Padding(2);
            this.Enemies.Name = "Enemies";
            this.Enemies.Padding = new System.Windows.Forms.Padding(2);
            this.Enemies.Size = new System.Drawing.Size(208, 499);
            this.Enemies.TabIndex = 1;
            this.Enemies.Text = "Enemies";
            this.Enemies.UseVisualStyleBackColor = true;
            // 
            // enemyResourceListView
            // 
            this.enemyResourceListView.HideSelection = false;
            this.enemyResourceListView.LargeImageList = this.enemyResourceList;
            this.enemyResourceListView.Location = new System.Drawing.Point(5, 3);
            this.enemyResourceListView.Margin = new System.Windows.Forms.Padding(2);
            this.enemyResourceListView.MultiSelect = false;
            this.enemyResourceListView.Name = "enemyResourceListView";
            this.enemyResourceListView.Size = new System.Drawing.Size(203, 498);
            this.enemyResourceListView.TabIndex = 0;
            this.enemyResourceListView.UseCompatibleStateImageBehavior = false;
            // 
            // enemyResourceList
            // 
            this.enemyResourceList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.enemyResourceList.ImageSize = new System.Drawing.Size(16, 16);
            this.enemyResourceList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Powerups
            // 
            this.Powerups.Controls.Add(this.powerupsResourceListView);
            this.Powerups.Location = new System.Drawing.Point(4, 22);
            this.Powerups.Margin = new System.Windows.Forms.Padding(2);
            this.Powerups.Name = "Powerups";
            this.Powerups.Padding = new System.Windows.Forms.Padding(2);
            this.Powerups.Size = new System.Drawing.Size(208, 499);
            this.Powerups.TabIndex = 2;
            this.Powerups.Text = "Powerups";
            this.Powerups.UseVisualStyleBackColor = true;
            // 
            // powerupsResourceListView
            // 
            this.powerupsResourceListView.HideSelection = false;
            this.powerupsResourceListView.LargeImageList = this.powerupsResourceList;
            this.powerupsResourceListView.Location = new System.Drawing.Point(5, 5);
            this.powerupsResourceListView.Margin = new System.Windows.Forms.Padding(2);
            this.powerupsResourceListView.MultiSelect = false;
            this.powerupsResourceListView.Name = "powerupsResourceListView";
            this.powerupsResourceListView.Size = new System.Drawing.Size(203, 496);
            this.powerupsResourceListView.TabIndex = 0;
            this.powerupsResourceListView.UseCompatibleStateImageBehavior = false;
            // 
            // powerupsResourceList
            // 
            this.powerupsResourceList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.powerupsResourceList.ImageSize = new System.Drawing.Size(16, 16);
            this.powerupsResourceList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // StartGoal
            // 
            this.StartGoal.Controls.Add(this.startgoalResourceListView);
            this.StartGoal.Location = new System.Drawing.Point(4, 22);
            this.StartGoal.Margin = new System.Windows.Forms.Padding(2);
            this.StartGoal.Name = "StartGoal";
            this.StartGoal.Size = new System.Drawing.Size(208, 499);
            this.StartGoal.TabIndex = 3;
            this.StartGoal.Text = "Start/Finish";
            this.StartGoal.UseVisualStyleBackColor = true;
            // 
            // startgoalResourceListView
            // 
            this.startgoalResourceListView.HideSelection = false;
            this.startgoalResourceListView.LargeImageList = this.startgoalResourceList;
            this.startgoalResourceListView.Location = new System.Drawing.Point(3, 3);
            this.startgoalResourceListView.Margin = new System.Windows.Forms.Padding(2);
            this.startgoalResourceListView.MultiSelect = false;
            this.startgoalResourceListView.Name = "startgoalResourceListView";
            this.startgoalResourceListView.Size = new System.Drawing.Size(207, 500);
            this.startgoalResourceListView.TabIndex = 0;
            this.startgoalResourceListView.UseCompatibleStateImageBehavior = false;
            // 
            // startgoalResourceList
            // 
            this.startgoalResourceList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.startgoalResourceList.ImageSize = new System.Drawing.Size(16, 16);
            this.startgoalResourceList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // invisibleResourceList
            // 
            this.invisibleResourceList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.invisibleResourceList.ImageSize = new System.Drawing.Size(16, 16);
            this.invisibleResourceList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // breakableResourceList
            // 
            this.breakableResourceList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.breakableResourceList.ImageSize = new System.Drawing.Size(16, 16);
            this.breakableResourceList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Invisible
            // 
            this.Invisible.Controls.Add(this.invisibleResourceListView);
            this.Invisible.Location = new System.Drawing.Point(4, 22);
            this.Invisible.Name = "Invisible";
            this.Invisible.Size = new System.Drawing.Size(208, 499);
            this.Invisible.TabIndex = 4;
            this.Invisible.Text = "Invisible";
            this.Invisible.UseVisualStyleBackColor = true;
            // 
            // Breakable
            // 
            this.Breakable.Controls.Add(this.breakableResourceListView);
            this.Breakable.Location = new System.Drawing.Point(4, 22);
            this.Breakable.Name = "Breakable";
            this.Breakable.Size = new System.Drawing.Size(208, 499);
            this.Breakable.TabIndex = 5;
            this.Breakable.Text = "Breakable";
            this.Breakable.UseVisualStyleBackColor = true;
            // 
            // invisibleResourceListView
            // 
            this.invisibleResourceListView.HideSelection = false;
            this.invisibleResourceListView.LargeImageList = this.invisibleResourceList;
            this.invisibleResourceListView.Location = new System.Drawing.Point(3, 3);
            this.invisibleResourceListView.MultiSelect = false;
            this.invisibleResourceListView.Name = "invisibleResourceListView";
            this.invisibleResourceListView.Size = new System.Drawing.Size(202, 493);
            this.invisibleResourceListView.TabIndex = 0;
            this.invisibleResourceListView.UseCompatibleStateImageBehavior = false;
            // 
            // breakableResourceListView
            // 
            this.breakableResourceListView.HideSelection = false;
            this.breakableResourceListView.LargeImageList = this.breakableResourceList;
            this.breakableResourceListView.Location = new System.Drawing.Point(4, 4);
            this.breakableResourceListView.MultiSelect = false;
            this.breakableResourceListView.Name = "breakableResourceListView";
            this.breakableResourceListView.Size = new System.Drawing.Size(201, 492);
            this.breakableResourceListView.TabIndex = 0;
            this.breakableResourceListView.UseCompatibleStateImageBehavior = false;
            // 
            // clearInvisibleToolStripMenuItem
            // 
            this.clearInvisibleToolStripMenuItem.Name = "clearInvisibleToolStripMenuItem";
            this.clearInvisibleToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.clearInvisibleToolStripMenuItem.Text = "Clear &Invisible";
            this.clearInvisibleToolStripMenuItem.Click += new System.EventHandler(this.clearInvisibleToolStripMenuItem_Click);
            // 
            // clearBreakableToolStripMenuItem
            // 
            this.clearBreakableToolStripMenuItem.Name = "clearBreakableToolStripMenuItem";
            this.clearBreakableToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.clearBreakableToolStripMenuItem.Text = "Clear &Breakable";
            this.clearBreakableToolStripMenuItem.Click += new System.EventHandler(this.clearBreakableToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(796, 590);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.insertAssetButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Level Editor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.Platforms.ResumeLayout(false);
            this.Enemies.ResumeLayout(false);
            this.Powerups.ResumeLayout(false);
            this.StartGoal.ResumeLayout(false);
            this.Invisible.ResumeLayout(false);
            this.Breakable.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ImageList resourceList;
        private System.Windows.Forms.Button insertAssetButton;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_FileNew;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_EditClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem helpAboutMenu;
        private System.Windows.Forms.ToolStripMenuItem fileLoadMenu;
        private System.Windows.Forms.ToolStripMenuItem fileSaveMenu;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.ToolStripMenuItem fileExitMenu;
        private System.Windows.Forms.OpenFileDialog dlgSave;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage Platforms;
        private System.Windows.Forms.ListView resourceListView;
        private System.Windows.Forms.TabPage Enemies;
        private System.Windows.Forms.TabPage Powerups;
        private System.Windows.Forms.ImageList enemyResourceList;
        private System.Windows.Forms.ListView enemyResourceListView;
        private System.Windows.Forms.ListView powerupsResourceListView;
        private System.Windows.Forms.TabPage StartGoal;
        private System.Windows.Forms.ImageList powerupsResourceList;
        private System.Windows.Forms.ImageList startgoalResourceList;
        private System.Windows.Forms.ListView startgoalResourceListView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem clearPlatformsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearEnemiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearPoweToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearStartGoalToolStripMenuItem;
        private System.Windows.Forms.ImageList invisibleResourceList;
        private System.Windows.Forms.ImageList breakableResourceList;
        private System.Windows.Forms.TabPage Invisible;
        private System.Windows.Forms.TabPage Breakable;
        private System.Windows.Forms.ListView invisibleResourceListView;
        private System.Windows.Forms.ListView breakableResourceListView;
        private System.Windows.Forms.ToolStripMenuItem clearInvisibleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearBreakableToolStripMenuItem;
    }
}

