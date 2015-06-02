using System;
using System.Linq;
using System.Text;
using Antlr4.Runtime.Tree;
using Delta.BridgeCode.Analysis.Model;
using Delta.BridgeCode.Antlr;

namespace Delta.BridgeCode.Analysis
{
    internal class Visitor : BridgeCodeBaseVisitor<string>
    {
        ////private int tabCount = 0;

        private readonly Ast ast;
        private readonly BridgeCodeParser parser;
        //private readonly StringBuilder output;

        public Visitor(BridgeCodeParser bridgeCodeParser, Ast astToFill)
        {
            parser = bridgeCodeParser;
            ast = astToFill;
        }

        ////public override string Visit(IParseTree tree)
        ////{
        ////    return ">\r\n" + base.Visit(tree);
        ////}

        public override string VisitNamespaceDeclaration(BridgeCodeParser.NamespaceDeclarationContext context)
        {
            var name = VisitQualifiedName(context.qualifiedName());
            var qname = new Identifier(name);
            var ns = new Namespace(qname);
            ast.AddNamespace(ns);

            currentNamespace = ns;
            var body = VisitNamespaceBody(context.namespaceBody());                        
            return string.Format("namespace {0}\r\n{1}", name, body);
        }

        // HACK!!! Find a way to pass the parent AstNode to the currently processed parser node
        private Namespace currentNamespace = null;

        public override string VisitNamespaceBody(BridgeCodeParser.NamespaceBodyContext context)
        {
            var builder = new StringBuilder();
            Append(builder, 0, "{\r\n");

            foreach (var ctx in context.typeDeclaration())
            {
                var decl = VisitTypeDeclaration(ctx);                
                Append(builder, 1, decl);
            }

            Append(builder, 0, "}\r\n");

            return builder.ToString();
        }

        public override string VisitModuleDeclaration(BridgeCodeParser.ModuleDeclarationContext context)
        {
            var name = VisitIdentifier(context.identifier());
            
            var identifier = new Identifier(name);
            var tdecl = new TypeDeclaration(identifier, TypeKind.Module);
            currentNamespace.AddTypeDeclaration(tdecl); // HACK!!!

            var body = VisitModuleBody(context.moduleBody());
            return string.Format("public static class {0}\r\n{1}", name, body);
        }

        public override string VisitModuleBody(BridgeCodeParser.ModuleBodyContext context)
        {
            var builder = new StringBuilder();
            Append(builder, 0, "{\r\n");

            foreach (var ctx in context.moduleBodyDeclaration())
            {
                var decl = VisitModuleBodyDeclaration(ctx);
                Append(builder, 1, decl);
            }

            Append(builder, 0, "}\r\n");

            return builder.ToString();
        }

        ////public override string VisitModuleBodyDeclaration(BridgeCodeParser.ModuleBodyDeclarationContext context)
        ////{
        ////    var builder = new StringBuilder();
        ////    foreach (var ctx in context.fieldDeclaration())
        ////    {
        ////        return VisitFieldDeclaration(context.fieldDeclaration());
        ////        Append(builder, 0, decl);
        ////    }

        ////    return builder.ToString();
        ////}

        public override string VisitFieldDeclaration(BridgeCodeParser.FieldDeclarationContext context)
        {
            return string.Format("{0} {1};\r\n", context.type().GetText(), context.variableDeclarator().GetText());
        }

        public override string VisitQualifiedName(BridgeCodeParser.QualifiedNameContext context)
        {
            //return base.VisitQualifiedName(context);
            return string.Join(".", context.children
                .Where(child => child is BridgeCodeParser.IdentifierContext)
                .Select(child => child.GetText()));
        }

        public override string VisitIdentifier(BridgeCodeParser.IdentifierContext context)
        {
            //return base.VisitIdentifier(context);
            return context.GetText();
        }

        protected override bool ShouldVisitNextChild(IRuleNode node, string currentResult)
        {
            //return base.ShouldVisitNextChild(node, currentResult);
            return true;
        }

        protected override string AggregateResult(string aggregate, string nextResult)
        {
            //return base.AggregateResult(aggregate, nextResult);
            return aggregate + nextResult;
        }

        ////public override string VisitErrorNode(IErrorNode node)
        ////{
        ////    return GetTabs() + string.Format("ERROR   : {0}\r\n", node.ToString());
        ////}

        ////public override string VisitChildren(IRuleNode node)
        ////{
        ////    //return base.VisitChildren(node);
        ////    var ctx = node.RuleContext;
        ////    switch (ctx.RuleIndex)
        ////    {
        ////        case BridgeCodeParser.RULE_compileUnit:
        ////            return VisitCompileUnit();
        ////        case BridgeCodeParser.RULE_namespaceDeclaration:
        ////            return "NAMESPACE\r\n";
        ////            //break;
        ////        case BridgeCodeParser.RULE_moduleDeclaration:
        ////            return "MODULE\r\n";
        ////            //break;
        ////        default:
        ////            return GetTabs() + string.Format("RULE {0}:'{1}' - {2}\r\n", ctx.RuleIndex, parser.RuleNames[ctx.RuleIndex], ctx.ToString());
        ////            //break;
        ////    }
        ////}
        
        ////public override string VisitTerminal(ITerminalNode node)
        ////{
        ////    return GetTabs() + string.Format("TERMINAL: {0}\r\n", node.ToString());
        ////}

        ////protected override bool ShouldVisitNextChild(IRuleNode node, string currentResult)
        ////{
        ////    return true;
        ////    //return base.ShouldVisitNextChild(node, currentResult);
        ////}

        ////private string VisitCompileUnit()
        ////{
        ////    return "PROGR ";
        ////}

        ////#region IParseTreeVisitor<string> Members

        ////public string Visit(IParseTree tree)
        ////{
        ////    return GetTabs() + string.Format("TREE    : {0}\r\n", tree.ToString());
        ////}

        ////public string VisitChildren(IRuleNode node)
        ////{
        ////    var ctx = node.RuleContext;
        ////    return GetTabs() + string.Format("Exiting rule {0}:'{1}' - {2}\r\n", ctx.RuleIndex, parser.RuleNames[ctx.RuleIndex], ctx.ToString());
        ////}

        ////public string VisitErrorNode(IErrorNode node)
        ////{
        ////    return GetTabs() + string.Format("ERROR   : {0}\r\n", node.ToString());
        ////}

        ////public string VisitTerminal(ITerminalNode node)
        ////{
        ////    return GetTabs() + string.Format("TERMINAL: {0}\r\n", node.ToString());
        ////}

        ////#endregion

        ////private string GetTabs()
        ////{
        ////    //const char tabChar = '\t';
        ////    const char tabChar = ' ';
        ////    return new string(tabChar, tabCount < 0 ? 0 : tabCount);
        ////}

        private string Tabify(string input, int level)
        {
            //const char tabChar = '\t';
            const char tabChar = ' ';
            if (level <= 0)
                return input;
            var tabs = new string(tabChar, level);
            return string.Join("\r\n", input
                .Split(new[] { "\r\n" }, StringSplitOptions.None)
                .Select(s => string.IsNullOrEmpty(s) ? s : tabs + s));
        }

        private void AppendFormat(StringBuilder builder, int level, string format, params object[] args)
        {
            Append(builder, level, string.Format(format, args));
        }

        private void Append(StringBuilder builder, int level, string toAppend)
        {
            if (string.IsNullOrEmpty(toAppend)) return;
            builder.Append(Tabify(toAppend, level));
        }
    }
}
