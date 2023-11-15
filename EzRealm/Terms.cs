using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EzRealm
{
    public partial class Terms : Form
    {
        public Terms()
        {
            InitializeComponent();
            button1.MouseHover += new EventHandler(enterAnim);
            FetchButton.MouseHover += new EventHandler(enterAnim);
            button1.MouseLeave += new EventHandler(exitAnim);
            FetchButton.MouseLeave += new EventHandler(exitAnim);
        }

        bool entered;
        public void enterAnim(object s, EventArgs e) 
        {
            if (s is Button button1)
            {
                if (entered != true)
                {
                 entered = true;
                    button1.Location = new Point(button1.Location.X + 5, button1.Location.Y + 5);
                }
                else { }
               
                
            }
        }

        public void exitAnim(object s, EventArgs e) 
        {
            if (s is Button button1) 
            {
                if (entered == true)
                {
                    button1.Location = new Point(button1.Location.X - 5, button1.Location.Y - 5);
                    entered = false;
                }
            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.hasAgreed = true;
            this.Close();
        }

        private void FetchButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Terms_Load(object sender, EventArgs e)
        {
            int radius = 26;
            GraphicsPath path = new GraphicsPath();
            int arcPoints = 2; // Number of points to define the arc

            // Enable anti-aliasing
            Graphics graphics = CreateGraphics();
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Top left corner
            path.AddArc(0, 0, radius * 2, radius * 2, 180, arcPoints);
            path.AddLine(radius, 0, this.Width - radius, 0);

            // Top right corner
            path.AddArc(this.Width - radius * 2, 0, radius * 2, radius * 2, 270, arcPoints);
            path.AddLine(this.Width, radius, this.Width, this.Height - radius);

            // Bottom right corner
            path.AddArc(this.Width - radius * 2, this.Height - radius * 2, radius * 2, radius * 2, 0, arcPoints);
            path.AddLine(this.Width - radius, this.Height, radius, this.Height);

            // Bottom left corner
            path.AddArc(0, this.Height - radius * 2, radius * 2, radius * 2, 90, arcPoints);
            path.AddLine(0, this.Height - radius, 0, radius);

            this.Region = new Region(path);

            // Dispose the Graphics object
            graphics.Dispose();
        }
    }
}
