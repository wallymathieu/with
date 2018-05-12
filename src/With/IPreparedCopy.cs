namespace With
{
    public interface IPreparedCopy
    {
    }
    public interface IPreparedCopy<TObj,TValue>:IPreparedCopy
    {
        TObj Copy(TObj from, TValue value);
    }
    public interface IPreparedCopy<TObj,TValue1,TValue2>:IPreparedCopy
    {
        TObj Copy(TObj from, TValue1 value1, TValue2 value2);
    }
}
