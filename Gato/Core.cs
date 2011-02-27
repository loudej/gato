using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gato
{
    public delegate Action GatoBodyObservable(
        Func<ArraySegment<byte>, Action, bool> next,
        Action<Exception> fault,
        Action complete);

    public delegate void GatoApp(
        // input
        IDictionary<string, object> env, 

        // output
        Action<
            string, // status (e.g., "200 OK")
            IDictionary<string, string>, // headers
            GatoBodyObservable // body
        > response,

        Action<Exception> fault);



}
