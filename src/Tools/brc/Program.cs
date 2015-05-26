using System;
using Delta.BridgeCode;
namespace brc
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var filename = @"D:\HOME\Delta.BridgeCode\src\Tests\test1.br";
            var compiler = new BridgeCodeCompiler(BridgeTarget.CSharp);

            var tree = compiler.ParseFile(filename);

            var csharp = compiler.Transpile(tree);

            //Console.WriteLine(tree.Dump());
            Console.WriteLine(csharp);
            Console.ReadKey();
        }
    }
}
