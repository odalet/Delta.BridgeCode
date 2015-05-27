using System.Text;
using Antlr4.Runtime.Tree;
using Delta.BridgeCode.Analysis.Model;
using Delta.BridgeCode.Antlr;

namespace Delta.BridgeCode.Analysis
{
    internal class Listener : BridgeCodeBaseListener
    {
        private int tabCount = 0;

        private readonly Ast ast;

        private readonly BridgeCodeParser parser;
        private readonly StringBuilder output;

        public Listener(BridgeCodeParser bridgeCodeParser, StringBuilder builder)
        {
            parser = bridgeCodeParser;
            output = builder;

            ast = new Ast();
        }

        public override void EnterNamespaceDeclaration(BridgeCodeParser.NamespaceDeclarationContext context)
        {
            AppendFormat("namespace\r\n");
            var name = context.children[1];
            //name
            //var c = context.children;
        }

        public override void ExitNamespaceDeclaration(BridgeCodeParser.NamespaceDeclarationContext context)
        {
            AppendFormat(" // End namespace\r\n");
        }

        public override void EnterNamespaceBody(BridgeCodeParser.NamespaceBodyContext context)
        {
            AppendFormat("{{\r\n");
            tabCount++;
            //base.EnterNamespaceBody(context);
        }

        public override void ExitNamespaceBody(BridgeCodeParser.NamespaceBodyContext context)
        {
            tabCount--;
            AppendFormat("}}");
        }

        public override void EnterQualifiedName(BridgeCodeParser.QualifiedNameContext context)
        {
            base.EnterQualifiedName(context);
        }

        public override void ExitQualifiedName(BridgeCodeParser.QualifiedNameContext context)
        {
            base.ExitQualifiedName(context);
        }

        public override void VisitTerminal(ITerminalNode node)
        {
            //Append(node.Symbol.Text);
        }

        ////#region IParseTreeListener Members

        ////public void EnterEveryRule(ParserRuleContext ctx)
        ////{
        ////    switch (ctx.RuleIndex)
        ////    {
        ////        case BridgeCodeParser.RULE_namespaceDeclaration:
        ////            var namespaceName = "?";                    
        ////            output.Append(Tabify(string.Format("namespace {0}\r\n{{", namespaceName)));
        ////            tabCount++;
        ////            break;
        ////        case BridgeCodeParser.RULE_moduleDeclaration:
        ////            output.Append(Tabify("public static class\r\n{"));
        ////            tabCount++;
        ////            break;
        ////        default:
        ////            output.AppendFormat(GetTabs() + "Entering rule {0}:'{1}' - {2}\r\n", ctx.RuleIndex, parser.RuleNames[ctx.RuleIndex], ctx.ToString());
        ////            break;
        ////    }

        ////    //tabs++;
        ////}

        ////public void ExitEveryRule(ParserRuleContext ctx)
        ////{
        ////    switch (ctx.RuleIndex)
        ////    {
        ////        case BridgeCodeParser.RULE_namespaceDeclaration:
        ////            tabCount--;
        ////            output.Append(Tabify("} // End namespace"));
        ////            break;
        ////        case BridgeCodeParser.RULE_moduleDeclaration:
        ////            tabCount--;
        ////            output.Append(Tabify("} // End module"));
        ////            break;
        ////        default:
        ////            output.AppendFormat(GetTabs() + "Exiting rule {0}:'{1}' - {2}\r\n", ctx.RuleIndex, parser.RuleNames[ctx.RuleIndex], ctx.ToString());
        ////            break;
        ////    }

        ////    //tabs--;
        ////}

        ////public void VisitErrorNode(IErrorNode node)
        ////{
        ////    output.AppendFormat(GetTabs() + "ERROR   : {0}\r\n", node.ToString());
        ////}

        ////public void VisitTerminal(ITerminalNode node)
        ////{
        ////    output.AppendFormat(GetTabs() + "TERMINAL: {0}\r\n", node.ToString());
        ////}

        ////#endregion

        private string GetTabs()
        {
            //const char tabChar = '\t';
            const char tabChar = ' ';
            return new string(tabChar, tabCount < 0 ? 0 : tabCount);
        }

        private string Tabify(string input)
        {
            //const char tabChar = '\t';
            const char tabChar = ' ';
            var tabs = new string(tabChar, tabCount < 0 ? 0 : tabCount);
            return tabs + input.Replace("\r\n", "\r\n" + tabs);
        }

        private void AppendFormat(string format, params object[] args)
        {
            Append(string.Format(format, args));
        }

        private void Append(string toAppend)
        {
            output.Append(Tabify(toAppend));
        }
    }
}
