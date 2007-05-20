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

//Contributed by: Ian Cooper

using System.Windows.Forms;

namespace NUnit.Extensions.Forms.TestApplications
{
    public class ToolbarTestForm : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components;

        private System.Windows.Forms.ToolBarButton toolBarButtonToggle;

        private System.Windows.Forms.MenuItem menuItemBlue;

        private System.Windows.Forms.ImageList imageListToolbar;

        private System.Windows.Forms.MenuItem menuItemViolet;

        private System.Windows.Forms.Label labelToolbarSelection;

        private System.Windows.Forms.ToolBarButton toolBarButtonSeperator2;

        private System.Windows.Forms.ToolBar toolBarTest;

        private System.Windows.Forms.ContextMenu contextMenuColor;

        private System.Windows.Forms.MenuItem menuItemYellow;

        private System.Windows.Forms.ToolBarButton toolBarButtonSeperator;

        private System.Windows.Forms.MenuItem menuItemRed;

        private System.Windows.Forms.ToolBarButton toolBarButtonNext;

        private System.Windows.Forms.MenuItem menuItemOrange;

        private System.Windows.Forms.ToolBarButton toolBarButtonPrevious;

        private System.Windows.Forms.MenuItem menuItemIndigo;

        private System.Windows.Forms.ToolBarButton toolBarButtonColorPicker;

        private System.Windows.Forms.ToolBarButton toolBarButtonClose;

        private System.Windows.Forms.MenuItem menuItemGreen;

        private System.Windows.Forms.ToolBarButton toolBarButtonOpen;

