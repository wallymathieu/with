namespace With.Plumbing
{
    internal class NameAndValue
    {
        public NameAndValue()
        {

        }
        public NameAndValue(string name, object value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}