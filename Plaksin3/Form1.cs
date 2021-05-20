using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace Plaksin3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<(string Name, DateTime Date, int Time)> mas = new List<(string Name, DateTime Date, int Time)>();

        private void button1_Click(object sender, EventArgs e)
        {
            mas.Add((
                Name: textBox1.Text,
                Date: dateTimePicker1.Value,
                Time: trackBar1.Value
                ));
            richTextBox1.Text +=
                $"Name: {textBox1.Text}, Date: {dateTimePicker1.Value.ToString("dd.MM.yyyy")}, Time: {((trackBar1.Value * 15) / 60).ToString() + ":" + ((trackBar1.Value * 15) % 60).ToString()}\n";
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timeLabel.Text = ((trackBar1.Value * 15) / 60).ToString() + ":" + ((trackBar1.Value * 15) % 60).ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            (string Name, DateTime Date, int Time) o;
            if (!checkBox1.Checked)
            {
                for (int i = 0; i < mas.Count; i++)
                {
                    for (int j = 0; j < mas.Count; j++)
                    {
                        if (mas[i].Date.Year * 1000000 + mas[i].Date.Month * 10000 + mas[i].Date.Day * 100 +
                            mas[i].Time < mas[j].Date.Year * 1000000 + mas[j].Date.Month * 10000 +
                            mas[j].Date.Day * 100 + mas[j].Time)
                        {
                            o = mas[i];
                            mas[i] = mas[j];
                            mas[j] = o;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < mas.Count; i++)
                {
                    for (int j = 0; j < mas.Count; j++)
                    {
                        if (mas[i].Date.Year * 1000000 + mas[i].Date.Month * 10000 + mas[i].Date.Day * 100 +
                            mas[i].Time > mas[j].Date.Year * 1000000 + mas[j].Date.Month * 10000 +
                            mas[j].Date.Day * 100 + mas[j].Time)
                        {
                            o = mas[i];
                            mas[i] = mas[j];
                            mas[j] = o;
                        }
                    }
                }
            }

            List<(int nachalo, int konec)> t = new List<(int nachalo, int konec)>();
            richTextBox1.Text = "";
            for (int i = 0; i < mas.Count; i++)
            {
                // ((trackBar1.Value * 15) / 60)
                if (mas[i].Date.Date > DateTime.Now.Date)
                {
                    richTextBox1.Text +=
                        $"Name: {mas[i].Name}, Date: {mas[i].Date.ToString("dd.MM.yyyy")}, Time: {((mas[i].Time * 15) / 60).ToString() + ":" + ((mas[i].Time * 15) % 60).ToString()}\n";
                    Debug.Print("1");
                    continue;
                }
                else
                {
                    if (mas[i].Date.Date == DateTime.Now.Date)
                        if (((mas[i].Time * 15) / 60) > DateTime.Now.Hour)
                        {
                            richTextBox1.Text +=
                                $"Name: {mas[i].Name}, Date: {mas[i].Date.ToString("dd.MM.yyyy")}, Time: {((mas[i].Time * 15) / 60).ToString() + ":" + ((mas[i].Time * 15) % 60).ToString()}\n";
                            Debug.Print("1");
                            continue;
                        }
                        else
                        {
                            if (((mas[i].Time * 15) / 60) == DateTime.Now.Hour)
                                if (((mas[i].Time * 15) % 60) > DateTime.Now.Minute)
                                {
                                    richTextBox1.Text +=
                                        $"Name: {mas[i].Name}, Date: {mas[i].Date.ToString("dd.MM.yyyy")}, Time: {((mas[i].Time * 15) / 60).ToString() + ":" + ((mas[i].Time * 15) % 60).ToString()}\n";
                                    Debug.Print("1");
                                    continue;
                                }
                        }
                }

                string text =
                    $"Name: {mas[i].Name}, Date: {mas[i].Date.ToString("dd.MM.yyyy")}, Time: {((mas[i].Time * 15) / 60).ToString() + ":" + ((mas[i].Time * 15) % 60).ToString()}\n";
                richTextBox1.Text += text;
                t.Add((
                    nachalo: richTextBox1.Text.Length - text.Length,
                    konec: text.Length
                    ));
                Debug.Print(((trackBar1.Value * 15) / 60).ToString() + DateTime.Now.Hour.ToString());
                Debug.Print(((trackBar1.Value * 15) % 60).ToString() + DateTime.Now.Minute.ToString());
                Debug.Print("2");
            }

            foreach ((int nachalo, int konec) tuple in t)
            {
                richTextBox1.SelectionStart = tuple.nachalo;
                richTextBox1.SelectionLength = tuple.konec;
                richTextBox1.SelectionColor = Color.Red;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}