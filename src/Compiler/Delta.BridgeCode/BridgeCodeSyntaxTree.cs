using Antlr4.Runtime;
using Delta.BridgeCode.Antlr;
using Delta.Compilation.Antlr;

namespace Delta.BridgeCode
{
    public class BridgeCodeSyntaxTree : BaseAntlrSyntaxTree
    {
        public BridgeCodeSyntaxTree(BridgeCodeParser parser, ParserRuleContext initialRule)
            : base(parser, initialRule) { }
    }
}
