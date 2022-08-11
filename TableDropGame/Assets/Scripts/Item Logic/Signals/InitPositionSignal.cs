using UnityEngine;

namespace ATG.TableDrop
{
    public struct InitPositionSignal
    {
        public readonly int Id;

        public readonly Transform Parent;
        
        public readonly Vector3 Position;

        public InitPositionSignal(int id, Transform par, Vector3 pos)
        {
            Id = id;
            Parent = par;
            Position = pos;
        }
    }
}