using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Delta.BridgeCode.Analysis;
using Delta.BridgeCode.Analysis.Model;
using Delta.BridgeCode.Codegen;
using Delta.BridgeCode.Parsing;

namespace Delta.BridgeCode
{
    /// <summary>
    /// Public facade for the compilation and transpilation services.
    /// </summary>
    public class Compiler
    {
        private readonly List<string> sourceFiles = new List<string>();
        private readonly ServiceContainer services = new ServiceContainer();
        
        private readonly Lazy<IParsingService> parsingService = new Lazy<IParsingService>(() => new ParsingService());
        private readonly Lazy<IAnalysisService> analysisService = new Lazy<IAnalysisService>(() => new AnalysisService());
        
        public Compiler() 
        {
        }

        private IParsingService ParsingService
        {
            get { return parsingService.Value;  }
        }

        private IAnalysisService AnalysisService
        {
            get { return analysisService.Value; }
        }

        public List<string> SourceFiles
        {
            get { return sourceFiles; }
        }
        
        public IEnumerable<SyntaxTree> Parse()
        {
            foreach (var file in SourceFiles)
            {
                SyntaxTree tree = null;
                try
                {
                    tree = ParsingService.Parse(file);
                }
                catch (Exception ex)
                {
                    // TODO: trace errors
                    var debugException = ex;
                }

                if (tree != null) yield return tree;
            }
        }

        public IAst Analyze()
        {
            return AnalysisService.Analyze(Parse());
        }

        public string Transpile(GenerationTarget target)
        {
            //return "TODO: " + target.ToString();

            var analysisService = AnalysisService;
            var ast = analysisService.Analyze(Parse());

            var generator = new CodegenService();
            var result = generator.Generate(ast);

            return result.Result;
            //return analysisService.StringResult;
        }
    }
}
