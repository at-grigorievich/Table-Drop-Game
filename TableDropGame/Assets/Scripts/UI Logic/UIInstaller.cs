using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public class UIInstaller: MonoInstaller
    {
        public const string RotateButtonId = "RotateButton";
        public const string FlipButtonId = "FlipButton";

        [SerializeField] private ButtonElement _rotateButton;
        [SerializeField] private ButtonElement _flipButton;

        public override void InstallBindings()
        {
            Container.BindInstance(_rotateButton).WithId(RotateButtonId)
                .AsCached().NonLazy();
            Container.BindInstance(_flipButton).WithId(FlipButtonId)
                .AsCached().NonLazy();
        }
    }
}