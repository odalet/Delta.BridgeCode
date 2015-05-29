using System;
using System.Collections.Generic;

namespace Delta.BridgeCode.Analysis.Model
{
    public interface IAstNode
    {
        IAstNode Parent { get; }
     
        IEnumerable<IAstNode> Children { get; }

        int ChildCount { get; }
    }

    internal abstract class AstNode : IAstNode
    {
        #region IAstNode Members

        public IAstNode Parent { get; protected set; }

        public virtual IEnumerable<IAstNode> Children
        {
            get 
            {
                if (ChildrenProvider == null)
                    return new AstNode[0];
                return ChildrenProvider();
            }
        }

        public abstract int ChildCount { get; }

        #endregion

        protected Func<IEnumerable<IAstNode>> ChildrenProvider { get; set; }
    }

    internal abstract class AstChildNode : AstNode
    {
        protected internal void SetParent(AstNode parent)
        {
            base.Parent = parent;
        }
    }

    internal abstract class AstTerminalNode : AstChildNode
    {
        public override IEnumerable<IAstNode> Children
        {
            get { yield break; }
        }

        public override int ChildCount
        {
            get { return 0; }
        }
    }
}
