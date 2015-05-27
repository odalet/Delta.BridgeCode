using System.Collections.Generic;

namespace Delta.BridgeCode.Analysis.Model
{
    public interface IAst 
    {
        IList<INamespace> Namespaces { get; }
    }

    internal class Ast : IAst
    {
        public Ast()
        {
            Namespaces = new List<INamespace>();            
        }

        #region IAst Members

        public IList<INamespace> Namespaces { get; private set; }

        #endregion
    }
}
