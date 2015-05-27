using Antlr4.Runtime;

namespace Delta.BridgeCode.Parsing
{
    internal class LexerErrorListener : IAntlrErrorListener<int>
    {
        #region IAntlrErrorListener<int> Members

        public void SyntaxError(IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
        }

        #endregion
    }
}
