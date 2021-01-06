using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using WMPLib;

namespace PlatformGame
{
    public partial class Form1 : Form
    {


        WindowsMediaPlayer sound = new WindowsMediaPlayer();
        bool goLeft, goRight, jumping, isGameOver;
        int jumpSpeed;
        int force;
        int score = 0;
        int playerSpeed = 7;

        
        

        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;
        public Form1()
        {
            InitializeComponent();
            sound.URL = "thememusic.MP3";
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;

            player.Top += jumpSpeed;
            if (goLeft == true)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true)
            {
                player.Left += playerSpeed;
            }
            if (jumping == true && force < 0)
            {
                jumping = false;
            }
            if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;


                            if((string)x.Name == "horizontalPlatform" && goLeft == false || (string)x.Name == "horizontalPlatform" && goRight == false)
                            {
                                player.Left -= horizontalSpeed;
                            }
                        }
                        x.BringToFront();
                    }


                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                      
                    }
                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            txtScore.Text = "Score :" + score + Environment.NewLine + "You were killed";
                        }
                        x.BringToFront();
                    }
                }
            }

            horizontalPlatform.Left -= horizontalSpeed;
            if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;

            }
            verticalPlatform.Top += verticalSpeed;

            if (verticalPlatform.Top < 300 || verticalPlatform.Top > 484)
            {
                verticalSpeed = -verticalSpeed;
            }
            //603//484

            enemyOne.Left -= enemyOneSpeed;

            if(enemyOne.Left < pictureBox4.Left || enemyOne.Left + enemyOne.Width > pictureBox4.Left + pictureBox4.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }
            enemyTwo.Left -= enemyTwoSpeed;

            if (enemyTwo.Left < pictureBox2.Left || enemyTwo.Left + enemyTwo.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            if(player.Top + player.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                MessageBox.Show("Game Over");
                txtScore.Text = "Score : " + score + Environment.NewLine + "You fell to your death";
            }

            if (player.Bounds.IntersectsWith(door.Bounds) && score == 13) //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = " Score : " + score + Environment.NewLine + " Your quest is complete ";
            }
            else
            {
                txtScore.Text = " Score : " + score + Environment.NewLine + " Collect all coins! ";

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sound.controls.play();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
                player.Image = Properties.Resources.klf;
            }
            if(e.KeyCode == Keys.Right)
            {
                goRight = true;
                player.Image = Properties.Resources.krf1;

            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;

                if(player.Image == Properties.Resources.klf)
                {
                    player.Image = Properties.Resources.kjlf;
                }
                if(player.Image == Properties.Resources.klf)
                {
                    player.Image = Properties.Resources.krf1;
                }

            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if(jumping == true)
            {
                jumping = false;
            }
            if(e.KeyCode == Keys.Enter && isGameOver == true)
            {
                RestartGame();
            }
        }
        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;


            txtScore.Text = "Score: " + score;

            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            player.Left = 26;
            player.Top = 696;

            enemyOne.Left = 444;
            enemyTwo.Left = 604;

            horizontalPlatform.Left = 347;
            verticalPlatform.Top = 532;

            gameTimer.Start();
        }
    }
}
