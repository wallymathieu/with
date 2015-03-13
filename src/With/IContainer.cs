using System;

namespace With
{
    public interface IContainer<T>
    {
        bool Contains (T value);
    }
}

