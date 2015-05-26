using System;

namespace Delta.BridgeCode.Analysis
{
    internal class Namespace : AstNode
    {
        public Namespace(Identifier identifier)
        {
            if (identifier == null) throw new ArgumentNullException("identifier");
            Identifier = identifier;
        }

        public Identifier Identifier { get; private set; }
    }
}
