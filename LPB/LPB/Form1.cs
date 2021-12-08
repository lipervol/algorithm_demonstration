using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LPB
{
    public partial class Form1 : Form
    {
        public int[] inputdata = new int[8];
        public int[] dataindex = new int[8];
        public int Choise=-1;
        public int[][] sortindex = new int[100][];
        public int ChangeTimes = 0;
        public int CurrentTimes = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("未选择算法！");
            }
            else
            {
                Choise = comboBox1.SelectedIndex;             
                MessageBox.Show("设置成功："+comboBox1.SelectedItem.ToString()+"\n请点击设置数据应用算法！");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                inputdata[0] = int.Parse(textBox1.Text);
                inputdata[1] = int.Parse(textBox2.Text);
                inputdata[2] = int.Parse(textBox3.Text);
                inputdata[3] = int.Parse(textBox4.Text);
                inputdata[4] = int.Parse(textBox5.Text);
                inputdata[5] = int.Parse(textBox6.Text);
                inputdata[6] = int.Parse(textBox7.Text);
                inputdata[7] = int.Parse(textBox8.Text);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("输入数据错误！");
            }
            finally
            {
                if (Choise == -1)
                {
                    MessageBox.Show("请先选择算法！");
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        dataindex[i] = i;
                    }
                    CurrentTimes = 0;
                    Array.Clear(sortindex, 0, sortindex.Length);
                    sortindex[0] = new int[8];
                    for (int i = 0; i < 8; i++)
                    {
                        sortindex[0][i] = dataindex[i];
                    }
                    DrawBMP(sortindex[0]);
                    label6.Text = "当前排序次数：" + CurrentTimes.ToString();
                    label7.Text = "当前排序算法：" + comboBox1.Text.ToString();
                    if (Choise == 0)
                    {
                        Bubblesort(inputdata, dataindex);
                    }
                    else
                    {
                        Insertsort(inputdata, dataindex);
                    }
                }
            }
        }

        private void DrawBMP(int[] index)
        {
            Bitmap bitM = new Bitmap(this.panel1.Width, this.panel1.Height);
            Graphics g = Graphics.FromImage(bitM);
            g.Clear(Color.FromArgb(0xf0, 0xfc, 0xff));
            int x, y, w, h;
            for (int i = 0; i < 8; i++)
            {
                g.DrawString(" D" + (index[i] + 1), new Font("宋体", 8, FontStyle.Regular), new SolidBrush(Color.Black), 76 + 40 * i, this.panel1.Height - 16);
                x = 78 + 40 * i;
                int num = inputdata[index[i]];
                y = this.panel1.Height - 20 - (num * this.panel1.Height / 100) * 4 / 5;
                w = 24;
                h = (num * this.panel1.Height / 100) * 4 / 5;
                g.FillRectangle(new SolidBrush(Color.FromArgb(220, 48, 35)), x, y, w, h);
            }
            this.panel1.BackgroundImage = bitM;
        }

        private void Bubblesort(int[] inputdata, int[] dataindex)
        {
            ChangeTimes = 0;
            int[] sortdata = new int[8];
            for (int i=0;i<8; i++)
            {
                sortdata[i] = inputdata[i];
            }
            for (int i=0; i<7; i++)
            {
                for (int j=0; j<7-i;j++)
                {
                    if (sortdata[j]>sortdata[j+1])
                    {
                        int temp1 = sortdata[j];
                        sortdata[j] = sortdata[j + 1];
                        sortdata[j + 1] = temp1;
                        int temp2 = dataindex[j];
                        dataindex[j] = dataindex[j + 1];
                        dataindex[j + 1] = temp2;
                        ChangeTimes++;
                        sortindex[ChangeTimes] = new int[8];
                        for (int n = 0; n < 8; n++)
                        {
                            sortindex[ChangeTimes][n] = dataindex[n];
                        }                                           
                    }
                }
            }
        }
        private void Insertsort(int[] inputdata, int[] dataindex)
        {
            ChangeTimes = 0;
            int[] sortdata = new int[8];
            for (int k = 0; k < 8; k++)
            {
                sortdata[k] = inputdata[k];
            }
            int i,j;
            for (i = 1; i < 8; i++)
            {
                int temp = sortdata[i];               
                for (j = i; j > 0 && sortdata[j - 1] > temp; j--)
                {                   
                    sortdata[j] = sortdata[j - 1];
                    int indextemp = dataindex[j];
                    dataindex[j] = dataindex[j - 1];
                    dataindex[j - 1] = indextemp;
                    ChangeTimes++;
                    sortindex[ChangeTimes] = new int[8];
                    for (int n = 0; n < 8; n++)
                    {
                        sortindex[ChangeTimes][n] = dataindex[n];
                    }
                }
                sortdata[j] = temp;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Random ran = new Random();
            textBox1.Text = ran.Next(0, 100).ToString();
            textBox2.Text = ran.Next(0, 100).ToString();
            textBox3.Text = ran.Next(0, 100).ToString();
            textBox4.Text = ran.Next(0, 100).ToString();
            textBox5.Text = ran.Next(0, 100).ToString();
            textBox6.Text = ran.Next(0, 100).ToString();
            textBox7.Text = ran.Next(0, 100).ToString();
            textBox8.Text = ran.Next(0, 100).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CurrentTimes--;
            if (CurrentTimes<0)
            {
                CurrentTimes++;
                MessageBox.Show("已到达起始位置！");
            }
            else
            {
                DrawBMP(sortindex[CurrentTimes]);
                label6.Text = "当前排序次数：" + CurrentTimes.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CurrentTimes++;
            if (CurrentTimes >ChangeTimes)
            {
                CurrentTimes--;
                MessageBox.Show("排序结束！");
            }
            else
            {
                DrawBMP(sortindex[CurrentTimes]);
                label6.Text = "当前排序次数："+CurrentTimes.ToString();
            }
        }
        public static void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            while (true)
            {
                CurrentTimes++;
                if (CurrentTimes > ChangeTimes)
                {
                    CurrentTimes--;
                    MessageBox.Show("排序结束！");
                    break;
                }
                else
                {
                    DrawBMP(sortindex[CurrentTimes]);
                    label6.Text = "当前排序次数：" + CurrentTimes.ToString();
                }
                Delay(600);
            }
        }
    }
}
