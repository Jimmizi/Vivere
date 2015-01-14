using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsGraphicsDevice
{
    public partial class frmNew : Form
    {
        //tell whether the user has actually entered something 
        public bool newDimensionsPicked = false;
        public int x, y;

        public frmNew()
        {
            InitializeComponent();
            dimensionsPositiveError.Hide();
        }

        private void mapHeight_TextChanged(object sender, EventArgs e)
        {

        }

        private void mapWidth_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(mapHeight.Text) <= 0 ||
               Convert.ToInt32(mapWidth.Text) <= 0)
                dimensionsPositiveError.Show();
            else
            {
                newDimensionsPicked = true;

                x = Convert.ToInt32(mapWidth.Text);
                y = Convert.ToInt32(mapHeight.Text);
               
                this.Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
