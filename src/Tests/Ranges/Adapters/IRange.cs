namespace Tests.Ranges.Adapters
{
    /// <summary>
    /// we want to test against an api that is simpler to write test code against. Objects instead of specific types.
    /// </summary>
    public interface IRange
    {
        bool Contain(object o);
        object[] ToArray();
    }
    
}
