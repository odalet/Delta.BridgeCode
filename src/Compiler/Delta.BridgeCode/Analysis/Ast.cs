using System.Collections.Generic;

namespace Delta.BridgeCode.Analysis
{
    internal class Ast
    {
        public Ast()
        {
            Namespaces = new List<Namespace>();
        }

        public List<Namespace> Namespaces { get; private set; }
    }
}
