using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public class FlipRotateView: RotateView
    {
        private const float FlipRotateAngle = 180.01f;
        
        protected sealed override ButtonElement _buttonElement { get; set; }
        
        public FlipRotateView(Transform transform, RotateModel model, RotatePresenter presenter, 
            [Inject(Id = UIInstaller.FlipButtonId)] ButtonElement buttonElement,
            IIdentifier identifier, SignalBus signalBus) 
            : base(transform, FlipRotateAngle, model,presenter, identifier, signalBus)
        {
            _buttonElement = buttonElement;
        }
    }
}