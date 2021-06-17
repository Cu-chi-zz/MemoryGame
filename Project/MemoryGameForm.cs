using MemoryGame.Project.Data;
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
using System.IO;
using MemoryGame.Project.Json;

namespace MemoryGame
{
    public partial class MemoryGameForm : Form
    {
        private List<Panel> gamePads;
        private bool[] selectedPads;
        private bool showingPads = false;
        private int currentScore = 0;
        private System.Media.SoundPlayer goodAnswerSound = new(@"sounds/good-answer.wav");
        private System.Media.SoundPlayer wrongAnswerSound = new(@"sounds/wrong-answer.wav");
        private UserData data = new();
        private Json json = new();

        public MemoryGameForm()
        {
            InitializeComponent();
            if (!Directory.Exists("data") || !File.Exists("data\\udata.json"))
            {
                Directory.CreateDirectory("data");
                FileStream file = File.Create("data\\udata.json");
                file.Close();

                data = new UserData { MaxScore = 0 };

                json.WriteData(data, "data\\udata.json");
            }
            else
            {
                data = json.ReadData("data\\udata.json");
            }

            maxScoreLabel.Text += data.MaxScore.ToString();
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
            List<int> genValues = new List<int>(neededBlackPad); // Initializing an int list which one we're gonna put all generated values for the black pads.

            for (int i = 0; i < neededBlackPad; i++) // Here, we generate value for all needed black pads
            {
                int genValue;

                do
                {
                    genValue = random.Next(padNecessary);
                } while (genValues.Contains(genValue));

                genValues.Add(genValue);
            }

            for (int i = 0; i < padNecessary; i++) // For all pads necessary
            {
                Panel GamePad = new Panel(); // Initialize a new Panel
                if (genValues.Contains(i)) GamePad.BackColor = Color.FromArgb(24, 24, 24); // If i equals a generate value (in genValues list), then set he's color to black.
                else GamePad.BackColor = Color.FromArgb(241, 250, 238);
                GamePad.BorderStyle = BorderStyle.FixedSingle;
                GamePad.Size = PadSizeGen(padNecessary); // Calling this functions is gonna generate a size according to the necessary pads
                GamePad.Location = PadLocationGen(i + 1, padNecessary, GamePad.Size); // Calling this functions is gonna set the panel emplacement according to the necessary pads
                Controls.Add(GamePad);
                GamePad.BringToFront();

                gamePads.Add(GamePad);
            }

            await Task.Delay(showDelay);

            for (int i = 0; i < padNecessary; i++) // After the show delay, we set all panels to white color
            {
                int x = i;
                gamePads[i].BackColor = Color.FromArgb(241, 250, 238);
                gamePads[i].Click += delegate { PadsSelect(x); }; // Associate a function according to the panel
            }

            showingPads = false;

            await Task.Delay(4000);

            bool isCorrect = false;
            
            for (int i = 0; i < selectedPads.Length; i++) // For all panels
            {
                if (selectedPads[i] && genValues.Contains(i)) // If the panel is selected by the user and it is in the generate values,
                {
                    isCorrect = true; // it's a good answer
                }
                else if ((selectedPads[i] && !genValues.Contains(i)) /* If the panel is selected but it is not in the generate values, */ || (!selectedPads[i] && genValues.Contains(i)) /* If the panel is not selected but it is in the generate values, */)
                {
                    isCorrect = false; // it's not a good answer
                    break; // So break the for loop because it's wrong in all cases
                }
            }

            if (isCorrect)
            {
                goodAnswerSound.Play();
                currentScore++;
                scoreLabel.Text = $"Score: {currentScore}";

                for (int i = 0; i < padNecessary; i++) // Reset all panels color to white
                    gamePads[i].BackColor = Color.FromArgb(241, 250, 238);

                if (currentScore >= 0 && currentScore <= 5)
                    Game(9, (currentScore != 0 ? 1000 / currentScore : 1000), (currentScore > 0 ? 1 * currentScore : 2));
                else if (currentScore >= 5 && currentScore <= 12)
                    Game(16, 1000, (currentScore > 5 ? 6 : 3));
                else if (currentScore >= 12)
                    Game(64, 2500, 8);
                else if (currentScore >= 20)
                    Game(100, 3500, 10);
            }
            else
            {
                wrongAnswerSound.Play();

                if (currentScore > data.MaxScore)
                {
                    data = new UserData { MaxScore = currentScore };
                    json.WriteData(data, "data\\udata.json");
                    maxScoreLabel.Text = $"Max score: {data.MaxScore}";
                }

                for (int i = 0; i < padNecessary; i++)
                {
                    if (selectedPads[i] && genValues.Contains(i)) // If the panel is selected by the user and it is in the generate values
                    {
                        gamePads[i].BackColor = Color.FromArgb(65, 255, 65); // Set the panel color to green (to show that was a good answer)
                    }
                    else if (selectedPads[i] && !genValues.Contains(i)) // If the panel is selected but it is not in the generate values
                    {
                        gamePads[i].BackColor = Color.FromArgb(170, 120, 60); // Set the panel color to brown (to show that was a wrong answer)
                    }
                    else if (!selectedPads[i] && genValues.Contains(i)) // If the panel is not selected but it is in the generate values
                    {
                        gamePads[i].BackColor = Color.FromArgb(255, 65, 65); // Set the panel color to red (to show that was a wrong answer)
                    }
                }

                await Task.Delay(3000);

                currentScore = 0;
                scoreLabel.Text = $"Score: {currentScore}";
                startButton.Text = "START";

                for (int i = 0; i < padNecessary; i++) // Reset all panels color to white
                    gamePads[i].BackColor = Color.FromArgb(241, 250, 238);

                startButton.Enabled = true;
            }
        }

