using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Gato
{

    public class ObservableInputStream : Stream
    {
        ArraySegment<byte> current;
        Exception exception;
        Action cancel;
        Action continuation;
        IAsyncResult ar;

        bool completed;

        public ObservableInputStream(Func<
            Func<ArraySegment<byte>, Action, bool>, 
            Action<Exception>, 
            Action, 
            Action> input)
        {
            cancel = input(OnNext, OnError, OnCompleted);
        }

        void OnCompleted()
        {
            completed = true;
        }

        void OnError(Exception error)
        {
            exception = error;
        }

        bool OnNext(ArraySegment<byte> data, Action ct)
        {
            continuation = ct;

            if (continuation == null)
            {
                // must return false.

                // if buffer is empty and async read is pending
                //   copy data to pending read buffer
                //   complete async read (asynchronously)
                // else
                //   add data to buffer
            }
            else
            {
                // if async read is pending
                //   copy data to pending read buffer
                //   complete async read (asynchronously)
                // else
                //   add data to buffer
                //   make continuation pending
            }

            // XXX don't reallocate every time.
            current = new ArraySegment<byte>(new byte[data.Count]);
            Buffer.BlockCopy(data.Array, data.Offset, current.Array, current.Offset, data.Count);
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            // if data is buffered
            //   copy data to pending buffer
            //   complete async read (synchronously).
            // else if continuation pending
            //   make async read pending
            //   invoke continuation
            // else 
            //   make async read pending (wait for next)

            return null;
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            return base.EndRead(asyncResult);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            // copy from buffer. if no buffered data and not complete or exception, block until data available.
            throw new NotImplementedException();
        }

        #region Stream boilerplate

        public override bool CanRead { get { return true; } }
        public override bool CanSeek { get { return false; } }
        public override bool CanWrite { get { return false; } }
        public override void Flush() { }
        public override long Length { get { throw new NotSupportedException(); } }
        public override long Position { 
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
