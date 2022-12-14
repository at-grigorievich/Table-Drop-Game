using System;
using UniRx;
using Zenject;

namespace ATG.TableDrop
{
    public abstract class SignalView: IInitializable
    {
        protected readonly SignalBus SignalBus;
        
        protected readonly int InstanceId;
        
        public SignalView(IIdentifier identifier, SignalBus signalBus)
        {
            InstanceId = identifier.InstanceId;
            SignalBus = signalBus;
            
            SetupSignals();
        }

        public void Initialize() => SetupObserves();
        

        protected abstract void SetupSignals();
        protected abstract void SetupObserves();
    }
}
