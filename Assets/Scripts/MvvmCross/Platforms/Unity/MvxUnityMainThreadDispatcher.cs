using System;
using System.Threading;
using MvvmCross.Base;

namespace MvvmCross.Platforms.Unity
{
    public class MvxUnityMainThreadDispatcher : MvxMainThreadAsyncDispatcher
    {
        private readonly SynchronizationContext mainThreadContext;
        
        public override bool IsOnMainThread => mainThreadContext == SynchronizationContext.Current;

        public MvxUnityMainThreadDispatcher()
        {
            mainThreadContext = SynchronizationContext.Current;
        }
        
        public override bool RequestMainThreadAction(Action action, bool maskExceptions = true)
        {
            if (IsOnMainThread)
                ExceptionMaskedAction(action, maskExceptions);
            else
            {
                mainThreadContext.Post(ignored =>
                {
                    ExceptionMaskedAction(action, maskExceptions);
                }, null);
            }

            return true;
        }
    }
}