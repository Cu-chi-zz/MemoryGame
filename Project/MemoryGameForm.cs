using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class MemoryGameForm : Form
    {
        public MemoryGameForm()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            #region First line
            Panel GamePad1 = new Panel();
            GamePad1.BackColor = Color.FromArgb(241, 250, 238);
            GamePad1.BorderStyle = BorderStyle.FixedSingle;
            GamePad1.Size = PadSizeGen(9);
            GamePad1.Location = PadLocationGen(1, 9, GamePad1.Size);
            Controls.Add(GamePad1);
            GamePad1.BringToFront();

            Panel GamePad2 = new Panel();
            GamePad2.BackColor = Color.FromArgb(241, 250, 238);
            GamePad2.BorderStyle = BorderStyle.FixedSingle;
            GamePad2.Size = PadSizeGen(9);
            GamePad2.Location = PadLocationGen(2, 9, GamePad2.Size);
            Controls.Add(GamePad2);
            GamePad2.BringToFront();

            Panel GamePad3 = new Panel();
            GamePad3.BackColor = Color.FromArgb(241, 250, 238);
            GamePad3.BorderStyle = BorderStyle.FixedSingle;
            GamePad3.Size = PadSizeGen(9);
            GamePad3.Location = PadLocationGen(3, 9, GamePad3.Size);
            Controls.Add(GamePad3);
            GamePad3.BringToFront();
            #endregion

            #region Second line
            Panel GamePad4 = new Panel();
            GamePad4.BackColor = Color.FromArgb(241, 250, 238);
            GamePad4.BorderStyle = BorderStyle.FixedSingle;
            GamePad4.Size = PadSizeGen(9);
            GamePad4.Location = PadLocationGen(4, 9, GamePad4.Size);
            Controls.Add(GamePad4);
            GamePad4.BringToFront();

            Panel GamePad5 = new Panel();
            GamePad5.BackColor = Color.FromArgb(241, 250, 238);
            GamePad5.BorderStyle = BorderStyle.FixedSingle;
            GamePad5.Size = PadSizeGen(9);
            GamePad5.Location = PadLocationGen(5, 9, GamePad5.Size);
            Controls.Add(GamePad5);
            GamePad5.BringToFront();

            Panel GamePad6 = new Panel();
            GamePad6.BackColor = Color.FromArgb(241, 250, 238);
            GamePad6.BorderStyle = BorderStyle.FixedSingle;
            GamePad6.Size = PadSizeGen(9);
            GamePad6.Location = PadLocationGen(6, 9, GamePad6.Size);
            Controls.Add(GamePad6);
            GamePad6.BringToFront();
            #endregion

            #region Third line
            Panel GamePad7 = new Panel();
            GamePad7.BackColor = Color.FromArgb(241, 250, 238);
            GamePad7.BorderStyle = BorderStyle.FixedSingle;
            GamePad7.Size = PadSizeGen(9);
            GamePad7.Location = PadLocationGen(7, 9, GamePad7.Size);
            Controls.Add(GamePad7);
            GamePad7.BringToFront();

            Panel GamePad8 = new Panel();
            GamePad8.BackColor = Color.FromArgb(241, 250, 238);
            GamePad8.BorderStyle = BorderStyle.FixedSingle;
            GamePad8.Size = PadSizeGen(9);
            GamePad8.Location = PadLocationGen(8, 9, GamePad8.Size);
            Controls.Add(GamePad8);
            GamePad8.BringToFront();

            Panel GamePad9 = new Panel();
            GamePad9.BackColor = Color.FromArgb(241, 250, 238);
            GamePad9.BorderStyle = BorderStyle.FixedSingle;
            GamePad9.Size = PadSizeGen(9);
            GamePad9.Location = PadLocationGen(9, 9, GamePad9.Size);
            Controls.Add(GamePad9);
            GamePad9.BringToFront();
            #endregion
        }

        private Point PadLocationGen(int padNumber, int totalPadInThisGame, Size padSize)
        {
            int posX = 0, posY = 0;

            switch (padNumber)
            {
                #region First line
                case 1:
                if (totalPadInThisGame == 9)
                {
                    posX = gamePanel.Location.X;
                    posY = gamePanel.Location.Y;
                }
                break;
                case 2:
                if (totalPadInThisGame == 9)
                {
                    posX = gamePanel.Location.X + padSize.Width;
                    posY = gamePanel.Location.Y;
                }
                break;
                case 3:
                if (totalPadInThisGame == 9)
                {
                    posX = gamePanel.Location.X + padSize.Width * 2;
                    posY = gamePanel.Location.Y;
                }
                break;
                #endregion

                #region Second line 
                case 4:
                if (totalPadInThisGame == 9)
                {
                    posX = gamePanel.Location.X;
                    posY = gamePanel.Location.Y + padSize.Height;
                }
                break;
                case 5:
                if (totalPadInThisGame == 9)
                {
                    posX = gamePanel.Location.X + padSize.Width;
                    posY = gamePanel.Location.Y + padSize.Height;
                }
                break;
                case 6:
                if (totalPadInThisGame == 9)
                {
                    posX = gamePanel.Location.X + padSize.Width * 2;
                    posY = gamePanel.Location.Y + padSize.Height;
                }
                break;
                #endregion

                #region Third line 
                case 7:
                if (totalPadInThisGame == 9)
                {
                    posX = gamePanel.Location.X;
                    posY = gamePanel.Location.Y + padSize.Height * 2;
                }
                break;
                case 8:
                if (totalPadInThisGame == 9)
                {
                    posX = gamePanel.Location.X + padSize.Width;
                    posY = gamePanel.Location.Y + padSize.Height * 2;
                }
                break;
                case 9:
                if (totalPadInThisGame == 9)
                {
                    posX = gamePanel.Location.X + padSize.Width * 2;
                    posY = gamePanel.Location.Y + padSize.Height * 2;
                }
                break;
                #endregion
            }

            return new Point(posX, posY);
        }

        private Size PadSizeGen(int totalPadInThisGame)
        {
            int gWidth = gamePanel.Size.Width / (totalPadInThisGame / 3);
            int gHeight = gamePanel.Size.Height / (totalPadInThisGame / 3);

            return new Size(gWidth, gHeight);
        }
    }
}
