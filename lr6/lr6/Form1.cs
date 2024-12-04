using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lr6
{
    public partial class Form1 : Form
    {
        private Thread thread1;
        private Thread thread2;
        private Thread thread3;

        private bool isRunning = false;

        public Form1()
        {
            InitializeComponent();
            InitializePanels();
        }

        private void InitializePanels()
        {
            panel1.BackColor = Color.Red;
            panel2.BackColor = Color.Green;
            panel3.BackColor = Color.Blue;

            panel1.Size = new Size(50, 50);
            panel2.Size = new Size(50, 50);
            panel3.Size = new Size(50, 50);

            panel1.Location = new Point(10, 10);
            panel2.Location = new Point(10, 100);
            panel3.Location = new Point(10, 190);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                isRunning = true;
                thread1 = new Thread(() => MovePanel(panel1));
                thread2 = new Thread(() => MovePanel(panel2));
                thread3 = new Thread(() => MovePanel(panel3));

                thread1.Start();
                thread2.Start();
                thread3.Start();
            }
        }

        private void MovePanel(Panel panel)
        {
            Random random = new Random();
            int direction = random.Next(1, 3); // 1 - вправо, 2 - влево
            int moveAmount = 10;

            while (isRunning)
            {
                if (direction == 1) // двигаемся вправо
                {
                    panel.Invoke((MethodInvoker)delegate
                    {
                        if (panel.Location.X < this.Width - panel.Width)
                        {
                            panel.Location = new Point(panel.Location.X + moveAmount, panel.Location.Y);
                        }
                        else
                        {
                            direction = 2; // меняем направление
                        }
                    });
                }
                else // двигаемся влево
                {
                    panel.Invoke((MethodInvoker)delegate
                    {
                        if (panel.Location.X > 0)
                        {
                            panel.Location = new Point(panel.Location.X - moveAmount, panel.Location.Y);
                        }
                        else
                        {
                            direction = 1; // меняем направление
                        }
                    });
                }

                Thread.Sleep(100); // Задержка для контролируемого движения
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isRunning = false; // Прекращаем движение при закрытии формы
            thread1?.Join();
            thread2?.Join();
            thread3?.Join();
            base.OnFormClosing(e);
        }
    }
}

