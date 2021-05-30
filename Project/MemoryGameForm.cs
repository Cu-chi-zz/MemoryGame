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
        private List<Panel> gamePads = new List<Panel>(144); // 144 correspond to the total of pads necessary for an array of 12x12 

        public MemoryGameForm()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                Panel GamePad = new Panel();
                GamePad.BackColor = Color.FromArgb(241, 250, 238);
                GamePad.BorderStyle = BorderStyle.FixedSingle;
                GamePad.Size = PadSizeGen(9);
                GamePad.Location = PadLocationGen(i + 1, 9, GamePad.Size);
                Controls.Add(GamePad);
                GamePad.BringToFront();

                gamePads.Add(GamePad);
            }

            #region First line
            
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
