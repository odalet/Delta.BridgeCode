﻿using System.Collections.Generic;
using System.Text;
using Delta.BridgeCode.Analysis.Model;
using Delta.BridgeCode.Parsing;

namespace Delta.BridgeCode.Analysis
{
    public interface IAnalysisService
    {
        IAst Analyze(IEnumerable<SyntaxTree> parsedContent);
        string StringResult { get; } // TODO: remove this and have the C# code generated by a transpiler (from the AST).
    }

    internal class AnalysisService : IAnalysisService
    {

        private StringBuilder csharpBuilder = new StringBuilder();

        #region IAnalysisService Members

        public IAst Analyze(IEnumerable<SyntaxTree> parsedContent)
        {
            var ast = new Ast();

            foreach (var content in parsedContent)
            {
                var visitor = new Visitor(content.Parser, ast);
                var stringResult = visitor.Visit(content.InitialRule);
                csharpBuilder.AppendLine(stringResult);
            }

            return ast;
        }

        #endregion

        public string StringResult 
        {
            get { return csharpBuilder.ToString(); }
        }
    }
}
