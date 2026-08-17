using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe_Game.Properties;

namespace Tic_Tac_Toe_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        stGameStatus GameStatus;
        enPlayers PlayerTrun = enPlayers.Player1;



        enum enPlayers
        {
            Player1,
            Player2
        }
        enum enWinner
        {
            Player1,
            Player2,
            Draw,
            GameInProgress
        }
        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;
        }

        //Logical Part
        void UpdateBtn(Button Btn)
        {
            if (Btn.Tag.ToString() == "?")
            {
                switch (PlayerTrun)
                {
                    case enPlayers.Player1:
                
                    Btn.Tag = "x";
                    Btn.Image = Resources.X1;
                    PlayerTrun = enPlayers.Player2;
                    GameStatus.PlayCount++;
                    lblPlayers.Text = "Player2";
                    CheckWinner();
                    break;


                case enPlayers.Player2:
                    Btn.Tag = "o";
                    Btn.Image = Resources.O1;
                    PlayerTrun = enPlayers.Player1;
                    lblPlayers.Text = "Player1";
                    CheckWinner();
                    break;


                }
                

            }
            else {
                MessageBox.Show("Wrong Choice", "Worng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if(GameStatus.PlayCount == 9)
            {
                GameStatus.GameOver = true;
                GameStatus.Winner = enWinner.Draw;
                EndGame();
            }
        }
        void EndGame()
        {
            lblPlayers.Text = "Game Over";
            switch (GameStatus.Winner)
            {
                case enWinner.Player1:
                    lblProgress.Text = "Player1";
                    break; 

                case enWinner.Player2:
                    lblProgress.Text = "Player2";
                    break;

                default:
                    lblProgress.Text = "Draw";
                    break;
                
            }
            MessageBox.Show("Game Over", "Final Result", MessageBoxButtons.OK);
        }
        bool CheckValues(Button button1 , Button button2 , Button button3)
        {
            if (button1.Tag.ToString() != "?" &&
                button1.Tag.ToString() == button2.Tag.ToString() &&
                button1.Tag.ToString() == button3.Tag.ToString()
                )
            {
                button1.BackColor = Color.GreenYellow;
                button2.BackColor = Color.GreenYellow;
                button3.BackColor = Color.GreenYellow;

                if (button1.Tag.ToString() == "x")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    
                    return true;
                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    
                    EndGame();
                    return true;
                }
                
            }
            GameStatus.GameOver = false;
            return false;
        }
        void CheckWinner()
        {
            //Rows
            if (CheckValues(btn1, btn2, btn3))
                return;

            if (CheckValues(btn4, btn5, btn6))
                return;

            if (CheckValues(btn7, btn8, btn9))
                return;

            //Colmns
            if (CheckValues(btn1, btn4, btn7))
                return;

            if (CheckValues(btn2, btn5, btn8))
                return;

            if (CheckValues(btn3, btn6, btn9))
                return;

            //Diagonal
            if (CheckValues(btn1, btn5, btn9))
                return;

            if (CheckValues(btn3, btn5, btn7))
                return;
        }
        void ResButton(Button btn)
        {
            btn.Tag = "?";
            btn.Image = Resources.question_mark_96;
            btn.BackColor = Color.Transparent;
        }
        void RestGame()
        {
            ResButton(btn1);
            ResButton(btn2);
            ResButton(btn3);
            ResButton(btn4);
            ResButton(btn5);
            ResButton(btn6);
            ResButton(btn7);
            ResButton(btn8);
            ResButton(btn9);

            PlayerTrun = enPlayers.Player1;
            GameStatus.PlayCount = 0;
            GameStatus.Winner = enWinner.GameInProgress;
            lblPlayers.Text = "Player1";
            GameStatus.GameOver = false;
            lblWinner.Text = "In Progress";

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen whitePen = new Pen(Color.White);
            whitePen.Width = 10;

            //draw Horizental lines
            e.Graphics.DrawLine(whitePen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(whitePen, 400, 460, 1050, 460);

            //draw Vertical lines
            e.Graphics.DrawLine(whitePen, 610, 140, 610, 620);
            e.Graphics.DrawLine(whitePen, 840, 140, 840, 620);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            UpdateBtn((Button)sender);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestGame();
        }
    }
}
