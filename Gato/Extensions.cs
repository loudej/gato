using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gato
{
    public static class GatoExtensions
    {
        public static string GetRequestMethod(this IDictionary<string, object> env)
        {
            return env["gato.RequestMethod"] as string;
        }

        public static void SetRequestMethod(this IDictionary<string, object> env, string value)
        {
            env["gato.RequestMethod"] = value;
        }

        public static string GetRequestUri(this IDictionary<string, object> env)
        {
            return env["gato.RequestUri"] as string;
        }

        public static void SetRequestUri(this IDictionary<string, object> env, string value)
        {
            env["gato.RequestUri"] = value;
        }

        public static IDictionary<string, string> GetRequestHeaders(this IDictionary<string, object> env)
        {
            return env["gato.RequestHeaders"] as IDictionary<string, string>;
        }

        public static void SetRequestHeaders(this IDictionary<string, object> env, IDictionary<string, string> value)
        {
            env["gato.RequestHeaders"] = value;
        }

        public static string GetRequestHeader(this IDictionary<string, object> env, string name)
        {
            var headers = env.GetRequestHeaders();
            return headers.ContainsKey(name) ? headers[name] : null;
        }

        public static GatoBodyObservable GetRequestBody(this IDictionary<string, object> env)
        {
            return env["gato.RequestBody"] as GatoBodyObservable;
        }

        public static void SetRequestBody(this IDictionary<string, object> env, GatoBodyObservable value)
        {
            env["gato.RequestBody"] = value;
        }
    }
}
