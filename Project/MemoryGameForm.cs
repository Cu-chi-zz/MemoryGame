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

        public MemoryGameForm()
        {
            InitializeComponent();
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

            for (int i = 0; i < padNecessary; i++)
            {
                int x = i;
                gamePads[i].BackColor = Color.FromArgb(241, 250, 238);
                gamePads[i].Click += delegate { PadsSelect(x); };
            }

            showingPads = false;

            await Task.Delay(4000);

            bool isCorrect = false;
            
            for (int i = 0; i < selectedPads.Length; i++)
            { // Passage a 16 bon si uniquement 1 de select
                if (selectedPads[i] && genValues.Contains(i))
                {
                    isCorrect = true;
                }
                else if (selectedPads[i] && !genValues.Contains(i))
                {
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

        private void PadsSelect(int x)
        {
            if (showingPads) return;

            selectedPads[x] = !selectedPads[x];
            gamePads[x].BackColor = (selectedPads[x] ? Color.FromArgb(190, 190, 190) : Color.FromArgb(241, 250, 238));
        }
    }
}
