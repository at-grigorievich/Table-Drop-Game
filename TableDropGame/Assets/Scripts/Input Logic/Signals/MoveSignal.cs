using UnityEngine;

namespace ATG.TableDrop
{
    public struct MoveSignal
    {
        public readonly int Id;
        public readonly Vector2 MoveDirection;

        public MoveSignal(int id, Vector2 move)
        {
            Id = id;
            MoveDirection = move;
        }
    }
}