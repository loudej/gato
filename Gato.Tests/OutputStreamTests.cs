using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Gato;

namespace Gato.Tests
{
    [TestFixture]
    public class OutputStreamTests
    {
        bool completed;
        StringBuilder sb;
        Func<ArraySegment<byte>, Action, bool> next;
        Action complete;

        [SetUp]
        public void SetUp()
        {
            var sb = new StringBuilder();
            next = (d, c) =>
            {
                sb.Append(Encoding.ASCII.GetString(d.Array, d.Offset, d.Count));
                return true;
            };
            complete = () => completed = true;
        }

        [Test]
        public void PsCs()
        {
            var stream = new OutputStream(next, completed);

            
        }
    }
}
