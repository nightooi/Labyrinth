public interface IMemento<T>
{
    public T Save();
    public void Restore(IMemento<T> toState);

}
