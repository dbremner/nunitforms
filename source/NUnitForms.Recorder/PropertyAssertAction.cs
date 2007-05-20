using System;

namespace NUnit.Extensions.Forms.Recorder
{
    public class PropertyAssertAction : Action
    {
        private string name;

        private object val;

        public PropertyAssertAction(string name, object val)
        {
            this.name = name;
            this.val = val;
        }

        public override string ToString()
        {
            string expected;
            if(val is bool)
            {
                expected = val.ToString().ToLower();
            }
            else if(val is String)
            {
                expected = "\"" + val + "\"";
            }
            else
            {
                expected = val.ToString();
            }

            return string.Format("Assert.AreEqual({0}, {1}.Properties.{2});", expected, Control, name);
        }
    }
}