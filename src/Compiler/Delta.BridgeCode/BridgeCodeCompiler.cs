using System;
using Antlr4.Runtime;
using Delta.BridgeCode.Antlr;
using Delta.Compilation.Antlr;

namespace Delta.BridgeCode
{
    public class BridgeCodeCompiler : BaseBridgeCodeCompiler
    {
        private readonly BridgeTarget targetLanguage;
        public BridgeCodeCompiler(BridgeTarget target)
        {
            targetLanguage = target;
        }

        public override string Transpile(BridgeCodeSyntaxTree tree)
        {
            IBridgeCodeTranspiler transpiler = null;

            switch (targetLanguage)
            {
                case BridgeTarget.CSharp:
                    transpiler = new CSharp.Transpiler();
                    break;
            }

            if (transpiler == null) throw new NotSupportedException(
                string.Format("Target {0} is not supported.", targetLanguage));

            return transpiler.Transpile(tree);
        }
    }
}