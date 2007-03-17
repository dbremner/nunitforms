#region Copyright (c) 2003-2005, Luke T. Maxon (Richard Schneider is the original author)

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
using System.Drawing;
using System.Windows.Forms;
using NUnit.Framework;

namespace NUnit.Extensions.Forms.TestApplications
{
	/// <summary>
	///   Tests the <see cref="MouseController"/>.
	/// </summary>
	[TestFixture]
	[Category("DisplayHidden")]
	[Category("ControlsMouse")]
	[Ignore]
	public class MouseControllerTest : NUnitFormTest
	{
		public override Type FormType
		{
			get { return typeof (TextBoxTestForm); }
		}

		public override bool DisplayHidden
		{
			get { return true; }
		}

		[Test]
		public void PositioningUnits()
		{
			using (MouseController mouse = new MouseController(new FormTester(CurrentForm.Name)))
			{
				int dpiX, dpiY;
				using (Graphics graphics = CurrentForm.CreateGraphics())
				{
					dpiX = (int) graphics.DpiX;
					dpiY = (int) graphics.DpiY;
				}
				Point targetPoint = CurrentForm.PointToScreen(new Point(dpiX, 2*dpiY));

				mouse.PositionUnit = GraphicsUnit.Pixel;
				mouse.Position = new PointF(10, 10);
				Assert.AreEqual(new PointF(10, 10), mouse.Position);
				Assert.AreEqual(CurrentForm.PointToScreen(new Point(10, 10)), Control.MousePosition);

				mouse.PositionUnit = GraphicsUnit.Display;
				mouse.Position = new PointF(75, 150);
				Assert.AreEqual(new PointF(75, 150), mouse.Position);
				Assert.AreEqual(targetPoint, Control.MousePosition);

				mouse.PositionUnit = GraphicsUnit.Document;
				mouse.Position = new PointF(300, 600);
				Assert.AreEqual(new PointF(300, 600), mouse.Position);
				Assert.AreEqual(targetPoint, Control.MousePosition);

				mouse.PositionUnit = GraphicsUnit.Inch;
				mouse.Position = new PointF(1, 2);
				Assert.AreEqual(new PointF(1, 2), mouse.Position);
				Assert.AreEqual(targetPoint, Control.MousePosition);

				mouse.PositionUnit = GraphicsUnit.Point;
				mouse.Position = new PointF(72, 144);
				Assert.AreEqual(new PointF(72, 144), mouse.Position);
				Assert.AreEqual(targetPoint, Control.MousePosition);

				mouse.PositionUnit = GraphicsUnit.Millimeter;
				mouse.Position = new PointF(25.40f, 50.80F);
				Assert.AreEqual(new PointF(25.40f, 50.80F), mouse.Position);
				Assert.AreEqual(targetPoint, Control.MousePosition);
			}
		}

		[Test]
		[ExpectedException(typeof (NotSupportedException))]
		public void PositioningUnitsWorld()
		{
			using (MouseController mouse = new MouseController(new FormTester(CurrentForm.Name)))
			{
				mouse.PositionUnit = GraphicsUnit.World;
			}
		}

		[Test]
		public void PositioningInForm()
		{
			using (MouseController mouse = new MouseController(new FormTester(CurrentForm.Name)))
			{
				mouse.Position = new PointF(10, 10);
				Assert.AreEqual(new PointF(10, 10), mouse.Position);
				Assert.AreEqual(CurrentForm.PointToScreen(new Point(10, 10)), Control.MousePosition);
			}
		}

		[Test]
		public void PositioningInControl()
		{
			TextBoxTester textBox = new TextBoxTester("myTextBox", CurrentForm);
			using (MouseController mouse = textBox.MouseController())
			{
				mouse.Position = new PointF(1, 1);
				Assert.AreEqual(new PointF(1, 1), mouse.Position);

				Assert.AreEqual(textBox.Properties.PointToScreen(new Point(1, 1)), Control.MousePosition);
			}
		}

		[Test]
		[ExpectedException(typeof (NoSuchControlException))]
		public void MissingControl()
		{
			using (MouseController mouse = new MouseController(new ControlTester("unknownTextBox")))
			{
				mouse.Position = new PointF(1, 1);
			}
		}

		private int enter;

		private int leave;

		private int move;

		[Test]
		public void PositioningEvents()
		{
			TextBoxTester textBox = new TextBoxTester("myTextBox", CurrentForm);
			textBox.Properties.MouseEnter += new EventHandler(OnMouseEnter);
			textBox.Properties.MouseLeave += new EventHandler(OnMouseLeave);
			textBox.Properties.MouseMove += new MouseEventHandler(OnMouseMove);

			using (MouseController mouse = textBox.MouseController())
			{
				enter = 0;
				leave = 0;
				move = 0;

				mouse.Position = new PointF(1, 1);
				Assert.AreEqual(1, enter);
				Assert.AreEqual(0, leave);
				Assert.AreEqual(1, move);

				mouse.Position = new PointF(1, 2);
				Assert.AreEqual(1, enter);
				Assert.AreEqual(0, leave);
				Assert.AreEqual(2, move);

				mouse.Position = (Point) textBox.Properties.Size + new Size(1, 1);
				Assert.AreEqual(1, enter);
				Assert.AreEqual(1, leave);
				Assert.AreEqual(2, move);
			}
		}

