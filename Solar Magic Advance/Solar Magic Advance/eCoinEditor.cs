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
    public partial class eCoinEditor : Form
    {
        LevelCard.header header;
        LevelCard.eCoin eCoinData;

        Bitmap eCoinGFXI;
        Bitmap eCoinPALI;

        byte[,] eCoinGFX;
        Color[] eCoinPAL;
        int colorID;

        bool drawing;

        public eCoinEditor()
        {
            InitializeComponent();
            header = LevelCard.level_header;
            eCoinData = LevelCard.level_eCoin;

            drawing = false;

            if (LevelCard.level_eCoin.gfx != null)
            {

                //Init
                eCoinGFX = new byte[24, 24];
                eCoinGFXI = new Bitmap(192, 192);
                eCoinPALI = new Bitmap(192, 48);
                eCoinPAL = new Color[16];

                //Get Palette
                loadPalette();

                //Get GFX to Bitmap
                loadGraphics();

                //Get numbers
                int position = header.eCoin;
                int floor = (int)Math.Ceiling((double)(position / 8));
                int pos = position - (floor * 8);

                numericUpDownFloor.Value = floor;
                numericUpDownPos.Value = pos;
            }
        }

        public void loadPalette()
        {
            int Red;
            int Green;
            int Blue;

            for (int i = 0; i < 16; i++)
            {
                Red = (int)(((float)((eCoinData.pal[i]) & 0x1F) / 31) * 255);
                Green = (int)(((float)((eCoinData.pal[i] >> 5) & 0x1F) / 31) * 255);
                Blue = (int)(((float)((eCoinData.pal[i] >> 10) & 0x1F) / 31) * 255);

                eCoinPAL[i] = Color.FromArgb(Red, Green, Blue);
            }

            updatePalette();
        }

        public void updatePalette()
        {
            Graphics palette = Graphics.FromImage(eCoinPALI);
            SolidBrush brush;

            for (int i = 0; i < 16; i++)
            {
                brush = new SolidBrush(eCoinPAL[i]);
                palette.FillRectangle(brush, (i - (i / 8 * 8)) * 24, (i / 8) * 24, 24, 24);
            }

            PAL.Image = eCoinPALI;
            PAL.Update();
        }

        public void loadGraphics()
        {
            for (int i = 0; i < 9; i++) //Tiles
                for (int j = 0; j < 64; j++)    //Pixel per tile
                {
                    //First nibble
                    eCoinGFX[(j - (j / 8 * 8)) + ((i % 3) * 8), (j / 8) + ((i / 3) * 8)] = (byte)((eCoinData.gfx[(j / 2) + (i * 32)]) & 0x0F);

                    //Second nibble
                    j++;
                    eCoinGFX[(j - (j / 8 * 8)) + ((i % 3) * 8), (j / 8) + ((i / 3) * 8)] = (byte)((eCoinData.gfx[(j / 2) + (i * 32)] >> 4) & 0x0F);
                }

            updateGraphics();
        }

        public void updateGraphics()
        {
            SolidBrush brush;
            Graphics graphics = Graphics.FromImage(eCoinGFXI);

            for (int y = 0; y < 24; y++)
                for (int x = 0; x < 24; x++)
                {
                    brush = new SolidBrush(eCoinPAL[eCoinGFX[x, y]]);
                    graphics.FillRectangle(brush, x * 8, y * 8, 8, 8);
                }

            GFX.Image = eCoinGFXI;
            GFX.Update();
        }

        private void PAL_MouseClick(object sender, MouseEventArgs e)
        {
            colorID = (e.X / 24) + ((e.Y / 24) * 8);
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                colorDialog1.ShowDialog();
                eCoinPAL[colorID] = Color.FromArgb(
                    (int)(((((float)colorDialog1.Color.R / 255) * 31) / 31) * 255),
                    (int)(((((float)colorDialog1.Color.G / 255) * 31) / 31) * 255),
                    (int)(((((float)colorDialog1.Color.B / 255) * 31) / 31) * 255)
                    );

                updatePalette();
                updateGraphics();
            }
        }

        private void GFX_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                colorID = eCoinGFX[e.X / 8, e.Y / 8];
        }

        private void GFX_MouseDown(object sender, MouseEventArgs e)
        {
            drawing = true;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                eCoinGFX[e.X / 8, e.Y / 8] = (byte)colorID;
                updateGraphics();
            }
        }

        private void GFX_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                eCoinGFX[e.X / 8, e.Y / 8] = (byte)colorID;
                updateGraphics();
            }
        }

        private void GFX_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
        }
    }
}
