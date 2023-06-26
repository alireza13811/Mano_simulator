using System.Windows.Forms;

namespace Mano_simulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F5)
            {
                runButton.PerformClick();
            }
        }
        private void runButton_Click(object sender, EventArgs e)
        {
            microprogramcodeText.Text = microprogramcodeText.Text + "\n" + "surprise mf";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void stepoverButton_Click(object sender, EventArgs e)
        {
            int y = label9.Location.Y;
            y = y + 20;
            label9.Location = new Point(label9.Location.X, y);
            if(y >= 480)
            {
                label9.Location = new Point(label9.Location.X, 78);
                microprogramcodeText.SelectionStart = 20;
                microprogramcodeText.ScrollToCaret();
            }
            
        }
    }
}