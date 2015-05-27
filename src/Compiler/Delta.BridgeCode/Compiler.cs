using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Delta.BridgeCode.Analysis;
using Delta.BridgeCode.Analysis.Model;
using Delta.BridgeCode.Parsing;

namespace Delta.BridgeCode
{
    /// <summary>
    /// Public facade for the compilation and transpilation services.
    /// </summary>
    public class Compiler : IServiceProvider
    {
        private readonly List<string> sourceFiles = new List<string>();
        private readonly ServiceContainer services = new ServiceContainer();
        
        public Compiler() 
        {
            // Fill the services container

            // TODO: allow instances caching (singleton) or re-creation
            services.AddService<IParsingService>(() => new ParsingService()); 
            services.AddService<IAnalysisService>(() => new AnalysisService());
        }

        public List<string> SourceFiles
        {
            get { return sourceFiles; }
        }
        
        public IEnumerable<SyntaxTree> Parse()
        {
            var parsingService = this.GetService<IParsingService>(mandatory: true);
            foreach (var file in SourceFiles)
            {
                SyntaxTree tree = null;
                try
                {
                    tree = parsingService.Parse(file);
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
            var analysisService = this.GetService<IAnalysisService>(true);
            return analysisService.Analyze(Parse());
        }

        public string Transpile(GenerationTarget target)
        {
            //return "TODO: " + target.ToString();

            var analysisService = this.GetService<IAnalysisService>(true);
            var ast = analysisService.Analyze(Parse());
            return analysisService.StringResult;
        }

        #region IServiceProvider Members

        object IServiceProvider.GetService(Type serviceType)
        {
            return services.GetService(serviceType);
        }

        #endregion
    }
}
