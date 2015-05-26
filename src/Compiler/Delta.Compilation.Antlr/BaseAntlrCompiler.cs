using System.IO;
using Antlr4.Runtime;
using Delta.Compilation;

namespace Delta.Compilation.Antlr
{
    public abstract class BaseAntlrCompiler<TTree, TLexer, TParser> : BaseCompiler<TTree> 
        where TTree : BaseSyntaxTree
        where TLexer : Lexer
        where TParser : Parser
    {
        private class LexerErrorListener : IAntlrErrorListener<int>
        {

            #region IAntlrErrorListener<int> Members

            public void SyntaxError(IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
            {
                var foo = 42;
            }

            #endregion
        }

        private class ParserErrorListener : IAntlrErrorListener<IToken>
        {

            #region IAntlrErrorListener<IToken> Members

            public void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
            {
                System.Console.Write("ERROR [{0}:{1}] - {2}\r\n", line, charPositionInLine, msg);
            }

            #endregion
        }

        protected BaseAntlrCompiler() { }

        public override TTree ParseFile(Stream inputStream)
        {
            AntlrInputStream input = new AntlrInputStream(inputStream);

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

        protected abstract TLexer MakeLexer(ICharStream input);

        protected abstract TParser MakeParser(ITokenStream input);

        protected abstract TTree MakeSyntaxTree(TParser parser, ParserRuleContext rule);

        protected abstract ParserRuleContext GetRootRule(TParser parser);
    }
}
