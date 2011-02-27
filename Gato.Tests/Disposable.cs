using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gato.Tests
{
    class Disposable : IDisposable
    {
        public static readonly Disposable NoOp = new Disposable();

        readonly Action dispose;
        private Disposable() : this(() => { }) { }

        public Disposable(Action dispose)
        {
            this.dispose = dispose;
        }

        public void Dispose()
        {
            dispose();
        }
    }
}
