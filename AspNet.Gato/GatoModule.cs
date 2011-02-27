using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using Gato;

namespace AspNet.Gato
{
    class Disposable : IDisposable
    {
        readonly Action dispose;

        public Disposable(Action d)
        {
            this.dispose = d;
        }

        public void Dispose()
        {
            dispose();
        }
    }

    public class GatoModule : IHttpModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.AddOnBeginRequestAsync(
                (sender, e, callback, state) => {
                    var httpApplication = (HttpApplication)sender;
                    var httpContext = httpApplication.Context;
                    var httpRequest = httpContext.Request;
                    var httpResponse = httpContext.Response;
                    var serverVariables = httpRequest.ServerVariables;

                    httpResponse.Buffer = false;

                    var env = new Dictionary<string, object>();

                    env.SetRequestMethod(httpRequest.HttpMethod);
                    env.SetRequestUri(httpRequest.RawUrl);

                    var headers = new Dictionary<string, string>();

                    foreach (var n in httpRequest.Headers.AllKeys)
                    {
                        headers[n] = httpRequest.Headers[n];
                    }

                    env.SetRequestHeaders(headers);
                    env.SetRequestBody((next, fault, complete) =>
                        {
                            

                            return () => { };
                        });

                    var task = Task.Factory.StartNew(_ => {

                    }, state);

                    if (callback != null)
                        task.ContinueWith(_ => callback(task));

                    return task;
                }, 
                ar => ((Task)ar).Wait());
        }
    }
}
