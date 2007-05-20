namespace NUnit.Extensions.Forms.Recorder
{
    /// <summary>
    /// This action allows to compare a capture of a <see cref="System.Windows.Forms.Control"/> with a stored capture.
    /// </summary>
    public class CompareControlCaptureAction : Action
    {
        /// <summary>
        /// Initialize this action.
        /// </summary>
        /// <param name="name">
        /// The file which contains the capture of a control.
        /// </param>
        /// <param name="val">
        /// </param>
        public CompareControlCaptureAction(string name, object val)
        {
            this.name = name;
            this.val = val;
        }

        public override string ToString()
        {
            return string.Format("CompareCapture({0},{1});","Please replace with a reference of your form", "@\"" + name + "\"");
        }

        private string name;

        private object val;
    }
}