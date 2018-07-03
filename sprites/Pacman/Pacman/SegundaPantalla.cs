using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    public partial class SegundaPantalla : Form
    {
        bool goup;
        bool godown;
        bool goleft;
        bool goright;
        int speed = 5;
        Random random = new Random();
        int ghost1x = 7;
        int ghost1y = 7;
        int ghost2x = 7;
        int ghost2y = 7;
        int score = 0;
        string user;
    
        public SegundaPantalla(string user)
        {
            InitializeComponent();
            label2.Visible = false;
            this.user = user;
          

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
                pacman.Image = Properties.Resources.pacman_left;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
                pacman.Image = Properties.Resources.pacman_right;
            }
            if (e.KeyCode == Keys.Up)
            {
                goup = true;
                pacman.Image = Properties.Resources.pacman_up;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = true;
                pacman.Image = Properties.Resources.pacman_down;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Score: " + score;
            if (goleft)
            {
                pacman.Left -= speed;
            }
            if (goright)
            {
                pacman.Left += speed;
            }
            if (goup)
            {
                pacman.Top -= speed;
            }
            if (godown)
            {
                pacman.Top += speed;
            }
            foreach(Control x in this.Controls)
            {
                if (x is PictureBox &&  x.Tag=="wall" || x.Tag=="ghost")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(pacman.Bounds))
                    {
                        Record newrecord = new Record(user, score);
                        label2.Text = "Game Over";
                        label2.Visible = true;
                        timer1.Stop();
                        MessageBox.Show("User: " + user + ". Score: " + score);
                        BaseDeDatos b1 = new BaseDeDatos();
                        List<Record> records = b1.DesRecords();
                        records.Add(newrecord);
                        List<Record> recList = records.OrderByDescending(rec => rec.score).ToList();
                        string g = "-------Tabla historica-------\n";
                        int lugar = 1;
                        foreach (Record rec in recList)
                        {
                            g += lugar + ".- User: " + rec.usuario + ". Su score: " + rec.score + ".\n";
                            lugar++;
                        }
                        MessageBox.Show(g);
                        b1.SerRecords(records);
                        this.Hide();
                        Form1 inicio = new Form1();
                        inicio.ShowDialog();
                    }

                }
                if (x is PictureBox && x.Tag == "fruit")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(pacman.Bounds))
                    {
                        if (x.Name == "cherry")
                        {
                            x.Left = 357;
                            x.Top = 7;
                            score += 10;
                        }
                        if (x.Name == "melon")
                        {
                            x.Left = 390;
                            x.Top = 7;
                            score += 15;
                        }
                        
                    }
                }
            }
            pinkGhost.Left += ghost1x;
            pinkGhost.Top += ghost1y;
            redGhost.Left += ghost2x;
            redGhost.Top += ghost2y;
            if (pinkGhost.Left<1||pinkGhost.Left+pinkGhost.Width> ClientSize.Width-2||(pinkGhost.Bounds.IntersectsWith(pictureBox1.Bounds))|| (pinkGhost.Bounds.IntersectsWith(pictureBox2.Bounds))|| (pinkGhost.Bounds.IntersectsWith(pictureBox3.Bounds))|| (pinkGhost.Bounds.IntersectsWith(pictureBox4.Bounds)))
            {
                ghost1x = -ghost1x;
            }
            if (pinkGhost.Top<1||pinkGhost.Top + pinkGhost.Height > ClientSize.Height - 2 || (pinkGhost.Bounds.IntersectsWith(pictureBox1.Bounds)) || (pinkGhost.Bounds.IntersectsWith(pictureBox2.Bounds)) || (pinkGhost.Bounds.IntersectsWith(pictureBox3.Bounds)) || (pinkGhost.Bounds.IntersectsWith(pictureBox4.Bounds)))
            {
                ghost1y = -ghost1y;
            }
            
            if (redGhost.Left < 1 || redGhost.Left + redGhost.Width > ClientSize.Width - 2 || (redGhost.Bounds.IntersectsWith(pictureBox1.Bounds)) || (redGhost.Bounds.IntersectsWith(pictureBox2.Bounds)) || (redGhost.Bounds.IntersectsWith(pictureBox3.Bounds)) || (redGhost.Bounds.IntersectsWith(pictureBox4.Bounds)))
            {
                ghost2x = -ghost2x;
            }
            if (redGhost.Top < 1 || redGhost.Top + pinkGhost.Height > ClientSize.Height - 2 || (redGhost.Bounds.IntersectsWith(pictureBox1.Bounds)) || (redGhost.Bounds.IntersectsWith(pictureBox2.Bounds)) || (redGhost.Bounds.IntersectsWith(pictureBox3.Bounds)) || (redGhost.Bounds.IntersectsWith(pictureBox4.Bounds)))
            {
                ghost2y = -ghost2y;
            }
        }

        private void SegundaPantalla_Load(object sender, EventArgs e)
        {
            
            int pinkx = random.Next(28, 550);
            int pinky = random.Next(48, 490);
            int redx = random.Next(28, 550);
            int redy = random.Next(48, 490);
            pinkGhost.Left = pinkx;
            pinkGhost.Top = pinky;
            redGhost.Left = redx;
            redGhost.Top = redy;
            int cherryx = random.Next(28, 550);
            int cherryy = random.Next(48, 490);
            int melonx = random.Next(28, 490);
            int melony = random.Next(48, 490);
            cherry.Left = cherryx;
            cherry.Top = cherryy;
            melon.Left = melonx;
            melon.Top = melony;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int cherryx = random.Next(28, 550);
            int cherryy = random.Next(48, 490);
            int melonx = random.Next(28, 490);
            int melony = random.Next(48, 490);
            cherry.Left = cherryx;
            cherry.Top = cherryy;
            melon.Left = melonx;
            melon.Top = melony;
            timer3.Enabled = true;
            timer2.Enabled = false;

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            cherry.Left = 357;
            cherry.Top = 7;
            melon.Left = 390;
            melon.Top = 7;
            timer2.Enabled = true;
            timer3.Enabled = false;
        }
    }
}
