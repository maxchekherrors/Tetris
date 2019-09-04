using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Tetris
{
    public partial class Main_wind : Form
    {
        const int a_size = 40;
        const int intro_period = 50;
        const int game_period = 500;
        Graphics g;
        int coints = 0;
        int record = 0;
        int[,] monitor_matr;
        Rectangle[,] monitor_rect;
        Item object_item;
        bool game = false;
        bool pause = false;


        public Main_wind()
        {
            InitializeComponent();
            g = this.CreateGraphics();

        }
        void start_game()
        {
            if (game)
            {
                timer1.Interval = game_period;
                label1.Visible = false;
                label3.Visible = false;
                coints = 0;
                label2.Text = "0";
            }
            else
            {
                timer1.Interval = intro_period;
                label3.Visible = true;
            }


            monitor_matr = new int[this.Height / a_size, this.Width / a_size];
            monitor_rect = new Rectangle[this.Height / a_size, this.Width / a_size];
            for (int i = 0; i < monitor_matr.GetLength(0); i++)
                for (int j = 0; j < monitor_matr.GetLength(1); j++)
                {
                    monitor_rect[i, j] = new Rectangle(a_size * j, a_size * i, a_size, a_size);
                    monitor_matr[i, j] = 0;
                }

            new_item();

            timer1.Start();
        }
        void intro()
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < monitor_matr.GetLength(1); i++)
                    if (monitor_matr[3, i] != 0)
                    {
                        flag = true;
                        rjad_clear(monitor_matr.GetLength(0) - 1);
                    }
            }


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try { record = Convert.ToInt32(File.ReadAllText("Record")); }
            catch { record = 0; }
            record_refresh();

            start_game();
        }
        void refresh(bool st=true)
        {

            SolidBrush w = new SolidBrush(Color.White);
            SolidBrush b = new SolidBrush(Color.Black);
            for (int i = 0; i < monitor_matr.GetLength(0); i++)
            {
                bool rjad = true;

                for (int j = 0; j < monitor_matr.GetLength(1); j++)
                {
                    if (monitor_matr[i, j] == 0)
                    {
                        g.FillRectangle(b, monitor_rect[i, j]);
                        rjad = false;
                    }

                    if (monitor_matr[i, j] != 0)
                    {

                        switch (monitor_matr[i, j])
                        {
                            case 1:
                                w = new SolidBrush(Color.Blue);
                                break;
                            case 2:
                                w = new SolidBrush(Color.Green);
                                break;
                            case 3:
                                w = new SolidBrush(Color.Red);
                                break;
                            case 4:
                                w = new SolidBrush(Color.DarkRed);
                                break;
                            case 5:
                                w = new SolidBrush(Color.DeepPink);
                                break;
                            case 6:
                                w = new SolidBrush(Color.Aqua);
                                break;
                            case 7:
                                w = new SolidBrush(Color.Purple);
                                break;

                        }
                        g.FillRectangle(w, monitor_rect[i, j]);
                    }

                }

                if (rjad && !st)
                    rjad_clear(i);
            }
        }
        void new_item()
        {
            object_item = new Item(monitor_matr);
        }
        bool step()
        {
            object_item.clear(monitor_matr);
            bool flag = object_item.control(monitor_matr, 1, 0);
            if (flag)
                object_item.obj_I++;
            object_item.draw(monitor_matr);
            if (!flag)
            {
                if (object_item.obj_I != 0)
                {
                    if (!game)
                        intro();
                    new_item();
                }
                else
                {
                    if (game)
                    {
                        game_over();
                    }
                }
            }
            return flag;
        }
        void game_over()
        {
            game = false;
            label1.Visible = true;
            start_game();

        }
       /* int de_sikret( int sikr)
        {
            try
            {
                int key = sikr % 100;
                sikr /= 100;
                return sikr / (+2*key - key * key);
            }
            catch { return 0; }
            
        }
        int sikret(int key,int data)
        {
            return (2*data * key - key * key * data) * 100 + key;
        }*/
        void record_refresh()
        {
            if (coints >= record)
            {
                int n = DateTime.Now.Second+1;
                record = coints;
                File.WriteAllText("Record", Convert.ToString (record));

            }
            label4.Text = "Record " + record;

        }
        void rjad_clear(int k)
        {
            if (game)
                label2.Text = Convert.ToString(++coints);
            for (int i = k; i > 0; i--)
                for (int j = 0; j < monitor_matr.GetLength(1); j++)
                    monitor_matr[i, j] = monitor_matr[i - 1, j];

            for (int i = 0; i < monitor_matr.GetLength(1); i++)
                monitor_matr[0, i] = 0;
            refresh(true);
            record_refresh();



        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            refresh(step());



        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // label2.Text = Convert.ToString(e.KeyValue);
            if (e.KeyValue == 38 && game)

            {
                object_item.clear(monitor_matr);
                object_item.transform(monitor_matr);
                object_item.draw(monitor_matr);
                refresh();

            }
            if (e.KeyValue == 40 && game)
            {                
                timer1.Interval = game_period / 10;
            }
            if (e.KeyValue == 96 && game)
            {
                while (step())
                {
                    refresh(true);
                }
                refresh(false);
            }
                if (e.KeyValue == 37 && game)
                if (object_item.obj_J > 0)
                {


                    object_item.clear(monitor_matr);
                    if (object_item.control(monitor_matr, 0, -1, false))
                    {

                        object_item.obj_J--;
                        object_item.draw(monitor_matr);
                        refresh();

                    }


                }
            if (e.KeyValue == 39 && game)
                if (object_item.obj_J + object_item.obj.GetLength(1) < monitor_matr.GetLength(1))
                {
                    object_item.clear(monitor_matr);
                    if (object_item.control(monitor_matr, 0, 1, false))
                    {

                        object_item.obj_J++;
                        object_item.draw(monitor_matr);
                        refresh();
                    }
                }
            if (e.KeyValue == 32 && game)
            {
                if (pause)
                {
                    pause = false;
                    timer1.Start();
                }
                else
                {
                    pause = true;
                    timer1.Stop();
                    g.DrawString("Pause", this.Font, Brushes.White, 160, 225);
                }

            }
            if (e.KeyValue == 13 && !game)
            {

                game = true;
                start_game();

            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void Main_wind_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40 && game)
                timer1.Interval = game_period;

        }
    }
  
}
