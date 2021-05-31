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
        private int currentScore = 0;
        private System.Media.SoundPlayer goodAnswerSound = new System.Media.SoundPlayer(@"sounds/good-answer.wav");
        private System.Media.SoundPlayer wrongAnswerSound = new System.Media.SoundPlayer(@"sounds/wrong-answer.wav");
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

            padsFunctions.Add(10, PadsSelect10);
            padsFunctions.Add(11, PadsSelect11);
            padsFunctions.Add(12, PadsSelect12);
            padsFunctions.Add(13, PadsSelect13);
            padsFunctions.Add(14, PadsSelect14);
            padsFunctions.Add(15, PadsSelect15);
            padsFunctions.Add(16, PadsSelect16);
            padsFunctions.Add(17, PadsSelect17);
            padsFunctions.Add(18, PadsSelect18);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (currentScore >= 0 && currentScore <= 5)
                Game(9, (currentScore != 0 ? 1000 / currentScore : 1000), (currentScore > 0 ? 1 * currentScore : 2));
            else if (currentScore >= 5)
                Game(16, (currentScore != 0 ? 1000 / currentScore : 1000), (currentScore > 5 ? 6 : 3));
        }

        private async void Game(int padNecessary, int showDelay, int neededBlackPad)
        {
            startButton.Enabled = false;
            startButton.Text = "CONTINUE";
            showingPads = true;

            gamePads = new List<Panel>(padNecessary);
            selectedPads = new bool[padNecessary];

            Random random = new Random();
            List<int> genValues = new List<int>(neededBlackPad);

            for (int i = 0; i < neededBlackPad; i++)
            {
                int genValue;

                do
                {
                    genValue = random.Next(padNecessary);
                } while (genValues.Contains(genValue));

                genValues.Add(genValue);
            }

            for (int i = 0; i < padNecessary; i++)
            {
                Panel GamePad = new Panel();
                if (genValues.Contains(i)) GamePad.BackColor = Color.FromArgb(24, 24, 24);
                else GamePad.BackColor = Color.FromArgb(241, 250, 238);
                GamePad.BorderStyle = BorderStyle.FixedSingle;
                GamePad.Size = PadSizeGen(padNecessary);
                GamePad.Location = PadLocationGen(i + 1, padNecessary, GamePad.Size);
                Controls.Add(GamePad);
                GamePad.BringToFront();

                gamePads.Add(GamePad);
            }

            await Task.Delay(showDelay);

            for (int i = 0; i < padNecessary; i++)
            {
                gamePads[i].BackColor = Color.FromArgb(241, 250, 238);
                gamePads[i].Click += new EventHandler(padsFunctions[i + 1]);
            }

            showingPads = false;

            await Task.Delay(4000);

            startButton.Enabled = true;

            bool isCorrect = false;
            for (int i = 0; i < selectedPads.Length; i++)
            {
                if (selectedPads[i] && genValues.Contains(i))
                {
                    isCorrect = true;
                }
                else if (selectedPads[i] && !genValues.Contains(i))
                {
                    isCorrect = false;
                    break;
                }
            }

            if (isCorrect)
            {
                goodAnswerSound.Play();
                currentScore++;
                scoreLabel.Text = $"Score: {currentScore}";
                selectedPads = null;

                for (int i = 0; i < padNecessary; i++)
                    gamePads[i].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                wrongAnswerSound.Play();
                currentScore = 0;
                scoreLabel.Text = $"Score: {currentScore}";

                for (int i = 0; i < padNecessary; i++)
                    gamePads[i].BackColor = Color.FromArgb(241, 250, 238);
            }
        }

        private Point PadLocationGen(int padNumber, int totalPadInThisGame, Size padSize)
        {
            int posX = 0, posY = 0;

            if (totalPadInThisGame == 9)
            {
                switch (padNumber)
                {
                    case 1:
                        posX = gamePanel.Location.X;
                        posY = gamePanel.Location.Y;
                        break;
                    case 2:
                        posX = gamePanel.Location.X + padSize.Width;
                        posY = gamePanel.Location.Y;
                        break;
                    case 3:
                        posX = gamePanel.Location.X + padSize.Width * 2;
                        posY = gamePanel.Location.Y;
                        break;

                    case 4:
                        posX = gamePanel.Location.X;
                        posY = gamePanel.Location.Y + padSize.Height;
                        break;
                    case 5:
                        posX = gamePanel.Location.X + padSize.Width;
                        posY = gamePanel.Location.Y + padSize.Height;
                        break;
                    case 6:
                        posX = gamePanel.Location.X + padSize.Width * 2;
                        posY = gamePanel.Location.Y + padSize.Height;
                        break;

                    case 7:
                        posX = gamePanel.Location.X;
                        posY = gamePanel.Location.Y + padSize.Height * 2;
                        break;
                    case 8:
                        posX = gamePanel.Location.X + padSize.Width;
                        posY = gamePanel.Location.Y + padSize.Height * 2;
                        break;
                    case 9:
                        posX = gamePanel.Location.X + padSize.Width * 2;
                        posY = gamePanel.Location.Y + padSize.Height * 2;
                        break;
                }
            }
            else if (totalPadInThisGame == 16)
            {
                switch (padNumber)
                {
                    case 1:
                    posX = gamePanel.Location.X;
                    posY = gamePanel.Location.Y;
                    break;
                    case 2:
                    posX = gamePanel.Location.X + padSize.Width;
                    posY = gamePanel.Location.Y;
                    break;
                    case 3:
                    posX = gamePanel.Location.X + padSize.Width * 2;
                    posY = gamePanel.Location.Y;
                    break;
                    case 4:
                    posX = gamePanel.Location.X + padSize.Width * 3;
                    posY = gamePanel.Location.Y;
                    break;

                    case 5:
                    posX = gamePanel.Location.X;
                    posY = gamePanel.Location.Y + padSize.Height;
                    break;
                    case 6:
                    posX = gamePanel.Location.X + padSize.Width;
                    posY = gamePanel.Location.Y + padSize.Height;
                    break;
                    case 7:
                    posX = gamePanel.Location.X + padSize.Width * 2;
                    posY = gamePanel.Location.Y + padSize.Height;
                    break;
                    case 8:
                    posX = gamePanel.Location.X + padSize.Width * 3;
                    posY = gamePanel.Location.Y + padSize.Height;
                    break;

                    case 9:
                    posX = gamePanel.Location.X;
                    posY = gamePanel.Location.Y + padSize.Height * 2;
                    break;
                    case 10:
                    posX = gamePanel.Location.X + padSize.Width;
                    posY = gamePanel.Location.Y + padSize.Height * 2;
                    break;
                    case 11:
                    posX = gamePanel.Location.X + padSize.Width * 2;
                    posY = gamePanel.Location.Y + padSize.Height * 2;
                    break;
                    case 12:
                    posX = gamePanel.Location.X + padSize.Width * 3;
                    posY = gamePanel.Location.Y + padSize.Height * 2;
                    break;

                    case 13:
                    posX = gamePanel.Location.X;
                    posY = gamePanel.Location.Y + padSize.Height * 3;
                    break;
                    case 14:
                    posX = gamePanel.Location.X + padSize.Width;
                    posY = gamePanel.Location.Y + padSize.Height * 3;
                    break;
                    case 15:
                    posX = gamePanel.Location.X + padSize.Width * 2;
                    posY = gamePanel.Location.Y + padSize.Height * 3;
                    break;
                    case 16:
                    posX = gamePanel.Location.X + padSize.Width * 3;
                    posY = gamePanel.Location.Y + padSize.Height * 3;
                    break;
                }
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
            else if (totalPadInThisGame == 16)
            {
                gWidth = gamePanel.Size.Width / (totalPadInThisGame / 4);
                gHeight = gamePanel.Size.Height / (totalPadInThisGame / 4);
            }

            return new Size(gWidth, gHeight);
        }

        private void PadsSelect1(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[0])
            {
                selectedPads[0] = false;
                gamePads[0].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[0].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[0] = true;
            }
        }

        private void PadsSelect2(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[1])
            {
                selectedPads[1] = false;
                gamePads[1].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[1].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[1] = true;
            }
        }

        private void PadsSelect3(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[2])
            {
                selectedPads[2] = false;
                gamePads[2].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[2].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[2] = true;
            }
        }

        private void PadsSelect4(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[3])
            {
                selectedPads[3] = false;
                gamePads[3].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[3].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[3] = true;
            }
        }

        private void PadsSelect5(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[4])
            {
                selectedPads[4] = false;
                gamePads[4].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[4].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[4] = true;
            }
        }

        private void PadsSelect6(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[5])
            {
                selectedPads[5] = false;
                gamePads[5].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[5].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[5] = true;
            }
        }

        private void PadsSelect7(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[6])
            {
                selectedPads[6] = false;
                gamePads[6].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[6].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[6] = true;
            }
        }

        private void PadsSelect8(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[7])
            {
                selectedPads[7] = false;
                gamePads[7].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[7].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[7] = true;
            }
        }

        private void PadsSelect9(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[8])
            {
                selectedPads[8] = false;
                gamePads[8].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[8].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[8] = true;
            }
        }

        private void PadsSelect10(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[9])
            {
                selectedPads[9] = false;
                gamePads[9].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[9].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[9] = true;
            }
        }

        private void PadsSelect11(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[10])
            {
                selectedPads[10] = false;
                gamePads[10].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[10].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[10] = true;
            }
        }

        private void PadsSelect12(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[11])
            {
                selectedPads[11] = false;
                gamePads[11].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[11].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[11] = true;
            }
        }

        private void PadsSelect13(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[12])
            {
                selectedPads[12] = false;
                gamePads[12].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[12].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[12] = true;
            }
        }

        private void PadsSelect14(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[13])
            {
                selectedPads[13] = false;
                gamePads[13].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[13].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[13] = true;
            }
        }

        private void PadsSelect15(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[14])
            {
                selectedPads[14] = false;
                gamePads[14].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[14].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[14] = true;
            }
        }

        private void PadsSelect16(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[15])
            {
                selectedPads[15] = false;
                gamePads[15].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[15].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[15] = true;
            }
        }

        private void PadsSelect17(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[16])
            {
                selectedPads[16] = false;
                gamePads[16].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[16].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[16] = true;
            }
        }

        private void PadsSelect18(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[17])
            {
                selectedPads[17] = false;
                gamePads[17].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[17].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[17] = true;
            }
        }
    }
}
