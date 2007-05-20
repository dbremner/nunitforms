namespace NUnit.Extensions.Forms
{
    ///<summary>
    /// Additional assertion methods for NUnitForms.
    ///</summary>
    public class FormsAssert
    {
        ///<summary>
        /// Asserts that the given objects are equal.
        ///</summary>
        ///<param name="o"></param>
        ///<param name="o2"></param>
        ///<param name="error"></param>
        ///<exception cref="FormsTestAssertionException"></exception>
        public static void AreEqual(object o, object o2, string error)
        {
            if(!o.Equals(o2))
            {
                throw new FormsTestAssertionException("should be equal " + o + " : " + o2 + " , " + error);
            }
        }

        ///<summary>
        /// Asserts that the given value is true.
        ///</summary>
        ///<param name="val"></param>
        ///<exception cref="FormsTestAssertionException"></exception>
        public static void IsTrue(bool val)
        {
            if(!val)
            {
                throw new FormsTestAssertionException("was not true");
            }
        }
    }
}