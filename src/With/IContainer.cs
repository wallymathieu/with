using System;

namespace With
{
    public interface IContainer<T>
    {
        bool Contain (T value);
    }
}

