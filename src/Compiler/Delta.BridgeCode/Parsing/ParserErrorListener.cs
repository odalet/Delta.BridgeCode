using System;
using Antlr4.Runtime;

namespace Delta.BridgeCode.Parsing
{
    internal class ParserErrorListener : IAntlrErrorListener<IToken>
    {
        #region IAntlrErrorListener<IToken> Members

        public void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            Console.Write("PARSING ERROR [{0}:{1}] - {2}\r\n", line, charPositionInLine, msg);
        }

        #endregion
    }
}
