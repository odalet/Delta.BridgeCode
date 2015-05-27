namespace Delta.BridgeCode.Antlr
{
    // Used by BridgeCodeV001...
    internal static class LexerHelper
    {
        // Usages in the grammar:
        //
        // LexerHelper.IsIdentifierStart(_input.La(-1))
        // LexerHelper.IsIdentifierStart(LexerHelper.GetCodePoint(_input.La(-2), _input.La(-1)))
        //
        // LexerHelper.IsIdentifierPart(_input.La(-1))
        // LexerHelper.IsIdentifierPart(LexerHelper.GetCodePoint(_input.La(-2), _input.La(-1)))

        public static bool IsIdentifierStart(int codepoint)
        {
            return true;
        }

        public static bool IsIdentifierPart(int codepoint)
        {
            return true;
        }

        public static int GetCodePoint(int high, int low)
        {
            return high << 16 + low;
        }
    }
}
