namespace With.WithPlumbing
{
    internal class NameAndValue
    {
        public NameAndValue(string name, object value)
        {
            Name = name;
            Value = value;
        }
        public readonly string Name;
        public readonly object Value;
    }
}