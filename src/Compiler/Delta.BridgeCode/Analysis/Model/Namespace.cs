using System;
using System.Collections.Generic;

namespace Delta.BridgeCode.Analysis.Model
{
    public interface INamespace : IAstNode
    {
        IIdentifier Identifier { get; }

        IReadOnlyList<ITypeDeclaration> TypeDeclarations { get; }
    }
    
    internal class Namespace : AstChildNode, INamespace
    {
        private readonly List<TypeDeclaration> typeDeclarations = new List<TypeDeclaration>();

        public Namespace(Identifier identifier)
        {
            if (identifier == null) throw new ArgumentNullException("identifier");
            identifier.SetParent(this);
            Identifier = identifier;

            TypeDeclarations = typeDeclarations;

            base.ChildrenProvider = GetChildren;
        }
        
        #region INamespace Members

        public IIdentifier Identifier { get; private set; }

        public IReadOnlyList<ITypeDeclaration> TypeDeclarations { get; private set; }

        #endregion
        
        private IEnumerable<IAstNode> GetChildren()
        {
            yield return Identifier;
            foreach (var decl in TypeDeclarations)
                yield return decl;
        }

        public override int ChildCount
        {
            get { return 1 + TypeDeclarations.Count; }
        }

        public void AddTypeDeclaration(TypeDeclaration decl)
        {
            decl.SetParent(this);
            typeDeclarations.Add(decl);
        }
    }
}
