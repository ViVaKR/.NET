using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BinaryForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            textBox1.KeyPress += TextBox1_KeyPress;
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //textBox2.Focus();
            //SendKeys.SendWait(e.KeyChar.ToString());
            //Thread.Sleep(1);
            //textBox1.Focus();
            //textBox1.SelectionStart = textBox1.TextLength;
        }

        private const int age = 30;
        private const string bookTitle = "C# Programming";
        private const bool mvp = false;
        private const double price = 54.99;
        private const string fileName = @"D:\Temp\MC.bin";
        private readonly Encoding encoding = Encoding.UTF8;

        private void Button_Write_Click(object sender, EventArgs e)
        {
            using (var bw = new BinaryWriter(File.OpenWrite(fileName), encoding, false))
            {
                var decimalValue = Convert.ToByte(textBox1.Text);

                //for (int i = 0x0000_0000; i <= 0x0000_0042; i++)
                //{
                //    bw.BaseStream.Position = i;
                //    bw.Write(0);
                //}

                bw.BaseStream.Position = 0;
                bw.Write(decimalValue);

                bw.BaseStream.Position = 1;
                bw.Write(decimalValue + 1);

                bw.BaseStream.Position = 2;
                bw.Write(decimalValue + 2);

                label1.Text = $"2진수: {Convert.ToString(decimalValue, 2)} - " +
                              $"16진수: {Convert.ToString(decimalValue, 16)} - " +
                              $"byte : {Convert.ToByte(textBox1.Text)}";
            }
        }

        private void Button_Read_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(textBox1.Text ?? "16385");
            byte[] array = BitConverter.GetBytes(value);

            byte[] result = array;

            textBox2.Text = string.Join("-", result);

            //listBox1.Items.Add(((byte)Convert.ToInt32(textBox1.Text)).ToString());
        }
    }
}
