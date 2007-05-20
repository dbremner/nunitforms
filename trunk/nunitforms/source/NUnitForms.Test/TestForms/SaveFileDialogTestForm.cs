using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms.TestApplications
{
  public partial class SaveFileDialogTestForm : Form
  {
    public SaveFileDialogTestForm()
    {
      InitializeComponent();
    }

    private void btSave_Click(object sender, EventArgs e)
    {
      SaveFileDialog save_dlg = new SaveFileDialog();
      if (save_dlg.ShowDialog() == DialogResult.OK)
      {
        lblFileName.Text = save_dlg.FileName;
      }
      else
      {
        lblFileName.Text = "cancel pressed";
      }
      
    } 
  }
}