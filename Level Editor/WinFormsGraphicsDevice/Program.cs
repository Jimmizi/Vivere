#region File Description
//-----------------------------------------------------------------------------
// Program.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Windows.Forms;
#endregion

namespace WinFormsGraphicsDevice
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    public static class Program
    {
        //create a public static main form to access variables from it
        //in the MapArea class
        public static MainForm mainForm = new MainForm();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(mainForm);
        }
    }
}
