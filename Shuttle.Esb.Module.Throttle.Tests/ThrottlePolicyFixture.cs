using System;
using NUnit.Framework;

namespace Shuttle.Esb.Module.Throttle.Tests
{
    [TestFixture]
    public class ThrottlePolicyFixture
    {
        [Test]
        public void Should_be_able_to_determine_whether_to_abort()
        {
            var policy = new ThrottlePolicy(new ThrottleConfiguration());

            Assert.IsFalse(policy.ShouldAbort());

            var aborted = false;
            var timeout = DateTime.Now.AddSeconds(2);

            policy = new ThrottlePolicy(new ThrottleConfiguration { CpuUsagePercentage = 1 });

            while (!aborted && DateTime.Now < timeout)
            {
                aborted = policy.ShouldAbort();
            }

            Assert.IsTrue(aborted);
        }
    }
}