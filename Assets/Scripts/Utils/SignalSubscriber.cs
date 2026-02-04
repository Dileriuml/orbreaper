using System;
using System.Collections.Generic;
using Zenject;

namespace OrbReaper.Utils
{
    public abstract class SignalSubscriber : IDisposable, IInitializable
    {
        private List<(Type type, Action action)> subscriptions = new ();
        private readonly SignalBus signalBus;
        
        public SignalSubscriber(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        protected void Subscribe<TSignal>(Action<TSignal> onSignal)
        {
            signalBus.Subscribe(onSignal);
        }

        public void Dispose()
        {
            UnsubscribeAll();
        }

        public void Initialize()
        {
            OnSubscribeSignals();
        }

        private void UnsubscribeAll()
        {
            subscriptions.ForEach(s => signalBus.TryUnsubscribe(s.type, s.action));
        }

        protected abstract void OnSubscribeSignals();
    }
}