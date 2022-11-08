namespace PingI
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            StreamReader sr = new StreamReader("Settings.ini");
            textBox1.Text = sr.ReadLine();
            textBox2.Text = sr.ReadLine();
            textBox3.Text = sr.ReadLine();
            textBox4.Text = sr.ReadLine();
            textBox5.Text = sr.ReadLine();
            textBox6.Text = sr.ReadLine();
            textBox7.Text = sr.ReadLine();
            textBox8.Text = sr.ReadLine();
            textBox9.Text = sr.ReadLine();
            textBox10.Text = sr.ReadLine();
            sr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("settings.ini");
            sw.WriteLine(textBox1.Text);
            sw.WriteLine(textBox2.Text);
            sw.WriteLine(textBox3.Text);
            sw.WriteLine(textBox4.Text);
            sw.WriteLine(textBox5.Text);
            sw.WriteLine(textBox6.Text);
            sw.WriteLine(textBox7.Text);
            sw.WriteLine(textBox8.Text);
            sw.WriteLine(textBox9.Text);
            sw.WriteLine(textBox10.Text);
            sw.Close();
            this.Close();
        }
    }
}
