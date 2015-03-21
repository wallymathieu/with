using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tests.With.TestData
{
    class ReadonlyDictionaryDataAttribute : AutoDataAttribute
    {
        public ReadonlyDictionaryDataAttribute()
        {
            this.Fixture.Customize(new LookupDataCustomization());
        }
        class LookupDataCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                var dic2 = fixture.Create<Dictionary<int, MyClass>>();
                fixture.Inject<IReadOnlyDictionary<int,MyClass>>(new ReadOnlyDictionary<int, MyClass>(dic2));
            }
        }
    }
}
