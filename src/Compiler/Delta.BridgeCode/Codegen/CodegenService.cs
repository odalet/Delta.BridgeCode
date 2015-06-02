using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delta.BridgeCode.Analysis.Model;

namespace Delta.BridgeCode.Codegen
{
    public interface ICodegenService
    {
        IGenerationResult Generate(IAst ast);
    }

    public class CodegenService : ICodegenService
    {
        private class SimpleGenerationResult : GenerationResult
        {
            private readonly string generatedResult;

            public SimpleGenerationResult(string result)
            {
                generatedResult = result;
            }

            protected override string GetResult()
            {
                return generatedResult;
            }
        }

        public CodegenService()
        {
            // TODO choose the correct generator
        }

        #region ICodegenService Members

        public IGenerationResult Generate(IAst ast)
        {
            var generator = new CSharp.CodeGenerator();
            var csharp = generator.Generate(ast);
            var result = new SimpleGenerationResult(csharp);
            return result;
        }

        #endregion
    }
}
