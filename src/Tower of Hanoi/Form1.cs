using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Tower_of_Hanoi
{
    public partial class Form1 : Form
    {
        #region Variable Declaration
        public bool[,] tower = new bool[3, 5] {{ true, true, true, true, true },
        { false, false, false, false, false },{ false, false, false, false, false } };
        Timer t, t1;
        TextBox ring, topRing;
        bool ringIsUp = false;
        int mid;
        int count = 0;
        int time = 0;
        int step = 0;
        int oldX = 0;
        int oldY = 0;
        #endregion
        public Form1()
        {
            InitializeComponent();
            Timer();
        }
        public void RingUp(int num)
        {
            int y = 178;
            for (int i = 0; i < 5; i++)
            {
                if (tower[num, i] == true)
                {
                    GetRing(num, y, ref ring);
                    oldX = ring.Location.X;
                    oldY = ring.Location.Y;
                    ring.Location = new Point(mid - (ring.Width / 2), 70);
                    tower[num, i] = false;
                    break;
                }
                y += 20;
            }
        }
        public void RingDown(int num)
        {
            int y = 258;
            for (int i = 4; i >= 0; i--)
            {
                if (tower[num, i] == false)
                {
                    SetRing(num, y);
                    tower[num, i] = true;
                    break;
                }
                y -= 20;
            }
        }
        public void GetRing(int num, int y, ref TextBox rings)
        {
            if (num == 0)
                mid = 192;
            else if (num == 1)
                mid = 365;
            else if (num == 2)
                mid = 535;
            if ((textBox9.Location.X + textBox9.Width / 2) == mid && textBox9.Location.Y == y)
                rings = textBox9;
            else if ((textBox8.Location.X + textBox8.Width / 2) == mid && textBox8.Location.Y == y)
                rings = textBox8;
            else if ((textBox7.Location.X + textBox7.Width / 2) == mid && textBox7.Location.Y == y)
                rings = textBox7;
            else if ((textBox6.Location.X + textBox6.Width / 2) == mid && textBox6.Location.Y == y)
                rings = textBox6;
            else if ((textBox5.Location.X + textBox5.Width / 2) == mid && textBox5.Location.Y == y)
                rings = textBox5;
        }
        public void GetTopRing(int num)
        {
            int y = 178;
            for (int i = 0; i < 5; i++)
            {
                if (tower[num, i] == true)
                {
                    GetRing(num, y, ref topRing);
                    break;
                }
                y += 20;
            }
        }
        public void SetRing(int mid, int y)
        {
            if (mid == 0)
                ring.Location = new Point(192 - (ring.Width / 2), y);
            else if (mid == 1)
                ring.Location = new Point(365 - (ring.Width / 2), y);
            else if (mid == 2)
                ring.Location = new Point(535 - (ring.Width / 2), y);
        }
        public void RingMove(int num)
        {
            if (tower[num, 4] == false)
            {
                if (ringIsUp == true)
                {
                    RingDown(num);
                    ringIsUp = false;
                    if (!(ring.Location.X == oldX && ring.Location.Y == oldY))
                    {
                        step++;
                        Steps.Text = "Steps: " + step;
                    }
                }
            }
            else
            {
                if (ringIsUp == true)
                {
                    GetTopRing(num);
                    if (ring.Width < topRing.Width)
                    {
                        RingDown(num);
                        ringIsUp = false;
                        if (!(ring.Location.X == oldX && ring.Location.Y == oldY))
                        {
                            step++;
                            Steps.Text = "Steps: " + step;
                        }
                    }
                }
                else
                {
                    RingUp(num);
                    ringIsUp = true;
                }
            }
        }
        public void LegalMoveBetween(int num1, int num2)
        {
            RingMove(num1);
            if (ringIsUp)
            {
                RingMove(num2);
                if (ringIsUp)
                {
                    RingMove(num1);
                    RingMove(num2);
                    RingMove(num1);
                }
            }
            else
            {
                RingMove(num2);
                RingMove(num1);
            }
        }
        public void Auto()
        {
            Clear();

            t = new Timer();
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }
        public void Clear()
        {
            for (int j = 0; j < 5; j++)
            {
                tower[0, j] = true;
                tower[1, j] = false;
                tower[2, j] = false;
            }
            textBox9.Location = new Point(162, 178);
            textBox8.Location = new Point(148, 198);
            textBox7.Location = new Point(138, 218);
            textBox6.Location = new Point(128, 238);
            textBox5.Location = new Point(113, 258);
            time = 0;
            step = 0;
            Timelapse.Text = "Time spent: " + time + "s";
            Steps.Text = "Steps: " + step;
        }
        public void Timer()
        {
            t1 = new Timer();
            t1.Interval = 1000;
            t1.Tick += new EventHandler(this.t1_Tick);
            t1.Start();
        }
        public void Save()
        {

            string tower1 = String.Format("{0};{1};{2};{3};{4}", tower[0, 0], tower[0, 1], tower[0, 2], tower[0, 3], tower[0, 4]);
            string tower2 = String.Format("{0};{1};{2};{3};{4}", tower[1, 0], tower[1, 1], tower[1, 2], tower[1, 3], tower[1, 4]);
            string tower3 = String.Format("{0};{1};{2};{3};{4}", tower[2, 0], tower[2, 1], tower[2, 2], tower[2, 3], tower[2, 4]);
            string values = String.Format("{0};{1};{2};{3}", oldX, oldY, time, step);
            string ringsposition = String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}", textBox9.Location.X, textBox9.Location.Y, textBox8.Location.X, textBox8.Location.Y, textBox7.Location.X, textBox7.Location.Y, textBox6.Location.X, textBox6.Location.Y, textBox5.Location.X, textBox5.Location.Y);
            string rings = String.Format("{0};{1}", ToStringRing(ref ring), ToStringRing(ref topRing));
            string file = ToStringDateTime();
            using (StreamWriter sw = new StreamWriter(file, true))
            {
                sw.WriteLine(tower1);
                sw.WriteLine(tower2);
                sw.WriteLine(tower3);
                sw.WriteLine(ringIsUp);
                sw.WriteLine(ringsposition);
                sw.WriteLine(values);
                sw.WriteLine(rings);
            }
        }
        public void LoadGame()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                {
                    string[] tower1 = sr.ReadLine().ToLower().Split(';');
                    string[] tower2 = sr.ReadLine().ToLower().Split(';');
                    string[] tower3 = sr.ReadLine().ToLower().Split(';');
                    bool ringisup = Convert.ToBoolean(sr.ReadLine());
                    string[] ringposition = sr.ReadLine().Split(';');
                    string[] values = sr.ReadLine().Split(';');
                    string[] rings = sr.ReadLine().Split(';');

                    for (int i = 0; i < 5; i++)
                    {
                        tower[0, i] = Convert.ToBoolean(tower1[i]);
                        tower[1, i] = Convert.ToBoolean(tower2[i]);
                        tower[2, i] = Convert.ToBoolean(tower3[i]);
                    }
                    ringIsUp = ringisup;
                    textBox9.Location = new Point(Convert.ToInt16(ringposition[0]), Convert.ToInt16(ringposition[1]));
                    textBox8.Location = new Point(Convert.ToInt16(ringposition[2]), Convert.ToInt16(ringposition[3]));
                    textBox7.Location = new Point(Convert.ToInt16(ringposition[4]), Convert.ToInt16(ringposition[5]));
                    textBox6.Location = new Point(Convert.ToInt16(ringposition[6]), Convert.ToInt16(ringposition[7]));
                    textBox5.Location = new Point(Convert.ToInt16(ringposition[8]), Convert.ToInt16(ringposition[9]));
                    oldX = Convert.ToInt16(values[0]);
                    oldY = Convert.ToInt16(values[1]);
                    time = Convert.ToInt16(values[2]);
                    step = Convert.ToInt16(values[3]);
                    Steps.Text = "Steps: " + step;
                    BackToBox(rings[0], ref ring);
                    BackToBox(rings[1], ref topRing);
                }
            }
        }
        public string ToStringDateTime()
        {
            DateTime now = DateTime.Now;
            string date = now.Day.ToString();
            string month = now.Month.ToString();
            string year = now.Year.ToString();

            string time = now.Hour.ToString();
            string minute = now.Minute.ToString();
            string second = now.Second.ToString();

            string str = String.Format("{0}-{1}-{2} {3}.{4}.{5}.txt", date, month, year, time, minute, second);

            return str;
        }
        public string ToStringRing(ref TextBox rings)
        {
            if (rings == textBox9)
            {
                string str = "textBox9";
                return str;
            }
            else if (rings == textBox8)
            {
                string str = "textBox8";
                return str;
            }
            else if (rings == textBox7)
            {
                string str = "textBox7";
                return str;
            }
            else if (rings == textBox6)
            {
                string str = "textBox6";
                return str;
            }
            else if (rings == textBox5)
            {
                string str = "textBox5";
                return str;
            }
            else return "";

        }
        public TextBox BackToBox(string ringtext, ref TextBox rings)
        {
            if (ringtext == "textBox9")
            {
                rings = textBox9;
                return rings;
            }
            else if (ringtext == "textBox8")
            {
                rings = textBox8;
                return rings;
            }
            else if (ringtext == "textBox7")
            {
                rings = textBox7;
                return rings;
            }
            else if (ringtext == "textBox6")
            {
                rings = textBox6;
                return rings;
            }
            else if (ringtext == "textBox5")
            {
                rings = textBox5;
                return rings;
            }
            else return null;
        }
        public void t1_Tick(object sender, EventArgs e)
        {
            time++;
            Timelapse.Text = "Time spent: " + time + "s";
            if ((textBox9.Location.X == 505 || textBox9.Location.X == 335) && textBox9.Location.Y == 178)
            {
                t1.Stop();
                t1.Dispose();
                MessageBox.Show("You Win!");
            }
        }
        public void t_Tick(object sender, EventArgs e)
        {
            if (count == 0)
            {
                LegalMoveBetween(0, 2);
                count++;
                if (textBox9.Location.X == 505 && textBox9.Location.Y == 178)
                {
                    t.Stop();
                    t.Dispose();
                }
            }
            else if (count == 1)
            {
                LegalMoveBetween(0, 1);
                count++;
            }
            else if (count == 2)
            {
                LegalMoveBetween(2, 1);
                count = 0;
            }
        }
        public void button1_Click(object sender, EventArgs e)
        {
            RingMove(0);

        }
        public void button2_Click(object sender, EventArgs e)
        {
            RingMove(1);
        }
        public void button3_Click(object sender, EventArgs e)
        {
            RingMove(2);
        }
        public void button4_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void button5_Click(object sender, EventArgs e)
        {
            Auto();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            LoadGame();
        }

    }
}
