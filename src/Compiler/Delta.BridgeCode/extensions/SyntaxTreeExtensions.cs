using System.IO;
using System.Text;
using Delta.BridgeCode.Parsing;

namespace Delta.BridgeCode
{
    public static class SyntaxTreeExtensions
    {
        public static string Dump(this SyntaxTree tree)
        {
            var builder = new StringBuilder();
            using (var writer = new StringWriter(builder))
                tree.DumpTo(writer);

            return builder.ToString();
        }
    }
}
