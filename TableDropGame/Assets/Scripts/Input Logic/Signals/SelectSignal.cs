namespace ATG.TableDrop
{
    public struct SelectSignal
    {
        public readonly int? SelectedId;

        public SelectSignal(IIdentifier identifier)
        {
            SelectedId = identifier?.InstanceId ?? null;
        }
    }
}