using UnityEngine;
using Zenject;

namespace ATG.TableDrop
{
    public sealed class SpawnerInstaller: MonoInstaller
    {
        [SerializeField] private SpawnerParameters _spawnerParameters;

        public override void InstallBindings()
        {
            
        }
    }
}