        /// <summary>
        /// Generating a position according to the panel index, the total of panels in this game and the panel size.
        /// </summary>
        /// <param name="padNumber">Panel Index</param>
        /// <param name="totalPadInThisGame">Total pads in this game</param>
        /// <param name="padSize">Panel Size</param>
        /// <returns>Panel Point</returns>
        private Point PadLocationGen(int padIndex, int totalPadInThisGame, Size padSize)
        {
            int posX, posY;
            double totalPadsSquareRoot = Math.Sqrt(totalPadInThisGame); // Square root of the totalPadInThisGame used to know panels number for each lines

            int i = 0;
            while (true)
            {
                if (padIndex <= totalPadsSquareRoot) // Then the panel is in the first line
                {
                    posX = gamePanel.Location.X + padSize.Width * (padIndex % (int)totalPadsSquareRoot); // X position plus panel size multiplied by the panel index divided by a euclidean division
                    posY = gamePanel.Location.Y;
                    break;
                }
                else if (padIndex > totalPadsSquareRoot * (i - 1) && padIndex <= totalPadsSquareRoot * i)
                {
                    posX = gamePanel.Location.X + padSize.Width * (padIndex % (int)totalPadsSquareRoot);
                    posY = gamePanel.Location.Y + padSize.Height * (i - 1); // Increase the Y position by the panel size multiplied by the line
                    break;
                }
                i++;
            }

            return new Point(posX, posY);
        }

        /// <summary>
        /// Generate a panel size according to the necessary panels
        /// </summary>
        /// <param name="totalPadInThisGame">Total of pads in this game</param>
        /// <returns>Panel size</returns>
        private Size PadSizeGen(int totalPadInThisGame)
        {
            // The panel size (width & height) divide by the square root of the total panels in this game
            int gWidth = gamePanel.Size.Width / (int)Math.Sqrt(totalPadInThisGame);
            int gHeight = gamePanel.Size.Height / (int)Math.Sqrt(totalPadInThisGame);

            return new Size(gWidth, gHeight);
        }

        /// <summary>
        /// This function is associate with all pads, it set if the panel is selected or not.
        /// </summary>
        /// <param name="x">panel index</param>
        private void PadsSelect(int x)
        {
            if (showingPads) return;

            selectedPads[x] = !selectedPads[x];
            gamePads[x].BackColor = (selectedPads[x] ? Color.FromArgb(190, 190, 190) : Color.FromArgb(241, 250, 238));
        }
    }
}
