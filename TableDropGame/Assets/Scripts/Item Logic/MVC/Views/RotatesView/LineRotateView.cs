using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public class LineRotateView: RotateView
    {
        private const float LineRotateAngle = 90f;
        
        protected sealed override ButtonElement _buttonElement { get; set; }
        
        public LineRotateView
            (Transform transform, RotateModel model, RotatePresenter presenter,
                [Inject(Id = UIInstaller.RotateButtonId)] ButtonElement buttonElement,
                IIdentifier identifier, SignalBus signalBus) 
            : base(transform,LineRotateAngle, model,presenter, identifier, signalBus)
        {
            _buttonElement = buttonElement;
        }
    }
}