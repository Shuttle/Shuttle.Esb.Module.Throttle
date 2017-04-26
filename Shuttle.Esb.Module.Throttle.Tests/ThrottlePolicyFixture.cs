using System;
using NUnit.Framework;

namespace Shuttle.Esb.Module.Throttle.Tests
{
    [TestFixture]
    public class ThrottlePolicyFixture
    {
        [Test]
        public void Should_be_able_to_()
        {
            var policy = new ThrottlePolicy(new ThrottleConfiguration());

            Assert.IsFalse(policy.ShouldAbort());
        }
    }
}