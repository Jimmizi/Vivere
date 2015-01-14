﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsGraphicsDevice
{
    public partial class frmSave : Form
    {
        public int toSave = 1;
        public bool canceled = true;

        public frmSave()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            List<RadioButton> radioButtons = new List<RadioButton>();

            radioButtons.Add(radioButton1);
            radioButtons.Add(radioButton2);
            radioButtons.Add(radioButton3);
            radioButtons.Add(radioButton4);
            radioButtons.Add(radioButton5);
            radioButtons.Add(radioButton6);
            radioButtons.Add(radioButton7);

            for (int i = 0; i < radioButtons.Count(); i++)
            {
                if (radioButtons[i].Checked)
                {
                    toSave = i + 1;
                    canceled = false;
                    this.Close();
                }   
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            canceled = true;
            this.Close();
        }
    }
}