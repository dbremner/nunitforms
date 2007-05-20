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

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms
{
    public class GutterControl : Control
    {
        public GutterControl()
        {
            InitializeComponent();
        }

        private int arrowAtLine = 1;

        private ToolTip toolTip1;

        private IContainer components;

        private int width = 0;

        private void InitializeComponent()
        {
            components = new Container();
            toolTip1 = new ToolTip(components);
            // 
            // toolTip1
            // 
            toolTip1.AutomaticDelay = 0;
            toolTip1.AutoPopDelay = 999999;
            toolTip1.InitialDelay = 0;
            toolTip1.ReshowDelay = 0;
            toolTip1.ShowAlways = true;
        }

        public void Add(GutterItem item)
        {
            Control control;

            if(item.Pass)
            {
                control = new PassMarker();
            }
            else
            {
                control = new FailMarker();
            }
            Rectangle rect = getRectangle(item.LineNumber);
            control.Size = rect.Size;
            control.Location = rect.Location;
            Controls.Add(control);

            toolTip1.SetToolTip(control, item.Message);
        }

        public void Clear()
        {
            Controls.Clear();
            toolTip1.RemoveAll();
        }

        public int ArrowAtLine
        {
            get
            {
                return arrowAtLine;
            }
            set
            {
                arrowAtLine = value;
                Invalidate();
            }
        }

        private int spacing;

        public int Spacing
        {
            get
            {
                return spacing;
            }
            set
            {
                spacing = value;
            }
        }

        private int offset;

        public int Offset
        {
            get
            {
                return offset;
            }
            set
            {
                offset = value;
            }
        }

        private int inset;

        public int Inset
        {
            get
            {
                return inset;
            }
            set
            {
                inset = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            width = e.ClipRectangle.Width;

            Pen pen = new Pen(Color.Blue);
            pen.Width = 2;

            int yCoord = Spacing*ArrowAtLine - Offset;

            Point p1 = new Point(inset, yCoord);
            Point p2 = new Point(width - inset, yCoord);
            Point p3 = new Point(width - inset - 8, yCoord - 8);
            Point p4 = new Point(width - inset - 8, yCoord + 8);

            e.Graphics.DrawLine(pen, p1, p2);
            e.Graphics.DrawLine(pen, p2, p3);
            e.Graphics.DrawLine(pen, p2, p4);
        }

        private Rectangle getRectangle(int lineNumber)
        {
            int size = Spacing - 4;
            int top = Spacing*lineNumber - Offset - size/2;
            return new Rectangle((width - size)/2, top, size, size);
        }
    }
}