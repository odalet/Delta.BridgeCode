using System;

namespace Delta.BridgeCode.Analysis.Model
{
    public interface INamespace : IAstNode
    {
        IIdentifier Identifier { get; }
    }
    
    internal class Namespace : AstChildNode, INamespace
    {
        public Namespace(Identifier identifier)
        {
            if (identifier == null) throw new ArgumentNullException("identifier");

            identifier.SetParent(this);
            Identifier = identifier;

            base.ChildrenProvider = () => new IAstNode[] { Identifier };
        }
        
        #region INamespace Members

        public IIdentifier Identifier { get; private set; }

        #endregion
        
        public override int ChildCount
        {
            get { return 1; }
        }
    }
}
