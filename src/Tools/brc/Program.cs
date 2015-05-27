using System;
using Delta.BridgeCode;

namespace brc
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var filename = @"D:\HOME\Delta.BridgeCode\src\Tests\test1.br";

            var compiler = new Compiler();
            compiler.SourceFiles.AddRange(new[] { filename });

            foreach (var tree in compiler.Parse())
            {
                Console.WriteLine(tree.Dump());
            }

            Console.WriteLine("************************************************");

            var csharp = compiler.Transpile(GenerationTarget.CSharp);
            Console.WriteLine(csharp);

            Console.ReadKey();
        }
    }
}
