using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Fibonacci.Tests
{

    public class FibonacciUnitTest
    {
        [Fact]
        public async Task Execute6ShouldRetrun8()
        {
            var result =   await Fibonacci.Compute.ExecuteAsync(new[] {"6"});
            Assert.Equal(1, result.Count);
            Assert.Equal(8, result[0]);
        }
    }
}