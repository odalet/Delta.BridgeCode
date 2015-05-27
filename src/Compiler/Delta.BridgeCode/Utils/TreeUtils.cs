using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace Delta.BridgeCode.Utils
{    
    internal class TreeUtils
    {
        private int tabCount = 0;
        private string[] ruleNames = null;
        private string[] tokenNames = null;

        public TreeUtils(Parser parser)
        {
            ruleNames = parser != null ? parser.RuleNames : null;
            tokenNames = parser != null ? parser.TokenNames : null;
        }

        public void DumpTo(TextWriter writer, ITree tree)
        {
            var tabs = new string(' ', tabCount);
            bool isError = false;
            var text = EscapeWhitespace(DumpNode(tree, ref isError), false);
            writer.Write(isError ? "E" : " ");
            writer.Write(tabs);
            writer.WriteLine(text);
            tabCount++;
            for (int i = 0; i < tree.ChildCount; i++)
                DumpTo(writer, tree.GetChild(i));

            tabCount--;
        }

        private string DumpNode(ITree node, ref bool isError)
        {
            isError = false;
            if (node is IErrorNode)
            {
                isError = true;
                return ErrorToString((IErrorNode)node);
            }

            if (node is IRuleNode)
                return RuleToString(((IRuleNode)node).RuleContext);

            if (node is ITerminalNode)
                return TokenToString(((ITerminalNode)node).Symbol);

            return "<?>";
        }

        private string ErrorToString(IErrorNode error)
        {
            return string.Format("{0} ERROR --> {1}", error, TokenToString(error.Symbol));
        }

        private string RuleToString(RuleContext rule)
        {
            var ruleName = rule.RuleIndex.ToString();
            if (ruleNames != null)
                ruleName = ruleNames[rule.RuleIndex] + ":" + rule.RuleIndex;

            return string.Format("{0} - [{1}]", ruleName, rule.SourceInterval);
        }

        private string TokenToString(IToken token)
        {
            var tokenText = EscapeWhitespace(token.Text, false);
            if (tokenText == null)
                tokenText = "no text";

            var tokenChannel = string.Empty;
            if (token.Channel > 0)
                tokenChannel = ", channel=" + token.Channel;

            var tokenType = token.Type.ToString();
            if (token.Type == -1)
                tokenType = "EOF" + " (" + token.Type + ")";
            else if (tokenNames != null)
                tokenType = tokenNames[token.Type] + ":" + token.Type;

            return string.Format(
                "'{0}' - #{1} <{2}> (@{3}:{4} - [{5}:{6}]){7}",
                tokenText, token.TokenIndex, tokenType, token.Line, token.Column, token.StartIndex, token.StopIndex, tokenChannel);
        }

        private static string EscapeWhitespace(string s, bool escapeSpaces)
        {
            if (s == null) return null;

            var buf = new StringBuilder();
            foreach (var c in s.ToCharArray())
                switch (c)
                {
                    case ' ':
                        buf.Append(escapeSpaces ? '\u00B7' : c);
                        break;
                    case '\t':
                        buf.Append("\\t");
                        break;
                    case '\r':
                        buf.Append("\\r");
                        break;
                    case '\n':
                        buf.Append("\\n");
                        break;
                    default:
                        buf.Append(c);
                        break;
                }
            return buf.ToString();
        }
    }

}
