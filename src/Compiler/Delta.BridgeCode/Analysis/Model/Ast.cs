using System.Linq;
using System.Collections.Generic;

namespace Delta.BridgeCode.Analysis.Model
{
    public interface IAst : IAstNode
    {
        IReadOnlyList<INamespace> Namespaces { get; }
    }

    internal class Ast : AstNode, IAst
    {
        private readonly List<Namespace> namespaces = new List<Namespace>();
        public Ast()
        {
            Namespaces = namespaces;

            base.Parent = null;
            base.ChildrenProvider = () => Namespaces.Cast<IAstNode>();            
        }

        #region IAst Members

        public IReadOnlyList<INamespace> Namespaces { get; private set; }

        #endregion

        public override int ChildCount
        {
            get { return Namespaces.Count; }
        }

        public void AddNamespace(Namespace ns)
        {
            ns.SetParent(this);
            namespaces.Add(ns);
        }
    }
}
