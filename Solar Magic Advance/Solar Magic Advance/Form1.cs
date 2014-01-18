using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Solar_Magic_Advance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton_Load_Click(object sender, EventArgs e)
        {
            openFileDialog_Level.ShowDialog();
        }

        private void openFileDialog_Level_FileOk(object sender, CancelEventArgs e)
        {
            Program.LoadLevel(this, openFileDialog_Level.FileName, openFileDialog_Level.OpenFile());
        }
    }
}
