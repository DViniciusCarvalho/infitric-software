using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace DEG_monitor
{
    public partial class Form1 : Form
    {
        SerialPort arduino = new SerialPort();
        string data;
        int time = 0;
        int tensao = 0;
        bool arduinoConectado = false;      
        string status = "normal";
        
        public delegate void FuncaoDelegate(string c);

        public Form1()
        {
            InitializeComponent();
            arduino.DataReceived += new SerialDataReceivedEventHandler(Data);
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        }

        void Data(object sender, SerialDataReceivedEventArgs e)
        {
            data = arduino.ReadLine();
            this.BeginInvoke(new FuncaoDelegate(VoltageShowing), new object[] { data });
        }

        void VoltageShowing(string c)
        {
            textBox2.Text = c;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.ScrollBars = ScrollBars.Vertical;            
        }
     
        private void button1_Click(object sender, EventArgs e)
        {
            arduino.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "" || comboBox2.Text == "")
                {
                    MessageBox.Show("Selecione uma frequência.", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {                                       
                    arduino.PortName = comboBox2.Text;
                    arduino.BaudRate = Convert.ToInt32(comboBox1.Text);
                    arduino.Open();
                    arduinoConectado = true;                                                     
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Selecione uma porta.", error.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            chart1.Visible = true;
            status = "normal";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (arduinoConectado == false)
            {
                MessageBox.Show("Faça a conexão com o arduino antes.");
            }
            else
            {
                status = "micro";               
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (arduinoConectado == false)
            {
                MessageBox.Show("Faça a conexão com o arduino antes.");
            }
            else
            {
                status = "normal";              
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (arduinoConectado == false)
            {
                MessageBox.Show("Faça a conexão com o arduino antes.");
            }
            else
            {
                status = "mili";                
            }
        }     

        private void timer1_Tick(object sender, EventArgs e)
        {
            tensao = Convert.ToInt32(textBox2.Text);
            chart1.Series[0].Points.AddXY(time, tensao);
            textBox1.Text = Convert.ToString(tensao) + "mV" + " " + "(" + time/1000 + "s" + ")" + "\r\n";
            time += 1000;
            if (status == "normal")
            {
                double tensaoV = tensao / 1000;
                label5.Text = Convert.ToString(tensaoV) + "V";
            }
            else if (status == "mili")
            {
                String mili = Convert.ToString(tensao);
                label5.Text = mili + "mV";
            }
            else if (status == "micro")
            {
                String micro = Convert.ToString(tensao * 1000);
                label5.Text = micro + "μV";
            }
        }
    }
}
