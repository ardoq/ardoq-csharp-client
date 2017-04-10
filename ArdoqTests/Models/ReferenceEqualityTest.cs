using NUnit.Framework;
using Ardoq.Models;
using System.Collections.Generic;
using System.Linq;

namespace ArdoqTest.Models
{
    class ReferenceEqualityTest
    {
        [Test]
        public void EqualityTest()
        {
            var componentA = new Component();
            componentA.Fields.Add("foo", "bar");
            componentA.Fields.Add("number", 1);

            var componentB = new Component();
            componentB.Fields.Add("foo", "bar");
            componentB.Fields.Add("number", 1);

            Assert.True(componentA.Equals(componentB));
            Assert.True(componentB.Equals(componentA));

        }

        [Test]
        public void InequalityTest()
        {
            var componentA = new Component();
            componentA.Fields.Add("foo", "bar");

            var componentB = new Component();
            componentB.Fields.Add("foo", "bar");

            Assert.True(componentA.Equals(componentB));
            Assert.True(componentB.Equals(componentA));

            componentA.Fields.Add("number", 1);
            Assert.False(componentA.Equals(componentB));
            Assert.False(componentB.Equals(componentA));

            componentB.Fields.Add("number", 1);
            Assert.True(componentA.Equals(componentB));
            Assert.True(componentB.Equals(componentA));

            componentA.Fields.Add("EmptyValue", "Non-Empty");
            componentB.Fields.Add("EmptyValue", null);
            Assert.False(componentA.Equals(componentB));
            Assert.False(componentB.Equals(componentA));

        }

        [Test]
        public void FieldsWithNullValuesEqualityTest()
        {
            var componentA = new Component();
            componentA.Fields.Add("foo", "bar");

            var componentB = new Component();
            componentB.Fields.Add("foo", null);

            var referenceA = new Reference();
            var referenceB = new Reference();

            referenceA.Fields.Add("foo", "bar");
            referenceB.Fields.Add("foo", null);

            Assert.False(componentA.Equals(componentB));
            Assert.False(componentB.Equals(componentA));
            Assert.False(referenceA.Equals(referenceB));
            Assert.False(referenceB.Equals(referenceA));

            Assert.False(referenceA.Equals(componentA));
            Assert.False(componentA.Equals(referenceA));

            Assert.True(componentA.Equals(componentA));
            Assert.False(referenceB.Equals(componentA));
            Assert.False(referenceB.Equals(componentB));
            Assert.False(componentA.Equals(referenceB));
            Assert.False(componentB.Equals(referenceB));

            Assert.True(referenceA.Equals(referenceA));
            Assert.True(referenceB.Equals(referenceB));
        }


        [Test]
        public void UnionTest()
        {
            var components = new List<IModelBase>();
            var compA = new Component("CompA", "workspaceid", "");
            compA.Fields.Add("foo", null);
            var compB = new Component("CompB", "workspaceid", "");
            compB.Fields.Add("foo", "bar");

            components.Add((IModelBase) compA);
            components.Add((IModelBase) compB);

            var referenceA = new Reference("workspaceid", "", "CompA", "CompB", 0);
            var referenceB = new Reference("workspaceid", "", "CompB", "CompA", 0);

            var references = new List<IModelBase>();
            references.Add((IModelBase) referenceA);
            references.Add((IModelBase) referenceB);

            var combined = components.Union(references).ToList();
            Assert.True(combined.LongCount() == 4);
        }
    }
}