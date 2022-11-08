using System.Net.NetworkInformation;

namespace PingI
{
    public partial class PingW : Form
    {
        string[] lasttime = new string[5];
        res ping1, ping2, ping3, ping4, ping5 = new res();
        
        string[] a = new string[10];
        string[] b = new string[10];
        public struct res
        {
            public string lastping,lasttime;
            public bool status;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            work = false;
            Thread.Sleep(1000);
            timer1.Enabled = false;
            button1.Enabled = true;
            button3.Enabled = false;
        }

        bool work = true;
        private void button2_Click(object sender, EventArgs e)
        {
            button3.PerformClick();
            StreamReader sr = new StreamReader("Settings.ini");
            for (int i = 0; i<10; i++)
            {
                a[i] = sr.ReadLine();
            }
            sr.Close();
            work = false;
            Thread.Sleep(1000);
            label1.Text = a[0];
            label2.Text = a[2];
            label3.Text = a[4];
            label4.Text = a[6];
            label5.Text = a[8];
            button1.PerformClick();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = ping1.lastping;
            label7.Text = ping2.lastping;
            label8.Text = ping3.lastping;
            label9.Text = ping4.lastping;
            label10.Text = ping5.lastping;
            if (!ping1.status)
            {
                label11.Text = ping1.lasttime;
            }
            if (!ping2.status)
            {
                label12.Text = ping2.lasttime;
            }
            if (!ping3.status)
            {
                label13.Text = ping3.lasttime;
            }
            if (!ping4.status)
            {
                label14.Text = ping4.lasttime;
            }
            if (!ping5.status)
            {
                label15.Text = ping5.lasttime;
            }
            pictureBox1.Visible = ping1.status;
            pictureBox2.Visible = ping2.status;
            pictureBox3.Visible = ping3.status;
            pictureBox4.Visible = ping4.status;
            pictureBox5.Visible = ping5.status;
        }

        public PingW()
        {
            InitializeComponent();
            FileStream? fstream = null;
            fstream = new FileStream("settings.ini", FileMode.OpenOrCreate);
            fstream?.Close();
            for (int i = 0; i < 5; i++)
            {
                lasttime[i] = Convert.ToString(DateTime.Now);
            }
            label11.Text = lasttime[0];
            label12.Text = lasttime[1];
            label13.Text = lasttime[2];
            label14.Text = lasttime[3];
            label15.Text = lasttime[4];
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            work = false;
        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings newSettings = new Settings();
            newSettings.Show();
        }

        void Pinging1()
        {
            work = true;
            while (work)
            {
                ping1 = Startpinging(a[1]);
                Thread.Sleep(1000);
            }
        }

        void Pinging2()
        {
            work = true;
            while (work)
            {
                ping2 = Startpinging(a[3]);
                Thread.Sleep(1000);
            }
        }

        void Pinging3()
        {
            work = true;
            while (work)
            {
                ping3 = Startpinging(a[5]);
                Thread.Sleep(1000);
            }
        }

        void Pinging4()
        {
            work = true;
            while (work)
            {
                ping4 = Startpinging(a[7]);
                Thread.Sleep(1000);
            }
        }

        void Pinging5()
        {
            work = true;
            while (work)
            {
                ping5 = Startpinging(a[9]);
                Thread.Sleep(1000);
            }
        }

        private static res Startpinging(string address)
        {
            string a;
            bool ch = false;
            res ping = new res();
            Ping pingN = new();
            PingReply reply = pingN.Send("localhost");
            try
            {
                reply = pingN.Send(address);
                ping.status = false;
                ch = false;
            }
            catch
            {
                ping.status = true;
                ch = true;
            }
            a = Convert.ToString(reply.Status);
            if (a == "Success" & !ch)
            {
                ping.lasttime = Convert.ToString(DateTime.Now);
            }
            else
            {
                ping.status = true;
            }
            ping.lastping = Convert.ToString(reply.RoundtripTime);
            return ping;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            Thread pinging1 = new Thread(Pinging1);
            Thread pinging2 = new Thread(Pinging2);
            Thread pinging3 = new Thread(Pinging3);
            Thread pinging4 = new Thread(Pinging4);
            Thread pinging5 = new Thread(Pinging5);
            pinging1.Start();
            pinging2.Start();
            pinging3.Start();
            pinging4.Start();
            pinging5.Start();
            button1.Enabled = false;
            button3.Enabled = true;
        }
    }
}