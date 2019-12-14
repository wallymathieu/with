using System.Collections.Generic;

namespace Tests.With.TestData
{
    public class CustomerWithEmptyCtor : Customer
    {
        public CustomerWithEmptyCtor()
            : this(-1, null, new string[0])
        {
        }
        public CustomerWithEmptyCtor(int id, string name, IEnumerable<string> preferences)
            : base(id, name, preferences)
        {
        }
    }
}
