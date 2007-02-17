using System;
using NUnit.Extensions.Forms.Test.TestForms;
using NUnit.Framework;

namespace NUnit.Extensions.Forms.Test
{
	[TestFixture]
	public class ToborTest
	{
		[Test]
		public void Test1()
		{
			ToborTestForm f = new ToborTestForm();
			f.Show();

			FormTester formTester = new FormTester(f.Name);

			Tobor t = new Tobor(formTester);

			t.Type("textBoxA", "3");
			t.Type("textBoxB", "4");
			t.Click("buttonCalc");
			t.VerifyText("labelResult", "7");
		}
	}
}
