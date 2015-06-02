using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.BridgeCode.Analysis.Model
{
    public enum TypeKind
    {
        Module
    }

    public interface ITypeDeclaration : IAstNode
    {
        TypeKind Kind { get; }
        IIdentifier Identifier { get; }
    }

    internal class TypeDeclaration : AstChildNode, ITypeDeclaration
    {
        public TypeDeclaration(Identifier identifier, TypeKind typeKind)
        {
            if (identifier == null) throw new ArgumentNullException("identifier");

            identifier.SetParent(this);
            Identifier = identifier;

            Kind = typeKind;

            base.ChildrenProvider = () => new IAstNode[] { Identifier };
        }

        #region ITypeDeclaration Members

        public TypeKind Kind { get; private set; }
        public IIdentifier Identifier { get; private set; }

        #endregion

        public override int ChildCount
        {
            get { return 1; }
        }
    }
}