		private void OnMouseEnter(object sender, EventArgs e)
		{
			++enter;
		}

		private void OnMouseLeave(object sender, EventArgs e)
		{
			++leave;
		}

		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			++move;
		}

		private int hover;


		[Test]
		public void Hovering()
		{
			TextBoxTester textBox = new TextBoxTester("myTextBox", CurrentForm);
			textBox.Properties.MouseHover += delegate { ++hover; };

			using (MouseController mouse = Mouse)
			{
				hover = 0;

				mouse.Hover();
				Assert.AreEqual(1, hover);

				mouse.Hover();
				Assert.AreEqual(2, hover);

				mouse.Hover(1, 2);
				Assert.AreEqual(3, hover);
				Assert.AreEqual(new Point(1, 2), textBox.Properties.PointToClient(Control.MousePosition));

				mouse.Hover(new PointF(1, 3));
				Assert.AreEqual(4, hover);
				Assert.AreEqual(new Point(1, 3), textBox.Properties.PointToClient(Control.MousePosition));
			}
		}

		[Test]
		public void Disposing()
		{
			Point originalPosition = Control.MousePosition;
			MouseButtons originalButtons = Control.MouseButtons;
			Keys originalModifiers = Control.ModifierKeys;

//			TextBoxTester textBox = new TextBoxTester("myTextBox", CurrentForm);
			Mouse.Position = new PointF(10, 10);
			Mouse.Press(MouseButtons.Middle);
			Mouse.Press(Keys.Shift);

			Assert.AreEqual(originalPosition, Control.MousePosition);
			Assert.AreEqual(originalButtons, Control.MouseButtons);
			Assert.AreEqual(originalModifiers, Control.ModifierKeys);
		}

		[Test]
		public void Buttons()
		{
			//FormTester formTester = new FormTester(CurrentForm.Name);
			Mouse.Press(MouseButtons.Left);
			Assert.AreEqual(MouseButtons.Left, Control.MouseButtons);

			Mouse.Release(MouseButtons.Left);
			Assert.AreEqual(MouseButtons.None, Control.MouseButtons);

			Mouse.Press(MouseButtons.Right);
			Assert.AreEqual(MouseButtons.Right, Control.MouseButtons);

			Mouse.Release(MouseButtons.Right);
			Assert.AreEqual(MouseButtons.None, Control.MouseButtons);

			Mouse.Press(MouseButtons.Middle);
			Assert.AreEqual(MouseButtons.Middle, Control.MouseButtons);

			Mouse.Release(MouseButtons.Middle);
			Assert.AreEqual(MouseButtons.None, Control.MouseButtons);
		}

		private int up;

		private int down;

