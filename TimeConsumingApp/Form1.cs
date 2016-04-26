using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeConsumingApp
{

    public partial class Form1 : Form
    {
        bool Cancel;

        public Form1()
        {
            InitializeComponent();
        }

        private void TimeConsumingMethod(int seconds)
        {

            int seconds;
            seconds = (int)Time;

            for (int j = 1; j <= seconds; j++)
            {
                System.Threading.Thread.Sleep(1000);
                SetProgress((int)(j * 100) / seconds);

                if (Cancel)
                {
                    MessageBox.Show("Cancelled");
                    Cancel = false;
                }
                else
                {
                    MessageBox.Show("Complete");
                }
            }
        }

        public delegate void SetProgressDelegate(int val);
        public void SetProgress(int val)
        {
            if (ProgressBar1.InvokeRequired)
            {
                SetProgressDelegate del = new SetProgressDelegate(SetProgress);
                this.Invoke(del, new object[] { val });
            }
            else
            {
                ProgressBar1.Value = val;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var aThread = new System.Threading.Thread(TimeConsumingMethod);
            aThread.Start(int.Parse(textBox1.Text));
        }

    }
}
