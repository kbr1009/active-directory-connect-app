namespace ADApp.Application.OUs.Queries.ShowAllOUs
{
    public interface ITreeNode<T>
    {
        T Parent { get; set; }
        IList<T> Children { get; set; }
        T AddChild(T child);
        T RemoveChild(T child);
        bool TryRemoveChild(T child);
        T ClearChildren();
        bool TryRemoveOwn();
        T RemoveOwn();
    }
}
