using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweep
{
    //0: not assigned
    //1-8: numbers of mines around the cell
    //10: mines
    //20: empty
    
    public partial class Form1 : Form
    {
        byte[,] Positions = new byte[15, 15];
        Button[,] ButtonList = new Button[15, 15];
        System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer("C:\\Users\\kient\\source\\repos\\Minesweep\\Resources\\vine_boom.wav");
        System.Media.SoundPlayer win = new System.Media.SoundPlayer("C:\\Users\\kient\\source\\repos\\Minesweep\\Resources\\Tada.wav");
        public Form1()
        {
            InitializeComponent();
            //this.Controls.Add(explode);
            GenerateMines();
            GeneratePositionValue();
            GenerateButton();
            btn_restart_Click(btn_restart, EventArgs.Empty);
        }

        Random rnd = new Random();
        private void GenerateMines()
        {
            int mines = 30;
            while(mines != 0)
            {
                int x = rnd.Next(0, 15);
                int y = rnd.Next(0, 15);
                if (Positions[x,y] == 0)
                {
                    Positions[x,y] = 10;
                    mines--;
                }
            }
        }

        private void GeneratePositionValue()
        {
            for(int x = 0; x < 15; x++)
            {
                for(int y = 0; y < 15; y++)
                {
                    byte minesCounter = 0; //number of adjacent mines
                    //avoid checking mine cell
                    if (Positions[x, y] == 10) continue;
                    
                    for (int countX = -1; countX < 2; countX++)
                    {
                        int checkX = x + countX;

                        for (int countY = -1; countY < 2; countY++)
                        {
                            int checkY = y + countY;

                            //cell out of bound, avoid checking these cells
                            if (checkX == -1 || checkY == -1 || checkX > 14 || checkY > 14) continue;

                            if (checkX == x && checkY == y) continue;

                            if (Positions[checkX, checkY] == 10) minesCounter++;
                        }
                    }

                    if (minesCounter == 0)
                        Positions[x, y] = 20;
                    else
                        Positions[x, y] = minesCounter;
                }
            }
        }


        private void GenerateButton()
        {
            int xLocation = 7;
            int yLocation = 10;
            for(int x = 0; x < 15; x++)
            {
                for(int y = 0; y < 15; y++)
                {
                    Button btn = new Button();
                    btn.Parent = panel1;
                    btn.Location = new Point(xLocation, yLocation);
                    btn.Size = new Size(26, 26);
                    btn.Tag = $"{x},{y}";
                    btn.Click += BtnClick;
                    btn.MouseUp += Btn_MouseUp;
                    xLocation += 26;
                    ButtonList[x,y] = btn;
                }
                yLocation += 26;
                xLocation = 7;
            }
        }
        private void BtnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int x = Convert.ToInt32(btn.Tag.ToString().Split(',').GetValue(0));
            int y = Convert.ToInt32(btn.Tag.ToString().Split(',').GetValue(1));
            byte value = Positions[x, y];

            if(value == 10)
            {
                soundPlayer.Play();
                btn.Image = Properties.Resources.mines;

                explode.Location = new Point(
                    btn.Location.X - (btn.Width - explode.Width) /6,
                    btn.Location.Y - (btn.Height - explode.Height) 
                );

                explode.BackColor = Color.Transparent;

                explode.Visible = true;
                panel1.Enabled = false;
                Timer timer = new Timer();
                timer.Interval = 800; // Thời gian hiển thị GIF (ms)
                timer.Tick += (s, args) =>
                {
                    explode.Visible = false;
                    timer.Stop();
                };
                timer.Start();
                MessageBox.Show("Game Over");
                
            }
            else if(value == 20)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = SystemColors.ControlDark;
                btn.Enabled = false;
                OpenAdjacentCell(btn);
                points++;
            }
            else
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = SystemColors.ControlDark;
                btn.FlatAppearance.MouseDownBackColor = btn.BackColor;
                btn.FlatAppearance.MouseOverBackColor = btn.BackColor;
                btn.Text = Positions[x,y].ToString();
                btn.Font = new Font(btn.Font.Name, btn.Font.Size, FontStyle.Bold);
                switch (Positions[x, y])
                {
                    case 1:
                        btn.ForeColor = Color.Blue;
                        break;
                    case 2:
                        btn.ForeColor = Color.Green;
                        break;
                    case 3:
                        btn.ForeColor= Color.Red;
                        break;
                    case 4:
                        btn.ForeColor = Color.DarkBlue;
                        break;
                    case 5:
                        btn.ForeColor = Color.DarkRed;
                        break;
                    case 6:
                        btn.ForeColor = Color.DarkCyan;
                        break;
                    case 7:
                        btn.ForeColor = Color.Purple;
                        break;
                    case 8:
                        btn.ForeColor = Color.LightGray;
                        break;
                }
                points++;
            }
            btn.Click -= BtnClick;
            txtScore.Text = points.ToString();

            int totalSafeCells = 15 * 15 - 30; // Tổng số ô không phải mìn
            if (points == totalSafeCells)
            {
                //panel1.Enabled = false;
                win.Play();
                MessageBox.Show("Congratulations! You win!");
                btn_restart_Click(btn_restart, EventArgs.Empty);
            }
        }

        private void OpenAdjacentCell(Button btn)
        {
            int x = Convert.ToInt32(btn.Tag.ToString().Split(',').GetValue(0));
            int y = Convert.ToInt32(btn.Tag.ToString().Split(',').GetValue(1));

            List<Button> emptyButtons = new List<Button>();

            for (int countX = -1; countX < 2; countX++)
            {
                int checkX = x + countX;

                for (int countY = -1; countY < 2; countY++)
                {
                    int checkY = y + countY;

                    //Cell out of bound, avoid checking these cells
                    if (checkX < 0 || checkY < 0 || checkX > 14 || checkY > 14) continue;

                    if (checkX == x && checkY == y) continue;

                    Button btnAdjacent = ButtonList[checkX, checkY];

                    if (btnAdjacent == null) continue;
                    //Advoid opening cells with flag
                    if (btnAdjacent.Image != null && btnAdjacent.Image == Properties.Resources.flag) continue;

                    if (btnAdjacent.Tag == null || !btnAdjacent.Tag.ToString().Contains(","))
                    {
                        continue;
                    }

                    int xAdjacent = Convert.ToInt32(btnAdjacent.Tag.ToString().Split(',').GetValue(0));
                    int yAdjacnet = Convert.ToInt32(btnAdjacent.Tag.ToString().Split(',').GetValue(1));

                    byte value = Positions[xAdjacent, yAdjacnet];

                    if (value ==20)
                    {
                        if(btnAdjacent.FlatStyle != FlatStyle.Flat)
                        {
                            btnAdjacent.FlatStyle = FlatStyle.Flat;
                            btnAdjacent.FlatAppearance.BorderSize = 1;
                            btnAdjacent.FlatAppearance.BorderColor = SystemColors.ControlDark;
                            btnAdjacent.Enabled = false;
                            emptyButtons.Add(btnAdjacent);
                            points++;
                        }
                    }
                    else if(value != 10)
                    {
                        btnAdjacent.PerformClick();
                    }
                }
            }

            foreach(var btnEmpty in emptyButtons)
            {
                OpenAdjacentCell(btnEmpty);
            }

            txtScore.Text = points.ToString();

        }

        int flag = 30;

        int points = 0;

        private void Btn_MouseUp(object sender, MouseEventArgs e)
        {
            txtFlag.Text = "30";
            if (e.Button == MouseButtons.Right)
            {
                Button btn = (Button)sender;
                if (btn.FlatStyle != FlatStyle.Flat)
                {
                    if (btn.Image == null)
                    {
                        btn.Image = Properties.Resources.flag;
                        flag--;
                        btn.Click -= BtnClick;
                    }
                    else
                    {
                        btn.Image = null;
                        flag++;
                        btn.Click += BtnClick;
                    }
                    txtFlag.Text = flag.ToString();
                }
            }
        }

        private void btn_restart_Click(object sender, EventArgs e)
        {
            points = 0;
            flag = 30;
            txtFlag.Text = flag.ToString();
            for (int x = 0; x < 15; x++)
            {
                for(int y = 0; y < 15; y++)
                {
                    ButtonList[x,y].Dispose();
                }
            }
            txtScore.Text = "0";
            //txtFlag.Text = "30";
            panel1.Controls.Clear();
            panel1.Enabled = true;
            ButtonList = new Button[15, 15];
            Positions = new byte[15, 15];
            GenerateMines();
            GeneratePositionValue();
            GenerateButton();
        }
    }
}
