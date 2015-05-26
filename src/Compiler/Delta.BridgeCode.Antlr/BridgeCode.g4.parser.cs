namespace Delta.BridgeCode.Antlr
{
    partial class BridgeCodeParser
    {
        ////partial class NamespaceDeclarationContext
        ////{
        ////    public QualifiedNameContext Name { get { return (QualifiedNameContext)children[1]; } }

        ////    public NamespaceBodyContext Body { get { return (NamespaceBodyContext)children[0]; } }
        ////}

        public override void NotifyErrorListeners(Antlr4.Runtime.IToken offendingToken, string msg, Antlr4.Runtime.RecognitionException e)
        {
            base.NotifyErrorListeners(offendingToken, msg, e);
        }
    }
}
