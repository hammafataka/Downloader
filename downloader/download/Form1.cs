using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace download
{
    public partial class Form1 : Form
    {
        private Rectangle label1OriginalRect;
        private Rectangle enterButtonOriginalRect;
        private Size form1size;
        public Form1()
        {

            InitializeComponent();

        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainPage main = new MainPage();
            main.Show();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            form1size = this.Size;
            label1OriginalRect = new Rectangle(label1.Location.X,label1.Location.Y,label1.Width,label1.Height);
            enterButtonOriginalRect = new Rectangle(EnterButton.Location.X, EnterButton.Location.Y, EnterButton.Width, EnterButton.Height);


            //label1.BackColor = Color.FromArgb(100,0,0,0);
            EnterButton.ForeColor = Color.FromArgb(100, 0, 0, 0);


        }
        private void resizeChildrenControls()
        {
            resizeControl(label1OriginalRect, label1);
            resizeControl(enterButtonOriginalRect, EnterButton);
    


        }
        private void resizeControl(Rectangle OriginalControlRect, Control control)
        {
            float xRatio = (float)(this.Width) / (float)(form1size.Width);
            float yRatio = (float)(this.Height) / (float)(form1size.Height);


            int newX = (int)(OriginalControlRect.X * xRatio);
            int newY = (int)(OriginalControlRect.Y * yRatio);

            int newWidth = (int)(OriginalControlRect.Width * xRatio);
            int newHeight = (int)(OriginalControlRect.Height * yRatio);

            control.Location = new Point(newX, newY);
            control.Size = new Size(newWidth, newHeight);



        }

    

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Resize_1(object sender, EventArgs e)
        {
            resizeChildrenControls();


        }
    }
}
