using Nito.AsyncEx;
using System;
using System.Threading.Tasks;

namespace MemoryGame.Core
{
    public class TurnResultEventArgs : EventArgs
    {
        private readonly DeferralManager deferrals = new DeferralManager();

        public TurnResult Result { get; }

        public TurnResultEventArgs(TurnResult result)
        {
            Result = result;
        }

        public IDisposable GetDeferral() => deferrals.GetDeferral();

        internal Task WaitForDeferralsAsync() => deferrals.SignalAndWaitAsync();
    }
}