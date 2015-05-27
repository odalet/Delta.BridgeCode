using System.IO;
using Antlr4.Runtime;
using Delta.BridgeCode.Antlr;
using Delta.BridgeCode.Utils;

namespace Delta.BridgeCode.Parsing
{
    public class SyntaxTree
    {
        private readonly BridgeCodeParser parser;
        private readonly ParserRuleContext initialRule;

        public SyntaxTree(BridgeCodeParser antlrParser, ParserRuleContext antlrInitialRule)
        {
            parser = antlrParser;
            initialRule = antlrInitialRule;
        }

        public BridgeCodeParser Parser
        {
            get { return parser; }
        }

        public ParserRuleContext InitialRule
        {
            get { return initialRule; }
        }

        public void DumpTo(TextWriter writer)
        {
            new TreeUtils(parser).DumpTo(writer, initialRule);
        }
    }
}
