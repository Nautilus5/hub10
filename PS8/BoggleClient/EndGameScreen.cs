using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoggleClient
{
    public partial class EndGameScreen : Form
    {
        public Client client;
        public EndGameScreen()
        {
            InitializeComponent();

            // Set up all the score data for player1 which is local player.
            Player1textBox.Text = client.PlayerLocal.Nickname;
            string player1scoreboard = "";
            foreach (Tuple<string, int> WordScore in client.wordlist1)
            {
                player1scoreboard += WordScore.Item1 + ": ";
                player1scoreboard += WordScore.Item2 + Environment.NewLine;

            }
            Player1ScoretextBox.Text = player1scoreboard;
            P1ScoretextBox.Text = client.PlayerLocal.Score;

            // Set up all the score data for player2 which is the other player.
            Player2textBox.Text = client.AnotherPlayer.Nickname;
            string player2scoreboard = "";
            foreach (Tuple<dynamic, dynamic> WordScore in client.wordlist2)
            {
                player2scoreboard += WordScore.Item1 + ": ";
                player2scoreboard += WordScore.Item2 + Environment.NewLine;
            }
            Player2ScoretextBox.Text = player2scoreboard;
            P2ScoretextBox.Text = client.AnotherPlayer.Score;
        }

        //this.Close();//play again


        //Environment.Exit(1);//quit game program

        private void EndGameScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
