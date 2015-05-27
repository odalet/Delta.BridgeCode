using System.IO;
using Antlr4.Runtime;
using Delta.BridgeCode.Antlr;

namespace Delta.BridgeCode.Parsing
{
    public interface IParsingService
    {
        SyntaxTree Parse(Stream inputStream);
    }

    public static class ParsingServiceExtensions
    {
        public static SyntaxTree Parse(this IParsingService service, string filename)
        {
            using (var stream = File.OpenRead(filename))
                return service.Parse(stream);
        }
    }

    internal class ParsingService : IParsingService
    {
        #region IParsingService Members

        public SyntaxTree Parse(Stream inputStream)
        {
            var input = new AntlrInputStream(inputStream);

            var lexer = MakeLexer(input);
            lexer.AddErrorListener(new LexerErrorListener());
            var tokStream = new CommonTokenStream(lexer);

            var parser = MakeParser(tokStream);
            parser.BuildParseTree = true;
            parser.AddErrorListener(new ParserErrorListener());

            //var tree = parser.Context;
            //var stree = tree.ToStringTree(parser);

            return MakeSyntaxTree(parser, GetRootRule(parser));
        }

        #endregion

        private BridgeCodeLexer MakeLexer(ICharStream input)
        {
            var lexer = new BridgeCodeLexer(input);
            lexer.RemoveErrorListeners(); // This removes the default Console error listener
            return lexer;
        }

        private BridgeCodeParser MakeParser(ITokenStream input)
        {
            var parser = new BridgeCodeParser(input);
            parser.RemoveErrorListeners(); // This removes the default Console error listener
            return parser;
        }

        private SyntaxTree MakeSyntaxTree(BridgeCodeParser parser, ParserRuleContext rule)
        {
            return new SyntaxTree(parser, rule);
        }

        private ParserRuleContext GetRootRule(BridgeCodeParser parser)
        {
            var root = parser.compileUnit();
            return root;
        }
    }
}
