using System.Collections;
using System.Text;

namespace NUnit.Extensions.Forms.Recorder
{
    public class CompositeAction : EventAction
    {
        private ArrayList actions = new ArrayList();

        public CompositeAction(string name) : base(name)
        {
        }

        public void Add(Action action)
        {
            actions.Add(action);
        }

        public void Add(string name, params object[] args)
        {
            actions.Add(new EventAction(name, args));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(Action action in actions)
            {
                action.Definition = Definition;
                sb.Append(action.ToString());
                sb.Append("\r\n\t");
            }
            return sb.ToString().Trim();
        }
    }
}