using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class MemoryGameForm : Form
    {
        private List<Panel> gamePads;
        private bool[] selectedPads;
        private bool showingPads = false;
        Dictionary<int, Action<object, EventArgs>> padsFunctions = new Dictionary<int, Action<object, EventArgs>>();

        public MemoryGameForm()
        {
            InitializeComponent();
            padsFunctions.Add(1, PadsSelect1);
            padsFunctions.Add(2, PadsSelect2);
            padsFunctions.Add(3, PadsSelect3);
            padsFunctions.Add(4, PadsSelect4);
            padsFunctions.Add(5, PadsSelect5);
            padsFunctions.Add(6, PadsSelect6);
            padsFunctions.Add(7, PadsSelect7);
            padsFunctions.Add(8, PadsSelect8);
            padsFunctions.Add(9, PadsSelect9);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Game(9);
        }

        private async void Game(int padNecessary)
        {
            startButton.Visible = false;
            confirmButton.Visible = true;
            showingPads = true;

            gamePads = new List<Panel>(padNecessary);
            selectedPads = new bool[padNecessary];

            Random random = new Random();
            int genValue = random.Next(padNecessary);
            for (int i = 0; i < padNecessary; i++)
            {
                Panel GamePad = new Panel();
                if (i == genValue) GamePad.BackColor = Color.FromArgb(24, 24, 24);
                else GamePad.BackColor = Color.FromArgb(241, 250, 238);
                GamePad.BorderStyle = BorderStyle.FixedSingle;
                GamePad.Size = PadSizeGen(padNecessary);
                GamePad.Location = PadLocationGen(i + 1, padNecessary, GamePad.Size);
                Controls.Add(GamePad);
                GamePad.BringToFront();

                gamePads.Add(GamePad);
            }

            await Task.Delay(500);

            for (int i = 0; i < padNecessary; i++)
            {
                gamePads[i].BackColor = Color.FromArgb(241, 250, 238);
                gamePads[i].Click += new EventHandler(padsFunctions[i + 1]);
            }

            showingPads = false;

            await Task.Delay(1000);

            if (selectedPads[genValue]) MessageBox.Show("BON");
            else MessageBox.Show("MAUVAIS");
        }

        private Point PadLocationGen(int padNumber, int totalPadInThisGame, Size padSize)
        {
            int posX = 0, posY = 0;

            switch (padNumber)
            {
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
            }

            return new Point(posX, posY);
        }

        private Size PadSizeGen(int totalPadInThisGame)
        {
            int gWidth = 0, gHeight = 0;

            if (totalPadInThisGame == 9)
            {
                gWidth = gamePanel.Size.Width / (totalPadInThisGame / 3);
                gHeight = gamePanel.Size.Height / (totalPadInThisGame / 3);
            }

            return new Size(gWidth, gHeight);
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            Game(9);
        }

        private void PadsSelect1(object sender, EventArgs e)
        {
            if (showingPads) return;

            for (int i = 0; i < selectedPads.Length; i++)
            {
                if (selectedPads[i] && i != 0)
                {
                    selectedPads[i] = false;
                    gamePads[i].BackColor = Color.FromArgb(241, 250, 238);
                }
            }

            gamePads[0].BackColor = Color.FromArgb(207, 207, 207);
            selectedPads[0] = true;
        }

        private void PadsSelect2(object sender, EventArgs e)
        {
            if (showingPads) return;

            for (int i = 0; i < selectedPads.Length; i++)
            {
                if (selectedPads[i] && i != 1)
                {
                    selectedPads[i] = false;
                    gamePads[i].BackColor = Color.FromArgb(241, 250, 238);
                }
            }

            gamePads[1].BackColor = Color.FromArgb(207, 207, 207);
            selectedPads[1] = true;
        }

        private void PadsSelect3(object sender, EventArgs e)
        {
            if (showingPads) return;

            for (int i = 0; i < selectedPads.Length; i++)
            {
                if (selectedPads[i] && i != 2)
                {
                    selectedPads[i] = false;
                    gamePads[i].BackColor = Color.FromArgb(241, 250, 238);
                }
            }

            gamePads[2].BackColor = Color.FromArgb(207, 207, 207);
            selectedPads[2] = true;
        }

        private void PadsSelect4(object sender, EventArgs e)
        {
            if (showingPads) return;

            for (int i = 0; i < selectedPads.Length; i++)
            {
                if (selectedPads[i] && i != 3)
                {
                    selectedPads[i] = false;
                    gamePads[i].BackColor = Color.FromArgb(241, 250, 238);
                }
            }

            gamePads[3].BackColor = Color.FromArgb(207, 207, 207);
            selectedPads[3] = true;
        }

        private void PadsSelect5(object sender, EventArgs e)
        {
            if (showingPads) return;

            for (int i = 0; i < selectedPads.Length; i++)
            {
                if (selectedPads[i] && i != 4)
                {
                    selectedPads[i] = false;
                    gamePads[i].BackColor = Color.FromArgb(241, 250, 238);
                }
            }

            gamePads[4].BackColor = Color.FromArgb(207, 207, 207);
            selectedPads[4] = true;
        }

        private void PadsSelect6(object sender, EventArgs e)
        {
            if (showingPads) return;

            for (int i = 0; i < selectedPads.Length; i++)
            {
                if (selectedPads[i] && i != 5)
                {
                    selectedPads[i] = false;
                    gamePads[i].BackColor = Color.FromArgb(241, 250, 238);
                }
            }

            gamePads[5].BackColor = Color.FromArgb(207, 207, 207);
            selectedPads[5] = true;
        }

        private void PadsSelect7(object sender, EventArgs e)
        {
            if (showingPads) return;

            for (int i = 0; i < selectedPads.Length; i++)
            {
                if (selectedPads[i] && i != 6)
                {
                    selectedPads[i] = false;
                    gamePads[i].BackColor = Color.FromArgb(241, 250, 238);
                }
            }

            gamePads[6].BackColor = Color.FromArgb(207, 207, 207);
            selectedPads[6] = true;
        }

        private void PadsSelect8(object sender, EventArgs e)
        {
            if (showingPads) return;

            for (int i = 0; i < selectedPads.Length; i++)
            {
                if (selectedPads[i] && i != 7)
                {
                    selectedPads[i] = false;
                    gamePads[i].BackColor = Color.FromArgb(241, 250, 238);
                }
            }

            gamePads[7].BackColor = Color.FromArgb(207, 207, 207);
            selectedPads[7] = true;
        }

        private void PadsSelect9(object sender, EventArgs e)
        {
            if (showingPads) return;

            for (int i = 0; i < selectedPads.Length; i++)
            {
                if (selectedPads[i] && i != 8)
                {
                    selectedPads[i] = false;
                    gamePads[i].BackColor = Color.FromArgb(241, 250, 238);
                }
            }

            gamePads[8].BackColor = Color.FromArgb(207, 207, 207);
            selectedPads[8] = true;
        }
    }
}
