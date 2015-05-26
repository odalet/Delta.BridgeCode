using System;
using System.IO;
using System.Text;

namespace Delta.Compilation
{
    public abstract class BaseSyntaxTree
    {
        public string Dump()
        {
            var builder = new StringBuilder();
            using (var writer = new StringWriter(builder))            
                DumpTo(writer);

            return builder.ToString();            
        }

        public abstract void DumpTo(TextWriter writer);
    }
}