        public ToolbarTestForm()
        {
            InitializeComponent();
        }

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
            this.components = new System.ComponentModel.Container();
            this.toolBarButtonOpen = new System.Windows.Forms.ToolBarButton();
            this.menuItemGreen = new System.Windows.Forms.MenuItem();
            this.toolBarButtonClose = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonColorPicker = new System.Windows.Forms.ToolBarButton();
            this.menuItemIndigo = new System.Windows.Forms.MenuItem();
            this.toolBarButtonPrevious = new System.Windows.Forms.ToolBarButton();
            this.menuItemOrange = new System.Windows.Forms.MenuItem();
            this.toolBarButtonNext = new System.Windows.Forms.ToolBarButton();
            this.menuItemRed = new System.Windows.Forms.MenuItem();
            this.toolBarButtonSeperator = new System.Windows.Forms.ToolBarButton();
            this.menuItemYellow = new System.Windows.Forms.MenuItem();
            this.contextMenuColor = new System.Windows.Forms.ContextMenu();
            this.toolBarTest = new System.Windows.Forms.ToolBar();
            this.toolBarButtonSeperator2 = new System.Windows.Forms.ToolBarButton();
            this.labelToolbarSelection = new System.Windows.Forms.Label();
            this.menuItemViolet = new System.Windows.Forms.MenuItem();
            this.imageListToolbar = new System.Windows.Forms.ImageList(this.components);
            this.menuItemBlue = new System.Windows.Forms.MenuItem();
            this.toolBarButtonToggle = new System.Windows.Forms.ToolBarButton();
            this.SuspendLayout();
            // 
            // toolBarButtonOpen
            // 
            this.toolBarButtonOpen.ImageIndex = 0;
            this.toolBarButtonOpen.Text = "Open";
            this.toolBarButtonOpen.ToolTipText = "Open File";
            // 
            // menuItemGreen
            // 
            this.menuItemGreen.Index = 3;
            this.menuItemGreen.Text = "Green";
            this.menuItemGreen.Click += new System.EventHandler(this.menuItemGreen_Click);
            // 
            // toolBarButtonClose
            // 
            this.toolBarButtonClose.ImageIndex = 1;
            this.toolBarButtonClose.Text = "Close";
            this.toolBarButtonClose.ToolTipText = "Close File";
            // 
            // toolBarButtonColorPicker
            // 
            this.toolBarButtonColorPicker.DropDownMenu = this.contextMenuColor;
            this.toolBarButtonColorPicker.ImageIndex = 4;
            this.toolBarButtonColorPicker.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            this.toolBarButtonColorPicker.Text = "Color";
            this.toolBarButtonColorPicker.ToolTipText = "Pick a Color";
            // 
            // menuItemIndigo
            // 
            this.menuItemIndigo.Index = 5;
            this.menuItemIndigo.Text = "Indigo";
            this.menuItemIndigo.Click += new System.EventHandler(this.menuItemIndigo_Click);
            // 
            // toolBarButtonPrevious
            // 
            this.toolBarButtonPrevious.ImageIndex = 3;
            this.toolBarButtonPrevious.Text = "Previous";
            this.toolBarButtonPrevious.ToolTipText = "Previous Record";
            // 
            // menuItemOrange
            // 
            this.menuItemOrange.Index = 1;
            this.menuItemOrange.Text = "Orange";
            this.menuItemOrange.Click += new System.EventHandler(this.menuItemOrange_Click);
            // 
            // toolBarButtonNext
            // 
            this.toolBarButtonNext.ImageIndex = 2;
            this.toolBarButtonNext.Text = "Next";
            this.toolBarButtonNext.ToolTipText = "Next Record";
            // 
            // menuItemRed
            // 
            this.menuItemRed.Index = 0;
            this.menuItemRed.Text = "Red";
            this.menuItemRed.Click += new System.EventHandler(this.menuItemRed_Click);
            // 
            // toolBarButtonSeperator
            // 
            this.toolBarButtonSeperator.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // menuItemYellow
            // 
            this.menuItemYellow.Index = 2;
            this.menuItemYellow.Text = "Yellow";
            this.menuItemYellow.Click += new System.EventHandler(this.menuItemYellow_Click);
            // 
            // contextMenuColor
            // 
            this.contextMenuColor.MenuItems.AddRange(
                    new System.Windows.Forms.MenuItem[]
                            {
                                    this.menuItemRed, this.menuItemOrange, this.menuItemYellow, this.menuItemGreen,
                                    this.menuItemBlue, this.menuItemIndigo, this.menuItemViolet
                            });
            // 
            // toolBarTest
            // 
            this.toolBarTest.Buttons.AddRange(
                    new System.Windows.Forms.ToolBarButton[]
                            {
                                    this.toolBarButtonOpen, this.toolBarButtonClose, this.toolBarButtonNext,
                                    this.toolBarButtonPrevious, this.toolBarButtonSeperator, this.toolBarButtonColorPicker,
                                    this.toolBarButtonSeperator2, this.toolBarButtonToggle
                            });
            this.toolBarTest.DropDownArrows = true;
            this.toolBarTest.ImageList = this.imageListToolbar;
            this.toolBarTest.Location = new System.Drawing.Point(0, 0);
            this.toolBarTest.Name = "toolBarTest";
            this.toolBarTest.ShowToolTips = true;
            this.toolBarTest.Size = new System.Drawing.Size(352, 50);
            this.toolBarTest.TabIndex = 0;
            this.toolBarTest.ButtonClick +=
                    new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // toolBarButtonSeperator2
            // 
            this.toolBarButtonSeperator2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // labelToolbarSelection
            // 
            this.labelToolbarSelection.Location = new System.Drawing.Point(80, 80);
            this.labelToolbarSelection.Name = "labelToolbarSelection";
            this.labelToolbarSelection.TabIndex = 1;
            this.labelToolbarSelection.Text = "label1";
            // 
            // menuItemViolet
            // 
            this.menuItemViolet.Index = 6;
            this.menuItemViolet.Text = "Violet";
            this.menuItemViolet.Click += new System.EventHandler(this.menuItemViolet_Click);
            // 
            // imageListToolbar
            // 
            this.imageListToolbar.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListToolbar.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // menuItemBlue
            // 
            this.menuItemBlue.Index = 4;
            this.menuItemBlue.Text = "Blue";
            this.menuItemBlue.Click += new System.EventHandler(this.menuItemBlue_Click);
            // 
            // toolBarButtonToggle
            // 
            this.toolBarButtonToggle.ImageIndex = 5;
            this.toolBarButtonToggle.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // ToolbarTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5, 14);
            this.ClientSize = new System.Drawing.Size(352, 272);
            this.Controls.Add(this.labelToolbarSelection);
            this.Controls.Add(this.toolBarTest);
            this.Name = "ToolbarTestForm";
            this.Text = "ToolbarTestForm";
            this.ResumeLayout(false);
        }

        #endregion

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            labelToolbarSelection.Text = e.Button.Text;
        }

        private void menuItemRed_Click(object sender, System.EventArgs e)
        {
            OnMenuItemClick((MenuItem) sender);
        }

        private void menuItemOrange_Click(object sender, System.EventArgs e)
        {
            OnMenuItemClick((MenuItem) sender);
        }

        private void menuItemYellow_Click(object sender, System.EventArgs e)
        {
            OnMenuItemClick((MenuItem) sender);
        }

        private void menuItemGreen_Click(object sender, System.EventArgs e)
        {
            OnMenuItemClick((MenuItem) sender);
        }

        private void menuItemBlue_Click(object sender, System.EventArgs e)
        {
            OnMenuItemClick((MenuItem) sender);
        }

        private void menuItemIndigo_Click(object sender, System.EventArgs e)
        {
            OnMenuItemClick((MenuItem) sender);
        }

        private void menuItemViolet_Click(object sender, System.EventArgs e)
        {
            OnMenuItemClick((MenuItem) sender);
        }

        private void OnMenuItemClick(MenuItem sender)
        {
            labelToolbarSelection.Text = sender.Text;
        }
    }
}