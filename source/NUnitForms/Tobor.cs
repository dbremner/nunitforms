using System;
using NUnit.Framework;

namespace NUnit.Extensions.Forms
{
	/// <summary>
	/// Experimental "FIT" interface.
	/// </summary>
	public class Tobor
	{
		private readonly FormTester targetForm;
		/*
		 
		 General idea: https://sourceforge.net/forum/forum.php?thread_id=1336913&forum_id=331583
		 
		NUnitFormsFit.Type(form,"txtA","3"); 
		NUnitFormsFit.Type(form,"txtB","4"); 
 
		NUnitFormsFit.Click(form,"btnCompute"); 
		NUnitFormsFit.VerifyText(form,"lblResult","7");

		*/


		/// <summary>
		/// Constructor.
		/// </summary>
		public Tobor(FormTester targetForm) {
			this.targetForm = targetForm;
		}

		///<summary>
		/// Types into the named control.
		///</summary>
		public void Type(string controlName, string newText)
		{
			TextBoxTester textBox = new TextBoxTester(controlName, targetForm.Properties);
			textBox.Enter(newText);
		}

		///<summary>
		/// Clicks the named control.
		///</summary>
		public void Click(string controlName)
		{
			ButtonTester button = new ButtonTester(controlName, targetForm.Properties);
			button.Click();
		}

		///<summary>
		/// Verifies the text of the named control matches the expected text.
		///</summary>
		public void VerifyText(string controlName, string expectedText)
		{
			ControlTester anyControl = new ControlTester(controlName, targetForm.Properties);
			Assert.AreEqual(expectedText, anyControl.Text);
		}
	}
}
