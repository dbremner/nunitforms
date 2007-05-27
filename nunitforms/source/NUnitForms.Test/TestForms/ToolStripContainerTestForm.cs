using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms.Test.TestForms
{
    public partial class ToolStripContainerTestForm : Form
    {
        public ToolStripContainerTestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tb1.Text = "Clicked";
        }

        private void tsb_Click(object sender, EventArgs e)
        {
            tsl.Text = "Clicked";
        }

    }
}