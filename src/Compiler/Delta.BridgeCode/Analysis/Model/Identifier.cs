using System;

namespace Delta.BridgeCode.Analysis.Model
{
    public interface IIdentifier
    {
        string Name { get; }
    }

    internal class Identifier : AstNode, IIdentifier
    {
        public Identifier(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("name");
            Name = name;
        }

        #region IIdentifier Members
        
        public string Name { get; private set; }

        #endregion
    }
}
