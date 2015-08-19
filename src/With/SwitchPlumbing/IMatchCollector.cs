namespace With.SwitchPlumbing
{
    public interface IMatchCollector<T, In, Out>
    {
        T Add(IMatcher<In, Out> m);
    }
}