using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace workCounterGUI
{
    public partial class Form1 : Form
    {
        Timer t = new Timer();

        // DateTime now  = DateTime.Now;
        // TimeSpan update;
        // TimeSpan update2;

        Boolean starttime = false;

        int d;
        int h;
        int m;
        int s;
        int sCount;
        int tot = 0;
        int total = 0;

        FolderBrowserDialog folderselect ;

        string path = Directory.GetCurrentDirectory() + "/workCounterDATE.txt";
        string path1 = Directory.GetCurrentDirectory() + "/workCounterTEMP.txt";
        string path2 = Directory.GetCurrentDirectory() + "/workCounterTOTAL.txt";
        string path3 = Directory.GetCurrentDirectory() + "/workCounterTOTALTemp.txt";
        string path4 = Directory.GetCurrentDirectory() + "/Folderselect.txt";


        List<string> allLinesText;
        List<string> allLinesText1;
        List<string> allLinesText2;
        List<string> allLinesText3;
        

        String lastline;
        DateTime yesterday;
        int days = 1;
        int hours=13;
        String tTemp;
        DateTime value = new DateTime(2017, 5, 31, 16,00,00);
        DateTime value2 = new DateTime(2017, 6, 01, 08, 00, 00);
        int startTemp = 1;

        int showDays = 5;

        public Form1()
        {
            InitializeComponent();

            t.Interval = 1000;
            t.Tick += T_Tick;

           // numericUpDown1.Value = showDaysMax;
            if (!File.Exists(path4))
            {

                using (StreamWriter sw = File.CreateText(path4))
                {
                    sw.Write(" ");
                    
                }
            }

            if (!File.ReadLines(path4).First().Equals(" "))
            {
                path = File.ReadLines(path4).First() + "/workCounterDATE.txt";
                path1 = File.ReadLines(path4).First() + "/workCounterTEMP.txt";
                path2 = File.ReadLines(path4).First() + "/workCounterTOTAL.txt";
                path3 = File.ReadLines(path4).First() + "/workCounterTOTALTemp.txt";
            }
            else
            {
                
            }

            check();

            lastline = File.ReadLines(path).Last();
            if (lastline != " ")
            {
                
                yesterday = DateTime.Parse(lastline);
                days = Int32.Parse((DateTime.Now - yesterday).Days.ToString());
                if (days <= 0)
                {
                    hours = Int32.Parse(( DateTime.Now - yesterday).Hours.ToString());
                }

            }
            if (hours > 12 || days > 1)
            {

                if (new FileInfo(path3).Length > 0)
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                }
            }
            
            
            

            readfromfile(showDays);
            
          
           

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (WindowState == FormWindowState.Minimized && checkBox2.Checked.Equals(true))
            {

                Hide();
                notifyIcon1.Visible = true;


            }
          
        }

        //private void Form1_FormClosing(object sender, FormClosingEventArgs e){
        //    e.Cancel = true;
        //    this.Hide();
        //    notifyIcon1.Visible = true;
       // }

        private void Form1_Load(object sender, EventArgs e)
        {

           



        }

        public void check()
        {
            if (!File.Exists(path))
            {

                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write(" ");
                }
            }
            if (!File.Exists(path1))
            {

                using (StreamWriter sw = File.CreateText(path1))
                {
                    sw.Write(" ");
                }
            }
            if (!File.Exists(path2))
            {
                using (StreamWriter sw = File.CreateText(path2))
                {
                    
                }
                
            }
            if (!File.Exists(path3))
            {
                using (StreamWriter sw = File.CreateText(path3))
                {

                }

            }
           

        }

        public String timer1()
        {

            if (h >= 24)
            {
                d++;
                h = 0;
            }
            else if (m >= 59)
            {
                h++;
                m = 0;
            }
           else  if (s >= 59)
            {
                m++;
                s = 0;
            }
            else
            {
                s++;
                sCount++;
            }
            notifyIcon1.Text=("WorkCOunter: " + d.ToString("00") + " : " + h.ToString("00") + " : " + m.ToString("00") + " : " + s.ToString("00"));
            return label1.Text = (" " + d.ToString("00") + " : " + h.ToString("00") + " : " + m.ToString("00") + " : " + s.ToString("00"));
        }

        public String calc(int a)
        {

            TimeSpan time = TimeSpan.FromSeconds(a);



            return (time.ToString("''dd':'hh':'mm':'ss''"));
        }

        public void readfromfile(int daysShow)
        {
            allLinesText = File.ReadAllLines(path).ToList();
            allLinesText1 = File.ReadAllLines(path1).ToList();
            allLinesText2 = File.ReadAllLines(path2).ToList();
           



            for (int i = allLinesText.Count - 1; i >= Math.Max(0, allLinesText.Count - daysShow); i--)
            {

                listBox1.Items.Add(allLinesText[i]);

            }


            for (int i = allLinesText2.Count - 1; i >= Math.Max(0, allLinesText2.Count - daysShow); i--)
            {
                listBox2.Items.Add(calc(Int32.Parse(allLinesText2[i])));
               
            }



            for (int i = allLinesText2.Count - 1; i >= Math.Max(0, allLinesText2.Count - daysShow); i--)
            {
                tot += Int32.Parse(allLinesText2[i]);


            }
            textBox1.Text = calc(tot);
        }

        public void readfromfileAll()
        {
            allLinesText = File.ReadAllLines(path).ToList();
            allLinesText1 = File.ReadAllLines(path2).ToList();
            allLinesText2 = File.ReadAllLines(path2).ToList();
            allLinesText3 = File.ReadAllLines(path3).ToList();
            
            for (int i = allLinesText.Count - 1; i >= 0; i--)
            {

                listBox1.Items.Add(allLinesText[i]);

            }
            

            for (int i = allLinesText2.Count - 1; i >= 0; i--)
            {
                listBox2.Items.Add(calc(Int32.Parse(allLinesText2[i])));

            }
           

            for (int i = allLinesText2.Count - 1; i >= 0; i--)
            {
                tot += Int32.Parse(allLinesText2[i]);


            }
            textBox1.Text = calc(tot);
        }

        public void CopyTemp()
        {
            
                tTemp = File.ReadLines(path1).First();
                using (StreamWriter sw = File.AppendText(path3))
                {
                    sw.WriteLine(tTemp);
                }





        }

        private void T_Tick(object sender, EventArgs e)
        {

            timer1();
            using (StreamWriter sw = File.CreateText(path1))
            {
                sw.Write(sCount);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime startting = DateTime.Now;

            //Debug.WriteLine(days);
            //Debug.WriteLine(hours);
            if (hours > 12 || days>1)
            {
                starttime = false;
            }
            else
            {
                starttime = true;
            }
            if (startTemp == 1)
            {
                tTemp = File.ReadLines(path1).First();
                if (tTemp != " ")
                {
                    CopyTemp();
                }
                else
                {

                }
                using (StreamWriter sw = File.CreateText(path1))
                {
                    sw.WriteLine(" ");
                }
                startTemp = 0;
            }

            if (starttime == false)
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(startting);
                }
                starttime = true;
                hours = 0;
                days = 0;
            }
            allLinesText.Clear();
            allLinesText1.Clear();
            allLinesText2.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            tot = 0;

            readfromfile(showDays);

            button1.Enabled = false;
            t.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            t.Stop();


        }

        private void button3_Click(object sender, EventArgs e)
        {


            CopyTemp();
            allLinesText3 = File.ReadAllLines(path3).ToList();



            for (int i = allLinesText3.Count - 1; i >= 0; i--)
            {
                total += Int32.Parse(allLinesText3[i]);


            }


            using (StreamWriter sw = File.AppendText(path2))
            {
                sw.WriteLine(total);
               
            }
           

            using (StreamWriter sw = File.CreateText(path3))
            {
                sw.Write("");
            }
            using (StreamWriter sw = File.CreateText(path1))
            {
                sw.Write(" ");
            }
            allLinesText.Clear();
            allLinesText1.Clear();
            allLinesText2.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            tot = 0;

            readfromfile(showDays);

            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            folderselect = new FolderBrowserDialog();
            folderselect.SelectedPath = Directory.GetCurrentDirectory();
            if (folderselect.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                    using (StreamWriter sw = File.CreateText(path4))
                    {
                        sw.Write(folderselect.SelectedPath);
                    }
                
                
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked.Equals(true))
            {

                allLinesText.Clear();
                allLinesText1.Clear();
                allLinesText2.Clear();
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                tot = 0;
               
                readfromfileAll();

            }
            else
            {
                allLinesText.Clear();
                allLinesText1.Clear();
                allLinesText2.Clear();
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                tot = 0;

                readfromfile(showDays);
            }
        }

        public void uselesss()
        {
            //update = DateTime.Now - now;
            //update2 += update;
            // label1.Text = update2.ToString("' 'dd' : 'hh' : 'mm' : 'ss' '");
            // now = DateTime.Now;

            //List<string> lines = new List<string>();
            //  lines.Add(allLinesText[i]);
            // var message = string.Join("\n", lines);
            // richTextBox1.Text = message;



        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.SelectedIndex = listBox1.SelectedIndex;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = listBox2.SelectedIndex;
        }

        private void notifyIcon1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<String> linesList = File.ReadAllLines(path2).ToList();
            linesList.Reverse();
            linesList.RemoveAt(listBox1.SelectedIndex);
            linesList.Reverse();
            File.WriteAllLines(path2, linesList.ToArray());

            List<String> linesList1 = File.ReadAllLines(path).ToList();
            linesList1.Reverse();
            linesList1.RemoveAt(listBox1.SelectedIndex);
            linesList1.Reverse();
            File.WriteAllLines(path, linesList1.ToArray());

            allLinesText.Clear();
            allLinesText1.Clear();
            allLinesText2.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            tot = 0;

            readfromfile(showDays);
        }

       
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            

            allLinesText.Clear();
            allLinesText1.Clear();
            allLinesText2.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            tot = 0;

          
            readfromfile(Convert.ToInt32(numericUpDown1.Value));
        }
    }

}
