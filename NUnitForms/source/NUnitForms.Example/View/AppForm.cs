#region Copyright (c) 2003-2005, Luke T. Maxon

/********************************************************************************************************************
'
' Copyright (c) 2003-2005, Luke T. Maxon
' All rights reserved.
' 
' Redistribution and use in source and binary forms, with or without modification, are permitted provided
' that the following conditions are met:
' 
' * Redistributions of source code must retain the above copyright notice, this list of conditions and the
' 	following disclaimer.
' 
' * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and
' 	the following disclaimer in the documentation and/or other materials provided with the distribution.
' 
' * Neither the name of the author nor the names of its contributors may be used to endorse or 
' 	promote products derived from this software without specific prior written permission.
' 
' THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
' WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
' PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
' ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
' LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
' INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
' OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN
' IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'
'*******************************************************************************************************************/

#endregion

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms.ExampleApplication
{
    /// <summary>
    /// Summary description for AppForm.
    /// </summary>
    public class AppForm : Form
    {
        private IAppController controller;

        private Button countButton;

        private Button showModalButton;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public AppForm(IAppController controller)
        {
            this.controller = controller;

            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        private void UpdateDisplay()
        {
            countButton.Text = controller.GetData();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.countButton = new Button();
            this.showModalButton = new Button();
            this.SuspendLayout();
            // 
            // countButton
            // 
            this.countButton.Location = new Point(48, 40);
            this.countButton.Name = "countButton";
            this.countButton.TabIndex = 0;
            this.countButton.Text = "0";
            this.countButton.Click += new EventHandler(this.countButton_Click);
            // 
            // showModalButton
            // 
            this.showModalButton.Location = new Point(48, 96);
            this.showModalButton.Name = "showModalButton";
            this.showModalButton.TabIndex = 1;
            this.showModalButton.Text = "Show Modal";
            this.showModalButton.Click += new EventHandler(this.showModalButton_Click);
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new SizeF(5, 13);
            this.ClientSize = new Size(292, 273);
            this.Controls.Add(this.showModalButton);
            this.Controls.Add(this.countButton);
            this.Name = "AppForm";
            this.Text = "AppForm";
            this.Load += new EventHandler(this.AppForm_Load);
            this.ResumeLayout(false);
        }

        #endregion

        private void countButton_Click(object sender, EventArgs e)
        {
            controller.Count();
            UpdateDisplay();
        }

        private void showModalButton_Click(object sender, EventArgs e)
        {
            controller.ShowModal();
        }

        private void AppForm_Load(object sender, EventArgs e)
        {
            UpdateDisplay();
        }
    }
}