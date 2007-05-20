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
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms.Player
{
    /// <summary>
    /// Summary description for AppForm.
    /// </summary>
    public class AppForm : Form, IKeepAlive
    {
        /// <summary>
        /// Public for testing purposes.
        /// </summary>
        public StatusBar statusBar1;

        private ScriptPlayer player = new ScriptPlayer();

        private TrackBar speedBar;

        private Label lblSpeed;

        private GutterControl gutter;

        private RichTextBox txtScript;

        private Button btnStep;

        private Button btnLoad;

        private Button btnOpen;

        private Button btnRun;

        public AppForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.speedBar = new System.Windows.Forms.TrackBar();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.gutter = new NUnit.Extensions.Forms.GutterControl();
            this.txtScript = new System.Windows.Forms.RichTextBox();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize) (this.speedBar)).BeginInit();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 439);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(792, 22);
            this.statusBar1.TabIndex = 2;
            this.statusBar1.Text = "Ready...";
            // 
            // speedBar
            // 
            this.speedBar.Anchor =
                    ((System.Windows.Forms.AnchorStyles)
                     ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.speedBar.LargeChange = 10;
            this.speedBar.Location = new System.Drawing.Point(416, 396);
            this.speedBar.Maximum = 100;
            this.speedBar.Name = "speedBar";
            this.speedBar.Size = new System.Drawing.Size(368, 42);
            this.speedBar.SmallChange = 5;
            this.speedBar.TabIndex = 3;
            this.speedBar.Value = 100;
            this.speedBar.Scroll += new System.EventHandler(this.speedBar_Scroll);
            // 
            // lblSpeed
            // 
            this.lblSpeed.Anchor =
                    ((System.Windows.Forms.AnchorStyles)
                     ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpeed.Location = new System.Drawing.Point(368, 400);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(40, 23);
            this.lblSpeed.TabIndex = 4;
            this.lblSpeed.Text = "Speed";
            // 
            // gutter
            // 
            this.gutter.Anchor =
                    ((System.Windows.Forms.AnchorStyles)
                     (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) |
                       System.Windows.Forms.AnchorStyles.Left)));
            this.gutter.ArrowAtLine = 1;
            this.gutter.Inset = 2;
            this.gutter.Location = new System.Drawing.Point(0, 40);
            this.gutter.Name = "gutter";
            this.gutter.Offset = 6;
            this.gutter.Size = new System.Drawing.Size(32, 344);
            this.gutter.Spacing = 0;
            this.gutter.TabIndex = 5;
            this.gutter.Text = "gutterControl1";
            // 
            // txtScript
            // 
            this.txtScript.Anchor =
                    ((System.Windows.Forms.AnchorStyles)
                     ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) |
                        System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScript.Font = new System.Drawing.Font("Courier New", 10F);
            this.txtScript.Location = new System.Drawing.Point(32, 40);
            this.txtScript.Name = "txtScript";
            this.txtScript.ShowSelectionMargin = true;
            this.txtScript.Size = new System.Drawing.Size(760, 348);
            this.txtScript.TabIndex = 6;
            this.txtScript.Text = "";
            this.txtScript.WordWrap = false;
            this.txtScript.TextChanged += new System.EventHandler(this.txtScript_TextChanged);
            // 
            // btnStep
            // 
            this.btnStep.Anchor =
                    ((System.Windows.Forms.AnchorStyles)
                     ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStep.Enabled = false;
            this.btnStep.Location = new System.Drawing.Point(280, 400);
            this.btnStep.Name = "btnStep";
            this.btnStep.TabIndex = 8;
            this.btnStep.Text = "Step";
            this.btnStep.Visible = false;
            this.btnStep.Click += new System.EventHandler(this.step_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(32, 8);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(112, 23);
            this.btnLoad.TabIndex = 9;
            this.btnLoad.Text = "Load Assembly";
            this.btnLoad.Click += new System.EventHandler(this.load_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(152, 8);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.TabIndex = 10;
            this.btnOpen.Text = "Open Script";
            this.btnOpen.Click += new System.EventHandler(this.open_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(240, 8);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(160, 23);
            this.btnRun.TabIndex = 11;
            this.btnRun.Text = "Run";
            this.btnRun.Click += new System.EventHandler(this.play_Click);
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5, 13);
            this.ClientSize = new System.Drawing.Size(792, 461);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.txtScript);
            this.Controls.Add(this.gutter);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.speedBar);
            this.Controls.Add(this.statusBar1);
            this.Name = "AppForm";
            this.Text = "NUnitForms Script Runner";
            this.Resize += new System.EventHandler(this.OnResize);
            this.Load += new System.EventHandler(this.AppForm_Load);
            ((System.ComponentModel.ISupportInitialize) (this.speedBar)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        public void OpenScript()
        {
            txtScript.Text =
                    @"NUnit.Extensions.Forms.TestApplications.ScriptDemoForm, NUnitForms.Test
textBox Enter abC
textBox Assert Text abC
button Mouse Click
counter Assert Text 1

button Mouse Click
counter Assert Text 2
textBox Keys Type +(abc)def 3
textBox Assert Text ABCdef 3";
        }

        private bool ready = true;

        /// <summary>
        /// 
        /// </summary>
        public void Play()
        {
            if(ready)
            {
                ready = false;
                statusBar1.Text = "Running ...";
                gutter.Clear();
                try
                {
                    player = new ScriptPlayer();
                    player.Success += new ScriptPlayer.SuccessHandler(Success);
                    player.BeforeExecute += new ScriptPlayer.BeforeExecuteHandler(BeforeLine);
                    player.AfterExecute += new ScriptPlayer.AfterExecuteHandler(AfterLine);

                    player.Speed.Value = speedBar.Value;
                    player.setHidden(false);
                    keepAlive = true;
                    player.Play(txtScript.Text);
                }
                catch(FormsTestAssertionException ex)
                {
                    statusBar1.Text = "Failed!";
                    MessageBox.Show(ex.Message, "Script Failure");
                }
                finally
                {
                    keepAlive = false;
                }
                ready = true;
            }
        }

        public void Success(bool success)
        {
            if(success)
            {
                statusBar1.Text = "Success!";
            }
            else
            {
                statusBar1.Text = "Failed!";
            }
        }

        private int ComputeLineFromLineNumber(int lineNumber)
        {
            //should take scrolling into account here.
            return lineNumber + 1;
        }

        public void BeforeLine(int lineNumber)
        {
            statusBar1.Text = "Running Line " + lineNumber;
            gutter.ArrowAtLine = ComputeLineFromLineNumber(lineNumber);
            Application.DoEvents();
            while(stepCount == 0 && speedBar.Value == 0)
            {
                Application.DoEvents();
            }
            if(stepCount > 0)
            {
                stepCount--;
            }
        }

        public void AfterLine(int lineNumber, bool success, bool assert, string message)
        {
            int line = ComputeLineFromLineNumber(lineNumber);
            gutter.ArrowAtLine = line + 1;
            if(assert || !success)
            {
                gutter.Add(new GutterItem(line, success, message));
            }
            Application.DoEvents();
        }

        private void LoadAssembly()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"D:\opensource\nunit2.0\bin";
            ofd.Filter = "Dll files (*.dll)|*.dll|Exe files (*.exe)|*.exe|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = true;
            ofd.ReadOnlyChecked = true;
            ofd.RestoreDirectory = true;

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                foreach(string fileName in ofd.FileNames)
                {
                    Assembly.LoadFrom(new FileInfo(fileName).ToString());
                }
            }
        }

        private bool keepAlive = false;

        private void speedBar_Scroll(object sender, EventArgs e)
        {
            player.Speed.Value = speedBar.Value;
            btnStep.Enabled = player.Speed.StepMode;
            btnStep.Visible = player.Speed.StepMode;
        }

        private void OnResize(object sender, EventArgs e)
        {
            ResetGutter();
        }

        private void ResetGutter()
        {
            gutter.Spacing = txtScript.Font.Height;
            gutter.Invalidate();
        }

        private int stepCount = 0;

        private void step_Click(object sender, EventArgs e)
        {
            if(!ready)
            {
                stepCount++;
            }
            else
            {
                Play();
            }
        }

        private void load_Click(object sender, EventArgs e)
        {
            LoadAssembly();
        }

        private void open_Click(object sender, EventArgs e)
        {
            OpenScript();
        }

        private void play_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void AppForm_Load(object sender, EventArgs e)
        {
            ResetGutter();
        }

        private void txtScript_TextChanged(object sender, EventArgs e)
        {
            ResetGutter();
            gutter.Clear();
        }

        bool IKeepAlive.KeepAlive
        {
            get
            {
                return keepAlive;
            }
        }
    }
}