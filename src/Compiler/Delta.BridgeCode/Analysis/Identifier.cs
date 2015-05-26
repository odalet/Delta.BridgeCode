using System;

namespace Delta.BridgeCode.Analysis
{
    internal class Identifier : AstNode
    {
        public Identifier(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("name");
            Name = name;
        }

        public string Name { get; private set; }
    }
}
