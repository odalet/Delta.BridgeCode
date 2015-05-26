using Antlr4.Runtime;
using Delta.BridgeCode.Antlr;
using Delta.Compilation.Antlr;

namespace Delta.BridgeCode
{
    public abstract class BaseBridgeCodeCompiler : BaseAntlrCompiler<BridgeCodeSyntaxTree, BridgeCodeLexer, BridgeCodeParser>
    {
        protected override BridgeCodeLexer MakeLexer(ICharStream input)
        {
            var lexer = new BridgeCodeLexer(input);
            lexer.RemoveErrorListeners(); // This removes the default Console error listener
            return lexer;
        }

        protected override BridgeCodeParser MakeParser(ITokenStream input)
        {
            var parser = new BridgeCodeParser(input);
            parser.RemoveErrorListeners(); // This removes the default Console error listener
            return parser;
        }

        protected override BridgeCodeSyntaxTree MakeSyntaxTree(BridgeCodeParser parser, ParserRuleContext rule)
        {
            return new BridgeCodeSyntaxTree(parser, rule);
        }

        protected override ParserRuleContext GetRootRule(BridgeCodeParser parser)
        {
            var root = parser.compileUnit();
            return root;
        }
    }
}
