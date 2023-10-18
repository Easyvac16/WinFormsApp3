namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        private bool continueGenerating = true;
        private bool genFibonacci = true;
        private bool isPaused = false;
        private int lowerBound;
        private int upperBound;
        private int currentNumber;
        Thread generatorThread;

        public Form1()
        {
            InitializeComponent();
            lowerBound = 2;
            upperBound = int.MaxValue;
            currentNumber = lowerBound;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
        }

        private void GeneratePrimeNumbers()
        {
            while (continueGenerating)
            {
                if (!isPaused)
                {
                    if (IsPrime(currentNumber))
                    {
                        UpdateLabel(currentNumber);
                    }
                    currentNumber++;
                    Thread.Sleep(100);

                    if (currentNumber > upperBound)
                    {
                        continueGenerating = false;
                        EnabledDisabled();
                    }
                }
            }
        }

        private void GenerateFibonacci()
        {
            long a = 0;
            long b = 1;

            while (genFibonacci)
            {
                if (!isPaused)
                {
                    long temp = a;
                    a = b;
                    b = temp + b;

                    if (b > upperBound)
                    {
                        genFibonacci = false;
                        EnabledDisabled();
                        break;
                    }

                    if (IsFibonacci(b))
                    {
                        UpdateLabel((int)b);
                    }

                    Thread.Sleep(100);
                }
            }
        }


        private void UpdateLabel(int number)
        {
            if (label3.InvokeRequired)
            {
                label3.Invoke((MethodInvoker)(() => label3.Text = number.ToString()));
            }
            else
            {
                label3.Text = number.ToString();
            }
        }

        private bool IsPrime(int number)
        {
            if (number <= 1)
                return false;

            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        private bool IsFibonacci(long number)
        {
            if (number <= 1)
                return false;

            long a = 0;
            long b = 1;

            while (b < number)
            {
                long temp = a;
                a = b;
                b = temp + b;
            }

            return b == number;
        }

        private void EnabledDisabled()
        {
            if (comboBox1.InvokeRequired)
            {
                comboBox1.Invoke((MethodInvoker)(() => comboBox1.Enabled = true));
            }
            if (button1.InvokeRequired)
            {
                button1.Invoke((MethodInvoker)(() => button1.Enabled = true));
            }
            if (button2.InvokeRequired)
            {
                button2.Invoke((MethodInvoker)(() => button2.Enabled = false));
            }
            if (button3.InvokeRequired)
            {
                button3.Invoke((MethodInvoker)(() => button3.Enabled = false));
            }
            if (button4.InvokeRequired)
            {
                button4.Invoke((MethodInvoker)(() => button4.Enabled = false));
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;

            currentNumber = lowerBound;

            if (comboBox1.SelectedIndex == 0)
            {
                genFibonacci = false;
                continueGenerating = true;
                generatorThread = new Thread(GeneratePrimeNumbers);
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                continueGenerating = false;
                genFibonacci = true;
                generatorThread = new Thread(GenerateFibonacci);
            }

            generatorThread.Start();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            continueGenerating = false;
            genFibonacci = false;

            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            comboBox1.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                upperBound = int.Parse(textBox2.Text);
            }
            else if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                upperBound = int.MaxValue;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                lowerBound = int.Parse(textBox1.Text);
            }
            else if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                lowerBound = 2;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            isPaused = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            isPaused = true;

        }

    }
}