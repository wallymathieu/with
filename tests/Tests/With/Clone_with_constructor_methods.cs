using System;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using Tests.With.TestData;
using With;
using With.Lenses;
using Xunit;

namespace Tests.With
{
    public class Clone_with_constructor_methods
    {
        public Clone_with_constructor_methods ()
        {
        }
        private static readonly Lazy<DataLens<CSale, string>> SaleCustomerNameCopy = LazyT.Create (() =>
            LensBuilder<CSale>.Constructors(new [] { "MK", "New"}).Of (sp => sp.Customer.Name).Build ());

        [Theory, AutoData]
        public void Should_be_able_to_create_a_clone_law_of_demeter (CSale myClass, string newValue)
        {
            var ret = SaleCustomerNameCopy.Value.Set (myClass, newValue);
            Assert.Equal (newValue, ret.Customer.Name);
            Assert.Equal (myClass.Id, ret.Id);
            Assert.True (ret.FromMK, "from MK");
            Assert.True (ret.Customer.FromNew, "from New");
        }
        private static readonly Lazy<DataLens<ECCustomer, string>> CustomerNameCopy = LazyT.Create (() =>
            LensBuilder<ECCustomer>.Constructors (new [] { "MK", "New" }).Of (sp => sp.Name).Build ());
        private static readonly Lazy<DataLens<ECCustomer, IEnumerable<string>>> CustomerPrefCopy = LazyT.Create (() =>
            LensBuilder<ECCustomer>.Constructors (new [] { "MK", "New" }).Of (sp => sp.Preferences).Build ());

        [Theory, AutoData]
        public void Should_get_correct_exception (int id, string newValue)
        {
            var ecc = ECCustomer.New (id, "name1", new string [0]);
            var ret = CustomerNameCopy.Value.Set (ecc, newValue);
            Assert.Equal (newValue, ret.Name);
            Assert.True (ret.FromNew, "from New");
            var ex=Assert.Throws<ArgumentNullException> (() => CustomerNameCopy.Value.Set (ecc, null));
            Assert.Equal ("name", ex.ParamName);
        }

        [Theory, AutoData]
        public void Should_get_correct_exception_2 (int id, string newValue)
        {
            var ecc = ECCustomer.New (id, "name1", new string [0]);
            var prefs = new [] { newValue };
            var ret = CustomerPrefCopy.Value.Set (ecc, prefs);
            Assert.Equal (prefs, ret.Preferences);
            Assert.True (ret.FromNew, "from New");
            var ex = Assert.Throws<NullReferenceException> (() => CustomerPrefCopy.Value.Set (ecc, null));
            Assert.Equal ("preferences", ex.Message);
        }
    }
}
