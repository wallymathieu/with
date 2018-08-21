namespace With.Collections
{
    /// <summary>
    /// Represents a container, where the collection need not be enumerated.
    /// </summary>
    /// <typeparam name="T">Is an integer or decimal.</typeparam>
    public interface IContainer<in T>
    {
        /// <summary>
        /// Without enumerating this container, return true if the value is contained in the container.
        /// </summary>
        bool Contains(T value);
    }
}

