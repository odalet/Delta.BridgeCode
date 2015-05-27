using System;

namespace Delta.BridgeCode.Analysis.Model
{
    public interface INamespace
    {
        IIdentifier Identifier { get; }
    }
    
    internal class Namespace : AstNode, INamespace
    {
        public Namespace(Identifier identifier)
        {
            if (identifier == null) throw new ArgumentNullException("identifier");
            Identifier = identifier;
        }
        
        #region INamespace Members

        public IIdentifier Identifier { get; private set; }

        #endregion
    }
}
