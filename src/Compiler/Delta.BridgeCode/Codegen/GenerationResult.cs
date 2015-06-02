using System;

namespace Delta.BridgeCode.Codegen
{
    public interface IGenerationResult
    {
        string Result { get; }
    }

    public abstract class GenerationResult : IGenerationResult
    {
        private string cachedResult = null;
        private bool resultWasSet = false;

        #region IGenerationResult Members

        public string Result
        {
            get 
            {
                if (!resultWasSet)
                {
                    cachedResult = GetResult();
                    resultWasSet = true;
                }

                return cachedResult;
            }
        }

        #endregion

        protected abstract string GetResult();
    }
}
