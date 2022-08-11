using UniRx;

namespace ATG.TableDrop
{
    public class RotateModel
    {
        public ReactiveProperty<float> NextAngle { get; set; }

        public RotateModel()
        {
            NextAngle = new ReactiveProperty<float>(0f);
        }
    }
}