		[Test]
		public void ButtonEvents()
		{
			FormTester formTester = new FormTester(CurrentForm.Name);
			CurrentForm.MouseDown += new MouseEventHandler(OnMouseDown);
			CurrentForm.MouseUp += new MouseEventHandler(OnMouseUp);
			using (MouseController mouse = formTester.MouseController())
			{
				down = 0;
				up = 0;
				mouse.Press(MouseButtons.Left);
				Assert.AreEqual(1, down);
				Assert.AreEqual(0, up);

				mouse.Release(MouseButtons.Left);
				Assert.AreEqual(1, down);
				Assert.AreEqual(1, up);
			}
		}

		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			++down;
		}

		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			++up;
		}

		[Test]
		public void XButtons()
		{
			FormTester formTester = new FormTester(CurrentForm.Name);
			using (MouseController mouse = formTester.MouseController())
			{
				if (SystemInformation.MouseButtons > 3)
				{
					mouse.Press(MouseButtons.XButton1);
					Assert.AreEqual(MouseButtons.XButton1, Control.MouseButtons);
					mouse.Press(MouseButtons.XButton2);
					Assert.AreEqual(MouseButtons.XButton1 | MouseButtons.XButton2, Control.MouseButtons);
				}

				if (SystemInformation.MouseButtons > 3)
				{
					mouse.Release(MouseButtons.XButton2);
					Assert.AreEqual(MouseButtons.XButton1, Control.MouseButtons);

					mouse.Release(MouseButtons.XButton1);
					Assert.AreEqual(MouseButtons.None, Control.MouseButtons);
				}
			}
		}

		[Test]
		public void XButtonEvents()
		{
			FormTester formTester = new FormTester(CurrentForm.Name);
			CurrentForm.MouseDown += new MouseEventHandler(OnMouseDown);
			CurrentForm.MouseUp += new MouseEventHandler(OnMouseUp);
			using (MouseController mouse = formTester.MouseController())
			{
				down = 0;
				up = 0;
				if (SystemInformation.MouseButtons > 3)
				{
					mouse.Press(MouseButtons.XButton1);
					Assert.AreEqual(1, down);
					Assert.AreEqual(0, up);
				}

				if (SystemInformation.MouseButtons > 4)
				{
					mouse.Release(MouseButtons.Left);
					Assert.AreEqual(1, down);
					Assert.AreEqual(1, up);
				}
			}
		}

		private int click;

		[Test]
		public void Clicking()
		{
			TextBoxTester textBox = new TextBoxTester("myTextBox", CurrentForm);
			textBox.Properties.Click += new EventHandler(OnClick);
			textBox.Properties.MouseDown += new MouseEventHandler(OnMouseDown);
			textBox.Properties.MouseUp += new MouseEventHandler(OnMouseUp);
			using (MouseController mouse = textBox.MouseController())
			{
				down = 0;
				up = 0;
				click = 0;
				mouse.Click(1, 3);
				Assert.AreEqual(1, down);
				Assert.AreEqual(1, up);
				Assert.AreEqual(1, click);
			}
		}

		private void OnClick(object sender, EventArgs e)
		{
			++click;
		}

		private int doubleClick;

		[Test]
		public void DoubleClicking()
		{
			TextBoxTester textBox = new TextBoxTester("myTextBox", CurrentForm);
			textBox.Properties.DoubleClick += new EventHandler(OnDoubleClick);
			using (MouseController mouse = textBox.MouseController())
			{
				doubleClick = 0;
				mouse.DoubleClick(1, 3);
				Assert.AreEqual(1, doubleClick);
			}
		}

		private void OnDoubleClick(object sender, EventArgs e)
		{
			++doubleClick;
		}

		private int drag;

		private int lastX;

		private int lastY;

		[Test]
		public void Dragging()
		{
			FormTester formTester = new FormTester(CurrentForm.Name);
			CurrentForm.MouseMove += new MouseEventHandler(OnDrag);
			using (MouseController mouse = formTester.MouseController())
			{
				drag = 0;
				lastX = -1;
				lastY = -1;
				mouse.Drag(new PointF(0, 0), new PointF(1, 1), new PointF(2, 2));
				Assert.AreEqual(3, drag);

				drag = 0;
				lastX = -1;
				lastY = -1;
				mouse.Drag(0, 0, 1, 1, 2, 2);
				Assert.AreEqual(3, drag);
			}
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void DragNull()
		{
			FormTester formTester = new FormTester(CurrentForm.Name);
			using (MouseController mouse = formTester.MouseController())
			{
				mouse.Drag(new PointF(0, 0), null);
			}
		}

		[Test]
		[ExpectedException(typeof (ArgumentException))]
		public void DragOdd()
		{
			FormTester formTester = new FormTester(CurrentForm.Name);
			using (MouseController mouse = formTester.MouseController())
			{
				mouse.Drag(0, 0, 100, 100, 200);
			}
		}

		[Test]
		[ExpectedException(typeof (ArgumentException))]
		public void DragEmpty()
		{
			FormTester formTester = new FormTester(CurrentForm.Name);
			using (MouseController mouse = formTester.MouseController())
			{
				mouse.Drag(new PointF(0, 0), new PointF[0]);
			}
		}

		private void OnDrag(object sender, MouseEventArgs e)
		{
			if (lastX != e.X || lastY != e.Y)
			{
				Assert.AreEqual(lastX + 1, e.X);
				Assert.AreEqual(lastY + 1, e.Y);
				++drag;
				lastX = e.X;
				lastY = e.Y;
			}
		}

		[Test]
		[Ignore("")]
		public void Modifiers()
		{
			FormTester formTester = new FormTester(CurrentForm.Name);

			Mouse.Press(Keys.Alt);
			Assert.AreEqual(Keys.Alt, Control.ModifierKeys);
			Mouse.Release(Keys.Alt);
			Assert.AreEqual(Keys.None, Control.ModifierKeys);

			Mouse.Press(Keys.Shift);
			Assert.AreEqual(Keys.Shift, Control.ModifierKeys);
			Mouse.Release(Keys.Shift);
			Assert.AreEqual(Keys.None, Control.ModifierKeys);

			Mouse.Press(Keys.Control);
			Assert.AreEqual(Keys.Control, Control.ModifierKeys);
			Mouse.Release(Keys.Control);
			Assert.AreEqual(Keys.None, Control.ModifierKeys);

			Mouse.Press(Keys.Control | Keys.Alt | Keys.Shift);
			Assert.AreEqual(Keys.Control | Keys.Alt | Keys.Shift, Control.ModifierKeys);
			Mouse.Release(Keys.Control | Keys.Alt | Keys.Shift);
			Assert.AreEqual(Keys.None, Control.ModifierKeys);
		}

		[Test]
		[ExpectedException(typeof (ArgumentOutOfRangeException))]
		public void ModifiersInvalid1()
		{
			FormTester formTester = new FormTester(CurrentForm.Name);
			using (MouseController mouse = formTester.MouseController())
			{
				mouse.Press(Keys.A);
			}
		}

		[Test]
		[ExpectedException(typeof (ArgumentOutOfRangeException))]
		public void ModifiersInvalid2()
		{
			FormTester formTester = new FormTester(CurrentForm.Name);
			using (MouseController mouse = formTester.MouseController())
			{
				mouse.Release(Keys.A);
			}
		}
	}
}