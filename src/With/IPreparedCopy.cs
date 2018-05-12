namespace With
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPreparedCopy
    {
    }
    public interface IPreparedCopy<TObj, in TValue>:IPreparedCopy
    {
        TObj Copy(TObj from, TValue value);
    }
    public interface IPreparedCopy<TObj, in TValue1, in TValue2>:IPreparedCopy
    {
        TObj Copy(TObj from, TValue1 value1, TValue2 value2);
    }
}
