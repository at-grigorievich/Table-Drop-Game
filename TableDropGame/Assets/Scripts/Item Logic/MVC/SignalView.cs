using UniRx;
using Zenject;

namespace ATG.TableDrop
{
    public enum SelectionType
    {
        None,
        Select,
        Unselect
    }
    
    public abstract class SignalView: IInitializable
    {
        protected readonly CompositeDisposable _disposable 
            = new CompositeDisposable();
        
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
