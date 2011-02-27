using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace Gato
{
    public class EchoApp
    {
        public void Run(
        IDictionary<string, object> env,
        Action<
            string,
            IDictionary<string, string>,
            GatoBodyObservable> response,
        Action<Exception> fault)
        {
            response(
                "200 OK", 
                new Dictionary<string, string>() 
                {
                    { "Content-Type", "text/plain" }
                }, 
                (next, error, complete) =>
                {
                    return env.GetRequestBody()((ch, c) => next(Upcase(ch), c), error, complete);
                });
        }

        ArraySegment<byte> Upcase(ArraySegment<byte> chunk)
        {
            return new ArraySegment<byte>(Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(chunk.Array, chunk.Offset, chunk.Count).ToUpperInvariant()));
        }
    }
}