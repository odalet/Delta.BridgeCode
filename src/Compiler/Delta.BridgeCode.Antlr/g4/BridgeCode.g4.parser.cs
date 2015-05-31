using Antlr4.Runtime;

namespace Delta.BridgeCode.Antlr
{
    partial class BridgeCodeParser
    {
        /// <summary>
        /// Notifies the error listeners.
        /// </summary>
        /// <param name="offendingToken">The offending token.</param>
        /// <param name="msg">The message.</param>
        /// <param name="e">The recognition exception.</param>
        public override void NotifyErrorListeners(IToken offendingToken, string msg, RecognitionException e)
        {
            // Does nothing, but allows for breakpoints...
            base.NotifyErrorListeners(offendingToken, msg, e);
        }
    }
}
