using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            //buttonColorAt();
        }

        private void buttonColorAt()
        {
            MessageBox.Show(Controls.OfType<Button>().ToArray()[0].Text);
            foreach (Button btn in Controls.OfType<Button>())
                btn.BackColor = Color.RoyalBlue;
        }

        public char operationChar;
        private void button15_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string text = button.Text;
            int numeric;
            if (int.TryParse(text, out numeric)) textBox1.Text += text;
            else
            {
                string endChar = textBox1.Text == "" ? "" : textBox1.Text[textBox1.Text.Length - 1].ToString();
                if (text != "<=")
                {
                    if (new string[4] { "X", "+", "-", "%" }.Contains(endChar))
                        textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                    if (textBox1.Text.Length <= 0) return;
                    textBox1.Text += text;
                    operationChar = Convert.ToChar(text);
                }
                else
                {
                    if (textBox1.Text == "") return;
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") return;
            //MessageBox.Show(String.Join(", ", textBox1.Text.Split(operationChar)));
            Calculate();
        }

        private void Calculate()
        {
            string text;
            calculateMath math = new calculateMath();
            string[] charArray = textBox1.Text.Split(operationChar);
            switch (operationChar)
            {
                case '+':
                    text = math.Gather(charArray).ToString();
                    break;
                case '-':
                    text = math.Interest(charArray).ToString();
                    break;
                case 'X':
                    text = math.Multiply(charArray).ToString();
                    break;
                case '%':
                    text = math.Divide(charArray).ToString();
                    break;
                default:
                    text = "Sorun oluştu!";
                    break;
            }
            textBox1.Text = text;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        int index = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            Button[] buttons = Controls.OfType<Button>().ToArray();
            if (index >= buttons.Length-1)
            {
                timer1.Stop();
                index = 0;
            }
            ++index;
            buttons[index].BackColor = Color.RoyalBlue;
        }
    }

    public class calculateMath
    {
        //Array toplama işlemi
        public double Gather(string[] collection)
        {
            double total = 0;
            foreach (int i in collection.Select(i => Convert.ToInt16(i))) total += i;
            return total;
        }

        //Array çıkarma işlemi
        public double Interest(string[] collection)
        {
            double total = Convert.ToDouble(collection[0]);
            foreach (int i in collection.Select(i => Convert.ToInt16(i)).Where(i => i != Convert.ToInt16(collection[0]))) total -= i;
            return total;
        }

        //Array çarpma işlemi
        public double Multiply(string[] collection)
        {
            double total = 1;
            foreach (int i in collection.Select(i => Convert.ToInt16(i))) total *= i;
            return total;
        }

        //Array bölme işlemi
        public double Divide(string[] collection)
        {
            double total = Convert.ToDouble(collection[0]);
            foreach (int i in collection.Select(i => Convert.ToInt16(i)).Where(i => i != Convert.ToInt16(collection[0]))) total /= i;
            return total;
        }
    }
}