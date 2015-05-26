using System.IO;

namespace Delta.Compilation
{
    public abstract class BaseCompiler<TTree> where TTree : BaseSyntaxTree
    {
        public virtual TTree ParseFile(string filename)
        {
            using (var stream = File.OpenRead(filename))
                return ParseFile(stream);
        }

        public abstract TTree ParseFile(Stream inputStream);

        public abstract string Transpile(TTree tree);
    }
}
