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
            padsFunctions.Add(19, PadsSelect19);
            padsFunctions.Add(20, PadsSelect20);
            padsFunctions.Add(21, PadsSelect21);
            padsFunctions.Add(22, PadsSelect22);
            padsFunctions.Add(23, PadsSelect23);
            padsFunctions.Add(24, PadsSelect24);
            padsFunctions.Add(25, PadsSelect25);
            padsFunctions.Add(26, PadsSelect26);
            padsFunctions.Add(27, PadsSelect27);
            padsFunctions.Add(28, PadsSelect28);
            padsFunctions.Add(29, PadsSelect29);
            padsFunctions.Add(30, PadsSelect30);
            padsFunctions.Add(31, PadsSelect31);
            padsFunctions.Add(32, PadsSelect32);
            padsFunctions.Add(33, PadsSelect33);
            padsFunctions.Add(34, PadsSelect34);
            padsFunctions.Add(35, PadsSelect35);
            padsFunctions.Add(36, PadsSelect36);
            padsFunctions.Add(37, PadsSelect37);
            padsFunctions.Add(38, PadsSelect38);
            padsFunctions.Add(39, PadsSelect39);
            padsFunctions.Add(40, PadsSelect40);
            padsFunctions.Add(41, PadsSelect41);
            padsFunctions.Add(42, PadsSelect42);
            padsFunctions.Add(43, PadsSelect43);
            padsFunctions.Add(44, PadsSelect44);
            padsFunctions.Add(45, PadsSelect45);
            padsFunctions.Add(46, PadsSelect46);
            padsFunctions.Add(47, PadsSelect47);
            padsFunctions.Add(48, PadsSelect48);
            padsFunctions.Add(49, PadsSelect49);
            padsFunctions.Add(50, PadsSelect50);
            padsFunctions.Add(51, PadsSelect51);
            padsFunctions.Add(52, PadsSelect52);
            padsFunctions.Add(53, PadsSelect53);
            padsFunctions.Add(54, PadsSelect54);
            padsFunctions.Add(55, PadsSelect55);
            padsFunctions.Add(56, PadsSelect56);
            padsFunctions.Add(57, PadsSelect57);
            padsFunctions.Add(58, PadsSelect58);
            padsFunctions.Add(59, PadsSelect59);
            padsFunctions.Add(60, PadsSelect60);
            padsFunctions.Add(61, PadsSelect61);
            padsFunctions.Add(62, PadsSelect62);
            padsFunctions.Add(63, PadsSelect63);
            padsFunctions.Add(64, PadsSelect64);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Game(9, 1000, 1);
        }

        private async void Game(int padNecessary, int showDelay, int neededBlackPad)
        {
            startButton.Enabled = false;
            startButton.Text = "IN GAME";
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

            for(int i = 0; i < padNecessary; i++)
            {
                gamePads[i].BackColor = Color.FromArgb(241, 250, 238);
                gamePads[i].Click += new EventHandler(padsFunctions[i + 1]);
            }

            showingPads = false;

            await Task.Delay(4000);

            bool isCorrect = false;
            
            for (int i = 0; i < selectedPads.Length; i++)
            {
                if (selectedPads[i] && genValues.Contains(i))
                {
                    MessageBox.Show($"Correct");
                    isCorrect = true;
                }
                else if (selectedPads[i] && !genValues.Contains(i))
                {
                    MessageBox.Show($"Mauvais: {i}");
                    isCorrect = false;
                    selectedPads[i] = false;
                    break;
                }

                selectedPads[i] = false;
            }

            if (isCorrect)
            {
                goodAnswerSound.Play();
                currentScore++;
                scoreLabel.Text = $"Score: {currentScore}";

                for (int i = 0; i < padNecessary; i++)
                    gamePads[i].BackColor = Color.FromArgb(241, 250, 238);

                if (currentScore >= 0 && currentScore <= 5)
                    Game(9, (currentScore != 0 ? 1000 / currentScore : 1000), (currentScore > 0 ? 1 * currentScore : 2));
                else if (currentScore >= 5 && currentScore <= 12)
                    Game(16, 1000, (currentScore > 5 ? 6 : 3));
                else if (currentScore >= 12)
                    Game(64, 1500, 8);
            }
            else
            {
                wrongAnswerSound.Play();
                currentScore = 0;
                scoreLabel.Text = $"Score: {currentScore}";
                startButton.Text = "START";

                for (int i = 0; i < padNecessary; i++)
                    gamePads[i].BackColor = Color.FromArgb(241, 250, 238);

                startButton.Enabled = true;
            }
        }

        private Point PadLocationGen(int padNumber, int totalPadInThisGame, Size padSize)
        {
            int posX, posY;
            double totalPadsSquare = Math.Sqrt(totalPadInThisGame);

            int i = 0;
            while (true)
            {
                if (padNumber <= totalPadsSquare)
                {
                    posX = gamePanel.Location.X + padSize.Width * (padNumber % (int)totalPadsSquare);
                    posY = gamePanel.Location.Y;
                    break;
                }
                else if (padNumber > totalPadsSquare * (i - 1) && padNumber <= totalPadsSquare * i)
                {
                    posX = gamePanel.Location.X + padSize.Width * (padNumber % (int)totalPadsSquare);
                    posY = gamePanel.Location.Y + padSize.Height * (i - 1);
                    break;
                }
                i++;
            }

            return new Point(posX, posY);
        }

        private Size PadSizeGen(int totalPadInThisGame)
        {
            int gWidth = gamePanel.Size.Width / (int)(Math.Sqrt(totalPadInThisGame));
            int gHeight = gamePanel.Size.Height / (int)(Math.Sqrt(totalPadInThisGame));

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

        private void PadsSelect19(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[18])
            {
                selectedPads[18] = false;
                gamePads[18].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[18].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[18] = true;
            }
        }
        private void PadsSelect20(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[19])
            {
                selectedPads[19] = false;
                gamePads[19].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[19].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[19] = true;
            }
        }
        private void PadsSelect21(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[20])
            {
                selectedPads[20] = false;
                gamePads[20].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[20].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[20] = true;
            }
        }
        private void PadsSelect22(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[21])
            {
                selectedPads[21] = false;
                gamePads[21].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[21].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[21] = true;
            }
        }
        private void PadsSelect23(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[22])
            {
                selectedPads[22] = false;
                gamePads[22].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[22].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[22] = true;
            }
        }
        private void PadsSelect24(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[23])
            {
                selectedPads[23] = false;
                gamePads[23].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[23].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[23] = true;
            }
        }
        private void PadsSelect25(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[24])
            {
                selectedPads[24] = false;
                gamePads[24].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[24].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[24] = true;
            }
        }
        private void PadsSelect26(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[25])
            {
                selectedPads[25] = false;
                gamePads[25].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[25].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[25] = true;
            }
        }
        private void PadsSelect27(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[26])
            {
                selectedPads[26] = false;
                gamePads[26].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[26].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[26] = true;
            }
        }
        private void PadsSelect28(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[27])
            {
                selectedPads[27] = false;
                gamePads[27].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[27].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[27] = true;
            }
        }
        private void PadsSelect29(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[28])
            {
                selectedPads[28] = false;
                gamePads[28].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[28].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[28] = true;
            }
        }
        private void PadsSelect30(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[29])
            {
                selectedPads[29] = false;
                gamePads[29].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[29].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[29] = true;
            }
        }
        private void PadsSelect31(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[30])
            {
                selectedPads[30] = false;
                gamePads[30].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[30].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[30] = true;
            }
        }
        private void PadsSelect32(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[31])
            {
                selectedPads[31] = false;
                gamePads[31].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[31].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[31] = true;
            }
        }

        private void PadsSelect33(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[32])
            {
                selectedPads[32] = false;
                gamePads[32].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[32].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[32] = true;
            }
        }
        private void PadsSelect34(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[33])
            {
                selectedPads[33] = false;
                gamePads[33].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[33].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[33] = true;
            }
        }
        private void PadsSelect35(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[34])
            {
                selectedPads[34] = false;
                gamePads[34].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[34].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[34] = true;
            }
        }
        private void PadsSelect36(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[35])
            {
                selectedPads[35] = false;
                gamePads[35].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[35].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[35] = true;
            }
        }
        private void PadsSelect37(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[36])
            {
                selectedPads[36] = false;
                gamePads[36].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[36].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[36] = true;
            }
        }
        private void PadsSelect38(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[37])
            {
                selectedPads[37] = false;
                gamePads[37].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[37].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[37] = true;
            }
        }
        private void PadsSelect39(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[38])
            {
                selectedPads[38] = false;
                gamePads[38].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[38].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[38] = true;
            }
        }
        private void PadsSelect40(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[39])
            {
                selectedPads[39] = false;
                gamePads[39].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[39].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[39] = true;
            }
        }
        private void PadsSelect41(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[40])
            {
                selectedPads[40] = false;
                gamePads[40].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[40].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[40] = true;
            }
        }
        private void PadsSelect42(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[41])
            {
                selectedPads[41] = false;
                gamePads[41].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[41].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[41] = true;
            }
        }
        private void PadsSelect43(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[42])
            {
                selectedPads[42] = false;
                gamePads[42].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[42].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[42] = true;
            }
        }
        private void PadsSelect44(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[43])
            {
                selectedPads[43] = false;
                gamePads[43].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[43].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[43] = true;
            }
        }
        private void PadsSelect45(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[44])
            {
                selectedPads[44] = false;
                gamePads[44].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[44].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[44] = true;
            }
        }
        private void PadsSelect46(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[45])
            {
                selectedPads[45] = false;
                gamePads[45].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[45].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[45] = true;
            }
        }
        private void PadsSelect47(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[46])
            {
                selectedPads[46] = false;
                gamePads[46].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[46].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[46] = true;
            }
        }
        private void PadsSelect48(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[47])
            {
                selectedPads[47] = false;
                gamePads[47].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[47].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[47] = true;
            }
        }
        private void PadsSelect49(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[48])
            {
                selectedPads[48] = false;
                gamePads[48].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[48].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[48] = true;
            }
        }
        private void PadsSelect50(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[49])
            {
                selectedPads[49] = false;
                gamePads[49].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[49].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[49] = true;
            }
        }
        private void PadsSelect51(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[50])
            {
                selectedPads[50] = false;
                gamePads[50].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[50].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[50] = true;
            }
        }
        private void PadsSelect52(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[51])
            {
                selectedPads[51] = false;
                gamePads[51].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[51].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[51] = true;
            }
        }
        private void PadsSelect53(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[52])
            {
                selectedPads[52] = false;
                gamePads[52].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[52].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[52] = true;
            }
        }
        private void PadsSelect54(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[53])
            {
                selectedPads[53] = false;
                gamePads[53].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[53].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[53] = true;
            }
        }
        private void PadsSelect55(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[54])
            {
                selectedPads[54] = false;
                gamePads[54].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[54].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[54] = true;
            }
        }
        private void PadsSelect56(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[55])
            {
                selectedPads[55] = false;
                gamePads[55].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[55].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[55] = true;
            }
        }
        private void PadsSelect57(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[56])
            {
                selectedPads[56] = false;
                gamePads[56].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[56].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[56] = true;
            }
        }
        private void PadsSelect58(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[57])
            {
                selectedPads[57] = false;
                gamePads[57].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[57].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[57] = true;
            }
        }
        private void PadsSelect59(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[58])
            {
                selectedPads[58] = false;
                gamePads[58].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[58].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[58] = true;
            }
        }
        private void PadsSelect60(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[59])
            {
                selectedPads[59] = false;
                gamePads[59].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[59].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[59] = true;
            }
        }
        private void PadsSelect61(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[60])
            {
                selectedPads[60] = false;
                gamePads[60].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[60].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[60] = true;
            }
        }
        private void PadsSelect62(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[61])
            {
                selectedPads[61] = false;
                gamePads[61].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[61].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[61] = true;
            }
        }
        private void PadsSelect63(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[62])
            {
                selectedPads[62] = false;
                gamePads[62].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[62].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[62] = true;
            }
        }
        private void PadsSelect64(object sender, EventArgs e)
        {
            if (showingPads) return;

            if (selectedPads[63])
            {
                selectedPads[63] = false;
                gamePads[63].BackColor = Color.FromArgb(241, 250, 238);
            }
            else
            {
                gamePads[63].BackColor = Color.FromArgb(190, 190, 190);
                selectedPads[63] = true;
            }
        }
    }
}
