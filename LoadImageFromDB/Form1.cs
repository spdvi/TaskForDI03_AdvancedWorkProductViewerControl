using ProductViewControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestControlUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            productView1.SizeButtonClicked += SizeButton_Clicked;
        }

        private void SizeButton_Clicked(object sender, SizeButtonClickedEventArgs e)
        {
            textBox2.Text = e.ProductId.ToString();
        }

    }
}
