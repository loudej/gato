using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gato.Tests
{
    class Observable<T> : IObservable<T>
    {
        Func<IObserver<T>, Action> subscribe;

        public Observable(Func<IObserver<T>, Action> subscribe)
        {
            this.subscribe = subscribe;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return new Disposable(subscribe(observer));
        }
    }
}
