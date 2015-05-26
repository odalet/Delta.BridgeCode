using System;
using System.Text;
using Antlr4.Runtime.Tree;
using Delta.BridgeCode.Antlr;

namespace Delta.BridgeCode.CSharp
{
    public class Transpiler : IBridgeCodeTranspiler
    {
        #region IBridgeCodeTranspiler Members

        public string Transpile(BridgeCodeSyntaxTree tree)
        {
            if (tree == null) throw new ArgumentNullException("tree");

            //var builder = new StringBuilder();

            //var walker = new ParseTreeWalker();
            //var listener = new Listener((BridgeCodeParser)tree.Parser, builder);
            //walker.Walk(listener, tree.InitialRule);

            //return builder.ToString();

            var v = new Visitor((BridgeCodeParser)tree.Parser);
            return v.Visit(tree.InitialRule);
        }

        #endregion
    }
}
