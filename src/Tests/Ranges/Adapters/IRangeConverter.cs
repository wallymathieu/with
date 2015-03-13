namespace Tests.Ranges.Adapters
{
    /// <summary>
    /// Able to convert from object values to a specific type of range. Also able to convert array to specific type of array.
    /// </summary>
    public interface IRangeConverter
    {
		IRange Range(object @from,object @to,object @step);
        IRange Range(object @from,object @to);
        object[] ToArrayOf<T>(T[] array);
    }
}
